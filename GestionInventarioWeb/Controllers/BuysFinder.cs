using GestionInventarioWeb.Data;
using GestionInventarioWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventarioWeb.Controllers
{
    public class BuysFinder
    {
        private readonly GestionInventarioContext _context;
        private readonly ProductsFinder _productsFinder;

        public BuysFinder(GestionInventarioContext context)
        {
            _context = context;
            _productsFinder = new ProductsFinder(_context);
        }

        public async Task<IEnumerable<Buy>> FindAllAsync()
        {
            var buys = new List<Buy>();

            foreach(var buy in await _context.Compras.Include(s => s.IdUsuarioNavigation).Include(s => s.IdUsuarioNavigation.IdRolNavigation).ToListAsync() )
            {
                var user = buy.IdUsuarioNavigation;
                var buyer = new User(user.Id, user.Nombre, user.Rut, user.Telefono, user.IdRolNavigation.Rol);
                buys.Add(new Buy(buy.Id, buy.Fecha, buyer, await _productsFinder.FindByBuy(buy.Id)));
            }

            return buys;
        }

        public async Task<Buy?> Find(int id)
        {
            var buy = await _context.Compras
                .Include(s => s.IdUsuarioNavigation)
                .Include(s => s.IdUsuarioNavigation.IdRolNavigation)
                .SingleOrDefaultAsync(s => s.Id == id);
            if(buy == null) return null;
            var user = buy.IdUsuarioNavigation;
            var buyer = new User(user.Id, user.Nombre, user.Rut, user.Telefono, user.IdRolNavigation.Rol);
            return new Buy(buy.Id, buy.Fecha, buyer, await _productsFinder.FindByBuy(buy.Id));
        }

        public async Task<bool> HasProduct(int buyid, int productid)
        {
            return _context.ItemCompras.Any(i => i.IdCompra == buyid && i.IdProducto == productid);
        }
    }
}
