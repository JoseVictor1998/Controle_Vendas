namespace ComunicacaoVisual.Client.Models
{
    public class VwHistoricoPedidosClienteModel
    {
        public string Cliente { get; set; } = string.Empty;

        public string Os { get; set; } = string.Empty;

        public DateTime? DataPedido { get; set; }

        public string Produto { get; set; } = string.Empty;

        public decimal Largura { get; set; }

        public decimal Altura { get; set; }

        public int Quantidade { get; set; }

        public string StatusAtual { get; set; } = string.Empty;

        public string ObservacaoTecnica { get; set; } = string.Empty;
    }
}