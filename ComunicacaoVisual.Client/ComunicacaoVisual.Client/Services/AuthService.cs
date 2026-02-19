using ComunicacaoVisual.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace ComunicacaoVisual.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private readonly NavigationManager _navigationManager; // 1. Adicione esta linha

        // 2. Adicione 'NavigationManager nav' aqui no construtor
        public AuthService(HttpClient http, IJSRuntime js, NavigationManager nav)
        {
            _http = http;
            _js = js;
            _navigationManager = nav; // 3. E esta linha aqui
        }

        public async Task<bool> Login(object dadosLogin)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/login", dadosLogin);

            if (response.IsSuccessStatusCode)
            {
                var resultado = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (resultado != null && !string.IsNullOrEmpty(resultado.Token))
                {
                    await _js.InvokeVoidAsync("localStorage.setItem", "authToken", resultado.Token);
                    await _js.InvokeVoidAsync("localStorage.setItem", "userName", resultado.Nome);
                    await _js.InvokeVoidAsync("localStorage.setItem", "userLevel", resultado.Nivel);

                    // Agora esta linha vai funcionar e atualizar o nome no topo!
                    _navigationManager.NavigateTo("/", true);
                    return true;
                }
            }
            return false;
        }

        public async Task Logout()
        {
            // Remove o "crachá" do navegador
            await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
        }
    }
}