using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class Pedido
{
    public int PedidoId { get; set; }

    public int ClienteId { get; set; }

    public string OsExterna { get; set; } = null!;

    public DateTime? DataPedido { get; set; }

    public int StatusId { get; set; }

    public string ObservacaoGeral { get; set; } = null!;

    public decimal? ValorTotal { get; set; }

    public string? FormaPagamento { get; set; }

    public int? VendedorId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<HistoricoStatus> HistoricoStatuses { get; set; } = new List<HistoricoStatus>();

    public virtual ICollection<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();

    public virtual StatusProducao Status { get; set; } = null!;

    public virtual Usuario? Vendedor { get; set; }
}
