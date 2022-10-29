using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CqHttpSharp.Message;
using CqHttpSharp.Data;
using CqHttpSharp.Enum;
using CqHttpSharp.Log;
using Newtonsoft.Json.Linq;
using CqHttpSharp.WebSocket;

namespace CqHttpSharp.API
{
    //TODO: 待实现API
    //download_file
    //check_url safety
    //_get_model_show
    //_set_model_show

    public class CqHttpAPI : CqHttpAPIBase
    {
        public CqHttpAPI() : base() { }

        public CqHttpAPI(CqHttpWebSocket _webSocket) : base(_webSocket) { }

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="group_id">发起私聊的群号</param>
        /// <param name="message">消息内容</param>
        /// <param name="auto_escape">消息内容是否解析 CQ 码</param>
        /// <returns>发送的消息 ID</returns>
        public async Task<CqHttpAPIDataMessageID> SendPrivateMessage(long user_id, CqMessageChain message, long group_id = -1, bool auto_escape = false)
        {
            string msg = message.ToCqQuery();

            var req = new CqHttpAPIRequest("send_private_msg");
            req.AddParam("user_id", user_id.ToString());
            if (group_id != -1) req.AddParam("group_id", group_id.ToString());
            req.AddParam("message", msg);
            req.AddParam("auto_escape", auto_escape.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null)
            {
                APILogger?.Invoke($"API 调用失败或获取响应超时：send_private_msg", CqHttpLogType.error);
                return null;
            }
            if (group_id == -1)
            {
                APILogger?.Invoke($"发送消息至 QQ ({user_id}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", CqHttpLogType.normal);
            }
            else
            {
                APILogger?.Invoke($"发送消息至 QQ ({user_id}) 通过群 ({group_id}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", CqHttpLogType.normal);
            }
            return resp.ParseData<CqHttpAPIDataMessageID>();
        }

        /// <summary>
        /// 发送群聊消息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="message">消息内容</param>
        /// <param name="auto_escape">消息内容是否解析 CQ 码</param>
        /// <returns>发送的消息 ID</returns>
        public async Task<CqHttpAPIDataMessageID> SendGroupMessage(long group_id, CqMessageChain message, bool auto_escape = false)
        {
            string msg = message.ToCqQuery();

            var req = new CqHttpAPIRequest("send_group_msg");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("message", msg);
            req.AddParam("auto_escape", auto_escape.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null)
            {
                APILogger?.Invoke($"API 调用失败或获取响应超时：send_group_msg", CqHttpLogType.error);
                return null;
            }
            APILogger?.Invoke($"发送消息至群 ({group_id}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", CqHttpLogType.normal);
            return resp.ParseData<CqHttpAPIDataMessageID>();
        }

        /// <summary>
        /// 发送私聊合并转发消息
        /// </summary>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="message">消息内容（包含若干 forward / node 类型的消息）</param>
        /// <returns>无响应</returns>
        public async Task SendPrivateForwardMessage(long user_id, CqMessageChain message)
        {
            string msg = message.ToCqQuery();

            var req = new CqHttpAPIRequest("send_private_forward_msg");
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("message", message.ToJson());

            await SendAPIRequestNoRespAsync(req);

            APILogger?.Invoke($"发送合并转发消息至 QQ ({user_id}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", CqHttpLogType.normal);
        }

        /// <summary>
        /// 发送群聊合并转发消息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="message">消息内容（包含若干 forward / node 类型的消息）</param>
        /// <returns>无响应</returns>
        public async Task SendGroupForwardMessage(long group_id, CqMessageChain message)
        {
            string msg = message.ToCqQuery();

            var req = new CqHttpAPIRequest("send_group_forward_msg");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("message", message.ToJson());

            await SendAPIRequestNoRespAsync(req);

            APILogger?.Invoke($"发送合并转发消息至群 ({group_id}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", CqHttpLogType.normal);
        }

        /// <summary>
        /// 撤回消息
        /// </summary>
        /// <param name="message_id">需要撤回的消息 ID</param>
        /// <returns>无响应</returns>
        public async Task RecallMessage(int message_id)
        {
            var req = new CqHttpAPIRequest("delete_msg");
            req.AddParam("message_id", message_id.ToString());

            await SendAPIRequestNoRespAsync(req);

            APILogger?.Invoke($"撤回消息 : {message_id}", CqHttpLogType.normal);
        }

