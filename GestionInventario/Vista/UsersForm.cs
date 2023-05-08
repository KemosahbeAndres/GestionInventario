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
    public partial class usersForm : Form
    {
        public usersForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var u = User.find(1);
            if(u == null)
            {
                MessageBox.Show("No existe el usuario!");
            }
            else
            {
                MessageBox.Show(u.ToString());
            }
            
        }
    }
}
