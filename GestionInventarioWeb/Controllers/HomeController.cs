using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionInventarioWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GestionInventarioWeb.Data;

namespace GestionInventarioWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly ILogger _logger;

        public HomeController(ILogger<HomeController> logger, GestionInventarioContext context)
        {
            _context = context;
            _logger = logger;
        }
        [Route("/")]
        [HttpGet("/Dashboard", Name = "Dashboard")]
        [Authorize(Roles = "Administrador, Vendedor")]
        public IActionResult Index()
        {
            var model = GetLoggedUser();
            return View("Views/DashboardView.cshtml", model);
        }

        [HttpGet("/Privacy")]
        [Authorize(Roles = "Administrador, Vendedor")]
        public IActionResult Privacy()
        {
            return View(GetLoggedUser());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private User? GetLoggedUser()
        {
            var claims = HttpContext.User.Claims.ToList();
            string value = claims.FirstOrDefault(c => c.Type.Contains("Rut")).Value;
            /*
            string message = "";
            foreach(var c in claims)
            {
                message += " [" + c.Type.ToString()+ "] |";
            }
            throw new Exception($"RUT: {value} Claims data => {message}");
            return null;*/
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
    }
}
