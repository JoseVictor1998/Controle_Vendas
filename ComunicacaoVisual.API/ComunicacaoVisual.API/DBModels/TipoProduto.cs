using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class TipoProduto
{
    public int TipoProdutoId { get; set; }

    public int CategoriaId { get; set; }

    public string Nome { get; set; } = null!;

    public string DescricaoTecnica { get; set; } = null!;

    public bool? UsaAdesivo { get; set; }

    public bool? UsaMascara { get; set; }

    public bool Ativo { get; set; }

    public virtual CategoriaProduto Categoria { get; set; } = null!;

    public virtual ICollection<PedidoItem> PedidoItems { get; set; } = new List<PedidoItem>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
