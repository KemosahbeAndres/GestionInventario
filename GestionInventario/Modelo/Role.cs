using GestionInventario.Persistence;
using System.Collections.Generic;

namespace GestionInventario.Modelo
{
    public class Role
    {

        private static readonly RoleDao dao= new RoleDao();
        
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public Role(int id, string rol)
        {
            Id = id;
            Nombre = rol.Trim();
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
            dao.Delete(ToEntity());
            return true;
        }
    }
}
