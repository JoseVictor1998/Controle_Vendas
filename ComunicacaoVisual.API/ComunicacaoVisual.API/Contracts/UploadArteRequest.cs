using Microsoft.AspNetCore.Http;

namespace ComunicacaoVisual.API.Contracts;

public class UploadArteRequest
{
    public int ItemId { get; set; }
    public IFormFile File { get; set; } = default!;
}