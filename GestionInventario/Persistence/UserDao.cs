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

        public override void Insert(Usuarios item)
        {
            if (!Exists(item.id))
            {
                item.id = -1;
                ctx.Usuarios.InsertOnSubmit(item);
                ctx.SubmitChanges();
            }
        }

        public override void Modify(Usuarios item)
        {
            if (!Exists(item.id)) return;
            var entity = Get(item.id);
            entity.nombre = item.nombre;
            entity.telefono = item.telefono;
            entity.rut = item.rut;
            entity.clave = item.clave;
            entity.id_rol = item.id_rol;
            ctx.SubmitChanges();
        }

        public override void Delete(int id)
        {
            if (!Exists(id)) return;
            var entity = Get(id);
            ctx.Usuarios.DeleteOnSubmit(entity);
            ctx.SubmitChanges();
        }

    }
}
