using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador.Users
{
    class ListRolesController
    {
        private readonly RoleDao roleDao = new RoleDao();

        public List<Role> execute()
        {
            List<Role> list = new List<Role>();
            try
            {
                foreach (Roles role in roleDao.All())
                {
                    list.Add(new Role(role.id, role.rol));
                }
            } catch(Exception ex) { }
            return list;
        }
    }
}
