using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ComunicacaoVisual.API.DBModels;

public partial class VwFilaArte
{
    public int ItemId { get; set; }
    public int? ArquivoId { get; set; }
    public string Os { get; set; } = null!;

    [JsonPropertyName("caminhoFoto")] // 🚀 Isso garante que o JSON da API se conecte ao C#
    public string? Caminho_Foto { get; set; }

    public string Cliente { get; set; } = null!;

    public string Produto { get; set; } = null!;

    public int? DiasAguardandoArte { get; set; }

    public decimal Largura { get; set; }

    public decimal Altura { get; set; }

    public int Quantidade { get; set; }

    public string ObservacaoTecnica { get; set; } = null!;

    public string ObservacaoGeral { get; set; } = null!;

    public string StatusArte { get; set; } = null!;
}
