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

        public override int Delete(int id)
        {
            if (!Exists(id)) return 0;
            var e = Get(id);
            ctx.Categorias.DeleteOnSubmit(e);
            ctx.SubmitChanges();
            return id;
        }

        public override Categorias Get(int id)
        {
            return ctx.Categorias.SingleOrDefault(x => x.id == id);
        }

        public override int Insert(Categorias item)
        {
            if (!Exists(item.id))
            {
                Categorias e = new Categorias();
                e.categoria = item.categoria;
                ctx.Categorias.InsertOnSubmit(e);
                ctx.SubmitChanges();
                return ctx.Categorias.OrderByDescending(x => x.id).First().id;
            }
            return 0;
        }

        public override int Modify(Categorias item)
        {
            if (Exists(item.id))
            {
                Categorias e = Get(item.id);
                e.categoria = item.categoria;
                ctx.SubmitChanges();
                return item.id;
            }
            return 0;
        }

        public override List<Categorias> Take(int index = 0, int count = 20)
        {
            return ctx.Categorias.Skip(index).Take(count).ToList();
        }
    }
}
