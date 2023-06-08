using GestionInventario.Controlador;
using GestionInventario.Controlador.Users;
using GestionInventario.Modelo;
using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GestionInventario.Vista
{
    public partial class usersForm : Form
    {
        public User userSelected;
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
            foreach (User user in controller.execute())
            {
                userList.Add(new UserListViewItem(user));
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
                userSelected = ((UserListViewItem)usersListView.SelectedItems[0]).usr;
                int index = Convert.ToInt32(usersListView.SelectedIndices[0]) + 1;
                lblSelected.Text = $"Indice seleccionado: {index} ID {userSelected.Id}";
                //var item = ((UserListViewItem) usersListView.SelectedItems[0]).usr;

                if (!User.Exists(userSelected.Id))
                {
                    lblConection.Text = "No existe el usuario";
                }
                else
                {
                    //this.userSelected = User.Find(index);
                    userSelected = ((UserListViewItem)usersListView.SelectedItems[0]).usr;
                    lblConection.Text = "Usuario encontrado!!";
                    lblData.Text += $"ID: {userSelected.Id}\nNombre: {userSelected.Nombre}\nCorreo: {userSelected.Rut}\nTelefono: {userSelected.Telefono}";
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
            editForm.ShowCreateDialog(this);
            if(editForm.DialogResult == DialogResult.OK)
            {
                fillListView();
            }
        }
        private void btnUserDelete_Click(object sender, EventArgs e)
        {
            if (usersListView.SelectedItems.Count > 0)
            {
                //int index = Convert.ToInt32(usersListView.SelectedIndices[0]) + 1;
                //var userSelected = User.Find(index);
                if (userSelected != null)
                {
                    DialogResult user_resultado = MessageBox.Show
                        ("¿Esta seguro que desea eliminar al siguiente usuario? \nNombre: " + userSelected.Nombre + "\nRUT: " + userSelected.Rut, "Eliminar Usuario", MessageBoxButtons.YesNo);
                    if (user_resultado == DialogResult.Yes)
                    {
                        UserDao userdao = new UserDao();
                        userdao.Delete(userSelected.Id);
                        fillListView();
                        MessageBox.Show("Se ha eliminado el usuario seleccionado.");
                    }
                    else if (user_resultado == DialogResult.No)
                    {
                        MessageBox.Show("Se ha cancelado la operacion.");
                    }
                }
            }
        }

        private void btnModifyUser_Click(object sender, EventArgs e)
        {
            editForm = new EditUserForm();
            if(userSelected != null) editForm.ShowEditDialog(this, userSelected);
            if (editForm.DialogResult == DialogResult.OK)
            {
                fillListView();
            }
        }
    }
}
