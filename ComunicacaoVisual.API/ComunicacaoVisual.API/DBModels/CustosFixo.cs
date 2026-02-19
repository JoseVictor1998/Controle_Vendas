using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class CustosFixo
{
    public int CustoId { get; set; }

    public string Descricao { get; set; } = null!;

    public decimal Valor { get; set; }

    public DateOnly DataVencimento { get; set; }

    public bool? StatusPagamento { get; set; }
}
