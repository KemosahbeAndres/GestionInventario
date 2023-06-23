using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace GestionInventarioWeb.Models;

public partial class ItemVentum
{
    public int Id { get; set; }

    public int IdVenta { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    [ValidateNever]
    public virtual Producto IdProductoNavigation { get; set; } = null!;

    [ValidateNever]
    public virtual Venta IdVentaNavigation { get; set; } = null!;
}
