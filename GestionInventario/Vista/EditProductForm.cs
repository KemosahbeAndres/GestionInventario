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
    public partial class EditProductForm : Form
    {
        private bool edition;
        private Product product;
        private Product Producto
        {
            get
            {
                return product;
            }
        }
        private CreateProductController productCreator;
        private FindCategoryController categoryFinder;

        public EditProductForm()
        {
            InitializeComponent();
            productCreator = new CreateProductController();
            categoryFinder = new FindCategoryController();
            foreach(var c in categoryFinder.execute())
            {
                cbCategory.Items.Add(c.Nombre);
            }
        }
        public DialogResult ShowEditionDialog(IWin32Window owner, Product product)
        {
            edition = true;
            this.product = product;
            fillData(product);
            return ShowDialog(owner);
        }

        public DialogResult ShowCreationDialog(IWin32Window owner)
        {
            edition = false;
            emptyData();
            return ShowDialog(owner);
        }

        private void fillData(Product product)
        {
            lblCodeValue.Text = product.Ean;
            txtName.Text = product.Nombre;
            txtCost.Text = product.Precio.ToString();
            txtStock.Text = product.Existencias.ToString();
            txtDescription.Text = product.Descripcion;
            int index = cbCategory.FindStringExact(product.Categoria.Nombre);
            cbCategory.SelectedIndex = index;
        }
        private void emptyData()
        {
            lblCodeValue.Text = productCreator.getPreviewBarCode();
            txtName.Text = "";
            txtCost.Text = "";
            txtStock.Text = "";
            txtDescription.Text = "";
            cbCategory.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (edition)
            {
                // editar producto
                try
                {
                    string nombre = txtName.Text.Trim();
                    string descripcion = txtDescription.Text.Trim();
                    int precio = Convert.ToInt32(txtCost.Text);
                    int stock = Convert.ToInt32(txtStock.Text);
                    string categoria = cbCategory.SelectedItem.ToString().Trim();
                    productCreator.execute(nombre, descripcion, precio, stock, categoria, product.Id);
                    this.DialogResult = DialogResult.OK;
                    Close();
                }catch(Exception ex)
                {
                    showMessage("Datos incorrectos! Imposible modificar!\n" + ex.Message);
                }
            }
            else
            {
                // crear producto
                try
                {
                    string nombre = txtName.Text.Trim();
                    string descripcion = txtDescription.Text.Trim();
                    int precio = Convert.ToInt32(txtCost.Text);
                    int stock = Convert.ToInt32(txtStock.Text);
                    string categoria = cbCategory.SelectedItem.ToString();
                    productCreator.execute(nombre, descripcion, precio, stock, categoria);
                    this.DialogResult = DialogResult.OK;
                    Close();
                }catch(Exception ex)
                {
                    showMessage("Datos incorrectos! Imposible crear\n"+ex.Message);
                }
            }
        }

        public void showMessage(string mensaje)
        {
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtCost_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txtCost.Text, "[^0-9]"))
            {
                showMessage("Por favor solo ingresa numeros!");
                txtCost.Text = txtCost.Text.Remove(txtCost.Text.Length - 1);
            }
        }
    }
}
