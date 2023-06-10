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

        public async Task<IEnumerable<Product>> FindAllAsync()
        {
            var products = new List<Product>();

            foreach(var p in await _context.Productos.Include(p => p.IdCategoriaNavigation).ToListAsync())
            {
                var s = await _context.Inventarios.OrderByDescending(i => i.Fecha).FirstOrDefaultAsync(i => i.IdProducto == p.Id);
                var stock = 0;
                if(s != null) {
                    stock = s.Cantidad;
                }
                products.Add(new Product(p.Id, p.Ean, p.Nombre, p.Descripcion, p.Precio, p.IdCategoriaNavigation.Categoria1, stock));
            }

            return products;
        }
    }
}
