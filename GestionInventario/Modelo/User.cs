﻿using GestionInventario.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Modelo
{
    class User
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public string Telefono { get; private set; }
        public string Correo { get; private set; }
#pragma warning disable IDE0052 // Quitar miembros privados no leídos
        private string Clave { get; set; }
#pragma warning restore IDE0052 // Quitar miembros privados no leídos
        public Rol Tipo { get; private set; }

        private static UserDao udao = new UserDao();

        public User(int id, string nombre, string correo, string clave, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
            Clave = clave;
            Telefono = telefono;
        }

        public static User find(int id)
        {
            var e = udao.Get(id);
            if(e == null)
            {
                return null;
            }
            var user = new User(e.id, e.nombre, e.correo, e.clave, e.telefono);
            return user;
        }

        public void setTipo(Rol rol)
        {
            Tipo = rol;
        }

        public override string ToString()
        {
            return $"Usuario: {Nombre}";
        }

    }
}
