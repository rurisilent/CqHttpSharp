using CqHttpSharp.Data;
using CqHttpSharp.WebSocket;
using System.Threading.Tasks;

namespace CqHttpSharp.API
{
    public class CqHttpAPIAdvanced : CqHttpAPIBase
    {
        public CqHttpAPIAdvanced() : base() { }

        public CqHttpAPIAdvanced(CqHttpWebSocket _webSocket) : base(_webSocket) { }

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns>版本信息</returns>
        public async Task<CqHttpAPIDataGOCQVersionInfo> GetVersionInfo()
        {
            var req = new CqHttpAPIRequest("get_version_info");

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGOCQVersionInfo>();
        }

        /// <summary>
        /// 获取状态信息
        /// </summary>
        /// <returns>状态信息</returns>
        public async Task<CqHttpAPIDataGOCQStatus> GetStatus()
        {
            var req = new CqHttpAPIRequest("get_status");

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGOCQStatus>();
        }

        /// <summary>
        /// 重启 go-cqhttp
        /// </summary>
        /// <param name="delay">延迟（单位：毫秒）</param>
        /// <returns>无响应</returns>
        public async Task SetRestart(int delay)
        {
            var req = new CqHttpAPIRequest("set_restart");
            req.AddParam("delay", delay.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 清理缓存（未被支持）
        /// </summary>
        /// <returns>无响应</returns>
        public async Task CleanCache()
        {
            var req = new CqHttpAPIRequest("clean_cache");

            await SendAPIRequestNoRespAsync(req);
        }
    }
}
