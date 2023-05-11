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
    public partial class MainForm : Form
    {
        private usersForm users;
        private ListUsersController listUsersController;

        public MainForm()
        {
            listUsersController = new ListUsersController();
            InitializeComponent();
            users = new usersForm(listUsersController);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            users.ShowDialog(this);
            if(users.DialogResult == DialogResult.OK)
            {
                if(users.userSelected == null)
                {
                    MessageBox.Show("Usuario NULO");
                }else
                {
                    MessageBox.Show(users.userSelected.Nombre + " seleccionado");
                }
            }
        }
    }
}
