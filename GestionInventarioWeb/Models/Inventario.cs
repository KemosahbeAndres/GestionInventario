using System;
using System.Collections.Generic;

namespace GestionInventarioWeb.Models;

public partial class Inventario
{
    public int Id { get; set; }

    public int Cantidad { get; set; }

    public DateTime Fecha { get; set; }

    public int IdProducto { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
