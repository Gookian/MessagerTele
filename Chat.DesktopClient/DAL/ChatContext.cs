using Core;
using Microsoft.EntityFrameworkCore;

namespace Chat.DesktopClient.DAL
{
    public class ChatContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<User> Users { get; set; }

        public ChatContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-FMF1DD0\\SQLEXPRESS;Database=MyDB;Trusted_Connection=True;");
        }
    }
}
