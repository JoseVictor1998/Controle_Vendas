using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class VwBuscaRapidaPedido
{
    public string Os { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public DateTime? DataPedido { get; set; }

    public string StatusAtual { get; set; } = null!;

    public string Produto { get; set; } = null!;

    public decimal Altura { get; set; }

    public decimal Largura { get; set; }

    public int Quantidade { get; set; }
}
