using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Modelo
{
    class Item
    {
        public int Id { get; }
        public int Cantidad { get; }
        public Product Producto { get; }

        public Item(int id, Product product, int count = 1)
        {
            this.Id = id < 0 ? 0 : id;
            this.Producto = product;
            this.Cantidad = count < 1 ? 1 : count;
        }

        public Item(Product product, int count = 1) : this(0, product, count) { }
        
        public Item(Product product) : this(0, product) { }

        public int Subtotal
        {
            get
            {
                return Producto.Precio * Cantidad;
            }
        }
    }
}
