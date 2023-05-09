using GestionInventario.Modelo;
using GestionInventario.Vista.Controles;
using System;
using System.Windows.Forms;

namespace GestionInventario.Vista
{
    public partial class usersForm : Form
    {
        public usersForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = User.find(1);
            if (u == null)
            {
                MessageBox.Show("No existe el usuario!");
            }
            else
            {
                MessageBox.Show(u.ToString());
            }

        }

        private void usersForm_Load(object sender, EventArgs e)
        {
            
        }
    }
}
