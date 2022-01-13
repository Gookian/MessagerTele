using Chat.Server.SocketsManager;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using Core;
using Newtonsoft.Json;
using Core.Managers;
using static Core.Managers.LoggerManager;

namespace Chat.Server.Handlers
{
    public class MessageHandler : SocketHandler
    {
        private LoggerManager loggerManager = new LoggerManager("logFileServer.txt", LogLeavel.Debug, LogLeavel.Fatal);

        private readonly ConcurrentDictionary<User, WebSocket> _connections = new();

        public MessageHandler(ConnectionManager connectionManager) : base(connectionManager)
        {
            loggerManager.logger.Debug($"Сервер запущен");
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = $"User{_connections.Count}"
            };

            loggerManager.logger.Debug($"Пользователь подключился к серверу {user.Name}");

            _connections.TryAdd(user, socket);
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            await base.OnDisconnected(socket);

            loggerManager.logger.Debug($"Пользователь отключился от сервера");
        }

        public override async Task Receive(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Message messageObject = JsonConvert.DeserializeObject<Message>(message);

            loggerManager.logger.Info($"На сервер пришло сообщение от {messageObject.Name}");

            await SendMessageToAll(message);
            loggerManager.logger.Info($"Сервер отправил сообщения остальным пользователям");
        }
    }
}
