namespace GestionInventarioWeb.Data
{
    public class User
    {
        public int Id { get; }
        public string Name { get; }
        public string Rut { get; }
        public string Phone { get; }
        public string Role { get; }

        public User(int id, string name, string rut, string? phone, string? role)
        {
            Id = id;
            Name = name;
            Rut = rut;
            Phone = phone == null ? "" : phone;
            Role = role == null ? "Usuario" : role;
        }
    }
}
