using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Persistence
{
    class SQLConnectionManager
    {
        private static DataClassesDataContext instance;

        public static DataClassesDataContext getInstance()
        {
            if(instance == null)
            {
                instance = new DataClassesDataContext();
            }
            return instance;
        }
    }
}
