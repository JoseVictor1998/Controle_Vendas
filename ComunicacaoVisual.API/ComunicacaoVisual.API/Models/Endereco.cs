using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class Endereco
{
    public int EnderecoId { get; set; }

    public string Cidade { get; set; } = null!;

    public string Cep { get; set; } = null!;

    public string Bairro { get; set; } = null!;

    public string Rua { get; set; } = null!;

    public int? Numero { get; set; }

    public string? Referencia { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
