using GestionInventario.Controlador.Products;
using GestionInventario.Controlador.Products.Categories;
using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Linq;

namespace GestionInventario.Controlador
{
    class CreateProductController
    {
        private ProductDao productDao;
        private FindCategoryController categoryFinder;
        private CreateInventoryRecordController inventoryCreator;

        public CreateProductController()
        {
            productDao = new ProductDao();
            categoryFinder = new FindCategoryController();
            inventoryCreator = new CreateInventoryRecordController();
        }

        public string getPreviewBarCode()
        {
            int count = productDao.All().Count();
            return BarCode.generate13(count);
        }

        public void execute(string name, string desc, int cost, int stock, string category, int id = 0)
        {
            
            string nombre = name.Trim();
            if (string.IsNullOrEmpty(nombre)) throw new Exception("El debes ingresar un nombre de producto!");
            string descripcion = desc.Trim();
            string ean = getPreviewBarCode();
            int precio = cost > 0 ? cost : 0;
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
                if(id <= 0)
                {
                    int pid = productDao.Insert(product);
                    inventoryCreator.execute(pid, stock);
                }
                else
                {
                    product.id = id;
                    int pid = productDao.Modify(product);
                    inventoryCreator.execute(pid, stock);
                }
                
            }catch(Exception ex)
            {
                throw new Exception("Error al guardar producto!\n"+ex.Message);
            }
        }

    }
}
