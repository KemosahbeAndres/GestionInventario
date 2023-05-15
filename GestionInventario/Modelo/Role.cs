using GestionInventario.Persistence;
using System.Collections.Generic;

namespace GestionInventario.Modelo
{
    public class Role
    {
        private static readonly RoleDao dao = new RoleDao();
        
        public int Id { get; }
        private RoleType role;
        public string Nombre
        {
            get;
        }

        public Role(int id, string rol)
        {
            Id = id;
            Nombre = rol.Trim();
            if(rol == "Vendedor")
            {
                this.role = RoleType.VENDEDOR;
            }else if (rol == "Administrador")
            {
                this.role = RoleType.ADMIN;
            }
        }
        public Role(string rol) : this(0, rol) { }

        public Roles ToEntity()
        {
            return new Roles
            {
                id = Id,
                rol = Nombre
            };
        }

        public static bool Exists(int id)
        {
            return dao.Exists(id);
        }

        public static List<Role> All()
        {
            List<Role> list = new List<Role>();
            var roles = dao.All();
            foreach(Roles rol in roles)
            {
                list.Add(Find(rol.id));
            }
            return list;
        }

        public static Role Find(int id)
        {
            if (!Exists(id)) return null;
            Roles e = dao.Get(id);
            return new Role(e.id, e.rol);
        }
        public static Role Find(string rol)
        {
            var eList = dao.All();
            foreach(Roles role in eList)
            {
                if(role.rol == rol.Trim())
                {
                    return new Role(role.id, rol);
                }
            }
            return null;
        }

        public static Role Find(RoleType rol)
        {
            if (rol == RoleType.ADMIN)
            {
                return Find("Administrador");
            }
            else if (rol == RoleType.VENDEDOR)
            {
                return Find("Vendedor");
            }else if(rol == RoleType.USUARIO)
            {
                return Find("Usuario");
            }
            return null;
        }

        public void Save()
        {
            var entity = ToEntity();
            if(Exists(Id))
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
    }
}
