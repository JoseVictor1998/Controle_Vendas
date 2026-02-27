using System.Net.Http.Json;
using ComunicacaoVisual.Client.Models;

namespace ComunicacaoVisual.Client.Services
{

    public class TipoProdutoService
    {

        private readonly HttpClient _http;


        public TipoProdutoService(HttpClient http)
        {

            _http = http;

        }



        public async Task<List<TipoProdutoDTO>> Listar()
        {

            var result = await _http.GetFromJsonAsync<List<TipoProdutoDTO>>("api/Producao/TiposProdutoListar");

            return result ?? new List<TipoProdutoDTO>();

        }


    }

}