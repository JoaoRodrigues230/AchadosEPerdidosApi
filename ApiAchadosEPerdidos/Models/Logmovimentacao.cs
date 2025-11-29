using System;
using System.Collections.Generic;

namespace ApiAchadosEPerdidos.Models;

public partial class Logmovimentacao
{
    public Guid Id { get; set; }

    public Guid? Itemid { get; set; }

    public Guid? Usuarioactorid { get; set; }

    public string Tipoacao { get; set; } = null!;

    public DateTime Dataocorrencia { get; set; }

    public string? Detalhes { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Usuario? Usuarioactor { get; set; }
}
