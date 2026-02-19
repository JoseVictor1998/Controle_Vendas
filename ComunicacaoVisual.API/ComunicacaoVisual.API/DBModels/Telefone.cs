using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class Telefone
{
    public int TelefoneId { get; set; }

    public string Ddd { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
}
