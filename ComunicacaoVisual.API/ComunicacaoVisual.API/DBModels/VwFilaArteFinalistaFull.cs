using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class VwFilaArteFinalistaFull
{
    public int? ArquivoId { get; set; }
    public int ItemId { get; set; }
    public int PedidoId { get; set; }
    public int PedidoStatusId { get; set; }
    public string Os { get; set; } = null!;

    public string Cliente { get; set; } = null!;

    public string Produto { get; set; } = null!;

    public decimal Largura { get; set; }

    public decimal Altura { get; set; }

    public int Quantidade { get; set; }

    public string ObservacaoTecnica { get; set; } = null!;

    public string? CaminhoArte { get; set; }

    public string? StatusArte { get; set; }

    public string SetorFila { get; set; } = null!;
}
