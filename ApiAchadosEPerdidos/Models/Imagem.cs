using System;
using System.Collections.Generic;

namespace ApiAchadosEPerdidos.Models;

public partial class Imagem
{
    public Guid Id { get; set; }

    public string? Urlimagem1 { get; set; }

    public string? Urlimagem2 { get; set; }

    public string? Urlimagem3 { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
