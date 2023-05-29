using System;
using System.Collections.Generic;

namespace GestionInventarioWeb.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string Categoria1 { get; set; } = null!;

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
