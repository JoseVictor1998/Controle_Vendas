using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class ArquivoArte
{
    public int ArquivoId { get; set; }

    public int ItemId { get; set; }

    public string NomeArquivo { get; set; } = null!;

    public string CaminhoArquivo { get; set; } = null!;

    public int StatusArteId { get; set; }

    public virtual PedidoItem Item { get; set; } = null!;

    public virtual StatusArte StatusArte { get; set; } = null!;
}
