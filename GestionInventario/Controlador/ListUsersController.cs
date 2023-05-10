using GestionInventario.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador
{
    public class ListUsersController
    {
        public List<User> Execute()
        {
            return User.All();
        }
    }
}
