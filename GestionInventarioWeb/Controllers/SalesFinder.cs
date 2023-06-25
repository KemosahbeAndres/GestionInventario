using GestionInventarioWeb.Data;
using GestionInventarioWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventarioWeb.Controllers
{
    public class SalesFinder
    {
        private readonly GestionInventarioContext _context;
        private readonly ProductsFinder _productsFinder;

        public SalesFinder(GestionInventarioContext context)
        {
            _context = context;
            _productsFinder = new ProductsFinder(_context);
        }

        public async Task<IEnumerable<Sale>> FindAllAsync()
        {
            var sales = new List<Sale>();

            foreach(var sale in await _context.Ventas.Include(s => s.IdVendedorNavigation).Include(s => s.IdVendedorNavigation.IdRolNavigation).ToListAsync() )
            {
                var user = sale.IdVendedorNavigation;
                var seller = new User(user.Id, user.Nombre, user.Rut, user.Telefono, user.IdRolNavigation.Rol);
                sales.Add(new Sale(sale.Id, sale.Fecha, seller, await _productsFinder.FindBySale(sale.Id)));
            }

            return sales;
        }

        public async Task<Sale?> Find(int id)
        {
            var sale = await _context.Ventas
                .Include(s => s.IdVendedorNavigation)
                .Include(s => s.IdVendedorNavigation.IdRolNavigation)
                .SingleOrDefaultAsync(s => s.Id == id);
            if(sale == null) return null;
            var user = sale.IdVendedorNavigation;
            var seller = new User(user.Id, user.Nombre, user.Rut, user.Telefono, user.IdRolNavigation.Rol);
            return new Sale(sale.Id, sale.Fecha, seller, await _productsFinder.FindBySale(sale.Id));
        }

        public async Task<bool> HasProduct(int saleid, int productid)
        {
            return _context.ItemVenta.Any(i => i.IdVenta == saleid && i.IdProducto == productid);
        }
    }
}
