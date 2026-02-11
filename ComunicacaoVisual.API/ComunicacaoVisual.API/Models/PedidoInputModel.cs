namespace ComunicacaoVisual.API.Models
{
    public class PedidoInputModel
    {
        public int ClienteId { get; set; }
        public string OsExterna { get; set; } = null!;
        public int VendedorId { get; set; }
        public string ObservacaoGeral { get; set; } = null!;
        public int TipoProdutoId { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string ObservacaoTecnica { get; set; } = null!;
        public string? CaminhoFoto { get; set; }
    }
}