using System.Text.Json.Serialization;

public class FilaImpressaoDTO
{
    [JsonPropertyName("itemId")]
    public int ItemId { get; set; }

    [JsonPropertyName("arquivoId")]
    public int? ArquivoId { get; set; }

    [JsonPropertyName("os")]
    public string OS { get; set; } = "";

    [JsonPropertyName("cliente")]
    public string Cliente { get; set; } = "";

    [JsonPropertyName("produto")]
    public string Produto { get; set; } = "";

    [JsonPropertyName("materialBase")]
    public string? MaterialBase { get; set; }

    [JsonPropertyName("diasEmImpressao")]
    public int DiasEmImpressao { get; set; }

    
    [JsonPropertyName("caminhoFoto")]
    public string? Caminho_Foto { get; set; }

    [JsonPropertyName("largura")]
    public decimal Largura { get; set; }

    [JsonPropertyName("altura")]
    public decimal Altura { get; set; }

    [JsonPropertyName("quantidade")]
    public int Quantidade { get; set; }

    [JsonPropertyName("observacaoGeral")]
    public string? Observacao_Geral { get; set; }

    [JsonPropertyName("observacaoTecnica")]
    public string? Observacao_Tecnica { get; set; }

    [JsonPropertyName("linkArte")]
    public string? LinkArte { get; set; }
}
