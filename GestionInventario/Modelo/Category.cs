using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Modelo
{
    public class Category
    {
        public int Id { get; }
        public string Nombre { get; }

        public Category(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }
        public Category(string nombre) : this(0, nombre) { }
    }
}
