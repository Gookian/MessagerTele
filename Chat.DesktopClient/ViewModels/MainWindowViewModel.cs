namespace Chat.DesktopClient.ViewModels
{
    using System;
    using Chat.DesktopClient.Data;
    using Chat.DesktopClient.Views;
    using Core;
    using Prism.Commands;
    using Prism.Mvvm;
    using Services;

    public class MainWindowViewModel : BindableBase
    {
        private MessageService _messageService;
        private string _message;

        public string Title { get; set; } = "Desktop Chat Client";

        public string SessionName { get; set; } = "No_Name";

        public string Message { get => _message; set => SetProperty(ref _message, value); }

        public DelegateCommand SendMessageCommand { get; private set; }

        public MainWindowViewModel()
        {
            _messageService = new MessageService();
            SendMessageCommand = new DelegateCommand(SendMessage);
        }

        private void SendMessage()
        {
            _messageService.SendMessage(_message);
            _messageService.ReceiveMessage();
        }
    }
}
