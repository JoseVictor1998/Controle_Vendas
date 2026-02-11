using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class VwMonitoramentoGlobal
{
    public string Os { get; set; } = null!;

    public string Cliente { get; set; } = null!;

    public string Produto { get; set; } = null!;

    public string StatusProducao { get; set; } = null!;

    public string? DataEntrada { get; set; }
}
