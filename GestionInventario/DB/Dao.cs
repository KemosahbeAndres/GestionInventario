using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.DB
{
    interface Dao<T>
    {
        List<T> All();
        T Get(int id);
        void Insert(T item);
        void Modify(T item);
        void Delete(T item);

    }
}
