using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Persistence
{
    class InventoryDao : Dao<Inventario>
    {
        public override List<Inventario> All()
        {
            return ctx.Inventario.OrderByDescending(x => x.fecha).ToList();
        }

        public override int Delete(int id)
        {
            if (!Exists(id)) return 0;
            var e = Get(id);
            ctx.Inventario.DeleteOnSubmit(e);
            ctx.SubmitChanges();
            return id;
        }

        public override Inventario Get(int id)
        {
            return ctx.Inventario.SingleOrDefault(x => x.id == id);
        }

        public override int Insert(Inventario item)
        {
            if (!Exists(item.id))
            {
                Inventario e = new Inventario();
                e.fecha = item.fecha;
                e.cantidad = item.cantidad;
                e.id_producto = item.id_producto;
                ctx.Inventario.InsertOnSubmit(e);
                ctx.SubmitChanges();
                return ctx.Inventario.OrderByDescending(x => x.id).First().id;
            }
            return 0;
        }

        public override int Modify(Inventario item)
        {
            if (Exists(item.id))
            {
                Inventario e = Get(item.id);
                e.fecha = item.fecha;
                e.cantidad = item.cantidad;
                e.id_producto = item.id_producto;
                ctx.SubmitChanges();
                return item.id;
            }
            return 0;
        }

        public override List<Inventario> Take(int index = 0, int count = 20)
        {
            return ctx.Inventario.Skip(index).Take(count).ToList();
        }

        public List<Inventario> For(int productid)
        {
            return ctx.Inventario.Where(x => x.id_producto == productid).OrderByDescending(x => x.fecha).ToList();
        }
    }
}
