namespace Chat.DesktopClient.Views
{
    using Chat.DesktopClient.Data;
    using System.Windows;

    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();

            SessionRepository.AuthorizationWindow = this;
        }
    }
}
