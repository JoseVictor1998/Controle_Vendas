using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class HistoricoStatus
{
    public int HistoricoId { get; set; }

    public int PedidoId { get; set; }

    public int StatusId { get; set; }

    public DateTime? DataMudanca { get; set; }

    public int UsuarioId { get; set; }

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual StatusProducao Status { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
