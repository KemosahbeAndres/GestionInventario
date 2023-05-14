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
        private static readonly UserDao userdao = new UserDao();

        public List<User> Execute()
        {
            return User.All();
        }

        protected static List<User> ToList(List<Usuarios> list)
        {
            List<User> result = new List<User>();
            foreach (Usuarios user in list)
            {
                result.Add(new User(
                    user.id,

                    ));
            }
            return result;
        }
    }
}
