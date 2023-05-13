using GestionInventario.Modelo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestionInventario.Vista
{
    public partial class EditUserForm : Form
    {
        private User user;
        private bool ready;
        public bool Ready
        {
            get
            {
                return ready;
            }
        }

        public User Model
        {
            get
            {
                return Ready ? user : null;
            }
        }

        public EditUserForm()
        {
            InitializeComponent();
            ready = false;
            foreach (Role role in Role.All())
            {
                cbRole.Items.Add(role.Nombre);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ready = false;
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (
                name.Length > 0 &&
                email.Length > 0 &&
                phone.Length > 0 &&
                password.Length > 0 &&
                cbRole.SelectedIndex != -1
                )
            {
                string role = cbRole.SelectedIndex.ToString();
                role = cbRole.SelectedItem.ToString();
                MessageBox.Show("ROLE: "+ role);
                this.user = new User(name, email, password, phone, Role.Find(role));
                this.user.Save();
                ready = true;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Datos vacios!!");
            }
        }
    }
}
