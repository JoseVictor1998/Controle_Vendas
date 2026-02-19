using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class VwFilaImpressao
{
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
