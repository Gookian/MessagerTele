namespace Chat.DesktopClient.Services
{
    using System;
    using System.Text;
    using System.Net.WebSockets;
    using System.Threading;
    using Core;
    using Chat.DesktopClient.Managers;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Chat.DesktopClient.DAL;
    using Core.Managers;
    using static Core.Managers.LoggerManager;
    using Chat.DesktopClient.Data;
    using Chat.DesktopClient.Views;

    class MessageService
    {
        private LoggerManager loggerManager = new LoggerManager("logFileClient.txt", LogLeavel.Debug, LogLeavel.Fatal);

        private const string API = "message";

        private readonly ConnectionManager _connectionManager;

        public MessageService()
        {
            _connectionManager = new ConnectionManager(API);
            _ = _connectionManager.StartConnection();

            Thread.Sleep(100);
            ReceiveMessage();

            loggerManager.logger.Debug($"Пользователь подключился к серверу");
        }

        public async void ReceiveMessage()
        {
            while (this is not null)
            {
                Message result = await ReciveAsync();
                UIManager.CreateMessageView(result);
            }
        }

        public void SendMessage(string message)
        {
            Message messageObject = new Message
            {
                Id = Guid.NewGuid(),
                SenderId = SessionRepository.targetUser.Id,
                Name = SessionRepository.targetUser.Name,
                Text = message,
                Time = DateTime.Now
            };

            var jsonMessage = JsonConvert.SerializeObject(messageObject);
            var bytes = Encoding.UTF8.GetBytes(jsonMessage);
            _connectionManager.Client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

            loggerManager.logger.Debug($"Пользователь [{messageObject.Name}] отправил сообщение на сервер");

            DatabaseRepository.Add(messageObject);

            loggerManager.logger.Debug($"Сообщение [{messageObject.Id}] сохранено в базу данных");
        }

        public async Task<Message> ReciveAsync()
        {
            var buffer = new byte[1024 * 4];

            var result = await _connectionManager.Client.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None);

            var message = Encoding.UTF8.GetString(buffer);

            return JsonConvert.DeserializeObject<Message>(message);
        }
    }
}
