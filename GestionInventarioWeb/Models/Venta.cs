using System;
using System.Collections.Generic;

namespace GestionInventarioWeb.Models;

public partial class Venta
{
    public int Id { get; set; }

    public DateTime Fecha { get; set; }

    public int IdVendedor { get; set; }

    public virtual Usuario IdVendedorNavigation { get; set; } = null!;

    public virtual ICollection<ItemVentum> ItemVenta { get; set; } = new List<ItemVentum>();
}
