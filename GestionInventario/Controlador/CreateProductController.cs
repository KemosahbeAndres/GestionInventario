using GestionInventario.Modelo;
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
        private FindCategoryController categoryFinder;
        public CreateProductController()
        {
            productDao = new ProductDao();
            categoryFinder = new FindCategoryController();
        }
        public void execute(string name, string desc, int cost, int stock, string category)
        {
            int nprod = productDao.All().Count();
            string nombre = name.Trim();
            if (string.IsNullOrEmpty(nombre)) throw new Exception("El debes ingresar un nombre de producto!");
            string descripcion = desc.Trim();
            string ean = BarCode.generate13(nprod);
            int precio = cost > 0 ? cost : 0;
            int inventario = stock > 0 ? stock : 0;
            Category cat = categoryFinder.execute(category.Trim());
            if (cat == null) throw new Exception("No existe la categoria seleccionada!");
            Productos product = new Productos();
            product.nombre = nombre;
            product.descripcion = descripcion;
            product.ean = ean;
            product.precio = precio;
            product.id_categoria = cat.Id;
            try
            {
                productDao.Insert(product);
            }catch(Exception ex)
            {
                throw new Exception("Error al guardar producto!\n"+ex.Message);
            }
        }
    }
}
