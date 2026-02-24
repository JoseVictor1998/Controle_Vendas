namespace ComunicacaoVisual.Client.Models
{
    public class VwMeusPedidosVendedorModel
    {
        public int? VendedorId { get; set; }

        public string OsExterna { get; set; } = string.Empty;

        public string Cliente { get; set; } = string.Empty;

        public string StatusAtual { get; set; } = string.Empty;

        public decimal? ValorTotal { get; set; }

        public DateTime? DataPedido { get; set; }
    }
}