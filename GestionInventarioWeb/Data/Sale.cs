using Org.BouncyCastle.Bcpg.OpenPgp;

namespace GestionInventarioWeb.Data
{
    public class Sale
    {
        public int Id { get; }
        public DateTime Date { get; }
        public User Seller { get; }
        public IEnumerable<Product> Products { get; }

        public Sale(int id, DateTime date, User seller, IEnumerable<Product> products)
        {
            Id = id;
            Date = date;
            Seller = seller;
            Products = products;
        }

        public int GetTotal()
        {
            int suma = 0;
            foreach (Product p in Products)
            {
                suma += p.Cantidad * p.Price;
            }
            return suma;
        }
    }
}
