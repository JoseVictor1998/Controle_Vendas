using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class VwFilaArte
{
    public string Os { get; set; } = null!;

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
