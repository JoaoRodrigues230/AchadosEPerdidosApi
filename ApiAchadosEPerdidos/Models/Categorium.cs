using System;
using System.Collections.Generic;

namespace ApiAchadosEPerdidos.Models;

public partial class Categorium
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
