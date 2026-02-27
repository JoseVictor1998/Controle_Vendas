namespace ComunicacaoVisual.Client.Models;

public class CriarPedidoComItemInput
{
    public int ClienteId { get; set; }
    public string OsExterna { get; set; } = "";
    public int VendedorID { get; set; }  // deixe igual ao backend por enquanto
    public string? ObservacaoGeral { get; set; }
    public int TipoProdutoId { get; set; }
    public decimal Largura { get; set; }
    public decimal Altura { get; set; }
    public int Quantidade { get; set; }
    public string? ObservacaoTecnica { get; set; }
    public string? CaminhoFoto { get; set; }
}