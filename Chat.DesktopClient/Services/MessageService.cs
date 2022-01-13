namespace Chat.DesktopClient.Services
{
    using System;
    using System.Text;
    using System.Net.WebSockets;
    using System.Threading;
    using Core;
    using Chat.DesktopClient.Managers;
    using Newtonsoft.Json;
    using Chat.DesktopClient.Views;
    using System.Threading.Tasks;
    using Chat.DesktopClient.DAL;
    using Core.Managers;
    using static Core.Managers.LoggerManager;
    using System.Windows;
    using Chat.DesktopClient.Data;

    class MessageService
    {
        private LoggerManager loggerManager = new LoggerManager("logFileClient.txt", LogLeavel.Debug, LogLeavel.Fatal);

        private const string API = "message";

        private readonly ConnectionManager _connectionManager;

        public MessageService()
        {
            _connectionManager = new ConnectionManager(API);
            _ = _connectionManager.StartConnection();

            loggerManager.logger.Debug($"Пользователь подключился к серверу");

            _ = ReciveAsync(_connectionManager.Client);
        }


        public async void SendMessage(string message)
        {
            Message messageObject = new Message
            {
                Id = Guid.NewGuid(),
                SenderId = SessionRepository.targetUser.Id,
                Name = SessionRepository.targetUser.Name,
                Text = message,
                Time = DateTime.Now
            };

            try
            {
                var jsonMessage = JsonConvert.SerializeObject(messageObject);
                var bytes = Encoding.UTF8.GetBytes(jsonMessage);
                await _connectionManager.Client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);

                loggerManager.logger.Debug($"Пользователь [{messageObject.Name}] отправил сообщение на сервер");

                DatabaseRepository.Add(messageObject);

                loggerManager.logger.Debug($"Сообщение [{messageObject.Id}] сохранено в базу данных");
            }
            catch (ObjectDisposedException e)
            {
                MessageBox.Show("Вы не подключены к серверу", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                loggerManager.logger.Error($"Попытка отправки сообщения без подключения к серверу");
            }
        }

        private async Task ReciveAsync(ClientWebSocket client)
        {
            while (true)
            {
                var buffer = new byte[1024 * 4];

                WebSocketReceiveResult result = null;

                do
                {
                    Thread.Sleep(100);
                    result = await client.ReceiveAsync(buffer, CancellationToken.None);
                }
                while (!result.EndOfMessage);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer);

                    Message messageObject = JsonConvert.DeserializeObject<Message>(message);
                    UIManager.CreateMessageView(messageObject);
                }
            }
        }
    }
}