        /// <summary>
        /// 获取消息细节
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns>消息细节</returns>
        public async Task<CqHttpAPIDataMessageDetail> GetMessage(int message_id)
        {
            var req = new CqHttpAPIRequest("get_msg");
            req.AddParam("message_id", message_id.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null)
            {
                APILogger?.Invoke($"API 调用失败或获取响应超时：get_msg", CqHttpLogType.error);
                return null;
            }
            APILogger?.Invoke($"获取消息细节 ({message_id})", CqHttpLogType.normal);
            return resp.ParseData<CqHttpAPIDataMessageDetail>();
        }

        /// <summary>
        /// 获取合并转发内容
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns>合并转发消息列表</returns>
        public async Task<CqHttpAPIDataForwardMessageChain> GetForwardMessage(int message_id)
        {
            var req = new CqHttpAPIRequest("get_forward_msg");
            req.AddParam("message_id", message_id.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataForwardMessageChain>();
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="file">图片缓存文件名称</param>
        /// <returns>图片信息</returns>
        public async Task<CqHttpAPIDataImageDetail> GetImage(string file)
        {
            var req = new CqHttpAPIRequest("get_image");
            req.AddParam("file", file);

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataImageDetail>();
        }

        /// <summary>
        /// 标记消息已读
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns>无响应</returns>
        public async Task MarkMessageAsRead(int message_id)
        {
            var req = new CqHttpAPIRequest("mark_msg_as_read");
            req.AddParam("message_id", message_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 群聊成员移除
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="reject_add_request">是否拒绝此人加群请求</param>
        /// <returns>无响应</returns>
        public async Task SetGroupKick(long group_id, long user_id, bool reject_add_request = false)
        {
            var req = new CqHttpAPIRequest("set_group_kick");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("reject_add_request", reject_add_request.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 群聊成员禁言
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="duration">禁言时长（单位：秒）</param>
        /// <returns>无响应</returns>
        public async Task SetGroupBan(long group_id, long user_id, int duration = 30 * 60)
        {
            var req = new CqHttpAPIRequest("set_group_ban");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("duration", duration.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 群聊匿名成员禁言
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="anonymous">匿名对象</param>
        /// <param name="flag">匿名对象 flag</param>
        /// <param name="duration">禁言时长（单位：秒）</param>
        /// <returns>无响应</returns>
        public async Task SetGroupAnonymousBan(long group_id, CqHttpAnonymous anonymous = null, string flag = "", int duration = 30 * 60)
        {
            var req = new CqHttpAPIRequest("set_group_anonymous_ban");
            req.AddParam("group_id", group_id.ToString());
            if (anonymous != null) req.AddParam("anonymous", JsonConvert.SerializeObject(anonymous));
            if (flag != "") req.AddParam("flag", flag);
            req.AddParam("duration", duration.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 群聊全员禁言
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="enable">是否禁言</param>
        /// <returns>无响应</returns>
        public async Task SetGroupWholeBan(long group_id, bool enable = true)
        {
            var req = new CqHttpAPIRequest("set_group_whole_ban");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("enable", enable.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 群聊设置管理员
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="enable">设置或取消</param>
        /// <returns>无响应</returns>
        public async Task SetGroupAdmin(long group_id, long user_id, bool enable = true)
        {
            var req = new CqHttpAPIRequest("set_group_admin");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("enable", enable.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 群聊匿名设置（未被支持）
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="enable">是否允许匿名聊天</param>
        /// <returns>无响应</returns>
        public async Task SetGroupAnonymous(long group_id, bool enable = true)
        {
            var req = new CqHttpAPIRequest("set_group_anonymous");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("enable", enable.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 设置群名片
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="card">群名片内容，留空则删除原先设置的</param>
        /// <returns>无响应</returns>
        public async Task SetGroupCard(long group_id, long user_id, string card = "")
        {
            var req = new CqHttpAPIRequest("set_group_card");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("card", card);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 设置群名
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="group_name">新群名</param>
        /// <returns>无响应</returns>
        public async Task SetGroupName(long group_id, string group_name)
        {
            var req = new CqHttpAPIRequest("set_group_name");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("group_name", group_name);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 退出群聊
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="is_dismiss">是否解散（需要群主权限）</param>
        /// <returns>无响应</returns>
        public async Task SetGroupLeave(long group_id, bool is_dismiss = false)
        {
            var req = new CqHttpAPIRequest("set_group_leave");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("is_dismiss", is_dismiss.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 设置群聊专属头衔
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="special_title">专属头衔，留空则删除原先已有的</param>
        /// <param name="duration">有效期，-1永久（？）</param>
        /// <returns>无响应</returns>
        public async Task SetGroupSpecialTitle(long group_id, long user_id, string special_title = "", int duration = -1)
        {
            var req = new CqHttpAPIRequest("set_group_special_title");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("special_title", special_title);
            req.AddParam("duration", duration.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 群聊打卡
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>无响应</returns>
        public async Task SetGroupSign(long group_id)
        {
            var req = new CqHttpAPIRequest("set_group_sign");
            req.AddParam("group_id", group_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 处理好友请求
        /// </summary>
        /// <param name="flag">好友请求 flag</param>
        /// <param name="approve">是否同意</param>
        /// <param name="remark">备注</param>
        /// <returns>无响应</returns>
        public async Task SetFriendAddRequest(string flag, bool approve = true, string remark = "")
        {
            var req = new CqHttpAPIRequest("set_friend_add_request");
            req.AddParam("flag", flag);
            req.AddParam("approve", approve.ToString());
            req.AddParam("remark", remark);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 处理加群请求
        /// </summary>
        /// <param name="flag">加群请求 flag</param>
        /// <param name="sub_type">请求类型</param>
        /// <param name="approve">是否同意</param>
        /// <param name="reason">拒绝理由</param>
        /// <returns>无响应</returns>
        public async Task SetGroupAddRequest(string flag, CqHttpRequestGroupSubType sub_type, bool approve = true, string reason = "")
        {
            var req = new CqHttpAPIRequest("set_group_add_request");
            req.AddParam("flag", flag);
            req.AddParam("sub_type", sub_type.ToString());
            req.AddParam("approve", approve.ToString());
            req.AddParam("reason", reason);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取登录号信息
        /// </summary>
        /// <returns>登录号信息</returns>
        public async Task<CqHttpAPIDataLoginInfo> GetLoginInfo()
        {
            var req = new CqHttpAPIRequest("get_login_info");

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataLoginInfo>();
        }

        /// <summary>
        /// 获取企点账号信息
        /// </summary>
        /// <returns>企点账号信息</returns>
        public async Task<CqHttpAPIDataQidianAccountInfo> GetQidianAccountInfo()
        {
            var req = new CqHttpAPIRequest("qidian_get_account_info");

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataQidianAccountInfo>();
        }

        /// <summary>
        /// 设置登录号资料
        /// </summary>
        /// <param name="nickname">昵称</param>
        /// <param name="company">公司</param>
        /// <param name="email">邮箱</param>
        /// <param name="college">学校</param>
        /// <param name="personal_note">个人说明</param>
        /// <returns>无响应</returns>
        public async Task SetQQProfile(string nickname, string company, string email, string college, string personal_note)
        {
            var req = new CqHttpAPIRequest("set_qq_profile");
            req.AddParam("nickname", nickname);
            req.AddParam("company", company);
            req.AddParam("email", email);
            req.AddParam("college", college);
            req.AddParam("personal_note", personal_note);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取陌生人信息
        /// </summary>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>陌生人信息</returns>
        public async Task<CqHttpAPIDataStrangerInfo> GetStrangerInfo(long user_id, bool no_cache = false)
        {
            var req = new CqHttpAPIRequest("get_stranger_info");
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("no_cache", no_cache.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataStrangerInfo>();
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <returns>好友列表</returns>
        public async Task<List<CqHttpAPIDataFriendInfo>> GetFriendList()
        {
            var req = new CqHttpAPIRequest("get_friend_list");

            CqHttpAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<CqHttpAPIDataFriendInfo>>();
        }

        /// <summary>
        /// 获取单向好友列表
        /// </summary>
        /// <returns>单向好友列表</returns>
        public async Task<List<CqHttpAPIDataFriendInfo>> GetUnidirectionalFriendList()
        {
            var req = new CqHttpAPIRequest("get_unidirectional_friend_list");

            CqHttpAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<CqHttpAPIDataFriendInfo>>();
        }

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="friend_id">目标 QQ 号</param>
        /// <returns>无响应</returns>
        public async Task DeleteFriend(long friend_id)
        {
            var req = new CqHttpAPIRequest("delete_friend");
            req.AddParam("friend_id", friend_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>群信息</returns>
        public async Task<CqHttpAPIDataGroupInfo> GetGroupInfo(long group_id, bool no_cache = false)
        {
            var req = new CqHttpAPIRequest("get_group_info");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("no_cache", no_cache.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGroupInfo>();
        }

        /// <summary>
        /// 获取群列表
        /// </summary>
        /// <returns>群信息列表</returns>
        public async Task<List<CqHttpAPIDataGroupInfo>> GetGroupList()
        {
            var req = new CqHttpAPIRequest("get_group_list");

            CqHttpAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<CqHttpAPIDataGroupInfo>>();
        }

        /// <summary>
        /// 获取群成员信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>群成员信息</returns>
        public async Task<CqHttpAPIDataGroupMember> GetGroupMemberInfo(long group_id, long user_id, bool no_cache = false)
        {
            var req = new CqHttpAPIRequest("get_group_member_info");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("no_cache", no_cache.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGroupMember>();
        }

        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>群成员列表</returns>
        public async Task<List<CqHttpAPIDataGroupMember>> GetGroupMemberList(long group_id, bool no_cache = false)
        {
            var req = new CqHttpAPIRequest("get_group_member_list");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("no_cache", no_cache.ToString());

            CqHttpAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<CqHttpAPIDataGroupMember>>();
        }

        /// <summary>
        /// 获取群荣誉信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="type">获取的群荣誉类型</param>
        /// <returns>群荣誉信息</returns>
        public async Task<CqHttpAPIDataGroupHonor> GetGroupHonorInfo(long group_id, CqHttpAPIGroupHonorType type)
        {
            var req = new CqHttpAPIRequest("get_group_honor_info");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("type", type.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGroupHonor>();
        }

        /// <summary>
        /// 获取 Cookies（未被支持）
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>Cookies</returns>
        public async Task<CqHttpAPIDataCookies> GetCookies(string domain = "")
        {
            var req = new CqHttpAPIRequest("get_cookies");
            req.AddParam("domain", domain);

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataCookies>();
        }

        /// <summary>
        /// 获取 CSRF（未被支持）
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>CSRF Token</returns>
        public async Task<CqHttpAPIDataCSRF> GetCSRFToken(string domain = "")
        {
            var req = new CqHttpAPIRequest("get_csrf_token");
            req.AddParam("domain", domain);

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataCSRF>();
        }

        /// <summary>
        /// 获取 QQ 相关接口凭证（未被支持）
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>Cookies & CSRF Token</returns>
        public async Task<CqHttpAPIDataCredentials> GetCredentials(string domain = "")
        {
            var req = new CqHttpAPIRequest("get_credentials");
            req.AddParam("domain", domain);

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataCredentials>();
        }

        /// <summary>
        /// 获取语音（未被支持）
        /// </summary>
        /// <param name="file">语音文件名</param>
        /// <param name="out_format">输出格式</param>
        /// <returns>转换后的语音文件路径</returns>
        public async Task<CqHttpAPIDataRecordFile> GetRecord(string file, CqHttpAPIAudioType out_format)
        {
            var req = new CqHttpAPIRequest("get_record");
            req.AddParam("file", file);
            req.AddParam("out_format", out_format.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataRecordFile>();
        }

        /// <summary>
        /// 检查是否可以发送图片
        /// </summary>
        /// <returns>是或否</returns>
        public async Task<CqHttpAPIDataPermission> CanSendImage()
        {
            var req = new CqHttpAPIRequest("can_send_image");

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataPermission>();
        }

        /// <summary>
        /// 检查是否可以发送语音
        /// </summary>
        /// <returns>是或否</returns>
        public async Task<CqHttpAPIDataPermission> CanSendRecord()
        {
            var req = new CqHttpAPIRequest("can_send_record");

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataPermission>();
        }

        /// <summary>
        /// 设置群头像（不稳定，登录后一段时间失效，谨慎使用）
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="file">图片文件名</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>无响应</returns>
        public async Task SetGroupPortrait(long group_id, string file, bool no_cache = false)
        {
            var req = new CqHttpAPIRequest("set_group_portrait");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("file", file);
            req.AddParam("cache", no_cache ? "0" : "1");

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取中文分词（隐藏API, 谨慎使用）
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>词组</returns>
        public async Task<CqHttpAPIDataCNWordSlices> GetCNWordSlices(string content)
        {
            var req = new CqHttpAPIRequest(".get_word_slices");
            req.AddParam("content", content);

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataCNWordSlices>();
        }

        /// <summary>
        /// 图片 OCR（未经测试）
        /// </summary>
        /// <param name="image">图片 ID</param>
        /// <returns>OCR 结果</returns>
        public async Task<CqHttpAPIDataOCR> ImageOCR(string image)
        {
            var req = new CqHttpAPIRequest("ocr_image");
            req.AddParam("image", image);

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataOCR>();
        }

        /// <summary>
        /// 获取群系统消息
        /// </summary>
        /// <returns>群系统消息</returns>
        public async Task<CqHttpAPIDataGroupSystemMessage> GetGroupSystemMessage()
        {
            var req = new CqHttpAPIRequest("get_group_system_msg");

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            if (resp.data.ContainsKey("invited_requests"))
            {
                foreach (JObject obj in (JArray)resp.data["invited_requests"])
                {
                    if (obj.ContainsKey("checked"))
                        obj["if_checked"] = obj["checked"];
                }
            }
            if (resp.data.ContainsKey("join_requests"))
            {
                foreach (JObject obj in (JArray)resp.data["join_requests"])
                {
                    if (obj.ContainsKey("checked"))
                        obj["if_checked"] = obj["checked"];
                }
            }
            return resp.ParseData<CqHttpAPIDataGroupSystemMessage>();
        }

        /// <summary>
        /// 上传私聊文件
        /// </summary>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="file">本地文件路径（网络文件请先下载到本地）</param>
        /// <param name="name">文件名称</param>
        /// <returns>无响应</returns>
        public async Task UploadPrivateFile(long user_id, string file, string name)
        {
            var req = new CqHttpAPIRequest("upload_private_file");
            req.AddParam("uesr_id", user_id.ToString());
            req.AddParam("file", file);
            req.AddParam("name", name);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 上传群文件
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="file">本地文件路径（网络文件请先下载到本地）</param>
        /// <param name="name">文件名称</param>
        /// <param name="folder">父目录 ID，默认根目录</param>
        /// <returns>无响应</returns>
        public async Task UploadGroupFile(long group_id, string file, string name, string folder = "")
        {
            var req = new CqHttpAPIRequest("upload_group_file");
            req.AddParam("uesr_id", group_id.ToString());
            req.AddParam("file", file);
            req.AddParam("name", name);
            if (folder != "" ) req.AddParam("folder", folder);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取群文件系统信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>群文件系统信息</returns>
        public async Task<CqHttpAPIDataGroupFileSystemInfo> GetGroupFileSystemInfo(long group_id)
        {
            var req = new CqHttpAPIRequest("get_group_file_system_info");
            req.AddParam("group_id", group_id.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGroupFileSystemInfo>();
        }

        /// <summary>
        /// 获取群根目录文件列表
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>文件结构</returns>
        public async Task<CqHttpAPIDataGroupFileSystemDetail> GetGroupRootFiles(long group_id)
        {
            var req = new CqHttpAPIRequest("get_group_root_files");
            req.AddParam("group_id", group_id.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGroupFileSystemDetail>();
        }

        /// <summary>
        /// 获取群子目录文件列表
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="folder_id">文件夹 ID</param>
        /// <returns>文件结构</returns>
        public async Task<CqHttpAPIDataGroupFileSystemDetail> GetGroupFilesByFolder(long group_id, string folder_id)
        {
            var req = new CqHttpAPIRequest("get_group_files_by_folder");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("folder_id", folder_id);

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGroupFileSystemDetail>();
        }

        /// <summary>
        /// 创建群文件文件夹
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="name">文件夹名称</param>
        /// <returns>无响应</returns>
        public async Task CreateGroupFileFolder(long group_id, string name)
        {
            var req = new CqHttpAPIRequest("create_group_file_folder");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("name", name);
            req.AddParam("parent_id", "/");

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 删除群文件文件夹
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="folder_id">文件夹 ID</param>
        /// <returns>无响应</returns>
        public async Task DeleteGroupFileFolder(long group_id, string folder_id)
        {
            var req = new CqHttpAPIRequest("delete_group_folder");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("folder_id", folder_id);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 删除群文件
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="file_id">文件 ID</param>
        /// <param name="busid">文件类型</param>
        /// <returns>无响应</returns>
        public async Task DeleteGroupFile(long group_id, string file_id, int busid)
        {
            var req = new CqHttpAPIRequest("delete_group_file");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("file_id", file_id);
            req.AddParam("busid", busid.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取群文件资源链接
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="file_id">文件 ID</param>
        /// <param name="busid">文件类型</param>
        /// <returns>资源链接</returns>
        public async Task<CqHttpAPIDataGroupFileUrl> GetGroupFileUrl(long group_id, string file_id, int busid)
        {
            var req = new CqHttpAPIRequest("get_group_file_url");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("file_id", file_id);
            req.AddParam("busid", busid.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGroupFileUrl>();
        }

        /// <summary>
        /// 获取剩余@全体成员次数
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>剩余次数</returns>
        public async Task<CqHttpAPIDataGroupAtAllRemain> GetGroupAtAllRemain(long group_id)
        {
            var req = new CqHttpAPIRequest("get_group_at_all_remain");
            req.AddParam("group_id", group_id.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataGroupAtAllRemain>();
        }

        /// <summary>
        /// 发送群公告
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="content">公告内容</param>
        /// <param name="image">图片路径</param>
        /// <returns>无响应</returns>
        public async Task SendGroupNotice(long group_id, string content, string image = "")
        {
            var req = new CqHttpAPIRequest("_send_group_notice");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("content", content);
            if (image != "") req.AddParam("image", image);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取群公告
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>群公告数据</returns>
        public async Task<List<CqHttpAPIDataGroupNotice>> GetGroupNotice(long group_id)
        {
            var req = new CqHttpAPIRequest("_get_group_notice");
            req.AddParam("group_id", group_id.ToString());

            CqHttpAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<CqHttpAPIDataGroupNotice>>();
        }

        /// <summary>
        /// 获取当前账号在线客户端列表
        /// </summary>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>在线列表</returns>
        public async Task<CqHttpAPIDataClient> GetOnlineClients(bool no_cache = false)
        {
            var req = new CqHttpAPIRequest("get_online_clients");
            req.AddParam("no_cache", no_cache.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataClient>();
        }

        /// <summary>
        /// 获取群消息历史记录
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="message_seq">起始消息序号</param>
        /// <returns>消息列表</returns>
        public async Task<CqHttpAPIDataMessageChain> GetGroupMessageHistory(long group_id, long message_seq = -1)
        {
            var req = new CqHttpAPIRequest("get_group_msg_history");
            req.AddParam("group_id", group_id.ToString());
            if (message_seq != -1) req.AddParam("message_seq", message_seq.ToString());

            CqHttpAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<CqHttpAPIDataMessageChain>();
        }

        /// <summary>
        /// 设置精华消息
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns>无响应</returns>
        public async Task SetEssenceMessage(long message_id)
        {
            var req = new CqHttpAPIRequest("set_essence_msg");
            req.AddParam("message_id", message_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 移除精华消息
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns>无响应</returns>
        public async Task DeleteEssenceMessage(long message_id)
        {
            var req = new CqHttpAPIRequest("delete_essence_msg");
            req.AddParam("message_id", message_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取精华消息列表
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>精华消息列表</returns>
        public async Task<List<CqHttpAPIDataEssence>> GetEssenceMessageList(long group_id)
        {
            var req = new CqHttpAPIRequest("get_essence_msg_list");
            req.AddParam("group_id", group_id.ToString());

            CqHttpAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<CqHttpAPIDataEssence>>();
        }

        /// <summary>
        /// 删除单向好友
        /// </summary>
        /// <param name="user_id">单向好友 QQ 号</param>
        /// <returns>无响应</returns>
        public async Task DeleteUniDirectionalFriend(long user_id)
        {
            var req = new CqHttpAPIRequest("delete_unidirectional_friend");
            req.AddParam("user_id", user_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }
    }
}
