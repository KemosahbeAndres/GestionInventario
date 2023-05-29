using System;
using System.Collections.Generic;

namespace GestionInventarioWeb.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Ean { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int Precio { get; set; }

    public int IdCategoria { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public virtual ICollection<ItemCompra> ItemCompras { get; set; } = new List<ItemCompra>();

    public virtual ICollection<ItemVentum> ItemVenta { get; set; } = new List<ItemVentum>();
}
