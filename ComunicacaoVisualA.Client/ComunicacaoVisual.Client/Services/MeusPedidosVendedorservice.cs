using System.Net.Http.Json;
using ComunicacaoVisual.Client.Models;

namespace ComunicacaoVisual.Client.Services;

public class MeusPedidosVendedorService
{

    private readonly HttpClient _http;

    public MeusPedidosVendedorService(HttpClient http)
    {
        _http = http;
    }


    public async Task<List<MeusPedidosVendedorDTO>> Listar(int vendedorId, string filtro)
    {

        var url = $"api/Producao/MeusPedidosVendedor?vendedorId={vendedorId}&filtro={filtro}";

        var result = await _http.GetFromJsonAsync<List<MeusPedidosVendedorDTO>>(url);

        return result ?? new List<MeusPedidosVendedorDTO>();

    }


}