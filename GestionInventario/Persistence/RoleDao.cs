using System;
using System.Collections.Generic;
using System.Linq;

namespace GestionInventario.Persistence
{
    public class RoleDao: Dao<Roles>
    {

        public override List<Roles> All()
        {
            return ctx.Roles.OrderBy(x => x.id).ToList();
        }

        public override List<Roles> Take(int index = 0, int count = 20)
        {
            return ctx.Roles.Skip(index).Take(count).ToList();
        }

        public override void Delete(int id)
        {
            if(!Exists(id))
            {
                return;
            }
            Roles e = ctx.Roles.Single(x => x.id == id);
            ctx.Roles.DeleteOnSubmit(e);
            ctx.SubmitChanges();
        }

        public override Roles Get(int id)
        {
            if (id <= 0) return null;
            Roles entity = ctx.Roles.Where(x => x.id == id).FirstOrDefault();
            return entity;
        }

        public override void Insert(Roles item)
        {
            if(!Exists(item.id))
            {
                ctx.Roles.InsertOnSubmit(item);
                ctx.SubmitChanges();
            }
        }

        public override void Modify(Roles item)
        {
            if (Exists(item.id))
            {
                Roles entity = Get(item.id);
                entity.rol = item.rol;
                ctx.SubmitChanges();
            }
        }

        
    }
}
