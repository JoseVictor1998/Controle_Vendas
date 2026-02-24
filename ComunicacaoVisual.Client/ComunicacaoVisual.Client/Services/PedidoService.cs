using System.Net;
using System.Net.Http.Json;
using ComunicacaoVisual.Client.Models;

namespace ComunicacaoVisual.Client.Services;

public class PedidoService
{
    private readonly HttpClient _http;

    public PedidoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<(bool ok, string mensagem)> CriarPedidoAsync(CriarPedidoComItemInput input)
    {
        var res = await _http.PostAsJsonAsync("api/Producao/CriarPedido", input);

        if (res.IsSuccessStatusCode)
        {
            var obj = await res.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            return (true, obj?.GetValueOrDefault("mensagem") ?? "Pedido criado!");
        }

        if (res.StatusCode == HttpStatusCode.Unauthorized)
            return (false, "Sessão expirada. Faça login novamente.");

        var body = await res.Content.ReadAsStringAsync();
        return (false, $"Erro ({(int)res.StatusCode}): {body}");
    }
}