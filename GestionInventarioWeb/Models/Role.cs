using System;
using System.Collections.Generic;

namespace GestionInventarioWeb.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Rol { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
