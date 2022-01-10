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

namespace Chat.DesktopClient.Services
{
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

            //_ = ReciveAsync(_connectionManager.Client);
        }


        public async void SendMessage(string message)
        {
            Message messageObject = new Message
            {
                Id = Guid.NewGuid(),
                Name = "Иван",
                Text = message,
                Time = DateTime.Now
            };

            DatabaseMessage.Add(messageObject);

            var jsonMessage = JsonConvert.SerializeObject(messageObject);
            var bytes = Encoding.UTF8.GetBytes(jsonMessage);
            await _connectionManager.Client.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, true, CancellationToken.None);
            
            await ReciveAsync(_connectionManager.Client);

            loggerManager.logger.Debug($"Пользователь отправил сообщение на сервер");
        }

        private async Task ReciveAsync(ClientWebSocket client)
        {
            var buffer = new byte[1024 * 4];

            //while (true)
            //{
            WebSocketReceiveResult result = null;

            do
            {
                result = await client.ReceiveAsync(buffer, CancellationToken.None);
            }
            while (!result.EndOfMessage);

            if (result.MessageType == WebSocketMessageType.Text)
            {
                var message = Encoding.UTF8.GetString(buffer);

                Message messageObject = JsonConvert.DeserializeObject<Message>(message);
                UIElements.CreateMessageView(messageObject);
            }

            /*if (result.MessageType == WebSocketMessageType.Close)
            {
                await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                break;
            }*/
            //}
        }
    }
}
