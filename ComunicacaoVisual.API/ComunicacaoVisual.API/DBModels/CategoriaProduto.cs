using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class CategoriaProduto
{
    public int CategoriaId { get; set; }

    public string Nome { get; set; } = null!;

    public string Descricao { get; set; } = null!;

    public bool Ativo { get; set; }

    public virtual ICollection<TipoProduto> TipoProdutos { get; set; } = new List<TipoProduto>();
}
