using Core;
using System.Windows;

namespace Chat.DesktopClient.Data
{
    public class SessionRepository
    {
        public static User targetUser { get; set; }

        public static Window AuthorizationWindow { get; set; }

        public static void Initialize(User user)
        {
            targetUser = user;
        }
    }
}
