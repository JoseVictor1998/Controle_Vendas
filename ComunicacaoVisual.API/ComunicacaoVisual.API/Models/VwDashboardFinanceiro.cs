using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class VwDashboardFinanceiro
{
    public string Os { get; set; } = null!;

    public string Cliente { get; set; } = null!;

    public decimal? ValorTotal { get; set; }

    public string? FormaPagamento { get; set; }

    public string? DataVenda { get; set; }

    public string StatusAtual { get; set; } = null!;
}
