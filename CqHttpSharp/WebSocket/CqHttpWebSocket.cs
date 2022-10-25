using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace CqHttpSharp.WebSocket
{
    public interface ICqHttpWSDebugOutput
    {
        void Log(string message);
    }


    public class CqHttpWebSocket
    {
        readonly string ip;
        readonly int port;

        ClientWebSocket client;
        CancellationTokenSource cancellationTokenSource;

        ICqHttpWSDebugOutput output;

        public CqHttpWebSocket(string _ip, int _port, ICqHttpWSDebugOutput _debugOutput = null)
        {
            ip = _ip;
            port = _port;
            output = _debugOutput;

            cancellationTokenSource = new CancellationTokenSource();
        }

        private async Task MainLoop()
        {
            byte[] buffer;
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                buffer = new byte[512 * 16];
                await client.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationTokenSource.Token);
                output?.Log(Encoding.UTF8.GetString(buffer));
            }
        }

        public void Start()
        {
            client = new ClientWebSocket();
            var connTask = client.ConnectAsync(new Uri($"ws://{ip}:{port}"), cancellationTokenSource.Token);
            connTask.ContinueWith(async task => await MainLoop());

            output?.Log("WS Start");

        }
    }
}
