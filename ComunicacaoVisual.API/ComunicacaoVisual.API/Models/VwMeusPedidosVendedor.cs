using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class VwMeusPedidosVendedor
{
    public int? VendedorId { get; set; }

    public string OsExterna { get; set; } = null!;

    public string Cliente { get; set; } = null!;

    public string StatusAtual { get; set; } = null!;

    public decimal? ValorTotal { get; set; }

    public DateTime? DataPedido { get; set; }
}
