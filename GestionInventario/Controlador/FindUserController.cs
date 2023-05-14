using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador
{
    class FindUserController
    {
        private UserDao userDao;
        private FindRoleController roleFinder;

        public FindUserController()
        {
            userDao = new UserDao();
            roleFinder = new FindRoleController();
        }
        public User execute(int id)
        {
            if (!userDao.Exists(id)) return null;
            var e = userDao.Get(id);
            return new User(
                e.id, e.nombre, e.correo, e.clave, e.telefono, roleFinder.execute(e.id_rol)
                );
        }
        public User execute(string correo)
        {
            foreach(Usuarios user in userDao.All())
            {
                if (user.correo.Trim().Equals(correo.Trim()))
                {
                    return this.execute(user.id);
                }
            }
            return null;
        }
    }
}
