using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador.Products.Categories
{
    class ModifyCategoryController
    {
        private CategoryDao categoryDao;
        public ModifyCategoryController()
        {
            categoryDao = new CategoryDao();
        }

        public void execute(Category category, string newname)
        {
            if (category == null) throw new Exception("No se encontro la categoria a modificar!");
            if (String.IsNullOrEmpty(newname)) throw new Exception("El nuevo nombre no es valido!");
            Categorias cat = categoryDao.Get(category.Id);
            cat.categoria = newname.Trim();
            try
            {
                categoryDao.Modify(cat);
            }catch(Exception ex)
            {
                throw new Exception("Error al modificar la categoria!\n" + ex.Message);
            }
        }
    }
}
