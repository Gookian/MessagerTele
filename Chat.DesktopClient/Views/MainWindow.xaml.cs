namespace Chat.DesktopClient.Views
{
    using System.Collections.Generic;
    using System.Windows;
    using Chat.DesktopClient.DAL;
    using Chat.DesktopClient.Data;
    using Core;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            UIManager.ViewTargetMessages = ViewMessages;
            UIManager.ViewTargetUsers = ViewUsers;
            UIManager.ViewTargetUser = ViewUser;

            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<Message> messages = new List<Message>();

            foreach (Message message in DatabaseRepository.GetAllMessages())
            {
                messages.Add(message);
            }

            messages.Sort((a, b) => a.Time.CompareTo(b.Time));

            foreach (Message message in messages)
            {
                UIManager.CreateMessageView(message);
            }

            foreach (User user in DatabaseRepository.GetAllUsers())
            {
                UIManager.CreateUserView(user);
                if (SessionRepository.targetUser.Id == user.Id)
                    UIManager.CreateUserView(user, true);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
