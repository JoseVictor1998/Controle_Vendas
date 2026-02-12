namespace ComunicacaoVisual.API.Models
{
    public class CriarPedidoComItemInput
    {
        public int ClienteId { get; set; }
        public string OsExterna { get; set; } = null!;
        public int VendedorID { get; set; }
        public string? ObservacaoGeral { get; set; }
        public int TipoProdutoId { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string? ObservacaoTecnica { get; set; }
        public string? CaminhoFoto { get; set; }

    }
}
