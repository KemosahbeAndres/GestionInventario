using GestionInventario.Controlador;
using GestionInventario.Modelo;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GestionInventario.Vista
{
    public partial class LoginForm : Form, ILoginVista
    {
        private readonly LoginController _controlador;
        private User loggedUser;
        public User GetUser
        {
            get
            {
                return loggedUser;
            }
        }

        public LoginForm()
        {
            InitializeComponent();
            _controlador = new LoginController(this);
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

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string username = txtRut.Text.Trim();
            string password = txtContraseña.Text.Trim();
            if(_controlador.IniciarSesion(username, password))
            {
                this.loggedUser = _controlador.GetUser;
                this.DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(loggedUser == null) Application.Exit();
        }

        private void ckClave_CheckedChanged(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = !txtContraseña.UseSystemPasswordChar;
        }

        private void txtRut_TextChanged(object sender, EventArgs e)
        {
            //string text = txtRut.Text.Replace(".", "").Replace("-", "");
            //txtRut.Text = Regex.Replace(text, @"(\w{0,3})(\w{0,3})(\w{1,3})(\w{1})", @"$1$2$3-$4");
            txtRut.Text = RunValidator.format(txtRut.Text);
            txtRut.SelectionStart = txtRut.Text.Length;
            txtRut.SelectionLength = 0;
        }
    }

    public interface ILoginVista
    {
        string Rut { get; }
        string Contraseña { get; }

        void LimpiarCampos();
        void MostrarMensaje(string v);
    }
}
