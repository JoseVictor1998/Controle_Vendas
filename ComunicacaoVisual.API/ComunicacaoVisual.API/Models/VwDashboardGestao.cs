using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class VwDashboardGestao
{
    public string Etapa { get; set; } = null!;

    public int? TotalPedidos { get; set; }
}
