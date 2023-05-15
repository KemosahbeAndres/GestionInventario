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
        public string Rut { get; }
        public string Clave { get; }
        private Role Tipo;

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

        public User(int id, string nombre, string rut, string clave, string telefono)
        {
            Id = id;
            Nombre = nombre;
            Rut = rut;
            Clave = clave;
            Telefono = telefono;
        }
        
        public User(string nombre, string rut, string clave, string telefono) : this(0, nombre, rut, clave, telefono) { }

        public User(int id, string nombre, string rut, string clave, string telefono, Role rol) : this(id, nombre, rut, clave, telefono)
        {
            Tipo = rol;
        }

        public User(string nombre, string rut, string clave, string telefono, Role rol) : this(0, nombre, rut, clave, telefono, rol) { }

        protected static Usuarios ToEntity(User u)
        {
            Usuarios entity;
            entity = new Usuarios
            {
                nombre = u.Nombre,
                telefono = u.Telefono,
                rut = u.Rut,
                clave = u.Clave
            };
            if(u.Rol.Equals(""))
            {
                entity.id_rol = Role.Find(RoleType.USUARIO).Id;
            }
            else
            {
                entity.id_rol = u.Tipo.Id;
            }
            
            if (u.Id > 0) entity.id = u.Id;
            return entity;
        }

        protected static User FromEntity(Usuarios e)
        {
            User user = new User(e.id, e.nombre, e.rut, e.clave, e.telefono, Role.Find(e.id_rol));
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
            dao.Delete(Id);
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
