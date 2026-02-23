using ComunicacaoVisual.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization; // <--- ADICIONE ESTA LINHA
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Security.Claims; // <--- ADICIONE ESTA PARA AS ROLES

namespace ComunicacaoVisual.Client.Services
{
    // O segredo está aqui: adicione o ": AuthenticationStateProvider"
    public class AuthService : AuthenticationStateProvider
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;
        private readonly NavigationManager _navigationManager;

        public AuthService(HttpClient http, IJSRuntime js, NavigationManager nav)
        {
            _http = http;
            _js = js;
            _navigationManager = nav;
        }

        // ADICIONE ESTE MÉTODO ABAIXO (O Blazor vai exigir ele)
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
            var nivel = await _js.InvokeAsync<string>("localStorage.getItem", "userLevel");

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, await _js.InvokeAsync<string>("localStorage.getItem", "userName")),
                new Claim(ClaimTypes.Role, nivel ?? "") // Aqui injetamos o "God", "Arte", etc.
            };

            var identity = new ClaimsIdentity(claims, "jwt");
            return new AuthenticationState(new ClaimsPrincipal(identity));
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