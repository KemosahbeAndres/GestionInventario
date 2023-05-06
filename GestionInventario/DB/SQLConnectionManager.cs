﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.DB
{
    class SQLConnectionManager
    {
        private static DataClassesDataContext instance;

        public static DataClassesDataContext getInstance()
        {
            if(instance != null)
            {
                instance.Dispose();
            }
            instance = new DataClassesDataContext();
            return instance;
        }
    }
}