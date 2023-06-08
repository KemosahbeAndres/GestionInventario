using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador.Users
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
            try
            {
                foreach (Usuarios user in userDao.All())
                {
                    list.Add(userFinder.execute(user.id));
                }
            }
            catch (Exception ex) { }
            return list;
        }

    }
}
