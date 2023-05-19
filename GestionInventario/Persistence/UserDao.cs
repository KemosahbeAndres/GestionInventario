using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Persistence
{
    class UserDao: Dao<Usuarios>
    {

        public override List<Usuarios> All()
        {
            return ctx.Usuarios.ToList();
        }

        public override List<Usuarios> Take(int index = 0, int count = 20)
        {
            return ctx.Usuarios.Skip(index).Take(count).ToList();
        }

        public override Usuarios Get(int id)
        {
            if (id <= 0) return null;
            // funcion lambda => funcion flecha => funcion anonima
            return ctx.Usuarios.Where( (x) => x.id == id ).FirstOrDefault();
        }

        public override int Insert(Usuarios item)
        {
            if (!Exists(item.id))
            {
                item.id = 0;
                ctx.Usuarios.InsertOnSubmit(item);
                ctx.SubmitChanges();
                return ctx.Usuarios.OrderByDescending(x => x.id).First().id;
            }
            return 0;
        }

        public override int Modify(Usuarios item)
        {
            if (!Exists(item.id)) return 0;
            var entity = Get(item.id);
            entity.nombre = item.nombre;
            entity.telefono = item.telefono;
            entity.rut = item.rut;
            entity.clave = item.clave;
            entity.id_rol = item.id_rol;
            ctx.SubmitChanges();
            return item.id;
        }

        public override int Delete(int id)
        {
            if (!Exists(id)) return 0;
            var entity = Get(id);
            ctx.Usuarios.DeleteOnSubmit(entity);
            ctx.SubmitChanges();
            return id;
        }

    }
}
