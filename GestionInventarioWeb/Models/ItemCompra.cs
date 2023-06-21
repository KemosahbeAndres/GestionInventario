using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace GestionInventarioWeb.Models;

public partial class ItemCompra
{
    public int Id { get; set; }

    public int IdCompra { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    [ValidateNever]
    public virtual Compra IdCompraNavigation { get; set; } = null!;

    [ValidateNever]
    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
