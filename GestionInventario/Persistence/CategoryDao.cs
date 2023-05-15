using System.Collections.Generic;
using System.Linq;

namespace GestionInventario.Persistence
{
    class CategoryDao : Dao<Categorias>
    {
        public override List<Categorias> All()
        {
            return ctx.Categorias.OrderBy(x => x.id).ToList();
        }

        public override void Delete(int id)
        {
            if (!Exists(id)) return;
            var e = Get(id);
            ctx.Categorias.DeleteOnSubmit(e);
            ctx.SubmitChanges();
        }

        public override Categorias Get(int id)
        {
            return ctx.Categorias.SingleOrDefault(x => x.id == id);
        }

        public override void Insert(Categorias item)
        {
            if (!Exists(item.id))
            {
                Categorias e = new Categorias();
                e.categoria = item.categoria;
                ctx.Categorias.InsertOnSubmit(e);
                ctx.SubmitChanges();
            }
        }

        public override void Modify(Categorias item)
        {
            if (Exists(item.id))
            {
                Categorias e = Get(item.id);
                e.categoria = item.categoria;
                ctx.SubmitChanges();
            }
        }

        public override List<Categorias> Take(int index = 0, int count = 20)
        {
            return ctx.Categorias.Skip(index).Take(count).ToList();
        }
    }
}
