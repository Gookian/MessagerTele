namespace Chat.DesktopClient.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using Services;
    using System.Threading;

    public class MainWindowViewModel : BindableBase
    {
        private readonly MessageService _messageService;

        public string Title { get; set; } = "Desktop Chat Client";

        public string Message { get; set; }

        public DelegateCommand SendMessageCommand { get; private set; }

        public MainWindowViewModel()
        {
            _messageService = new MessageService();
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        private void SendMessage()
        {
            _messageService.SendMessage(Message);
        }
    }
}
