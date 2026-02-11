using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComunicacaoVisual.API.Models;

public partial class Usuario
{
    [Key]
    public int UsuarioId { get; set; }

    public string Nome { get; set; } = null!;

    public string Funcao { get; set; } = null!;

    public string? Login { get; set; }

    public string? Senha { get; set; }

    public string? NivelAcesso { get; set; }

    public virtual ICollection<HistoricoStatus> HistoricoStatuses { get; set; } = new List<HistoricoStatus>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
