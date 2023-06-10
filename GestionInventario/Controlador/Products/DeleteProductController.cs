using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador
{
    class DeleteProductController
    {
        private ProductDao productDao;
        private InventoryDao inventoryDao;

        public DeleteProductController()
        {
            productDao = new ProductDao();
            inventoryDao = new InventoryDao();
        }
        public void execute(Product product)
        {
            if (!productDao.Exists(product.Id)) throw new Exception("No existe el producto seleccionado!");
            
            try
            {
                if (inventoryDao.For(product.Id).Count > 0)
                {
                    var inv = inventoryDao.For(product.Id).Last();
                    inventoryDao.Delete(inv.id);
                }
                productDao.Delete(product.Id);
            }catch(Exception ex)
            {
                throw new Exception("Error al borrar!");
            }
            
        }
    }
}
