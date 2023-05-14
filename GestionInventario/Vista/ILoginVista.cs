using GestionInventario.Controlador;
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
    public partial class LoginForm : Form, ILoginVista
    {
        private readonly LoginControlador _controlador;

        public LoginForm(string cadenaConexion)
        {
            InitializeComponent();
            _controlador = new LoginControlador(this, cadenaConexion);
            InicializarEventos();
        }

        private void InicializarEventos()
        {
            btnIniciarSesion.Click += (sender, e) => _controlador.IniciarSesion();
        }

        public string Rut
        {
            get { return txtRut.Text.Trim(); }
        }

        public string Contraseña
        {
            get { return txtContraseña.Text.Trim(); }
        }

        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LimpiarCampos()
        {
            txtRut.Text = "";
            txtContraseña.Text = "";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }

    internal interface ILoginVista
    {
        string Rut { get; }
        string Contraseña { get; }

        void LimpiarCampos();
        void MostrarMensaje(string v);
    }
}
