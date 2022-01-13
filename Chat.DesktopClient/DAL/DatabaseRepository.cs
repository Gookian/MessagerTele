using Core;
using Microsoft.EntityFrameworkCore;

namespace Chat.DesktopClient.DAL
{
    public class DatabaseRepository
    {
        private static ChatContext db = new ChatContext();

        public static DbSet<Message> GetAllMessages()
        {
            return db.Messages;
        }

        public static DbSet<User> GetAllUsers()
        {
            return db.Users;
        }

        public static void Add<T>(T element)
        {
            db.Add(element);
            db.SaveChanges();
        }
    }
}
