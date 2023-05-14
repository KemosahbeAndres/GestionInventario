using GestionInventario.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionInventario.Controlador
{
    class CreateUserController
    {
        private bool nameError;
        private bool emailError;
        private bool passwordError;
        private bool phoneError;
        private bool roleError;

        public CreateUserController()
        {
            nameError = false;
            emailError = false;
            passwordError = false;
            phoneError = false;
            roleError = false;
        }

        public void execute(string name, string email, string password, string phone, string role)
        {
            if (String.IsNullOrEmpty(name.Trim()) && hasNumber(name)) MessageBox.Show(null, "No puedes ingresar un numero en el nombre!", "Error de datos");
            if (String.IsNullOrEmpty(email.Trim())) nameError = true;
            if (String.IsNullOrEmpty(password.Trim())) nameError = true;
            if (String.IsNullOrEmpty(phone.Trim())) nameError = true;
            if (String.IsNullOrEmpty(role.Trim()) && Role.Find(role.Trim()) == null) nameError = true;
        }

        public bool hasNumber(string value)
        {
            foreach(char c in value)
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
