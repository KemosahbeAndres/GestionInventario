namespace GestionInventarioWeb.Data
{
    public class DataDashboard
    {
        public User Usuario { get; set; }
        public IEnumerable<Sale> Ventas { get; set; }
        public IEnumerable<Product> Productos { get; set; }
        public int VentasMes { get; set; }
        public int VentasDia { get; set; }
        public int TotalDia { get; set; }
        public int TotalMes { get; set; }

        public DataDashboard(User usuario, IEnumerable<Sale> ventas, IEnumerable<Product> productos, int ventasMes, int ventasDia, int totalDia, int totalMes)
        {
            Usuario = usuario;
            Ventas = ventas;
            Productos = productos;
            VentasMes = ventasMes;
            VentasDia = ventasDia;
            TotalDia = totalDia;
            TotalMes = totalMes;
        }
    }
}
