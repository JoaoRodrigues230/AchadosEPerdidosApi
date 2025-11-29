using System;
using System.Collections.Generic;

namespace ApiAchadosEPerdidos.Models;

public partial class Item
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string Status { get; set; } = null!;

    public DateTime Dataachado { get; set; }

    public Guid? Categoriaid { get; set; }

    public Guid? Localid { get; set; }

    public Guid? Usuarioqueachouid { get; set; }

    public Guid? Imagemid { get; set; }

    public virtual Categorium? Categoria { get; set; }

    public virtual ICollection<Devolucao> Devolucaos { get; set; } = new List<Devolucao>();

    public virtual Imagem? Imagem { get; set; }

    public virtual Local? Local { get; set; }

    public virtual ICollection<Logmovimentacao> Logmovimentacaos { get; set; } = new List<Logmovimentacao>();

    public virtual Usuario? Usuarioqueachou { get; set; }
}
