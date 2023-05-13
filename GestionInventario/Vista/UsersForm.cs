using GestionInventario.Controlador;
using GestionInventario.Modelo;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestionInventario.Vista
{
    public partial class usersForm : Form
    {
        public User userSelected { get; private set; }
        private List<UserListViewItem> userList;
        private ListUsersController controller;
        private EditUserForm editForm;

        private usersForm()
        {
            InitializeComponent();
        }

        public usersForm(ListUsersController controller) : this()
        {
            this.controller = controller;
        }

        private void fillListView()
        {
            userList = new List<UserListViewItem>();
            foreach (User u in controller.Execute())
            {
                userList.Add(new UserListViewItem(u));
            }
            usersListView.Items.Clear();
            usersListView.Items.AddRange(userList.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fillListView();
        }

        private void usersForm_Load(object sender, EventArgs e)
        {
            fillListView();
        }

        private void usersListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblConection.Text = "";
            lblSelected.Text = "No hay seleccion";
            lblData.Text = "Datos:\n";
            if (usersListView.SelectedItems.Count > 0)
            {
                int index = Convert.ToInt32(usersListView.SelectedIndices[0]) + 1;
                lblSelected.Text = $"Indice seleccionado: {index}";
                //var item = ((UserListViewItem) usersListView.SelectedItems[0]).usr;

                if (!User.Exists(index))
                {
                    lblConection.Text = "No existe el usuario";
                }
                else
                {
                    userSelected = User.Find(index);
                    lblConection.Text = "Usuario encontrado!!";
                    lblData.Text += $"ID: {userSelected.Id}\nNombre: {userSelected.Nombre}\nCorreo: {userSelected.Correo}\nTelefono: {userSelected.Telefono}";
                }

            }

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            /**
            Role r = new Role("Administrador");
            r.Save();
            r = new Role("Vendedor");
            r.Save();
            
            
            User admin = new User(0, "Andres", "a.cubillos@alumnos.santotomas.cl", "*****", "123456789", Role.Find("Administrador"), null);
            admin.Save();
            User u = new User(0, "Gerald", "g.perez@alumnos.santotomas.cl", "*****", "123456789", Role.Find("Vendedor"), admin);
            u.Save();
            u = new User(0, "Manuel", "m.rojas@alumnos.santotomas.cl", "*****", "123456789", Role.Find("Vendedor"), admin);
            u.Save();
            **/

        }


        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            editForm = new EditUserForm();
            editForm.ShowDialog(this);
            if(editForm.DialogResult == DialogResult.OK)
            {
                fillListView();
            }
        }
        private void btnUserDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
