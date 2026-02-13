using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComunicacaoVisual.API.Models;
using System.Linq.Expressions;

namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducaoController : ControllerBase
    {
        private readonly ControleVendasContext _context;

        public ProducaoController(ControleVendasContext context)
        {
            _context = context;
        }

        [HttpGet("fila-completa")]
        public async Task<IActionResult> GetFilaCompleta()
        {
            try
            {
                var dados = await _context.VwFilaProducaoCompleta.ToListAsync();
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao acessar a View", erro = ex.Message });
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoPorId(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Status)
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.PedidoId == id);

            if (pedido == null) return NotFound("Pedido não encontrado.");
            return Ok(pedido);
        }


        [HttpGet("Fila Arte")]

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

        [HttpGet("Fila Impressão")]

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

      

        [HttpGet("busca-rapida")]
        public async Task<IActionResult> GetBuscaRapida([FromQuery] string? filtro)
        {
            try
            {
                // Começamos pegando a View completa do Contexto
                var consulta = _context.VwBuscaRapidaPedidos.AsQueryable();

                // Se o usuário digitou algo no campo de busca, aplicamos o filtro
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

        [HttpGet("DashboardBIGerencia")]

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

        [HttpGet("BuscaRapidaPedido")]
        public async Task<IActionResult> GetBuscaRapidaPedido([FromQuery] string? filtro)
        {
            try
            {
                var consulta  =  _context.VwBuscaRapidaPedidos.AsQueryable();
               
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
                return StatusCode(500, new
                {
                    mensagem = "Erro Buscar Pedido",
                    erro = ex.Message
                });
            }
        }

        [HttpGet("DashboardFinanceiro")]
        public async Task<IActionResult> GetDashboardFinanceiros()
        {
            try
            {
                var dados = await _context.VwDashboardFinanceiros.ToListAsync();
                return Ok(dados); // 👈 ESTAVA FALTANDO ISSO
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

        [HttpGet("Dashboard Gestao")]
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
        [HttpGet("FilaProducaoCompleta")]
        public async Task<IActionResult> GetFilaProdutoCompleta()
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
        [HttpGet("HistoricoPedidosCliente")]
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


        [HttpGet("MeusPedidosVendedor")]
        public async Task<IActionResult> GetMeusPedidosVendedor([FromQuery] int vendedorId, [FromQuery] string? filtro)
        {
            try
            {
                // 1. Primeiro, filtramos OBRIGATORIAMENTE pelo ID do vendedor logado
                var consulta = _context.VwMeusPedidosVendedors
                                       .Where(p => p.VendedorId == vendedorId);

                // 2. Depois, se ele digitou algo na busca, filtramos DENTRO dos pedidos dele
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
                return StatusCode(500, new { mensagem = "Erro ao carregar seus pedidos", erro = ex.Message });
            }
        }


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
        [HttpGet("PesquisaClienteVenda")]
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
        

        [HttpGet("Fila Arte Finalista Full")]
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

        [HttpPost("Criar Pedido")]
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



        [HttpPost("Cadastrar Cliente")]

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


        [HttpPost("Cadastrar Pedido")]
        public async Task<IActionResult> CadastrarPedido([FromBody] PedidoInputModel model)
        {
            try
            {
                // Aqui o C# executa a sua procedure SP_Criar_Pedido_Com_Item
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
                    EXEC SP_Criar_Pedido_Com_Item 
                        @Cliente_ID = {model.ClienteId}, 
                        @OS_Externa = {model.OsExterna}, 
                        @Vendedor_ID = {model.VendedorId}, 
                        @Observacao_Geral = {model.ObservacaoGeral}, 
                        @Tipo_Produto_ID = {model.TipoProdutoId}, 
                        @Largura = {model.Largura}, 
                        @Altura = {model.Altura}, 
                        @Quantidade = {model.Quantidade}, 
                        @Observacao_Tecnica = {model.ObservacaoTecnica}, 
                        @Caminho_Foto = {model.CaminhoFoto}");

                return Ok(new { mensagem = "Pedido enviado para a produção com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao salvar pedido", detalhe = ex.Message });
            }
        }

        [HttpPut("atualizar-status")]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarStatusInput model)
        {
            try
            {
                // Executa a sua procedure SP_Atualizar_Status_Pedido
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC SP_Atualizar_Status_Pedido 
                @Pedido_ID = {model.PedidoId}, 
                @Novo_Status_ID = {model.NovoStatusId}, 
                @Usuario_ID = {model.UsuarioId}");

                return Ok(new { mensagem = "Status atualizado! O pedido avançou na fila." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao mudar status", detalhe = ex.Message });
            }
        }

        [HttpPut("Vincular Arquivo Arte")]
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

        [HttpPut("Atualizar Status Pedido")]
        public async Task<IActionResult> AtualizarStatusPedido([FromBody] AtualizarStatusPedidoInput model)
        {
            try
            {

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


        [HttpDelete("ocultar/{id}")]
        public async Task<IActionResult> OcultarPedido(int id, int usuarioId)
        {
            try
            {
                // Mudamos para o ID 99 (ou o ID que você criar para 'Oculto')
                // Isso faz o pedido sair das Views de produção sem apagar os dados
                await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC SP_Atualizar_Status_Pedido 
                @Pedido_ID = {id}, 
                @Novo_Status_ID = 8, 
                @Usuario_ID = {usuarioId}");

                return Ok(new { mensagem = "Pedido movido para o arquivo e ocultado da fila." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao ocultar", detalhe = ex.Message });
            }
        }

    }
}