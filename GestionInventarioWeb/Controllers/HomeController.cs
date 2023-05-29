using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionInventarioWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace GestionInventarioWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [Route("/")]
        [HttpGet("/Dashboard", Name = "Dashboard")]
        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Privacy")]
        public IActionResult Privacy()
        {
            if(HttpContext.Session.GetString("username") == null)
            {
                HttpContext.Session.SetString("ErrorMessage", "Debes iniciar sesion para entrar aqui!");
                return RedirectToRoute("Login");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
