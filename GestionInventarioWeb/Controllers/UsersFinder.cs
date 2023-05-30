using GestionInventarioWeb.Data;
using GestionInventarioWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;

namespace GestionInventarioWeb.Controllers
{
    public class UsersFinder : Controller
    {
        private readonly GestionInventarioContext _context;

        public UsersFinder(GestionInventarioContext context)
        {
            _context = context;
        }

        public IEnumerable<User> FindAll()
        {
            var userList = new Collection<User>();
            foreach(var user in _context.Usuarios.ToList())
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id.Equals(user.IdRol));
                if (role == null)
                {
                    userList.Add(new User(user.Id, user.Nombre, user.Rut, user.Telefono, null));
                }
                else
                {
                    userList.Add(new User(user.Id, user.Nombre, user.Rut, user.Telefono, role.Rol));
                }
                
            }
            return userList;
        }
    }
}
