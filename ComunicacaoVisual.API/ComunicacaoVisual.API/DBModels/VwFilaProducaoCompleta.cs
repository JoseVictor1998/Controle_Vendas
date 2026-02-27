using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

public partial class VwFilaProducaoCompleta
{
    public string Os { get; set; } = null!;
    public string Cliente { get; set; } = null!;
    public string Produto { get; set; } = null!;

    [Column("MaterialBase")]
    public string? MaterialBase { get; set; }

    public decimal Largura { get; set; }
    public decimal Altura { get; set; }
    public int Quantidade { get; set; }

    [Column("CaminhoFoto")]
    public string? CaminhoFoto { get; set; }

    [Column("HorasDesdeAbertura")]
    public int? HorasDesdeAbertura { get; set; }

    [Column("VendedorResponsavel")]
    public string VendedorResponsavel { get; set; } = null!;

    [Column("ObservacaoGeral")]
    public string? ObservacaoGeral { get; set; }

    [Column("ObservacaoTecnica")]
    public string? ObservacaoTecnica { get; set; }
}