using GestionInventario.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionInventario.Vista
{
    class UserListViewItem: ListViewItem
    {
        public User usr;

        public UserListViewItem(User user) : base()
        {
            this.usr = user;
            updateView();
        }

        protected void updateView()
        {
            this.SubItems.Clear();
            string[] range = {
                $"{usr.Id}",
                usr.Nombre,
                usr.Rut,
                usr.Telefono,
                usr.Rol
            };
            this.SubItems.AddRange(range);
        }
    }
}
