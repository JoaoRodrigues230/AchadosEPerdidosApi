using System;
using System.Collections.Generic;

namespace ApiAchadosEPerdidos.Models;

public partial class Usuario
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string? Cpf { get; set; }

    public string? Ra { get; set; }

    public virtual ICollection<Devolucao> Devolucaos { get; set; } = new List<Devolucao>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Logmovimentacao> Logmovimentacaos { get; set; } = new List<Logmovimentacao>();
}
