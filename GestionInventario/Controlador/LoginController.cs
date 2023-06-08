using GestionInventario.Controlador.Users;
using GestionInventario.Modelo;
using GestionInventario.Vista;

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

            if (!RunValidator.Validar(rut))
            {
                _vista.MostrarMensaje("El RUT ingresado no es válido");
                return false;
            }

            User user = userFinder.execute(rut);

            if ( user == null || !ValidarContraseña(user, contraseña) )
            {
                _vista.MostrarMensaje("El RUT o la contraseña son incorrectos");
                return false;
            }
            
            loggedUser = user;
            _vista.MostrarMensaje("Inicio de sesión exitoso");
            _vista.LimpiarCampos();
            return true;
        }

        

        private bool ValidarContraseña(User user, string password)
        {
            return user.Clave.Equals(password.Trim());
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
