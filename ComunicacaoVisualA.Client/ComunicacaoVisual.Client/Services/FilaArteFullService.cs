using System.Net.Http.Json;
using ComunicacaoVisual.Client.Models;

namespace ComunicacaoVisual.Client.Services
{
    public class FilaArteFullService
    {
        private readonly HttpClient _http;

        public FilaArteFullService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<FilaArteFullDTO>> Listar()
        {
            // 🚨 AJUSTE AQUI: Coloque o nome correto do seu Controller (ex: api/Producao/FilaArteFinalistaFull)
            var result = await _http.GetFromJsonAsync<List<FilaArteFullDTO>>("api/Producao/FilaArteFinalistaFull");
            return result ?? new List<FilaArteFullDTO>();
        }
    }
}