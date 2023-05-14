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
    public class LoginControlador
    {
        private readonly ILoginVista _vista;
        private readonly string _cadenaConexion;

        public LoginControlador(ILoginVista vista, string cadenaConexion)
        {
            _vista = vista;
            _cadenaConexion = cadenaConexion;
        }

        public void IniciarSesion()
        {
            string rut = _vista.Rut;
            string contraseña = _vista.Contraseña;

            if (string.IsNullOrEmpty(rut) || string.IsNullOrEmpty(contraseña))
            {
                _vista.MostrarMensaje("Debe ingresar el RUT y la contraseña");
                return;
            }

            if (!ValidarRut(rut))
            {
                _vista.MostrarMensaje("El RUT ingresado no es válido");
                return;
            }

            User usuario = ObtenerUsuario(rut, contraseña);

            if (usuario == null)
            {
                _vista.MostrarMensaje("El RUT o la contraseña son incorrectos");
                return;
            }

            _vista.MostrarMensaje("Inicio de sesión exitoso");
            _vista.LimpiarCampos();
        }

        private bool ValidarRut(string rut)
        {
            // Implementar la validación del RUT aquí
            return true;
        }

        private User ObtenerUsuario(string rut, string contraseña)
        {
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
        }
    }

}
