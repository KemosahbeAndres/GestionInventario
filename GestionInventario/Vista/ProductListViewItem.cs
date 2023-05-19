using GestionInventario.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionInventario.Vista
{
    class ProductListViewItem: ListViewItem
    {
        public Product product;
        public ProductListViewItem(Product producto) : base()
        {
            this.product = producto;
            updateView();
        }

        protected void updateView()
        {
            this.SubItems.Clear();
            string[] range =
            {
                $"{product.Id}",
                product.Ean,
                product.Nombre,
                product.Categoria.Nombre,
                $"${product.Precio}",
                $"{product.Existencias}",
                product.Descripcion
            };
            this.SubItems.AddRange(range);
        }
    }
}
