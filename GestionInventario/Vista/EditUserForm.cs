using GestionInventario.Controlador;
using GestionInventario.Controlador.Users;
using GestionInventario.Modelo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestionInventario.Vista
{
    public partial class EditUserForm : Form
    {
        private User userSelected;
        private CreateUserController userCreator;
        private ModifyUserController userModifier;
        private ListRolesController roleLister;
        private bool toEdition;
        private bool ready;
        public bool Ready
        {
            get
            {
                return ready;
            }
        }

        public EditUserForm()
        {
            toEdition = false;
            InitializeComponent();
            ready = false;
            roleLister = new ListRolesController();
            foreach (Role role in roleLister.execute())
            {
                cbRole.Items.Add(role.Nombre);
            }
            userCreator = new CreateUserController();
            userModifier = new ModifyUserController();
        }

        public DialogResult ShowCreateDialog(IWin32Window owner)
        {
            toEdition = false;
            return this.ShowDialog(owner);
        }

        public DialogResult ShowEditDialog(IWin32Window owner, User user)
        {
            toEdition = true;
            userSelected = user;
            txtName.Text = user.Nombre;
            txtRut.Text = user.Rut;
            txtPhone.Text = user.Telefono;
            txtPassword.Text = user.Clave;
            int index = cbRole.FindStringExact(user.Rol);
            cbRole.SelectedIndex = index;
            return this.ShowDialog(owner);
        }

        public void showMessage(string mensaje)
        {
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ready = false;
            string name = txtName.Text.Trim();
            string rut = txtRut.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string password = txtPassword.Text.Trim();

            string role = cbRole.SelectedItem.ToString();
            try
            {
                if (toEdition)
                {
                    userModifier.execute(userSelected.Id, name, rut, password, phone, role);
                }
                else
                {
                    userCreator.execute(name, rut, password, phone, role);
                }
                ready = true;
                DialogResult = DialogResult.OK;
            }catch(Exception error)
            {
                showMessage(error.Message);
            }
        }
    }
}
