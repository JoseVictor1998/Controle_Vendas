namespace ComunicacaoVisual.Client.Models
{
    public class FilaProducaoDTO
    {
        public string OS { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string Produto { get; set; } = string.Empty;
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        public string? Caminho_Foto { get; set; }
        public int Horas_Desde_Abertura { get; set; }
        public string Vendedor_Responsavel { get; set; } = string.Empty;
    }
}
