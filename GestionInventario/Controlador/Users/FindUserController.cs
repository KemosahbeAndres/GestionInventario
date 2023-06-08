using GestionInventario.Modelo;
using GestionInventario.Persistence;

namespace GestionInventario.Controlador.Users
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
                e.id, e.nombre, e.rut, e.clave, e.telefono, roleFinder.execute(e.id_rol)
                );
        }

        public User execute(string correo)
        {
            foreach(Usuarios user in userDao.All())
            {
                if (user.rut.Trim().Equals(correo.Trim()))
                {
                    return this.execute(user.id);
                }
            }
            return null;
        }
    }
}
