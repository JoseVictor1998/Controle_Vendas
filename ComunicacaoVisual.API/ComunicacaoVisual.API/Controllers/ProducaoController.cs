using ComunicacaoVisual.API.Contracts;
using ComunicacaoVisual.API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;

namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducaoController : ControllerBase
    {
        private readonly ControleVendasContext _context;
        private readonly IConfiguration _config;

        public ProducaoController(
            ControleVendasContext context,
            IConfiguration config) 
        {
            _context = context;
            _config = config; 
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

        [Authorize(Roles = "God,Admin,Arte,Vendedor")]
        [HttpGet("FilaArte")]

        public async Task<IActionResult> GetFilaArte()
        {
            try
            {
                var dados = await _context.VwFilaArtes.ToListAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao acessar a Fila de Artes", erro = ex.Message });

            }
        }

        [Authorize(Roles = "God,Admin,Impressao,Vendedor")]
        [HttpGet("FilaImpressao")]

        public async Task<IActionResult> GetFilaImpressao()
        {
            try
            {
                var dados = await _context.VwFilaImpressaos.ToListAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao ver a Fila de impressão", erro = ex.Message });
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

        [Authorize(Roles = "God,Admin")]
        [HttpGet("DashboardGerencia")]

        public async Task<IActionResult> GetDashboardBIGerencia()
        {
            try
            {
                var dados = await _context.VwDashboardBiGerencials.ToListAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao acessar o Dashboard", erro = ex.Message });
            }

        }

        [Authorize(Roles = "God,Admin,Vendedor,Arte,Impressao,Producao")]
        [HttpGet("AlertaSla")]
        public async Task<IActionResult> GetAlertaSla()
        {
            try
            {
                var dados = await _context.VwAlertasSlas.ToListAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao acessar o Alerta de SLA", erro = ex.Message });
            }

        }

       

        [Authorize(Roles = "God,Admin")]
        [HttpGet("DashboardFinanceiro")]
        public async Task<IActionResult> GetDashboardFinanceiros()
        {
            try
            {
                var dados = await _context.VwDashboardFinanceiros.ToListAsync();
                return Ok(dados); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao acessar Dashboard Financeiro",
                    erro = ex.Message
                });
            }
        }
        [Authorize(Roles = "God,Admin")]
        [HttpGet("DashboardGestao")]
        public async Task<IActionResult> GetDashboardGestaoAtiva()
        {
            try
            {
                var dados = await _context.VwDashboardGestaoAtiva.ToListAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro ao acessar Dashboard Gestão",
                    erro = ex.Message
                });
            }
        }

        [Authorize(Roles = "God,Admin,Producao")]
        [HttpGet("FilaProducaoCompleta")]
        public async Task<IActionResult> GetFilaProducaoCompleta()
        {
            try
            {
                var dados = await _context.VwFilaProducaoCompleta.ToListAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro em Fila Produção Completa",
                    erro = ex.Message
                });
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

                
                var consulta = _context.VwMeusPedidosVendedors
                                       .Where(p => p.VendedorId == vendedorId);

                
                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta = consulta.Where(p =>
                        p.OsExterna.Contains(filtro) ||
                        p.Cliente.Contains(filtro));
                }

                var resultado = await consulta.ToListAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao carregar pedidos", erro = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("MonitoramentoGlobal")]
        public async Task<IActionResult> GetMonitoramentoGlobal()
        {
            try
            {
                var dados = await _context.VwMonitoramentoGlobals.ToListAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro em Monitoramento Global",
                    erro = ex.Message
                });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("PesquisaClienteVendas")]
        public async Task<IActionResult> GetPesquisaClienteVenda([FromQuery] string? filtro)
        {
            try
                        {
                var consulta = _context.VwPesquisaClientesVendas.AsQueryable();
                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta = consulta.Where(p =>
                     p.Nome.Contains(filtro) ||
                    (p.Documento != null && p.Documento.Contains(filtro)));
                }
                var resultado = await consulta.ToListAsync();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro em Pesquisa Cliente Venda",
                    erro = ex.Message
                });
            }

        }

        [Authorize(Roles = "God,Admin,Arte")]
        [HttpGet("FilaArteFinalistaFull")]
        public async Task<IActionResult> GetFilaArteFinalistaFull()
        {
            try
            {
                var dados = await _context.VwFilaArteFinalistaFulls.ToListAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensagem = "Erro em Fila Arte Finalista Full",
                    erro = ex.Message
                });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("ClientesListar")]
        public async Task<IActionResult> ClientesListar()
        {
            var lista = await _context.Clientes
                .Select(c => new { id = c.ClienteId, nome = c.Nome })
                .OrderBy(x => x.nome)
                .ToListAsync();

            return Ok(lista);
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("TiposProdutoListar")]
        public async Task<IActionResult> TiposProdutoListar()
        {
            var lista = await _context.TipoProdutos
                .Select(t => new { id = t.TipoProdutoId, nome = t.Nome })
                .OrderBy(x => x.nome)
                .ToListAsync();

            return Ok(lista);
        }


        [Authorize(Roles = "God,Admin,Arte")]
        [HttpGet("StatusArteListar")]
        public async Task<IActionResult> StatusArteListar()
        {
            var lista = await _context.StatusArtes
                .OrderBy(s => s.StatusArteId)
                .Select(s => new StatusOption { Id = s.StatusArteId, Nome = s.Nome })
                .ToListAsync();

            return Ok(lista);
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
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_Criar_Pedido_Com_Item 
                        @Cliente_ID = {model.ClienteId}, 
                        @OS_Externa = {model.OsExterna}, 
                        @Vendedor_ID = {model.VendedorID}, 
                        @Observacao_Geral = {model.ObservacaoGeral}, 
                        @Tipo_Produto_ID = {model.TipoProdutoId}, 
                        @Largura = {model.Largura}, 
                        @Altura = {model.Altura}, 
                        @Quantidade = {model.Quantidade}, 
                        @Observacao_Tecnica = {model.ObservacaoTecnica}, 
                        @Caminho_Foto = {model.CaminhoFoto}");
                return Ok(new { mensagem = "Pedido criado com item e enviado para produção!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao criar pedido", detalhe = ex.Message });
            }

        }



        [Authorize(Roles = "God")] 
        [HttpPost("CadastrarUsuario")]
       public async Task<IActionResult> CadastrarUsuario([FromBody] CadastrarUsuarioInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_Cadastrar_Usuario 
                        @Nome = {model.Nome}, 
                        @Login = {model.Login}, 
                        @Senha = {model.Senha}, 
                        @NivelAcesso = {model.Nivel_Acesso}");
                return Ok(new { mensagem = "Usuário cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao cadastrar usuário", detalhe = ex.Message });
            }
        }

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpPost("CadastrarCliente")]

        public async Task<IActionResult> CadastrarCliente([FromBody] CadastrarClienteCompletoInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_Cadastrar_Cliente_Completo 
                        @Nome = {model.Nome}, 
                        @Email = {model.Email}, 
                        @DDD = {model.DDD}, 
                        @NumeroTelefone = {model.NumeroTelefone}, 
                        @Cidade = {model.Cidade}, 
                        @CEP = {model.CEP}, 
                        @Bairro = {model.Bairro}, 
                        @Rua = {model.Rua}, 
                        @NumeroEndereco = {model.NumeroEndereco}, 
                        @Documento = {model.Documento}, 
                        @Tipo = {model.Tipo}");
                return Ok(new { mensagem = "Cliente cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao cadastrar cliente", detalhe = ex.Message });
            }

        }

        [Authorize(Roles = "God,Admin,Arte,Vendedor")]
        [HttpGet("DownloadArte/{id:int}")]
        public async Task<IActionResult> DownloadArte(int id)
        {
            var arte = await _context.ArquivoArtes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ArquivoId == id);

            if (arte == null) return NotFound();
            if (string.IsNullOrWhiteSpace(arte.CaminhoFisico)) return NotFound(new { erro = "Sem caminho físico salvo." });
            if (!System.IO.File.Exists(arte.CaminhoFisico)) return NotFound(new { erro = "Arquivo não encontrado no servidor." });

            var contentType = string.IsNullOrWhiteSpace(arte.ContentType) ? "application/octet-stream" : arte.ContentType;
            var bytes = await System.IO.File.ReadAllBytesAsync(arte.CaminhoFisico);

            // baixa com o nome original
            return File(bytes, contentType, arte.NomeArquivo);
        }

        [Authorize(Roles = "God,Admin,Arte,Vendedor")]
        [HttpPost("UploadArte")]
        [Consumes("multipart/form-data")]
        [RequestSizeLimit(80 * 1024 * 1024)]
        public async Task<IActionResult> UploadArte([FromForm] UploadArteRequest req)
        {
            if (req.ItemId <= 0) return BadRequest(new { erro = "Item inválido." });
            if (req.File == null || req.File.Length == 0) return BadRequest(new { erro = "Arquivo inválido." });

            var itemId = req.ItemId;
            var file = req.File;

            var ext = Path.GetExtension(file.FileName);
            var permitidas = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".pdf", ".cdr", ".zip", ".png", ".jpg", ".jpeg" };
            if (!permitidas.Contains(ext)) return BadRequest(new { erro = $"Extensão não permitida: {ext}" });

            var root = _config["ArteStorage:RootPath"];
            if (string.IsNullOrWhiteSpace(root))
                return StatusCode(500, new { erro = "ArteStorage:RootPath não configurado." });

            var pasta = Path.Combine(root, $"Item-{itemId}");
            Directory.CreateDirectory(pasta);

            var nomeOriginal = Path.GetFileName(file.FileName);
            var nomeFinal = $"{itemId}_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}{ext}";
            var caminhoFisico = Path.Combine(pasta, nomeFinal);

            await using (var fs = System.IO.File.Create(caminhoFisico))
                await file.CopyToAsync(fs);

            const int STATUS_ENVIADA = 2;
            const string PLACEHOLDER_URL = "/api/Producao/DownloadArte/0"; // ✅ nunca nulo

            var existente = await _context.ArquivoArtes.FirstOrDefaultAsync(x => x.ItemId == itemId);

            if (existente != null)
            {
                existente.NomeArquivo = nomeOriginal;
                existente.CaminhoArquivo = PLACEHOLDER_URL;  // ✅ nunca nulo
                existente.CaminhoFisico = caminhoFisico;
                existente.StatusArteId = STATUS_ENVIADA;
                existente.ContentType = file.ContentType;
                existente.TamanhoBytes = file.Length;
                existente.UsuarioUpload = User.Identity?.Name;
                existente.DataUpload = DateTime.UtcNow;
            }
            else
            {
                existente = new ArquivoArte
                {
                    ItemId = itemId,
                    NomeArquivo = nomeOriginal,
                    CaminhoArquivo = PLACEHOLDER_URL,          // ✅ nunca nulo
                    CaminhoFisico = caminhoFisico,
                    StatusArteId = STATUS_ENVIADA,
                    ContentType = file.ContentType,
                    TamanhoBytes = file.Length,
                    UsuarioUpload = User.Identity?.Name,
                    DataUpload = DateTime.UtcNow
                };
                _context.ArquivoArtes.Add(existente);
            }

            await _context.SaveChangesAsync(); // ✅ agora passa

            existente.CaminhoArquivo = $"/api/Producao/DownloadArte/{existente.ArquivoId}";
            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = existente.ArquivoId,
                itemId = existente.ItemId,
                nomeArquivo = existente.NomeArquivo,
                downloadUrl = existente.CaminhoArquivo,
                statusArteId = existente.StatusArteId
            });
        }



        [Authorize(Roles = "God,Admin,Arte")]
        [HttpPut("AtualizarStatusArte")]
        public async Task<IActionResult> AtualizarStatusArte([FromBody] AtualizarStatusArteInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $@"EXEC sp_set_session_context @key = N'UsuarioId', @value = {model.UsuarioId};");

                await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC SP_Atualizar_Status_Arte 
                @Item_ID = {model.ItemId}, 
                @Novo_Status_Arte_ID = {model.NovoStatusArteId}, 
                @Usuario_ID = {model.UsuarioId}");

                return Ok(new { mensagem = "Status da arte atualizado!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro no SQL da Arte", detalhe = ex.Message });
            }
        }


        [Authorize(Roles = "God,Admin,Producao,Impressao,Arte")]
        [HttpPut("AtualizarStatus")]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarStatusInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
      $@"EXEC sp_set_session_context @key = N'UsuarioId', @value = {model.UsuarioId};"
  );

                await _context.Database.ExecuteSqlInterpolatedAsync($@"
    EXEC SP_Atualizar_Status_Pedido 
        @Pedido_ID = {model.ItemId}, 
        @Novo_Status_ID = {model.NovoStatusId}, 
        @Usuario_ID = {model.UsuarioId}");

                return Ok(new { mensagem = "Status atualizado! O pedido avançou na fila." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao mudar status", detalhe = ex.Message });
            }
        }


        [Authorize(Roles = "God,Admin,Arte")]
        [HttpPut("VincularArquivoArte")]
        public async Task<IActionResult> VincularArquivoArte([FromBody] VincularArquivoArteInput model)
        {
            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_Vincular_Arquivo_Arte 
                        @Item_ID = {model.ItemID}, 
                        @Nome_Arquivo = {model.NomeArquivo}, 
                        @Caminho_Arquivo = {model.CaminhoArquivo}, 
                        @Usuario_ID = {model.UsuarioID}");
                return Ok(new { mensagem = "Arquivo de arte vinculado ao item com sucesso!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao vincular arquivo de arte", detalhe = ex.Message });

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
                        @Forma_Pagamento = {model.FormaPagamento}");
                return Ok(new { mensagem = "Status do pedido atualizado com sucesso!" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao atualizar status do pedido", detalhe = ex.Message });

            }

        }


    }
}