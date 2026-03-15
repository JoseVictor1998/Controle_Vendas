using ComunicacaoVisual.API.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComunicacaoVisual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly ControleVendasContext _context;

        public DashboardsController(ControleVendasContext context)
        {
            _context = context;
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
            catch (Exception ex) { return StatusCode(500, new { mensagem = "Erro ao acessar o Dashboard", erro = ex.Message }); }
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
            catch (Exception ex) { return StatusCode(500, new { mensagem = "Erro ao acessar o Alerta de SLA", erro = ex.Message }); }
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
            catch (Exception ex) { return StatusCode(500, new { mensagem = "Erro ao acessar Dashboard Financeiro", erro = ex.Message }); }
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
            catch (Exception ex) { return StatusCode(500, new { mensagem = "Erro ao acessar Dashboard Gestão", erro = ex.Message }); }
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

                var queryBase = _context.Pedidos
                    .Include(p => p.Cliente).Include(p => p.Status).Include(p => p.PedidoItems)
                    .Where(p => p.VendedorId == vendedorId && p.DataPedido.HasValue && p.DataPedido.Value.Month == filtroMes && p.DataPedido.Value.Year == filtroAno);

                var totalVendidoMes = await queryBase.Where(p => p.StatusId != 8).SumAsync(p => p.ValorTotal ?? 0);
                var qtdArte = await queryBase.CountAsync(p => p.StatusId == 2 || p.StatusId == 3);
                var qtdImpressao = await queryBase.CountAsync(p => p.StatusId == 4);
                var qtdProducao = await queryBase.CountAsync(p => p.StatusId == 5);
                var qtdEntregues = await queryBase.CountAsync(p => p.StatusId == 6 || p.StatusId == 7);

                if (!string.IsNullOrEmpty(filtro))
                    queryBase = queryBase.Where(p => p.OsExterna.Contains(filtro) || p.Cliente.Nome.Contains(filtro));

                var historico = await queryBase.OrderByDescending(p => p.DataPedido).Select(p => new
                {
                    pedido_ID = p.PedidoId,
                    os = p.OsExterna,
                    cliente = p.Cliente.Nome,
                    status_ID = p.StatusId,
                    status_Atual = p.Status.Nome,
                    valor_Total = p.ValorTotal,
                    data_Pedido = p.DataPedido,
                    qtd_Itens = p.PedidoItems.Count()
                }).Take(100).ToListAsync();

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
            catch (Exception ex) { return StatusCode(500, new { erro = "Erro ao carregar dashboard", detalhe = ex.Message }); }
        }
    }

    
}