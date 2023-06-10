using GestionInventario.Controlador;
using GestionInventario.Controlador.Products.Categories;
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
    public partial class ModifyCategoryForm : Form
    {
        private FindCategoryController categoryFinder;
        private ModifyCategoryController categoryEditor;

        public ModifyCategoryForm()
        {
            InitializeComponent();
            categoryFinder = new FindCategoryController();
            categoryEditor = new ModifyCategoryController();
        }

        public DialogResult ShowCategoryDialog(string name, IWin32Window owner)
        {
            limpiarCampos();
            var cat = categoryFinder.execute(name.Trim());
            if(cat == null)
            {
                ShowMessage("No existe la categoria seleccionada!");
                return DialogResult.Cancel;
            }
            txtId.Text = cat.Id.ToString();
            txtName.Text = cat.Nombre;
            return ShowDialog(owner);
        }

        public void ShowMessage(string mensaje)
        {
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void limpiarCampos()
        {
            txtId.Text = string.Empty;
            txtName.Text = string.Empty;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(txtId.Text.Trim());
                string name = txtName.Text.Trim();
                var category = categoryFinder.execute(id);
                categoryEditor.execute(category, name);
            }
            catch(Exception ex)
            {
                limpiarCampos();
                ShowMessage("No se pudo guardar la categoria!\n" +ex.Message);
                DialogResult = DialogResult.Cancel;
                Close();
            }
            limpiarCampos();
            DialogResult |= DialogResult.OK;
            Close();
        }
    }
}
