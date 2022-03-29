using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using System;

namespace Chat.Server.Test
{
    public class Tests
    {
        private Mock<IMessageHandler> messageHandler;
        private Mock<IWebSocket> webSocket;

        private Task<bool> task;

        [SetUp]
        public void Setup()
        {
            messageHandler = new Mock<IMessageHandler>();
            webSocket = new Mock<IWebSocket>();
        }

        [Test]
        public void OnConnected_ConnectToServer_IsConnected()
        {
            webSocket.Setup(x => x.Create()).Returns(true);

            IWebSocket socket = webSocket.Object;

            task = new Task<bool>(() =>
            {
                return true;
            });

            messageHandler.Setup(x => x.OnConnected(socket)).Returns(task);

            IMessageHandler handler = messageHandler.Object;

            Task result = handler.OnConnected(socket);
            Assert.That(result == task);
        }
    }
}