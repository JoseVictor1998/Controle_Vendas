using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class ClientePf
{
    public int ClientePfId { get; set; }

    public string Cpf { get; set; } = null!;

    public DateTime DataCadastro { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
