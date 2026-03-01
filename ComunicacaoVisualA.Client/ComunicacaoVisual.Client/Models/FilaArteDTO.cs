using System.Text.Json.Serialization;

namespace ComunicacaoVisual.Client.Models
{
    public class FilaArteDTO
    {
        [JsonPropertyName("itemId")] // Adicione isso!
        public int ItemId { get; set; }

        [JsonPropertyName("arquivoId")] // Adicione este campo
        public int? ArquivoId { get; set; }

        [JsonPropertyName("os")]
        public string OS { get; set; } = "";

        [JsonPropertyName("cliente")]
        public string Cliente { get; set; } = "";

        [JsonPropertyName("produto")]
        public string Produto { get; set; } = "";

        [JsonPropertyName("diasAguardandoArte")]
        public int? DiasAguardandoArte { get; set; }

        [JsonPropertyName("largura")]
        public decimal Largura { get; set; }

        [JsonPropertyName("altura")]
        public decimal Altura { get; set; }

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("observacaoTecnica")]
        public string? ObservacaoTecnica { get; set; }

        [JsonPropertyName("observacaoGeral")]
        public string? ObservacaoGeral { get; set; }

        [JsonPropertyName("statusArte")]
        public string StatusArte { get; set; } = "";
    }
}