using NapCatSharpLib.Data;
using NapCatSharpLib.WebSocket;
using System.Threading.Tasks;

namespace NapCatSharpLib.API
{
    public class NapCatAPIAdvanced : NapCatAPIBase
    {
        public NapCatAPIAdvanced() : base() { }

        public NapCatAPIAdvanced(NapCatWebSocket _webSocket) : base(_webSocket) { }

        /// <summary>
        /// 获取版本信息
        /// </summary>
        /// <returns>版本信息</returns>
        public async Task<NapCatAPIDataGOCQVersionInfo> GetVersionInfo()
        {
            var req = new NapCatAPIRequest("get_version_info");

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGOCQVersionInfo>();
        }

        /// <summary>
        /// 获取状态信息
        /// </summary>
        /// <returns>状态信息</returns>
        public async Task<NapCatAPIDataGOCQStatus> GetStatus()
        {
            var req = new NapCatAPIRequest("get_status");

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGOCQStatus>();
        }

        /// <summary>
        /// 重启 go-NapCat
        /// </summary>
        /// <param name="delay">延迟（单位：毫秒）</param>
        /// <returns>无响应</returns>
        public async Task SetRestart(int delay)
        {
            var req = new NapCatAPIRequest("set_restart");
            req.AddParam("delay", delay.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 清理缓存（未被支持）
        /// </summary>
        /// <returns>无响应</returns>
        public async Task CleanCache()
        {
            var req = new NapCatAPIRequest("clean_cache");

            await SendAPIRequestNoRespAsync(req);
        }
    }
}
