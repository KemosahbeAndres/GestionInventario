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
            this.ctx = SQLConnectionManager.getInstance();
        }
        public List<Usuarios> All()
        {
            return ctx.Usuarios.ToList();
        }

        public Usuarios Get(int id)
        {
            return ctx.Usuarios.Where(x => x.id == id).FirstOrDefault();
        }

        public void Insert(Usuarios item)
        {
            var table = ctx.GetTable<Usuarios>();
        }

        public void Modify(Usuarios item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Usuarios item)
        {
            throw new NotImplementedException();
        }

        
    }
}
