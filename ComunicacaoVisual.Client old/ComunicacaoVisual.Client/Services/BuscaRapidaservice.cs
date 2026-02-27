using ComunicacaoVisual.Client.Models;
using System.Net.Http.Json;

namespace ComunicacaoVisual.Client.Services
{
    public class BuscaRapidaService
    {
        private readonly HttpClient http;

        public BuscaRapidaService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<List<BuscaRapidaDTO>> Buscar(string filtro)
        {

            var result = await http.GetFromJsonAsync<List<BuscaRapidaDTO>>(
                $"api/Producao/BuscaRapida?filtro={filtro}");

            return result ?? new List<BuscaRapidaDTO>();

        }
    }
}