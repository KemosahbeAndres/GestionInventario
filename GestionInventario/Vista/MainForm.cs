using GestionInventario.Controlador;
using GestionInventario.Controlador.Users;
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
        private bool logged;
        private User user;
        private usersForm users;
        private LoginForm loginForm;
        private ProductsForm productsForm;
        private SalesForm salesForm;
        private ListUsersController userLister;
        private FindUserController userFinder;

        public MainForm()
        {
            userLister = new ListUsersController();
            userFinder = new FindUserController();
            InitializeComponent();
            users = new usersForm(userLister);
            loginForm = new LoginForm();
            logged = false;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            login();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            users = new usersForm(userLister);
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

        private void MainForm_Load(object sender, EventArgs e)
        {
            login();
        }
        private void login()
        {
            logged = false;
            user = null;
            try
            {
                if (userLister.execute().Count <= 0) initApplication();
                if (!logged)
                {
                    if (loginForm.ShowDialog(this) == DialogResult.OK && loginForm.GetUser != null)
                    {
                        logged = true;
                        user = loginForm.GetUser;
                        if (user != null)
                        {
                            if (user.IsSeller())
                            {
                                btnUsers.Enabled = false;
                                btnBuys.Enabled = false;
                            }
                            else
                            {
                                btnUsers.Enabled = true;
                                btnBuys.Enabled = true;
                            }
                        }
                    }

                }
            }
            catch (Exception error)
            {
                message("Error de conexion con base de datos!! Cerrando por precaucion! " + error.Message);
                Application.Exit();
            }
        }

        private void initApplication()
        {
            if(new ListRolesController().execute().Count <= 0)
            {
                Role r = new Role("Administrador");
                r.Save();
                r = new Role("Vendedor");
                r.Save();
                r = new Role("Usuario");
                r.Save();
            }    
                        
            User admin = new User("Andres", "19149514-4", "admin", "123456789", Role.Find("Administrador"));
            admin.Save();
            User u = new User("Gerald", "21394949-7", "admin", "123456789", Role.Find("Vendedor"));
            u.Save();
            u = new User("Manuel", "20919788-K", "admin", "123456789", Role.Find("Vendedor"));
            u.Save();
            
        }

        protected void message(string mensaje)
        {
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            productsForm = new ProductsForm();
            productsForm.ShowDialog(this);
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            salesForm = new SalesForm();
            salesForm.ShowDialog(this);
        }
    }
}
