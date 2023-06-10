using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador.Products.Categories
{
    class DeleteCategoryController
    {
        private CategoryDao categoryDao;
        private FindCategoryController categoryFinder;
        public DeleteCategoryController()
        {
            categoryDao = new CategoryDao();
            categoryFinder = new FindCategoryController();
        }
        public void execute(string name)
        {
            var cat = categoryFinder.execute(name.Trim());
            if (cat == null) throw new Exception("La categoria seleccionada no existe!");
            try
            {
                categoryDao.Delete(cat.Id);
            }
            catch(Exception ex)
            {
                throw new Exception("Error al eliminar categoria!\n"+ex.Message);
            }
        }
    }
}
