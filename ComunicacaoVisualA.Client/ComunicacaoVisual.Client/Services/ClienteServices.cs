using ComunicacaoVisual.Client.Models;
using System.Net.Http.Json;

namespace ComunicacaoVisual.Client.Services
{
    public class ClienteService
    {
        private readonly HttpClient _http;

        public ClienteService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> CadastrarCliente(CadastrarClienteDTO cliente)
        {
            // O endereço "api/Cliente" deve ser igual ao do seu Controller na API
            var response = await _http.PostAsJsonAsync("api/Producao/Cliente", cliente);
            return response.IsSuccessStatusCode;
        }
    }
}