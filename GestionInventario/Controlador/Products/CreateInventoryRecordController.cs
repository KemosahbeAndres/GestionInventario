using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador.Products
{
    class CreateInventoryRecordController
    {
        private InventoryDao inventoryDao;
        public CreateInventoryRecordController()
        {
            inventoryDao = new InventoryDao();
        }

        public void execute(int productid, int stock)
        {
            int inventario = stock > 0 ? stock : 0;
            Inventario item = new Inventario();
            item.cantidad = inventario;
            item.fecha = DateTime.Now;
            item.id_producto = productid;
            try
            {
                inventoryDao.Insert(item);
            }catch(Exception ex)
            {
                throw new Exception("Error al guardar el inventario!\n"+ex.Message);
            }
            
        }
    }
}
