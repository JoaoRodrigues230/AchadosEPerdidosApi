using System;
using System.Collections.Generic;

namespace ApiAchadosEPerdidos.Models;

public partial class Imagemitem
{
    public Guid Id { get; set; }

    public Guid Itemid { get; set; }

    public string Urlimagem { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
