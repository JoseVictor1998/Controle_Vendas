using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class VwHistoricoPedidosCliente
{
    public string Cliente { get; set; } = null!;

    public string Os { get; set; } = null!;

    public DateTime? DataPedido { get; set; }

    public string Produto { get; set; } = null!;

    public decimal Largura { get; set; }

    public decimal Altura { get; set; }

    public int Quantidade { get; set; }

    public string StatusAtual { get; set; } = null!;

    public string ObservacaoTecnica { get; set; } = null!;
}
