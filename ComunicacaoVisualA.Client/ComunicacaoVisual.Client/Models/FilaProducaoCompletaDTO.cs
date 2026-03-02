namespace ComunicacaoVisual.Client.Models;

public class FilaProducaoCompletaDTO
{
    public int ItemId { get; set; } // 🚀 PRECISA ESTAR AQUI!
    public string OS { get; set; } = string.Empty; // Tudo maiúsculo conforme o banco
    public string Cliente { get; set; } = string.Empty;
    public string Produto { get; set; } = string.Empty;
    public string? CaminhoFoto { get; set; }
    public int? HorasDesdeAbertura { get; set; }
    public string? MaterialBase { get; set; }
    public decimal Largura { get; set; }
    public decimal Altura { get; set; }
    public int Quantidade { get; set; }
    public string? ObservacaoGeral { get; set; }
    public string? ObservacaoTecnica { get; set; }
    public string? VendedorResponsavel { get; set; }
}