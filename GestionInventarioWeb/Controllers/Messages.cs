using GestionInventarioWeb.Data;

namespace GestionInventarioWeb.Controllers
{
    public class Messages
    {
        private static readonly List<Message> messages = new List<Message>();

        public static void Set(Message message)
        {
            var found = messages.FirstOrDefault(m => m.Key == message.Key);
            if (found != null)
            {
                messages.Remove(found);
            }
            messages.Add(message);
        }

        public static void Set(String key, String text)
        {
            Messages.Set(new Message(key, text));
        }
        
        public static Message Get(String key)
        {
            var m = messages.FirstOrDefault(m => m.Key == key);
            return m != null ? m : new Message("empty", "");
        }
    }
}
