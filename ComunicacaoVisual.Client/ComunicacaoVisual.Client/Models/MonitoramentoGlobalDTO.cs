namespace ComunicacaoVisual.Client.Models
{
    public class VwMonitoramentoGlobalModel
    {
        public string Os { get; set; } = string.Empty;

        public string Cliente { get; set; } = string.Empty;

        public string Produto { get; set; } = string.Empty;

        public string StatusProducao { get; set; } = string.Empty;

        public string? DataEntrada { get; set; }
    }
}