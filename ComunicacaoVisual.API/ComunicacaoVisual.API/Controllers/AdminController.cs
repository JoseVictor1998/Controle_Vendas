using ComunicacaoVisual.API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "God,Admin")] // 🚀 SÓ O CHEFE PODE VER ISSO!
    public class AdminController : ControllerBase
    {
        private readonly ControleVendasContext _context;

        public AdminController(ControleVendasContext context)
        {
            _context = context;
        }

        // 1. LISTAR TODOS OS CUSTOS (Com filtro de Mês/Ano)
        [HttpGet("Custos")]
        public async Task<IActionResult> GetCustos([FromQuery] int mes, [FromQuery] int ano)
        {
            try
            {
                var custos = await _context.CustosFixos
                    .Where(c => c.DataVencimento.Month == mes && c.DataVencimento.Year == ano)
                    .OrderBy(c => c.DataVencimento)
                    .ToListAsync();

                return Ok(custos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao buscar custos", detalhe = ex.Message });
            }
        }

        // 2. CRIAR UM CUSTO NOVO
        [HttpPost("CriarCusto")]
        public async Task<IActionResult> CriarCusto([FromBody] CustoFixoInput model)
        {
            try
            {
                var novoCusto = new CustosFixo
                {
                    Descricao = model.Descricao,
                    Valor = model.Valor,
                    DataVencimento = DateOnly.FromDateTime(model.DataVencimento),
                    StatusPagamento = model.StatusPagamento
                };

                _context.CustosFixos.Add(novoCusto);
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = "Custo registrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao registrar custo", detalhe = ex.InnerException?.Message ?? ex.Message });
            }
        }

        // 3. MARCAR COMO PAGO / NÃO PAGO
        [HttpPut("AtualizarPagamentoCusto/{id}")]
        public async Task<IActionResult> AtualizarPagamento(int id, [FromBody] bool statusPago)
        {
            try
            {
                var custo = await _context.CustosFixos.FindAsync(id);
                if (custo == null) return NotFound("Custo não encontrado.");

                custo.StatusPagamento = statusPago;
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = statusPago ? "Conta marcada como paga!" : "Conta marcada como pendente!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao atualizar pagamento", detalhe = ex.Message });
            }
        }

        // 4. DELETAR UM CUSTO (Caso lançado errado)
        [HttpDelete("DeletarCusto/{id}")]
        public async Task<IActionResult> DeletarCusto(int id)
        {
            try
            {
                var custo = await _context.CustosFixos.FindAsync(id);
                if (custo == null) return NotFound("Custo não encontrado.");

                _context.CustosFixos.Remove(custo);
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = "Custo deletado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao deletar custo", detalhe = ex.Message });
            }
        }

        // 5. DASHBOARD GERENCIAL (A Visão de Deus) - 🚀 COM O FILTRO RIGOROSO DE MÊS
        [HttpGet("DashboardGerencial")]
        public async Task<IActionResult> GetDashboardGerencial([FromQuery] int mes, [FromQuery] int ano)
        {
            try
            {
                // 🚀 O SEGREDO ESTÁ AQUI: Nós filtramos TUDO logo no começo!
                var queryPedidosMes = _context.Pedidos
                    .Where(p => p.DataPedido.HasValue && p.DataPedido.Value.Month == mes && p.DataPedido.Value.Year == ano);

                // 1. Faturamento Total
                var faturamento = await queryPedidosMes
                    .Where(p => p.StatusId != 8)
                    .SumAsync(p => p.ValorTotal ?? 0);

                // 2. Custos Totais
                var custos = await _context.CustosFixos
                    .Where(c => c.DataVencimento.Month == mes && c.DataVencimento.Year == ano)
                    .SumAsync(c => c.Valor);

                // 3. Ponto de Equilíbrio / Lucro
                var lucro = faturamento - custos;

                // 4. Filas Operacionais rigorosamente do mês
                var qtdArte = await queryPedidosMes.CountAsync(p => p.StatusId == 2 || p.StatusId == 3);
                var qtdImpressao = await queryPedidosMes.CountAsync(p => p.StatusId == 4);
                var qtdProducao = await queryPedidosMes.CountAsync(p => p.StatusId == 5);
                var qtdAcabamento = await queryPedidosMes.CountAsync(p => p.StatusId == 6);

                return Ok(new
                {
                    Faturamento = faturamento,
                    Custos = custos,
                    Lucro = lucro,
                    QtdArte = qtdArte,
                    QtdImpressao = qtdImpressao,
                    QtdProducao = qtdProducao,
                    QtdAcabamento = qtdAcabamento
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao gerar a Visão de Deus", detalhe = ex.Message });
            }
        }
    }

    // 📦 A CAIXINHA PARA RECEBER OS DADOS (Fica fora da classe)
    public class CustoFixoInput
    {
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool StatusPagamento { get; set; } = false;
    }
}