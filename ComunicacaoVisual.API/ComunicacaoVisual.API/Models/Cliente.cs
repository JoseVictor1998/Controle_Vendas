using System;
using System.Collections.Generic;

namespace ComunicacaoVisual.API.Models;

public partial class Cliente
{
    public int ClienteId { get; set; }

    public int EnderecoId { get; set; }

    public int TelefoneId { get; set; }

    public int? PfId { get; set; }

    public int? PjId { get; set; }

    public string Email { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public bool Ativo { get; set; }

    public virtual Endereco Endereco { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

    public virtual ClientePf? Pf { get; set; }

    public virtual ClientePj? Pj { get; set; }

    public virtual Telefone Telefone { get; set; } = null!;
}
