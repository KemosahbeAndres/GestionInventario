using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System.Collections.Generic;

namespace GestionInventario.Controlador.Products.Categories
{
    class FindCategoryController
    {
        private CategoryDao categoryDao;
        public FindCategoryController()
        {
            categoryDao = new CategoryDao();
        }
        public List<Category> execute()
        {
            List<Category> list = new List<Category>();
            foreach (var e in categoryDao.All())
            {
                list.Add(new Category(e.id, e.categoria));
            }
            return list;
        }
        public Category execute(int id)
        {
            if (!categoryDao.Exists(id)) return null;
            var e = categoryDao.Get(id);
            return new Category(e.id, e.categoria);
        }
        public Category execute(string name)
        {
            foreach(var e in categoryDao.All())
            {
                if (e.categoria.Equals(name.Trim()))
                {
                    return new Category(e.id, e.categoria);
                }
            }
            return null;
        }
    }
}
