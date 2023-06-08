using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador
{
    class FindInventoryController
    {
        private InventoryDao inventoryDao;
        public FindInventoryController()
        {
            inventoryDao = new InventoryDao();
        }
        public int execute(int productid)
        {
            try
            {
                return inventoryDao.For(productid).First().cantidad;
            }catch(Exception ex)
            {
                return 0;
            }
        }
    }
}
