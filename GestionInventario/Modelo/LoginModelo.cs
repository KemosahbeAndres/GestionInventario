using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Modelo
{
    public class LoginModelo
    {
        
          private readonly string _cadenaConexion;

          public LoginModelo(string cadenaConexion)
          {
              _cadenaConexion = cadenaConexion;
          }

          public bool ValidarCredenciales(string rut, string contraseña)
          {
              using (SqlConnection conexion = new SqlConnection(_cadenaConexion))
              {
                  SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Rut=@Rut AND clave=@clave", conexion);
                  comando.Parameters.AddWithValue("@Rut", rut);
                  comando.Parameters.AddWithValue("@clave", contraseña);
                  conexion.Open();
                  int count = (int)comando.ExecuteScalar();
                  return count > 0;
              }
          }
        

    }
}
