using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.DBModels;

public partial class VwAlertasSla
{
    public string OsExterna { get; set; } = null!;

    public string Cliente { get; set; } = null!;

    public string StatusAtual { get; set; } = null!;

    public int? HorasNoStatus { get; set; }

    public string AlertaPrazo { get; set; } = null!;
}
