using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Chat.Server.Test
{
    public interface IMessageHandler
    {
        public Task OnConnected(IWebSocket socket);

        public Task OnDisconnected(IWebSocket socket);

        public Task Receive(IWebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
