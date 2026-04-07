namespace ComunicacaoVisual.Client.Models
{
    public class FilaArteFullDTO
    {
        public int? ArquivoId { get; set; }
        public int ItemId { get; set; }
        public int PedidoId { get; set; }
        public int PedidoStatusId { get; set; }
        public string Os { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string Produto { get; set; } = string.Empty;
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string ObservacaoTecnica { get; set; } = string.Empty;
        public string? CaminhoArte { get; set; }
        public string? StatusArte { get; set; }
        public string SetorFila { get; set; } = string.Empty;
    }
}