using System.Net.Http.Json;
using ComunicacaoVisual.Client.Models;

namespace ComunicacaoVisual.Client.Services;

public class FilaProducaoService
{
    private readonly HttpClient _http;

    public FilaProducaoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<FilaProducaoCompletaDTO>> Listar()
    {
        var result = await _http.GetFromJsonAsync<List<FilaProducaoCompletaDTO>>(
            "api/Producao/FilaProducaoCompleta");

        return result ?? new List<FilaProducaoCompletaDTO>();
    }
}