using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Controlador.Users
{
    class ModifyUserController
    {
        private FindRoleController roleFinder;
        private UserDao userDao;

        public ModifyUserController()
        {
            userDao = new UserDao();
            roleFinder = new FindRoleController();
        }

        public void execute(int id, string name, string rut, string password, string phone, string role)
        {
            if (id <= 0) throw new Exception("El id especificado no existe!");
            if (String.IsNullOrEmpty(name.Trim()) || hasNumber(name)) throw new Exception("Nombre invalido, los nombres no pueden llevar numeros en su contenido!");
            string nombre = name.Trim();
            if (String.IsNullOrEmpty(rut.Trim()) || !RunValidator.Validar(rut.Trim())) throw new Exception("Rut invalido, debes ingresar un numero de rut valido!");
            string username = rut.Trim();
            if (String.IsNullOrEmpty(password.Trim())) throw new Exception("Contraseña invalida, debes ingresar una contraseña valida!");
            string clave = password.Trim();
            string telefono = "";
            if (!String.IsNullOrEmpty(phone.Trim())) telefono = phone.Trim();
            if (String.IsNullOrEmpty(role.Trim()) && roleFinder.execute(role.Trim()) == null) throw new Exception("Rol invalido, debes seleccionar un rol valido de la lista!");
            var rol = roleFinder.execute(role.Trim());

            Usuarios user = new Usuarios();

            user.id = id;
            user.nombre = nombre;
            user.rut = username;
            user.clave = clave;
            user.telefono = telefono;
            user.id_rol = rol.Id;

            try
            {
                userDao.Modify(user);
            }
            catch (Exception e)
            {
                throw new Exception("Error al guardar usuario!\n" + e.Message);
            }
        }

        public bool hasNumber(string value)
        {
            foreach (char c in value)
            {
                if (Char.IsDigit(c) || Char.IsNumber(c) || Char.IsPunctuation(c) || Char.IsSymbol(c))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
