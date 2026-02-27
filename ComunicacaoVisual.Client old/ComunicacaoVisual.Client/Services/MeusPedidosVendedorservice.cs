using ComunicacaoVisual.Client.Models;
using System.Net.Http.Json;

namespace ComunicacaoVisual.Client.Services
{
    public class MeusPedidosVendedorService
    {
        private readonly HttpClient _http;

        public MeusPedidosVendedorService(HttpClient http)
        {
            _http = http;
        }

        // Buscar todos os pedidos
        public async Task<List<VwMeusPedidosVendedorModel>> GetMeusPedidosAsync()
        {
            var resultado = await _http.GetFromJsonAsync<List<VwMeusPedidosVendedorModel>>
                ("api/meuspedidosvendedor");

            return resultado ?? new List<VwMeusPedidosVendedorModel>();
        }


        // Buscar pedidos por vendedor (opcional - mais profissional)
        public async Task<List<VwMeusPedidosVendedorModel>> GetMeusPedidosPorVendedorAsync(int vendedorId)
        {
            var resultado = await _http.GetFromJsonAsync<List<VwMeusPedidosVendedorModel>>
                ($"api/meuspedidosvendedor/{vendedorId}");

            return resultado ?? new List<VwMeusPedidosVendedorModel>();
        }
    }
}