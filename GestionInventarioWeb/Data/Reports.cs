using GestionInventarioWeb.Models;

namespace GestionInventarioWeb.Data
{
    public class Reports
    {
        public IEnumerable<Sale> Ventas { get; set; }
        public IEnumerable<Buy> Compras { get; set; }
        public IEnumerable<Product> Productos { get; set; }
        public IEnumerable<User> Usuarios { get; set; }

        public Reports(IEnumerable<Sale> ventas, IEnumerable<Buy> compras, IEnumerable<Product> productos, IEnumerable<User> usuarios)
        {
            Ventas = ventas;
            Compras = compras;
            Productos = productos;
            Usuarios = usuarios;
        }
    }
}
