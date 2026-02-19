using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class Material
{
    public int MaterialId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public bool Ativo { get; set; }

    public virtual ICollection<TipoProduto> TipoProdutos { get; set; } = new List<TipoProduto>();
}
