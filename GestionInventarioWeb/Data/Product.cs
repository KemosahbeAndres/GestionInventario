namespace GestionInventarioWeb.Data
{
    public class Product
    {
        public int Id { get; }
        public string EAN { get; }
        public string Name { get; }
        public string Description { get; }
        public int Price { get; }
        public string Category { get; }
        public int Stock { get; }
        public int Cantidad { get; set; }

        public Product(int id, string ean, string name, string description, int price, string category, int stock)
        {
            Id = id;
            EAN = ean;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            Stock = stock;
            Cantidad = 0;
        }
    }
}
