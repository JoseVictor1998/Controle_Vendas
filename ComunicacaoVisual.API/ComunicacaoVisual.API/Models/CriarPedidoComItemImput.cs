namespace ComunicacaoVisual.API.Models
{
    public class CriarPedidoComItemImput
    {
        public int ClienteId { get; set; }
        public string OsExterna { get; set; } = null!;
        public int VendedorID { get; set; }
        public string? ObservacaoGeral { get; set; }
        public int TipoProdutooId { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string? ObservacaoTecnica { get; set; }
        public string? CaminhoFoto { get; set; }

    }
}
