using GestionInventario.Controlador;
using GestionInventario.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionInventario.Vista
{
    public partial class ProductsForm : Form
    {
        private FindCategoryController categoryFinder;
        private CreateCategoryController categoryCreator;
        private DeleteCategoryController categoryDeletor;
        private ListProductController productFinder;
        private List<ProductListViewItem> productList;
        public ProductsForm()
        {
            InitializeComponent();
            categoryFinder = new FindCategoryController();
            categoryCreator = new CreateCategoryController();
            categoryDeletor = new DeleteCategoryController();
            productFinder = new ListProductController();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            refreshCategoryList();
        }

        private void refreshCategoryList()
        {
            categoryList.Items.Clear();
            foreach(var c in categoryFinder.execute())
            {
                categoryList.Items.Add(c.Nombre);
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if(txtNewCategory.Text.Length > 0)
            {
                try
                {
                    categoryCreator.execute(txtNewCategory.Text.Trim());
                }
                catch (Exception ex)
                {
                    showMessage(ex.Message);
                }
                txtNewCategory.Text = "";
                refreshCategoryList();
            }
        }
        public void showMessage(string mensaje)
        {
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string text = categoryList.SelectedItem.ToString().Trim();
            if (!string.IsNullOrEmpty(text))
            {
                try
                {
                    categoryDeletor.execute(text);
                }catch(Exception ex)
                {
                    showMessage(ex.Message);
                }
                txtNewCategory.Text = "";
                categoryList.SelectedIndex = -1;
                refreshCategoryList();
            }
        }

        protected void refreshProductList()
        {
            productList = new List<ProductListViewItem>();
            foreach(Product p in productFinder.execute())
            {
                productList.Add(new ProductListViewItem(p));
            }
            listProductsView.Items.Clear();
            listProductsView.Items.AddRange(productList.ToArray());
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshProductList();
        }
    }
}
