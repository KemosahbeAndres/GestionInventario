using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador
{
    class ListProductController
    {
        private ProductDao productDao;
        private FindCategoryController categoryFinder;
        private FindInventoryController inventoryFinder;
        public ListProductController()
        {
            productDao = new ProductDao();
            inventoryFinder = new FindInventoryController();
            categoryFinder = new FindCategoryController();
        }
        public List<Product> execute()
        {
            List<Product> list = new List<Product>();
            foreach (var e in productDao.All())
            {
                int stock = inventoryFinder.execute(e.id);
                list.Add(new Product(e.id, e.ean, e.nombre, e.descripcion, e.precio, stock, categoryFinder.execute(e.id_categoria)));
            }
            return list;
        }
    }
}
