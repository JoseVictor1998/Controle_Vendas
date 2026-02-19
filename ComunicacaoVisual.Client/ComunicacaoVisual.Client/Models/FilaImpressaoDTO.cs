namespace ComunicacaoVisual.Client.Models
{
    public class FilaImpressaoDTO
    {
        public string OS { get; set; } = "";
        public string Cliente { get; set; } = "";
        public string Produto { get; set; } = "";
        public string Material_Base { get; set; } = "";
        public decimal Largura { get; set; }
        public decimal Altura { get; set; }
        public int Quantidade { get; set; }
        // Novos campos que você pediu:
        public string? Descricao_Tecnica { get; set; }
        public string? Observacao_Geral { get; set; }
        public string? Link_Arte { get; set; } // Caminho do arquivo
        public string? Caminho_Foto { get; set; } // Amostra da imagem
    }
}
