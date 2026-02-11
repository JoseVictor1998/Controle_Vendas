using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class PedidoItem
{
    public int ItemId { get; set; }

    public int PedidoId { get; set; }

    public int TipoProdutoId { get; set; }

    public decimal Largura { get; set; }

    public decimal Altura { get; set; }

    public int Quantidade { get; set; }

    public string ObservacaoTecnica { get; set; } = null!;

    public string? CaminhoFoto { get; set; }

    public virtual ICollection<ArquivoArte> ArquivoArtes { get; set; } = new List<ArquivoArte>();

    public virtual Pedido Pedido { get; set; } = null!;

    public virtual TipoProduto TipoProduto { get; set; } = null!;
}
