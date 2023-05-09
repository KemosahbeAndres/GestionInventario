using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.DB
{
    class UserDao: Dao<Usuarios>
    {
        private DataClassesDataContext ctx;
        public UserDao()
        {
            ctx = SQLConnectionManager.getInstance();
        }
        public List<Usuarios> All()
        {
            return ctx.Usuarios.ToList();
        }

        public Usuarios Get(int id)
        {
            // funcion lambda => funcion flecha => funcion anonima
            return ctx.Usuarios.Where( (x) => x.id == id ).FirstOrDefault();
        }

        public void Insert(Usuarios item)
        {
            ctx.Usuarios.InsertOnSubmit(item);
            ctx.SubmitChanges();
        }

        public void Modify(Usuarios item)
        {
            var entity = ctx.Usuarios.Where(x => x.id == item.id).FirstOrDefault();
            if (entity == null)
            {
                return;
            }
            entity.nombre = item.nombre;
            entity.telefono = item.telefono;
            entity.correo = item.correo;
            entity.clave = item.clave;
            entity.id_rol = item.id_rol;
            entity.id_creador = item.id_creador;
            ctx.SubmitChanges();
        }

        public void Delete(Usuarios item)
        {
            var entity = ctx.Usuarios.Where(x => x.id == item.id).FirstOrDefault();
            if(entity == null)
            {
                return;
            }
            ctx.Usuarios.DeleteOnSubmit(entity);
            ctx.SubmitChanges();
        }

    }
}
