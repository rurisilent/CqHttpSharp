using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NapCatSharpLib.WebSocket;
using NapCatSharpLib.Log;

namespace NapCatSharpLib.API
{
    public class NapCatAPIBase
    {
        protected NapCatWebSocket socket;

        protected Dictionary<int, NapCatAPIRespondBase> respMap;

        protected readonly int c_updateDeltaTime = 10; //ms
        protected readonly int c_timeoutCount = 1500; //15s

        public EvtNapCatLog LogAPI;

        public NapCatAPIBase()
        {
            respMap = new Dictionary<int, NapCatAPIRespondBase>();
        }

        public NapCatAPIBase(NapCatWebSocket _socket)
        {
            socket = _socket;
            socket.OnDataReceive += AnalyzeRespond;

            respMap = new Dictionary<int, NapCatAPIRespondBase>();
        }

        ~NapCatAPIBase()
        {
            if (socket != null)
                socket.OnDataReceive -= AnalyzeRespond;
        }

        /// <summary>
        /// 分析请求的响应
        /// </summary>
        /// <param name="data">JSON 响应</param>
        private void AnalyzeRespond(string data)
        {
            JObject jsonData = JObject.Parse(data);

            if (jsonData.ContainsKey("status") && jsonData.ContainsKey("data") && jsonData.ContainsKey("echo"))
            {
                if (jsonData["data"].Type == JTokenType.Array)
                {
                    try
                    {
                        var resp = JsonConvert.DeserializeObject<NapCatAPIRespondArray>(data);
                        if (int.TryParse(resp.echo, out var echo))
                        {
                            respMap.Add(echo, resp);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.Message}, {e.StackTrace}");
                    }
                }
                else if (jsonData["data"].Type == JTokenType.Object)
                {
                    try
                    {
                        var resp = JsonConvert.DeserializeObject<NapCatAPIRespondObject>(data);
                        if (int.TryParse(resp.echo, out var echo))
                        {
                            respMap.Add(echo, resp);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"{e.Message}, {e.StackTrace}");
                    }
                }
            }
        }

        protected int GetReqId()
        {
            Random r = new Random();
            int ret;
            do
            {
                ret = r.Next(1, 100000000);
            } while (respMap.ContainsKey(ret));
            return ret;
        }

        //TODO: 请求超时可能导致后面接收到的数据堆积在字典中，需要解决这个问题

        protected async Task<NapCatAPIRespondObject> SendAPIRequestObjectAsync(NapCatAPIRequest req)
        {
            if (!socket.IsConnected) return null;

            var id = GetReqId();
            req.echo = id.ToString();

            await socket.RequestAsync(req.Serialize());

            int count = 0;
            NapCatAPIRespondObject resp = null;

            await Task.Run(async () =>
            {
                while (count < c_timeoutCount)
                {
                    if (respMap.ContainsKey(id))
                    {
                        resp = (NapCatAPIRespondObject)respMap[id];
                        respMap.Remove(id);
                        return;
                    }
                    await Task.Delay(c_updateDeltaTime);
                    count++;
                }
            });

            return resp;
        }

        protected async Task<NapCatAPIRespondArray> SendAPIRequestArrayAsync(NapCatAPIRequest req)
        {
            if (!socket.IsConnected) return null;

            var id = GetReqId();
            req.echo = id.ToString();

            await socket.RequestAsync(req.Serialize());

            int count = 0;
            NapCatAPIRespondArray resp = null;

            await Task.Run(async () =>
            {
                while (count < c_timeoutCount)
                {
                    if (respMap.ContainsKey(id))
                    {
                        resp = (NapCatAPIRespondArray)respMap[id];
                        respMap.Remove(id);
                        return;
                    }
                    await Task.Delay(c_updateDeltaTime);
                    count++;
                }
            });

            return resp;
        }

        protected async Task SendAPIRequestNoRespAsync(NapCatAPIRequest req)
        {
            if (!socket.IsConnected) return;

            await socket.RequestAsync(req.Serialize());
        }
    }
}
