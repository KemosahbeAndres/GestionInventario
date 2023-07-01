using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace GestionInventarioWeb.Models;

public partial class Compra
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int IdUsuario { get; set; }

    [ValidateNever]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<ItemCompra> ItemCompras { get; set; } = new List<ItemCompra>();
}
