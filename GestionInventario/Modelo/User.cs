using GestionInventario.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Modelo
{
    public class User
    {
        public int Id { get; }
        public string Nombre { get; }
        public string Telefono { get; }
        public string Correo { get; }
        private string Clave;
        private Role Tipo;
        private User Parent { get; set; }

        public string Rol
        {
            get
            {
                if(Tipo != null)
                {
                    return Tipo.Nombre.Trim();
                }
                return "";
            }
        }

        private static readonly UserDao dao = new UserDao();

        public User(int id, string nombre, string correo, string clave, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Correo = correo;
            Clave = clave;
            Telefono = telefono;
        }
        public User(int id, string nombre, string correo, string clave, string telefono, Role rol, User creador) : this(id, nombre, correo, clave, telefono)
        {
            Tipo = rol;
            Parent = creador;
        }
        public User(string nombre, string correo, string clave, string telefono) : this(0, nombre, correo, clave, telefono) { }

        protected static Usuarios ToEntity(User u)
        {
            Usuarios entity;
            if (u.Parent == null)
            {
                entity = new Usuarios
                {
                    nombre = u.Nombre,
                    telefono = u.Telefono,
                    correo = u.Correo,
                    clave = u.Clave,
                    id_creador = null,
                    id_rol = u.Tipo.Id
                };
            }else
            {
                entity = new Usuarios
                {
                    nombre = u.Nombre,
                    telefono = u.Telefono,
                    correo = u.Correo,
                    clave = u.Clave,
                    id_creador = u.Parent.Id,
                    id_rol = u.Tipo.Id
                };
            }
            if (u.Id > 0) entity.id = u.Id;
            return entity;
        }

        protected static User FromEntity(Usuarios e)
        {
            User user = new User(e.id, e.nombre, e.correo, e.clave, e.telefono, Role.Find(e.id_rol), Find(e.id_creador ?? 0));
            return user;
        }

        protected static List<User> ToList(List<Usuarios> list)
        {
            List<User> result = new List<User>();
            foreach (Usuarios user in list)
            {
                result.Add(FromEntity(user));
            }
            return result;
        }

        public static User Find(int id)
        {
            if (!Exists(id)) return null;
            Usuarios e = dao.Get(id);
            User user = FromEntity(e);
            return user;
        }

        public static List<User> All()
        {
            return ToList(dao.All());
        }

        public static List<User> Take(int from = 0, int count = 20)
        {
            return ToList(dao.Take(from, count));
        }

        public static bool Exists(int id)
        {
            return dao.Exists(id);
        }

        public void Save()
        {
            Usuarios entity = ToEntity(this);
            if (Exists(Id))
            {
                dao.Modify(entity);
            } else
            {
                dao.Insert(entity);
            }
        }

        public bool Delete()
        {
            if (!Exists(Id)) return false;
            dao.Delete(ToEntity(this));
            return true;
        }

        public bool IsAdmin()
        {
            return Rol == "Administrador";
        }

        public bool IsSeller()
        {
            return Rol == "Vendedor";
        }

        public override string ToString()
        {
            return $"Usuario: {Nombre}";
        }

    }
}
