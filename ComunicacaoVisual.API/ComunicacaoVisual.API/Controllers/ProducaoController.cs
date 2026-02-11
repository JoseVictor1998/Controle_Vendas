using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComunicacaoVisual.API.Models;

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

        [HttpPost("cadastrar")]
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

        [HttpPut("Cadastrar Cliente")]

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
    }
}