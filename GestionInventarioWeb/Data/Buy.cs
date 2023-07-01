namespace GestionInventarioWeb.Data
{
    public class Buy
    {
        public int Id { get; }
        public DateTime Date { get; }
        public User Buyer { get;  }
        public int Cost { get; }
        public IEnumerable<Product> Products { get; }

        public Buy(int id, DateTime date, User buyer, IEnumerable<Product> products)
        {
            Id = id;
            Date = date;
            Buyer = buyer;
            Products = products;
            Cost = GetTotal();
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
