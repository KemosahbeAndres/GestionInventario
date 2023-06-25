namespace GestionInventarioWeb.Data
{
    public class Message
    {
        public String Key { get; private set; }
        public String Text { 
            get {
                Readed = true;
                return Key;
            } 
            private set { 
                Text = value; 
            } 
        }
        public bool Readed { get; private set; }

        public Message(String key, String value)
        {
            Key = key;
            Text = value;
            Readed = false;
        }
    }
}
