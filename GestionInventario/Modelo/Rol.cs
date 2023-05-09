using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Modelo
{
    enum Roles
    {
        ADMIN,
        VENDEDOR
    }
    class Rol
    {
        public int Id { get; private set; }
        public Roles Nombre { get; private set; }

        public Rol(int id, string rol)
        {
            Id = id;
            Nombre = rol == "Adminstrador" ? Roles.ADMIN : Roles.VENDEDOR;
        }
    }
}
