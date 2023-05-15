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
        public abstract bool Exists(int id);
        public abstract void Insert(T item);
        public abstract void Modify(T item);
        public abstract void Delete(int id);
    }
}
