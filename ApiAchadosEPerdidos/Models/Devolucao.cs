using System;
using System.Collections.Generic;

namespace ApiAchadosEPerdidos.Models;

public partial class Devolucao
{
    public Guid Id { get; set; }

    public Guid Itemid { get; set; }

    public Guid Usuarioresponsavelid { get; set; }

    public string Nomeretirada { get; set; } = null!;

    public string Documentoretirada { get; set; } = null!;

    public DateTime Datadevolucao { get; set; }

    public string? Observacao { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Usuario Usuarioresponsavel { get; set; } = null!;
}
