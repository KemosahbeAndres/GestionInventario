using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionInventarioWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using GestionInventarioWeb.Data;
using AspNetCoreHero.ToastNotification.Abstractions;

namespace GestionInventarioWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly SalesFinder _salesFinder;
        private readonly ProductsFinder _productsFinder;
        private readonly ILogger _logger;
        private readonly INotyfService _notifyService;

        public HomeController(ILogger<HomeController> logger, GestionInventarioContext context, INotyfService notify)
        {
            _context = context;
            _logger = logger;
            _notifyService = notify;
            _salesFinder = new SalesFinder(_context);
            _productsFinder = _salesFinder._productsFinder;
        }
        [Route("/")]
        [HttpGet("/Dashboard", Name = "Dashboard")]
        [Authorize(Roles = "Administrador, Vendedor")]
        public async Task<IActionResult> Index()
        {
            var ventas = (await _salesFinder.FindAllAsync());
            var prods = (await _productsFinder.FindAllAsync());

            //Ventasmes
            DateTime date = DateTime.Now;
            var first = new DateTime(date.Year, date.Month, 1);
            var last = first.AddMonths(1).AddSeconds(-1);

            var vmes = ventas.Where(v => v.Date >= first);
            var vhoy = ventas.Where(v => v.Date >= DateTime.Today);


            int ventashoy = vhoy.Count();
            int totalhoy = 0;
            foreach(var venta in vhoy)
            {
                totalhoy += venta.Cost;
            }

            int ventasmes  = vmes.Count();
            int totalmes = 0;
            foreach(var venta in vmes)
            {
                totalmes += venta.Cost;
            }

            var model = new DataDashboard(
                GetLoggedUser(),
                ventas.OrderByDescending(v => v.Date).Take(10),
                prods.OrderBy(p => p.Stock).Where(p => p.Stock <= 10),
                ventasmes,
                ventashoy,
                totalhoy,
                totalmes
                );
            return View("Views/DashboardView.cshtml", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/api/users")]
        [AllowAnonymous]
        public IActionResult ajax()
        {
            var users = _context.Usuarios.ToArray();
            var usuarios = new String[users.Length];

            for(int i = 0; i <= users.Length-1; i++)
            {
                usuarios[i] = users[i].Nombre;
            }

            return Json(usuarios);
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
