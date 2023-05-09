using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionInventario.Vista.Controles
{
    public partial class UserControlView : UserControl
    {
        public UserControlView()
        {
            InitializeComponent();
        }

        public UserControlView(string name) : this()
        {
            this.lblName.Text = name;
        }
    }
}
