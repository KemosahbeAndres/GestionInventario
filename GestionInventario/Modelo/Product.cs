using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Modelo
{
    public class Product
    {
        public int Id { get; }
        public string Ean { get; }
        public string Nombre { get; }
        public string Descripcion { get; }
        public int Precio { get; }
        public int Existencias { get; }
        public Category Categoria { get; }

        public Product(int id, string ean, string name, string desc, int cost, int stock, Category category)
        {
            this.Id = id < 0 ? 0 : id;
            this.Ean = ean.Trim();
            this.Nombre = name.Trim();
            this.Descripcion = desc.Trim();
            this.Precio = cost < 0 ? 0 : cost;
            this.Existencias = stock < 0 ? 0 : stock;
            this.Categoria = category;
        }

        public Product(string ean, string name, string desc, int cost, int stock, Category category) : this(0, ean, name, desc, cost, stock, category) { }


    }
}
