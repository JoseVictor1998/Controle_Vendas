using System.Net.Http.Json;
using ComunicacaoVisual.Client.Models;

namespace ComunicacaoVisual.Client.Services
{
    public class VendedorService
    {
        private readonly HttpClient _http;

        public VendedorService(HttpClient http)
        {
            _http = http;
        }

        // 1. Busca os pedidos do vendedor (COM SUPORTE A PESQUISA)
        public async Task<List<MeuPedidoVendedorDTO>?> ObterMeusPedidosAsync(int vendedorId, string? filtro = null)
        {
            try
            {
                var url = $"api/Pedidos/MeusPedidosVendedor?vendedorId={vendedorId}";
                if (!string.IsNullOrWhiteSpace(filtro)) url += $"&filtro={filtro}";

                return await _http.GetFromJsonAsync<List<MeuPedidoVendedorDTO>>(url);
            }
            catch { return new List<MeuPedidoVendedorDTO>(); }
        }

        // 2. Atualiza o status (Aprovar Arte ou Entregar OS Finalizada)
        public async Task<(bool sucesso, string mensagem)> AtualizarStatusPedidoAsync(AtualizarStatusPedidoInput input)
        {
            try
            {
                var res = await _http.PutAsJsonAsync("api/Pedidos/AtualizarStatusPedidoEntregue", input);

                if (res.IsSuccessStatusCode)
                    return (true, "Status atualizado com sucesso!");

                var erro = await res.Content.ReadAsStringAsync();
                return (false, $"Erro da API: {erro}");
            }
            catch (Exception ex)
            {
                return (false, $"Erro na comunicação: {ex.Message}");
            }
        }

        // 3. Cancela a OS
        public async Task<(bool sucesso, string mensagem)> SolicitarCorrecaoAsync(int pedidoId, int usuarioId, int novoStatusId, string novaObs)
        {
            try
            {
                var request = new { PedidoId = pedidoId, UsuarioId = usuarioId, NovoStatusId = novoStatusId, NovaObservacao = novaObs };
                var res = await _http.PutAsJsonAsync("api/Pedidos/SolicitarCorrecao", request);

                if (res.IsSuccessStatusCode) return (true, "Atualizado!");
                return (false, "Erro ao atualizar.");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }

        // 4. Salva as alterações de medidas e material da OS
        public async Task<(bool sucesso, string mensagem)> SalvarCorrecaoCompletaAsync(CorrecaoCompletaInput input)
        {
            try
            {
                var res = await _http.PutAsJsonAsync("api/Pedidos/SalvarCorrecaoCompleta", input);
                if (res.IsSuccessStatusCode) return (true, "Atualizado!");
                return (false, "Erro ao atualizar.");
            }
            catch (Exception ex) { return (false, ex.Message); }
        }

        // 5. Busca o Dashboard e o Histórico do Vendedor
        public async Task<DashboardVendedorResult?> ObterDashboardAsync(int vendedorId, int? mes = null, int? ano = null, string? filtro = null)
        {
            try
            {
                var url = $"api/Dashboards/DashboardVendedor?vendedorId={vendedorId}";
                if (mes.HasValue) url += $"&mes={mes.Value}";
                if (ano.HasValue) url += $"&ano={ano.Value}";
                if (!string.IsNullOrWhiteSpace(filtro)) url += $"&filtro={filtro}";

                return await _http.GetFromJsonAsync<DashboardVendedorResult>(url);
            }
            catch { return null; }
        }
    }
}