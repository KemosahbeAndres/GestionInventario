using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Persistence
{
    public abstract class Dao<T>
    {
        protected DataClassesDataContext ctx;
        public Dao()
        {
            ctx = SQLConnectionManager.getInstance();
        }
        public abstract List<T> All();
        public abstract List<T> Take(int index = 0, int count = 20);
        public abstract T Get(int id);
        public bool Exists(int id)
        {
            return Get(id) != null;
        }
        public abstract int Insert(T item);
        public abstract int Modify(T item);
        public abstract int Delete(int id);
    }
}
