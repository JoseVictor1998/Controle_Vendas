namespace ComunicacaoVisual.API.DBModels
{
    public class DashboardVendedorResult
    {
        public decimal TotalVendidoMes { get; set; }
        public int MesAlvo { get; set; }
        public int AnoAlvo { get; set; }
        public int QtdArte { get; set; }
        public int QtdImpressao { get; set; }
        public int QtdProducao { get; set; }
        public int QtdEntregues { get; set; }
        public object HistoricoPedidos { get; set; } = null!;
    }
}
