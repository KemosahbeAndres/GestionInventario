using GestionInventarioWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionInventarioWeb.Controllers
{
    public class ApiController : Controller
    {
        private readonly GestionInventarioContext _context;
        private readonly ProductsFinder _productsFinder;

        public ApiController(GestionInventarioContext context)
        {
            _context = context;
            _productsFinder = new ProductsFinder(_context);
        }

        [HttpGet("/api/Productos"), ActionName("GetAllProducts")]
        public async Task<IActionResult> GetProducts(int id)
        {
            return Json(await _productsFinder.FindAllAsync());
        }

        [HttpGet("/api/Productos/Find")]
        public async Task<IActionResult> FindProducts(string name = "", string desc = "")
        {
            return Json(await _productsFinder.Find(name, desc));
        }

        [HttpGet("/api/Productos/Venta/{id}")]
        public async Task<IActionResult> GetProductsFromSale(int id)
        {
            return Json(await _productsFinder.FindBySale(id));
        }

        [HttpGet("/api/Ventas")]
        public async Task<IActionResult> GetSales()
        {
            var ventas = await _context.Ventas.Include(s => s.IdVendedorNavigation).ToListAsync();
            var sales = new List<Object>();
            foreach (var venta in ventas)
            {
                var total = 0;
                var products = await _productsFinder.FindBySale(venta.Id);
                foreach (var product in products)
                {
                    total += product.Price * product.Cantidad;
                }
                sales.Add(new
                {
                    id = venta.Id,
                    date = venta.Fecha.ToShortDateString(),
                    seller = venta.IdVendedorNavigation.Nombre,
                    cost = total
                });
            }
            return Json(sales.ToArray());
        }
        
        private async Task<Inventario?> getLastInventory(int productid)
        {
            return await _context.Inventarios.OrderBy(i => i.Id).LastOrDefaultAsync(i => i.IdProducto == productid);
        }
    }
}
