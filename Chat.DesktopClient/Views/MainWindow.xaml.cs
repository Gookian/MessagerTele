namespace Chat.DesktopClient.Views
{
    using System.Collections.Generic;
    using System.Windows;
    using Chat.DesktopClient.DAL;
    using Core;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            UIElements.ViewTarget = ViewMessages;

            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            List<Message> messages = new List<Message>();

            foreach (Message message in DatabaseMessage.GetAllMessages())
            {
                messages.Add(message);
            }

            messages.Sort((a, b) => a.Time.CompareTo(b.Time));

            foreach (Message message in messages)
            {
                UIElements.CreateMessageView(message);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
