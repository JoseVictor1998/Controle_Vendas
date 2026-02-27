namespace ComunicacaoVisual.Client.Models;

public class FilaProducaoCompletaDTO
{
    public string Os { get; set; } = null!;
    public string Cliente { get; set; } = null!;
    public string Produto { get; set; } = null!;
    public string? MaterialBase { get; set; }
    public decimal Largura { get; set; }
    public decimal Altura { get; set; }
    public int Quantidade { get; set; }
    public string? CaminhoFoto { get; set; }
    public int? HorasDesdeAbertura { get; set; }
    public string VendedorResponsavel { get; set; } = null!;
    public string? ObservacaoGeral { get; set; }
    public string? ObservacaoTecnica { get; set; }
}