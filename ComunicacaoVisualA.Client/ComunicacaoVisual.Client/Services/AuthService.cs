using ComunicacaoVisual.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ComunicacaoVisual.Client.Services
{

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


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {

                var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
                var nivel = await _js.InvokeAsync<string>("localStorage.getItem", "userLevel");


                if (string.IsNullOrEmpty(token) || TokenExpirou(token))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }


                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, await _js.InvokeAsync<string>("localStorage.getItem", "userName") ?? "Usuário"),
            new Claim(ClaimTypes.Role, nivel ?? "")
        };

                var identity = new ClaimsIdentity(claims, "jwt");
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch
            {

                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }

        private static bool TokenExpirou(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length != 3)
                    return true;

                var payload = parts[1];
                payload = payload.Replace('-', '+').Replace('_', '/');

                switch (payload.Length % 4)
                {
                    case 2: payload += "=="; break;
                    case 3: payload += "="; break;
                }

                var jsonBytes = Convert.FromBase64String(payload);
                using var doc = System.Text.Json.JsonDocument.Parse(jsonBytes);

                if (!doc.RootElement.TryGetProperty("exp", out var exp))
                    return true;

                var expDate = DateTimeOffset.FromUnixTimeSeconds(exp.GetInt64()).UtcDateTime;

                return DateTime.UtcNow >= expDate;
            }
            catch
            {
                return true;
            }
        }

        public async Task<bool> Login(object dadosLogin)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/login", dadosLogin);

            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (resultado == null || string.IsNullOrEmpty(resultado.Token))
                return false;

            await _js.InvokeVoidAsync("localStorage.setItem", "authToken", resultado.Token);
            await _js.InvokeVoidAsync("localStorage.setItem", "userName", resultado.Nome);
            await _js.InvokeVoidAsync("localStorage.setItem", "userLevel", resultado.Nivel);


            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            return true;
        }

        public async Task Logout()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
            await _js.InvokeVoidAsync("localStorage.removeItem", "userName");
            await _js.InvokeVoidAsync("localStorage.removeItem", "userLevel");

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}