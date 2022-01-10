using Core;
using Microsoft.EntityFrameworkCore;

namespace Chat.DesktopClient.DAL
{
    public class DatabaseMessage
    {
        private static ChatContext db = new ChatContext();

        public static DbSet<Message> GetAllMessages()
        {
            return db.Messages;
        }

        public static void Add(Message message)
        {
            db.Add(message);
            db.SaveChanges();
        }
    }
}
