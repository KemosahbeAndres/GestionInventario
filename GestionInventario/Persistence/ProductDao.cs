using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Persistence
{
    class ProductDao : Dao<Productos>
    {
        public override List<Productos> All()
        {
            return ctx.Productos.OrderBy(x => x.id).ToList();
        }

        public override void Delete(int id)
        {
            if (!Exists(id)) return;
            ctx.Productos.DeleteOnSubmit(Get(id));
            ctx.SubmitChanges();
        }

        public override Productos Get(int id)
        {
            return ctx.Productos.SingleOrDefault(x => x.id == id);
        }

        public override void Insert(Productos item)
        {
            if (!Exists(item.id))
            {
                Productos e = new Productos()
                {
                    ean = item.ean,
                    nombre = item.nombre,
                    descripcion = item.descripcion,
                    precio = item.precio,
                    id_categoria = item.id_categoria
                };
                ctx.Productos.InsertOnSubmit(e);
                ctx.SubmitChanges();
            }
        }

        public override void Modify(Productos item)
        {
            if (!Exists(item.id))
            {
                Productos e = Get(item.id);

                e.id = item.id;
                e.ean = item.ean;
                e.nombre = item.nombre;
                e.descripcion = item.descripcion;
                e.precio = item.precio;
                e.id_categoria = item.id_categoria;

                ctx.SubmitChanges();
            }
        }

        public override List<Productos> Take(int index = 0, int count = 20)
        {
            return ctx.Productos.Skip(index).Take(count).ToList();
        }
    }
}
