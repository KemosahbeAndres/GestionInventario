using GestionInventario.Modelo;
using GestionInventario.Vista;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador
{
    public class LoginController
    {
        private readonly ILoginVista _vista;
        private FindUserController userFinder;
        private User loggedUser;

        public User GetUser
        {
            get
            {
                return loggedUser;
            }
        }

        public LoginController(ILoginVista vista)
        {
            _vista = vista;
            userFinder = new FindUserController();
        }

        public bool IniciarSesion(string username, string password)
        {
            string rut = username.Trim();
            string contraseña = password.Trim();

            if (string.IsNullOrEmpty(rut) || string.IsNullOrEmpty(contraseña))
            {
                _vista.MostrarMensaje("Debe ingresar el RUT y la contraseña");
                return false;
            }

            if (!ValidarRut(rut))
            {
                _vista.MostrarMensaje("El RUT ingresado no es válido");
                return false;
            }

            loggedUser = userFinder.execute(rut);

            if ( loggedUser == null || !ValidarContraseña(contraseña) )
            {
                _vista.MostrarMensaje("El RUT o la contraseña son incorrectos");
                return false;
            }

            _vista.MostrarMensaje("Inicio de sesión exitoso");
            _vista.LimpiarCampos();
            return true;
        }

        private bool ValidarRut(string rut)
        {
            // Implementar la validación del RUT aquí
            return true;
        }

        private bool ValidarContraseña(string password)
        {
            return this.loggedUser.Clave.Equals(password.Trim());
        }


        /**
        private User ObtenerUsuario(string rut, string contraseña)
        {
            return userFinder.execute(rut);
            using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
            {
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.CommandText = "SELECT * FROM Usuarios WHERE Rut = @rut AND clave = @clave";
                comando.Parameters.AddWithValue("@rut", rut);
                comando.Parameters.AddWithValue("@clave", contraseña);
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    User usuario = new User();
                    usuario.Rut = reader.GetString(0);
                    usuario.Contraseña = reader.GetString(1);
                    return usuario;
                }
                else
                {
                    return null;
                }
            }
        
        }*/
    }

}
