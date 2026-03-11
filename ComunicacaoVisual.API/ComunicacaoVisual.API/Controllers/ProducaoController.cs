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

        [Authorize(Roles = "God,Admin,Vendedor")]
        [HttpGet("DashboardVendedor")]
        public async Task<IActionResult> GetDashboardVendedor([FromQuery] int vendedorId, [FromQuery] int? mes, [FromQuery] int? ano, [FromQuery] string? filtro)
        {
            try
            {
                var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var idLogado = User.FindFirst("UsuarioId")?.Value;

                if (role == "Vendedor" && idLogado != vendedorId.ToString())
                    return Forbid("Acesso negado. Você só pode ver o seu próprio dashboard.");

                var dataAtual = DateTime.Now;
                var filtroMes = mes ?? dataAtual.Month;
                var filtroAno = ano ?? dataAtual.Year;

                // 1. Prepara a base de busca apenas para este vendedor
                var queryBase = _context.Pedidos
                    .Include(p => p.Cliente)
                    .Include(p => p.Status)
                    .Include(p => p.PedidoItems)
                    .Where(p => p.VendedorId == vendedorId);

                // 2. Calcula o Total Vendido NO MÊS SELECIONADO (Ignora cancelados = Status 8)
                var pedidosDoMes = await queryBase
                    .Where(p => p.DataPedido.HasValue && p.DataPedido.Value.Month == filtroMes && p.DataPedido.Value.Year == filtroAno && p.StatusId != 8)
                    .ToListAsync();

                var totalVendidoMes = pedidosDoMes.Sum(p => p.ValorTotal ?? 0);

                // 3. Conta o Funil em Tempo Real (Independente do mês)
                var qtdArte = await queryBase.CountAsync(p => p.StatusId == 2 || p.StatusId == 3);
                var qtdImpressao = await queryBase.CountAsync(p => p.StatusId == 4);
                var qtdProducao = await queryBase.CountAsync(p => p.StatusId == 5);
                var qtdEntregues = await queryBase.CountAsync(p => p.StatusId == 6 || p.StatusId == 7);

                // 4. Monta o Histórico (Com barra de pesquisa)
                if (!string.IsNullOrEmpty(filtro))
                {
                    queryBase = queryBase.Where(p => p.OsExterna.Contains(filtro) || p.Cliente.Nome.Contains(filtro));
                }

                var historico = await queryBase
                    .OrderByDescending(p => p.DataPedido)
                    .Select(p => new
                    {
                        pedido_ID = p.PedidoId,
                        os = p.OsExterna,
                        cliente = p.Cliente.Nome,
                        status_ID = p.StatusId,
                        status_Atual = p.Status.Nome,
                        valor_Total = p.ValorTotal,
                        data_Pedido = p.DataPedido,
                        qtd_Itens = p.PedidoItems.Count()
                    })
                    .Take(100) // Traz as 100 mais recentes para não travar a tela
                    .ToListAsync();

                // 5. Devolve tudo empacotado para o Blazor!
                return Ok(new DashboardVendedorResult
                {
                    TotalVendidoMes = totalVendidoMes,
                    MesAlvo = filtroMes,
                    AnoAlvo = filtroAno,
                    QtdArte = qtdArte,
                    QtdImpressao = qtdImpressao,
                    QtdProducao = qtdProducao,
                    QtdEntregues = qtdEntregues,
                    HistoricoPedidos = historico
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao carregar dashboard do vendedor", detalhe = ex.Message });
            }
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
                            StatusId = 1 ,
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

        [HttpPost("UploadFotoArte/{itemId}")]
        public async Task<IActionResult> UploadFotoArte(int itemId, IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Arquivo não enviado");

            // 1. Caminho físico onde o Docker/Windows salva o arquivo
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "fotos");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

            var fileName = $"foto_{itemId}.jpg"; // Nome fixo por ID para facilitar
            var filePath = Path.Combine(uploadsFolder, fileName);

            // 2. Salva o arquivo (FileMode.Create substitui se já existir)
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // 3. ATUALIZA O BANCO: O segredo está na barra inicial '/'
            var item = await _context.PedidoItems.FirstOrDefaultAsync(p => p.ItemId == itemId);
            if (item != null)
            {
                // 🚀 SALVE ASSIM: Começando com / para o navegador achar a raiz
                item.CaminhoFoto = $"/uploads/fotos/{fileName}";
                await _context.SaveChangesAsync();
            }

            return Ok();
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

            // ... (código existente que salva o arquivo)
            await _context.SaveChangesAsync(); // A linha que já existe aí

            // 🚀 NOVO: Avisa a OS que a arte chegou para o vendedor aprovar!
            var itemPedido = await _context.PedidoItems.FirstOrDefaultAsync(x => x.ItemId == itemId);
            if (itemPedido != null)
            {
                var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.PedidoId == itemPedido.PedidoId);
                if (pedido != null && pedido.StatusId < 3)
                {
                    pedido.StatusId = 3; // 3 = Arte em Análise
                    await _context.SaveChangesAsync();
                }
            }


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

        // Coloque esta classe lá no fundo do arquivo ou na sua pasta de Models
        public class SolicitarCorrecaoInput
        {
            public int PedidoId { get; set; }
            public int UsuarioId { get; set; }
            public int NovoStatusId { get; set; }
            public string NovaObservacao { get; set; } = "";
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

    }
}
public class CorrecaoCompletaInput
{
    public int PedidoId { get; set; }
    public int UsuarioId { get; set; }
    public string NovaObservacao { get; set; } = "";

    // Como a classe já existe no seu projeto, o C# vai achar ela sozinho!
    public List<ItemDetalhadoDto> Itens { get; set; } = new();
}

public class TipoProdutoOption
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
}

public class DashboardVendedorResult
{
    public decimal TotalVendidoMes { get; set; }
    public int MesAlvo { get; set; }
    public int AnoAlvo { get; set; }
    public int QtdArte { get; set; }
    public int QtdImpressao { get; set; }
    public int QtdProducao { get; set; }
    public int QtdEntregues { get; set; }
    public object HistoricoPedidos { get; set; } = null!;
}