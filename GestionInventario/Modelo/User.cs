using GestionInventario.DB;
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
        private int? Parent { get; set; }

        private static UserDao udao = new UserDao();

        public User(int id, string nombre, string correo, string clave, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
            Clave = clave;
            Telefono = telefono;
            Parent = null;
        }
        public User(int id, string nombre, string correo, string clave, string telefono, Rol rol, User creador) : this(id, nombre, correo, clave, telefono)
        {
            Tipo = rol;
            Parent = creador.Id;
        }
        public User(string nombre, string correo, string clave, string telefono) : this(0, nombre, correo, clave, telefono) { }

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

        public static bool exists(int id)
        {
            return udao.Get(id) != null;
        }

        private Usuarios ToEntity()
        {
            Usuarios entity = new Usuarios();
            entity.id = Id;
            entity.nombre = Nombre;
            entity.telefono = Telefono;
            entity.correo = Correo;
            entity.clave = Clave;
            entity.id_creador = Parent;
            entity.id_rol = Tipo.Id;
            return entity;
        }

        public void save()
        {
            Usuarios entity = ToEntity();
            if (this.Id > 0)
            {
                udao.Modify(entity);
            } else
            {
                udao.Insert(entity);
            }
        }

        public bool Delete()
        {
            if(Id == 0)
            {
                return false;
            }
            udao.Delete(ToEntity());
            return true;
        }

        public void SetTipo(Rol rol)
        {
            Tipo = rol;
        }

        public override string ToString()
        {
            return $"Usuario: {Nombre}";
        }

    }
}
