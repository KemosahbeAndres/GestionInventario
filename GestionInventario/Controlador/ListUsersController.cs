using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador
{
    public class ListUsersController
    {
        private FindUserController userFinder;
        private UserDao userDao;

        public ListUsersController()
        {
            userDao = new UserDao();
            userFinder = new FindUserController();
        }

        public List<User> execute()
        {
            List<User> list = new List<User>();
            foreach (Usuarios user in userDao.All())
            {
                list.Add(userFinder.execute(user.id));
            }
            return list;
        }

    }
}
