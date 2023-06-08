using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador.Products.Categories
{
    class CreateCategoryController
    {
        private CategoryDao categoryDao;
        private FindCategoryController categoryFinder;
        public CreateCategoryController()
        {
            categoryDao = new CategoryDao();
            categoryFinder = new FindCategoryController();
        }
        public void execute(string name)
        {
            if (categoryFinder.execute(name.Trim()) != null) throw new Exception("La categoria ya existe!");
            var e = new Categorias();
            e.categoria = name.Trim();
            try
            {
                categoryDao.Insert(e);
            }
            catch(Exception ex)
            {
                throw new Exception("Error al guardar categoria!\n"+ex.Message);
            }
        }
    }
}
