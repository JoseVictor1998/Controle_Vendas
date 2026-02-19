using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class StatusProducao
{
    public int StatusId { get; set; }

    public string Nome { get; set; } = null!;

    public int Ordem { get; set; }

    public bool Ativo { get; set; }

    public virtual ICollection<HistoricoStatus> HistoricoStatuses { get; set; } = new List<HistoricoStatus>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
