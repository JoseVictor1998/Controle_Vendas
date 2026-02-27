namespace ComunicacaoVisual.Client.Models
{
    public class FilaArteDTO
    {
        public string OS { get; set; } = "";
        public string Cliente { get; set; } = "";
        public string Produto { get; set; } = "";
        public int Dias_Aguardando_Arte { get; set; }
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string Status_Arte { get; set; } = "";
    }
}
