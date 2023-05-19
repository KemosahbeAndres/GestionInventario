using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Persistence
{
    class BuyItemDao : Dao<Item_Compra>
    {
        public override List<Item_Compra> All()
        {
            return ctx.Item_Compra.OrderBy(x => x.id).ToList();
        }

        public override int Delete(int id)
        {
            if (!Exists(id)) return 0;
            var e = Get(id);
            ctx.Item_Compra.DeleteOnSubmit(e);
            ctx.SubmitChanges();
            return id;
        }


        public override Item_Compra Get(int id)
        {
            return ctx.Item_Compra.SingleOrDefault(x => x.id == id);
        }

        public override int Insert(Item_Compra item)
        {
            if (!Exists(item.id))
            {
                Item_Compra e = new Item_Compra();
                e.id_producto = item.id_producto;
                e.id_compra = item.id_compra;
                ctx.Item_Compra.InsertOnSubmit(e);
                ctx.SubmitChanges();
                return ctx.Item_Compra.OrderByDescending(x => x.id).First().id;
            }
            return 0;
        }

        public override int Modify(Item_Compra item)
        {
            if (Exists(item.id))
            {
                Item_Compra e = Get(item.id);
                e.id_producto = item.id_producto;
                e.id_compra = item.id_compra;
                ctx.SubmitChanges();
                return item.id;
            }
            return 0;
        }

        public override List<Item_Compra> Take(int index = 0, int count = 20)
        {
            return ctx.Item_Compra.Skip(index).Take(count).ToList();
        }
    }
}
