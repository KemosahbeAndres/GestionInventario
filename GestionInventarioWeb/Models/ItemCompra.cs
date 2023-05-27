using System;
using System.Collections.Generic;

namespace GestionInventarioWeb.Models;

public partial class ItemCompra
{
    public int Id { get; set; }

    public int IdCompra { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public virtual Compra IdCompraNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
