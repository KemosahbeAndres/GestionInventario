using GestionInventario.Controlador;
using GestionInventario.Controlador.Products.Categories;
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
        private CreateProductController productCreator;
        private DeleteProductController productDeletor;

        private List<ProductListViewItem> productList;

        private Product selectedProduct;
        private EditProductForm editProductForm;
        private ModifyCategoryForm modifyCategoryForm;
        private String categorySelected;

        public ProductsForm()
        {
            InitializeComponent();
            categoryFinder = new FindCategoryController();
            categoryCreator = new CreateCategoryController();
            categoryDeletor = new DeleteCategoryController();
            productFinder = new ListProductController();
            productCreator = new CreateProductController();
            productDeletor = new DeleteProductController();
        }

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            refreshCategoryList();
            refreshProductList();
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
            if (!string.IsNullOrEmpty(categorySelected))
            {
                try
                {
                    categoryDeletor.execute(categorySelected);
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
            refreshCategoryList();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            editProductForm = new EditProductForm();
            if(editProductForm.ShowCreationDialog(this) == DialogResult.OK)
            {
                refreshProductList();
            }
        }

        private void listProductsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listProductsView.SelectedItems.Count > 0)
            {
                selectedProduct = (listProductsView.SelectedItems[0] as ProductListViewItem).product;
            }
            
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (listProductsView.SelectedItems.Count > 0 && selectedProduct != null)
            {
                try
                {
                    productDeletor.execute(selectedProduct);
                    refreshProductList();
                }catch(Exception ex)
                {
                    showMessage("Error!\n" + ex.Message);
                }
            }
        }

        private void btnModifyCategory_Click(object sender, EventArgs e)
        {

            if(categoryList.SelectedItems.Count > 0)
            {
                modifyCategoryForm = new ModifyCategoryForm();
                modifyCategoryForm.ShowCategoryDialog(categorySelected, this);
            }
            refreshCategoryList();
            refreshProductList();
        }

        private void categoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(categoryList.SelectedItems.Count > 0)
            {
                this.categorySelected = categoryList.SelectedItem.ToString().Trim();
            }
            else
            {
                this.categorySelected = "";
            }

        }

        private void btnEditProduct_Click(object sender, EventArgs e)
        {
            editProductForm = new EditProductForm();
            if(selectedProduct != null)
            {
                if (editProductForm.ShowEditionDialog(this, selectedProduct) == DialogResult.OK)
                {
                    refreshProductList();
                }
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                string keyword = txtSearch.Text.Trim();
                List<ProductListViewItem> newproductList = new List<ProductListViewItem>();
                foreach(ProductListViewItem item in productList)
                {
                    var p = item.product;
                    if(p.Nombre.Contains(keyword) || p.Descripcion.Contains(keyword) || p.Ean.Contains(keyword))
                    {
                        newproductList.Add(item);
                    }
                }
                listProductsView.Items.AddRange(newproductList.ToArray());
            }
        }
    }
}
