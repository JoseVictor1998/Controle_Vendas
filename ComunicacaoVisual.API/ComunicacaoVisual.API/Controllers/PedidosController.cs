using ComunicacaoVisual.API.Contracts;
using ComunicacaoVisual.API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ControleVendasContext _context;

        public PedidosController(ControleVendasContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoPorId(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Status)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.PedidoId == id);

            if (pedido == null) return NotFound("Pedido não encontrado.");

            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            var idUsuarioLogado = User.FindFirst("UsuarioId")?.Value;

            // Se for vendedor, só pode ver se ele for o dono do pedido
            if (role == "Vendedor" && idUsuarioLogado != pedido.VendedorId?.ToString())
                return Forbid("Você não tem permissão para ver pedidos de outros vendedores.");

            return Ok(pedido);
        }

        [Authorize(Roles = "God,Admin,Vendedor,Arte,Impressao,Producao")]
        [HttpGet("PedidoDetalhado/{id}")]
        public async Task<IActionResult> GetPedidoDetalhado(int id)
        {
            // 1) Busca o pedido base
            var pedido = await _context.Pedidos
                .AsNoTracking()
                .Where(p => p.PedidoId == id)
                .Select(p => new
                {
                    p.PedidoId,
                    p.OsExterna,
                    p.DataPedido,
                    p.ObservacaoGeral,
                    p.ValorTotal,
                    p.FormaPagamento,
                    StatusId = p.StatusId,
                    StatusNome = p.Status.Nome,
                    ClienteId = p.ClienteId,
                    ClienteNome = p.Cliente.Nome,
                    ClienteEmail = p.Cliente.Email,
                    VendedorId = p.VendedorId
                })
                .FirstOrDefaultAsync();

            if (pedido == null) return NotFound("Pedido não encontrado.");

            // 2) Regra: vendedor só vê os próprios pedidos
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            var idUsuarioLogado = User.FindFirst("UsuarioId")?.Value;

            if (role == "Vendedor" && idUsuarioLogado != pedido.VendedorId?.ToString())
                return Forbid("Você não tem permissão para ver pedidos de outros vendedores.");

            // 3) Busca itens + tipo produto + arte (LEFT JOIN)
            var itens = await (
                from pi in _context.PedidoItems.AsNoTracking()
                where pi.PedidoId == id
                join tp in _context.TipoProdutos.AsNoTracking() on pi.TipoProdutoId equals tp.TipoProdutoId
                join aa in _context.ArquivoArtes.AsNoTracking() on pi.ItemId equals aa.ItemId into aaJoin
                from aa in aaJoin.DefaultIfEmpty()
                join sa in _context.StatusArtes.AsNoTracking() on aa.StatusArteId equals sa.StatusArteId into saJoin
                from sa in saJoin.DefaultIfEmpty()
                select new ItemDetalhadoDto
                {
                    ItemId = pi.ItemId,
                    TipoProdutoId = tp.TipoProdutoId,
                    TipoProdutoNome = tp.Nome,
                    Largura = pi.Largura,
                    Altura = pi.Altura,
                    Quantidade = pi.Quantidade,
                    ObservacaoTecnica = pi.ObservacaoTecnica,
                    CaminhoFoto = pi.CaminhoFoto,

                    Arte = aa == null ? null : new ArquivoArteDto
                    {
                        ArquivoId = aa.ArquivoId,
                        NomeArquivo = aa.NomeArquivo,
                        CaminhoArquivo = aa.CaminhoArquivo,
                        StatusArteId = aa.StatusArteId,
                        StatusArteNome = sa != null ? sa.Nome : ""
                    }
                }
            ).ToListAsync();

            var dto = new PedidoDetalhadoDto
            {
                PedidoId = pedido.PedidoId,
                OsExterna = pedido.OsExterna,
                DataPedido = pedido.DataPedido,
                ObservacaoGeral = pedido.ObservacaoGeral,
                ValorTotal = pedido.ValorTotal,
                FormaPagamento = pedido.FormaPagamento,
                StatusId = pedido.StatusId,
                StatusNome = pedido.StatusNome,
                Cliente = new ClienteResumoDto
                {
                    ClienteId = pedido.ClienteId,
                    Nome = pedido.ClienteNome,
                    Email = pedido.ClienteEmail
                },
                Itens = itens
            };

            return Ok(dto);
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpPost("CriarPedido")]
        public async Task<IActionResult> CriarPedidoComItem([FromBody] CriarPedidoComItemInput model)
        {
            try
            {
                // 🚀 O SEGREDO: Envelopa a transação na estratégia de resiliência do EF Core
                var strategy = _context.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    // Agora sim podemos abrir a transação com segurança!
                    using var transaction = await _context.Database.BeginTransactionAsync();

                    try
                    {
                        // 1. Cria o Pedido Pai
                        var novoPedido = new Pedido
                        {
                            ClienteId = model.ClienteId,
                            OsExterna = model.OsExterna,
                            VendedorId = model.VendedorID,
                            StatusId = 1,
                            ObservacaoGeral = string.IsNullOrWhiteSpace(model.ObservacaoGeral) ? "" : model.ObservacaoGeral
                        };

                        _context.Pedidos.Add(novoPedido);
                        await _context.SaveChangesAsync();

                        // 2. Faz um laço de repetição para salvar CADA ITEM da lista
                        foreach (var item in model.Itens)
                        {
                            var novoItem = new PedidoItem // <- Ajuste o nome da classe se for diferente
                            {
                                PedidoId = novoPedido.PedidoId,
                                TipoProdutoId = item.TipoProdutoId,
                                Largura = item.Largura,
                                Altura = item.Altura,
                                Quantidade = item.Quantidade,
                                ObservacaoTecnica = string.IsNullOrWhiteSpace(item.ObservacaoTecnica) ? "" : item.ObservacaoTecnica,
                                CaminhoFoto = item.CaminhoFoto
                            };

                            _context.PedidoItems.Add(novoItem); // <- Ajuste o nome do DbSet se for diferente
                            await _context.SaveChangesAsync();

                            // 3. Salva a arte atrelada a esse item (se houver)
                            if (!string.IsNullOrWhiteSpace(item.CaminhoFoto))
                            {
                                var novaArte = new ArquivoArte // <- Lembre de ajustar para a sua classe (ex: ArquivoArte)
                                {
                                    ItemId = novoItem.ItemId,
                                    CaminhoArquivo = item.CaminhoFoto,

                                    // 🚀 A CORREÇÃO ESTÁ AQUI: Fatiamos o link e pegamos o nome real do arquivo!
                                    NomeArquivo = item.CaminhoFoto.Split('/', '\\').Last(),

                                    StatusArteId = 1 // Aguardando Arte Final
                                };

                                _context.ArquivoArtes.Add(novaArte);
                                await _context.SaveChangesAsync();
                            }
                        }

                        // 4. Confirma tudo no banco de dados!
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        // Se der erro lá dentro, desfaz a transação e repassa o erro para o catch de fora
                        await transaction.RollbackAsync();
                        throw;
                    }
                });

                return Ok(new { mensagem = $"Pedido criado com sucesso contendo {model.Itens.Count} item(ns)!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao criar pedido", detalhe = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Producao,Impressao,Arte")]
        [HttpPut("AtualizarStatus")]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarStatusInput model)
        {
            try
            {
                // 1. Informa o usuário para o histórico do banco
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $@"EXEC sp_set_session_context @key = N'UsuarioId', @value = {model.UsuarioId};");

                // 2. 🚀 A MÁGICA: Descobre qual é o Pedido verdadeiro daquele Item antes de atualizar
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
            DECLARE @RealPedidoId INT;
            
            -- Pega o ID do Pedido Pai amarrado a este Item
            SELECT @RealPedidoId = Pedido_ID FROM Pedido_Item WHERE Item_ID = {model.ItemId};

            -- Agora sim, manda o ID correto para a Procedure!
            EXEC SP_Atualizar_Status_Pedido 
                 @Pedido_ID = @RealPedidoId, 
                 @Novo_Status_ID = {model.NovoStatusId}, 
                 @Usuario_ID = {model.UsuarioId};
        ");

                return Ok(new { mensagem = "Status atualizado! O pedido avançou na fila." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao mudar status", detalhe = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpPut("AtualizarStatusPedidoEntregue")]
        public async Task<IActionResult> AtualizarStatusPedido([FromBody] AtualizarStatusPedidoInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $@"EXEC sp_set_session_context @key = N'UsuarioId', @value = {model.UsuarioId};"
                );

                await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC SP_Atualizar_Status_Pedido 
                @Pedido_ID = {model.PedidoId}, 
                @Novo_Status_ID = {model.NovoStatusId}, 
                @Usuario_ID = {model.UsuarioId}, 
                @Valor_Total = {model.ValorTotal}, 
                @Forma_Pagamento = {model.FormaPagamento},
                @Parcelas = {model.Parcelas}"); // 🚀 A API AGORA ENVIA A PARCELA

                return Ok(new { mensagem = "Status do pedido e financeiro atualizados com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao atualizar status do pedido", detalhe = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpPut("SolicitarCorrecao")]
        public async Task<IActionResult> SolicitarCorrecao([FromBody] SolicitarCorrecaoInput model)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(model.PedidoId);
                if (pedido == null) return NotFound("Pedido não encontrado");

                // Status 3 = Arte em Correção / Status 8 = Reprovado(Cancelado)
                pedido.StatusId = model.NovoStatusId;

                // Só atualiza o texto se o vendedor tiver digitado algo novo
                if (!string.IsNullOrWhiteSpace(model.NovaObservacao))
                {
                    pedido.ObservacaoGeral = model.NovaObservacao;
                }

                // Passa o ID do usuário para o log do banco
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $@"EXEC sp_set_session_context @key = N'UsuarioId', @value = {model.UsuarioId};");

                await _context.SaveChangesAsync();

                return Ok(new { mensagem = "Pedido atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao solicitar correção", detalhe = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpPut("SalvarCorrecaoCompleta")]
        public async Task<IActionResult> SalvarCorrecaoCompleta([FromBody] CorrecaoCompletaInput model)
        {
            try
            {
                var pedido = await _context.Pedidos
                    .Include(p => p.PedidoItems)
                    .FirstOrDefaultAsync(p => p.PedidoId == model.PedidoId);

                if (pedido == null) return NotFound("Pedido não encontrado");

                // Muda para Arte em Correção e atualiza a observação
                pedido.StatusId = 3;
                pedido.ObservacaoGeral = string.IsNullOrWhiteSpace(model.NovaObservacao) ? "" : model.NovaObservacao;

                // Atualiza a Medida, Quantidade e Material de CADA item da OS
                foreach (var itemModel in model.Itens)
                {
                    var itemDb = pedido.PedidoItems.FirstOrDefault(i => i.ItemId == itemModel.ItemId);
                    if (itemDb != null)
                    {
                        itemDb.TipoProdutoId = itemModel.TipoProdutoId;
                        itemDb.Largura = itemModel.Largura;
                        itemDb.Altura = itemModel.Altura;
                        itemDb.Quantidade = itemModel.Quantidade;
                        itemDb.ObservacaoTecnica = string.IsNullOrWhiteSpace(itemModel.ObservacaoTecnica) ? "" : itemModel.ObservacaoTecnica;
                    }
                }

                // Salva quem fez a alteração
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $@"EXEC sp_set_session_context @key = N'UsuarioId', @value = {model.UsuarioId};");

                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Correção salva com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao salvar correção", detalhe = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("BuscaRapida")]
        public async Task<IActionResult> GetBuscaRapida([FromQuery] string? filtro)
        {
            try
            {

                var consulta = _context.VwBuscaRapidaPedidos.AsQueryable();


                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta = consulta.Where(p =>
                        p.Nome.Contains(filtro) ||
                        p.Os.Contains(filtro) ||
                        p.Produto.Contains(filtro));
                }

                var resultado = await consulta.ToListAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro na busca", detalhe = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("HistoricoPedidoCliente")]
        public async Task<IActionResult> GetHistoricoPedidoCliente([FromQuery] string? filtro)
        {
            try
            {
                var consulta = _context.VwHistoricoPedidosClientes.AsQueryable();
                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta = consulta.Where(p =>
                        p.Cliente.Contains(filtro) ||
                        p.Os.Contains(filtro) ||
                        p.Produto.Contains(filtro));
                }

                var resultado = await consulta.ToListAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro em Historico Pedidos Cliente",
                    erro = ex.Message
                });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("MeusPedidosVendedor")]
        public async Task<IActionResult> GetMeusPedidosVendedor([FromQuery] int vendedorId, [FromQuery] string? filtro)
        {
            try
            {
                var roleUsuarioLogado = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var idUsuarioLogado = User.FindFirst("UsuarioId")?.Value;

                if (roleUsuarioLogado == "Vendedor" && idUsuarioLogado != vendedorId.ToString())
                {
                    return Forbid("Você não tem permissão para visualizar pedidos de outros vendedores.");
                }

                // 🚀 A MÁGICA: LINQ puro! Esquece a View, buscamos direto no banco e moldamos o JSON!
                var consulta = _context.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Status)
                    .Include(p => p.PedidoItems)
                    .Where(p => p.VendedorId == vendedorId)
                    .Select(p => new
                    {
                        pedido_ID = p.PedidoId,
                        os = p.OsExterna,
                        cliente = p.Cliente.Nome,
                        status_ID = p.StatusId,
                        status_Atual = p.Status.Nome,
                        valor_Total = p.ValorTotal,
                        data_Pedido = p.DataPedido,
                        qtd_Itens = p.PedidoItems.Count(),
                        observacao_Geral = p.ObservacaoGeral
                    });

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta = consulta.Where(p => p.os.Contains(filtro) || p.cliente.Contains(filtro));
                }

                var resultado = await consulta.ToListAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao carregar pedidos", erro = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor,Arte,Impressao,Producao")]
        [HttpGet("StatusProducaoListar")]
        public async Task<IActionResult> StatusProducaoListar()
        {
            var lista = await _context.StatusProducaos
                .Where(s => s.Ativo == true)
                .OrderBy(s => s.Ordem)
                .Select(s => new StatusOption { Id = s.StatusId, Nome = s.Nome })
                .ToListAsync();

            return Ok(lista);
        }
    }
}