using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class VwDashboardBiGerencial
{
    public decimal? TotalVendasMes { get; set; }

    public decimal? TotalCustosMes { get; set; }

    public decimal? LucroEstimado { get; set; }
}
