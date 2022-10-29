using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using CqHttpSharp.Event;
using CqHttpSharp.Event.Manager;

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

        CqHttpEventAnalyzer eventAnalyzer;
        UTF8Encoding utf8 = new UTF8Encoding(false);

        public delegate void EvtDataReceive(string data);
        public EvtDataReceive OnDataReceive;

        public CqHttpEventManager EventManager { get; private set; }
        public bool IsConnected { get; private set; } = false;

        public CqHttpWebSocket(string _ip, int _port, ICqHttpWSDebugOutput _debugOutput = null)
        {
            ip = _ip;
            port = _port;
            output = _debugOutput;

            cancellationTokenSource = new CancellationTokenSource();

            EventManager = new CqHttpEventManager();

            eventAnalyzer = new CqHttpEventAnalyzer(EventManager);
        }

        private async Task MainLoop()
        {
            IsConnected = true;

            byte[] buffer;
            while (!cancellationTokenSource.IsCancellationRequested)
            {
                buffer = new byte[512 * 16];
                await client.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationTokenSource.Token);
                string data = utf8.GetString(buffer);
                output?.Log(data);
                OnDataReceive?.Invoke(data);

                try
                {
                    eventAnalyzer.AnalyzeEvent(data);
                }
                catch (Exception e)
                {
                    output?.Log($"Error Analyzing Event : {e.Message}\n{e.StackTrace}");
                }
            }
        }

        public void Start()
        {
            client = new ClientWebSocket();
            var connTask = client.ConnectAsync(new Uri($"ws://{ip}:{port}"), cancellationTokenSource.Token);
            connTask.ContinueWith(async task => await MainLoop());

            output?.Log("WS Start");
        }

        public async Task RequestAsync(string data)
        {
            if (!IsConnected) return;

            byte[] byteData = utf8.GetBytes(data);
            ArraySegment<byte> reqData = new ArraySegment<byte>(byteData, 0, byteData.Length);
            await client.SendAsync(reqData, WebSocketMessageType.Text, true, cancellationTokenSource.Token);
        }
    }
}
