using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class ClientePj
{
    public int ClientePjId { get; set; }

    public string Cnpj { get; set; } = null!;

    public DateTime DataCadastro { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
