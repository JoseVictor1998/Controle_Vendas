namespace ComunicacaoVisual.Client.Models;

public class MeusPedidosVendedorDTO
{
    public int PedidoId { get; set; }

    public int VendedorId { get; set; }

    public string OsExterna { get; set; } = string.Empty;

    public string Cliente { get; set; } = string.Empty;

    public string? CaminhoFoto { get; set; }

    public string? TipoProdutoNome { get; set; }

    public decimal? Largura { get; set; }

    public decimal? Altura { get; set; }

    public int StatusId { get; set; }

    public string StatusNome { get; set; } = string.Empty;

    public string? VendedorNome { get; set; }

    public DateTime CreatedDate { get; set; }

    public decimal ValorTotal { get; set; }
}