using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComunicacaoVisual.API.DBModels;

public partial class VwFilaImpressao
{
    public int ItemId { get; set; }

    [Column("Caminho_Foto")]
    public string? CaminhoFoto { get; set; }
    public int? ArquivoId { get; set; }
    public string Os { get; set; } = null!;


    public string Cliente { get; set; } = null!;

    public string Produto { get; set; } = null!;

    public int? DiasEmImpressao { get; set; }

    public string? MaterialBase { get; set; }

    public decimal Largura { get; set; }

    public decimal Altura { get; set; }

    public int Quantidade { get; set; }

    public string LinkArte { get; set; } = null!;

    public string ObservacaoGeral { get; set; } = null!;

    public string ObservacaoTecnica { get; set; } = null!;
}
