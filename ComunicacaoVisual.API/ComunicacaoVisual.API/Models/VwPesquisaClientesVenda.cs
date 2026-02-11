using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class VwPesquisaClientesVenda
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Documento { get; set; }

    public string? TipoCliente { get; set; }

    public string Telefone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Localidade { get; set; } = null!;

    public bool Ativo { get; set; }
}
