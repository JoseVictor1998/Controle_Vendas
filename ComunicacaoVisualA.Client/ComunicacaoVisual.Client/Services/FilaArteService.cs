using ComunicacaoVisual.Client.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms; // Necessário para IBrowserFile

namespace ComunicacaoVisual.Client.Services
{
    public class FilaArteService
    {
        private readonly HttpClient _http;

        public FilaArteService(HttpClient http)
        {
            _http = http;
        }

        // Busca a lista de pedidos aguardando arte
        public async Task<List<FilaArteDTO>> Listar()
        {
            return await _http.GetFromJsonAsync<List<FilaArteDTO>>("api/Producao/FilaArte")
                   ?? new List<FilaArteDTO>();
        }

        // Realiza o download dos bytes da arte para visualização ou salvamento
        public async Task<(byte[] content, string contentType, string fileName)> BaixarArquivo(int arquivoId)
        {
            var response = await _http.GetAsync($"api/Producao/DownloadArte/{arquivoId}");

            if (response.IsSuccessStatusCode)
            {
                var bytes = await response.Content.ReadAsByteArrayAsync();
                var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
                var fileName = response.Content.Headers.ContentDisposition?.FileName ?? "arquivo";

                return (bytes, contentType, fileName);
            }

            return (null, null, null);
        }

        // Faz o upload do arquivo e vincula ao Item do Pedido
        public async Task<bool> VincularArte(int itemId, IBrowserFile file)
        {
            using var content = new MultipartFormDataContent();

            // Abre o fluxo do arquivo com limite de 80MB
            var fileContent = new StreamContent(file.OpenReadStream(80 * 1024 * 1024));
            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

            // Adiciona os campos que a API espera (itemId e file)
            content.Add(new StringContent(itemId.ToString()), "itemId");
            content.Add(fileContent, "file", file.Name);

            var response = await _http.PostAsync("api/Producao/UploadArte", content);

            return response.IsSuccessStatusCode;
        }

        public class AtualizarStatusArteInput
        {
            public int ItemId { get; set; }
            public int NovoStatusArteId { get; set; }
            public int UsuarioId { get; set; }
        }

        public async Task<bool> MudarStatus(int itemId, int novoStatusId, int usuarioId)
        {
            var input = new AtualizarStatusArteInput
            {
                ItemId = itemId,
                NovoStatusArteId = novoStatusId,
                UsuarioId = usuarioId
            };

            // 🔄 Mudamos para PutAsJsonAsync para bater com o [HttpPut] e [FromBody] da API
            var response = await _http.PutAsJsonAsync("api/Producao/AtualizarStatusArte", input);
            return response.IsSuccessStatusCode;
        }
    }
}