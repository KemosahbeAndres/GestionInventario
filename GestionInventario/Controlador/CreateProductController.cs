using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador
{
    class CreateProductController
    {
        private ProductDao productDao;
        public CreateProductController()
        {
            productDao = new ProductDao();
        }
        public void execute(string name, string desc, int cost, int stock, string category)
        {
            string nombre = name.Trim();
            string descripcion = desc.Trim();
            string ean = ;
            int precio = cost > 0 ? cost : 0;
            s
            if()
        }
    }
}
