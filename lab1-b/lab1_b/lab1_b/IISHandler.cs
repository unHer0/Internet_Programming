using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace lab1_b
{
    public class IISHandler : IHttpHandler
    {
        WebSocket socket;
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(WebSocketRequest);
            }
        }

        private async Task WebSocketRequest(AspNetWebSocketContext context)
        {
            socket = context.WebSocket;
            string s = await Receive();
            await Send(s);
            while (socket.State == WebSocketState.Open)
            {
                Thread.Sleep(2000);
                await Send($"[{DateTime.Now.ToString("h:mm:ss")}]");
            }
        }

        private async Task<string> Receive()
        {
            string rc = null;
            var buffer = new ArraySegment<byte>(new byte[512]);
            var result = await socket.ReceiveAsync(buffer, CancellationToken.None);
            rc = System.Text.Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
            return rc;
        }

        private async Task Send(string s)
        {
            var sendbuffer = new ArraySegment<byte>(System.Text.Encoding.UTF8.GetBytes("Ответ: " + s));
            await socket.SendAsync(sendbuffer, System.Net.WebSockets.WebSocketMessageType.Text,
                true, CancellationToken.None);
        }
    }
}
