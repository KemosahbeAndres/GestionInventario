using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador.Users
{
    class FindRoleController
    {
        private readonly RoleDao roleDao = new RoleDao();
        public Role execute(int id)
        {
            if (!roleDao.Exists(id)) return null;
            var e = roleDao.Get(id);
            return new Role(e.id, e.rol);
        }
        public Role execute(string name)
        {
            foreach(Roles role in roleDao.All())
            {
                if (role.rol.Trim().Equals(name.Trim()))
                {
                    return this.execute(role.id);
                }
            }
            return null;
        }
    }
}
