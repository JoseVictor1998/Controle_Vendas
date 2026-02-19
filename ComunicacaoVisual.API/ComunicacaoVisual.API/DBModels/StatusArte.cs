using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class StatusArte
{
    public int StatusArteId { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<ArquivoArte> ArquivoArtes { get; set; } = new List<ArquivoArte>();
}
