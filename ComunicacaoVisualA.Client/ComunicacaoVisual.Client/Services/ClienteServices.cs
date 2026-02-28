using ComunicacaoVisual.Client.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace ComunicacaoVisual.Client.Services
{
    public class ClienteService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public ClienteService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        private async Task AplicarTokenAsync()
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (!string.IsNullOrWhiteSpace(token))
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<(bool ok, string mensagem)> CadastrarCliente(CadastrarClienteCompletoInput cliente)
        {
            await AplicarTokenAsync();

            var response = await _http.PostAsJsonAsync("api/Producao/CadastrarCliente", cliente);

            if (response.IsSuccessStatusCode)
                return (true, "Cliente cadastrado com sucesso!");

            // tenta ler erro da API
            var body = await response.Content.ReadAsStringAsync();
            return (false, string.IsNullOrWhiteSpace(body) ? "Erro ao cadastrar cliente." : body);
        }
    }
}