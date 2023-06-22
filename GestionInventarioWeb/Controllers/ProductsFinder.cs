using GestionInventarioWeb.Data;
using GestionInventarioWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionInventarioWeb.Controllers
{
    public class ProductsFinder
    {
        private readonly GestionInventarioContext _context;

        public ProductsFinder(GestionInventarioContext context)
        {
            _context = context;
        }

        private async Task<Product> fromModel(Producto p)
        {
            var s = await _context.Inventarios.OrderByDescending(i => i.Fecha).FirstOrDefaultAsync(i => i.IdProducto == p.Id);
            var stock = 0;
            if (s != null)
            {
                stock = s.Cantidad;
            }
            return new Product(p.Id, p.Ean, p.Nombre, p.Descripcion, p.Precio, p.IdCategoriaNavigation.Categoria1, stock);
        }

        public async Task<IEnumerable<Product>> FindAllAsync()
        {
            var products = new List<Product>();

            foreach(var p in await _context.Productos.Include(p => p.IdCategoriaNavigation).ToListAsync())
            {
                products.Add(await fromModel(p));
            }

            return products;
        }

        public async Task<IEnumerable<Product>> FindBySaleAsync(int id)
        {
            var items = _context.ItemVenta
                .Include(i => i.IdProductoNavigation)
                .Where(i => i.Id == id).ToList();
            var products = new List<Product>();
            foreach (var item in items)
            {
                var p = await fromModel(item.IdProductoNavigation);
                p.Cantidad = item.Cantidad;
                products.Add(p);
            }
            return products;
        }

        public async Task<IEnumerable<Product>> FindByBuyAsync(int id)
        {
            var items = _context.ItemCompras
                .Include(i => i.IdProductoNavigation)
                .Where(i => i.Id == id).ToList();
            var products = new List<Product>();
            foreach (var item in items)
            {
                var p = await fromModel(item.IdProductoNavigation);
                p.Cantidad = item.Cantidad;
                products.Add(p);
            }
            return products;
        }
    }
}
