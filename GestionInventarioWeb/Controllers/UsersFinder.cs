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

        public User? GetLoggedUser(HttpContext http)
        {
            var claims = http.User.Claims.ToList();
            string value = claims.FirstOrDefault(c => c.Type.Contains("Rut")).Value;

            if (claims.Count <= 0)
            {
                return null;
            }
            var user = _context.Usuarios.SingleOrDefault(u => u.Rut.Equals(value));

            if (user == null)
            {

                return null;
            }
            var role = _context.Roles.SingleOrDefault(r => r.Id.Equals(user.IdRol));

            string phone = user.Telefono == null ? "" : user.Telefono;

            return new User(user.Id, user.Nombre, user.Rut, phone, role.Rol);
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
