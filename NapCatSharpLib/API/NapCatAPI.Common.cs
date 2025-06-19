using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NapCatSharpLib.Message;
using NapCatSharpLib.Data;
using NapCatSharpLib.Enum;
using NapCatSharpLib.Log;
using Newtonsoft.Json.Linq;
using NapCatSharpLib.WebSocket;

namespace NapCatSharpLib.API
{
    //TODO: 待实现API
    //download_file
    //check_url safety
    //_get_model_show
    //_set_model_show

    public class NapCatAPI : NapCatAPIBase
    {
        public NapCatAPI() : base() { }

        public NapCatAPI(NapCatWebSocket _webSocket) : base(_webSocket) { }

        #region User Operations

        /// <summary>
        /// 设置登录号资料
        /// </summary>
        /// <param name="nickname">昵称</param>
        /// <param name="personal_note">个人说明</param>
        /// <param name="sex">性别</param>
        /// <returns>标准响应</returns>
        public async Task<NapCatAPIDataCommon> SetQQProfile(string nickname, string personal_note, string sex)
        {
            var req = new NapCatAPIRequest("set_qq_profile");
            req.AddParam("nickname", nickname);
            req.AddParam("personal_note", personal_note);
            req.AddParam("sex", sex);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            return resp.ParseData<NapCatAPIDataCommon>();
        }

        /// <summary>
        /// 获取推荐好友/群聊卡片
        /// </summary>
        /// <param name="group_id">群号，与user_id二选一</param>
        /// <param name="user_id">QQ号，与group_id二选一</param>
        /// <param name="phone_number">对方手机号，可选</param>
        /// <returns>卡片 JSON</returns>
        public async Task<NapCatAPIDataArkJson> GetArkSharePeer(long group_id, long user_id, long phone_number)
        {
            var req = new NapCatAPIRequest("ArkSharePeer");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("phone_number", phone_number.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            return resp.ParseData<NapCatAPIDataArkJson>();
        }

        /// <summary>
        /// 获取推荐群聊卡片
        /// </summary>
        /// <param name="group_id">群号，与user_id二选一</param>
        /// <returns>卡片 JSON</returns>
        public async Task<string> GetArkShareGroup(long group_id)
        {
            var req = new NapCatAPIRequest("ArkShareGroup");
            req.AddParam("group_id", group_id.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            return resp.ParseData<string>();
        }

        /// <summary>
        /// 设置在线状态
        /// </summary>
        /// <param name="status">状态，数字请查阅文档</param>
        /// <param name="ext_status">扩展状态，数字请查阅文档</param>
        /// <param name="battery_status">电池状态，数字请查阅文档</param>
        /// <returns>无响应</returns>
        public async Task SetOnlineStatus(uint status, uint ext_status, uint battery_status)
        {
            var req = new NapCatAPIRequest("set_online_status");
            req.AddParam("status", status.ToString());
            req.AddParam("ext_status", ext_status.ToString());
            req.AddParam("battery_status", battery_status.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取好友分组列表
        /// </summary>
        /// <returns>分组列表</returns>
        public async Task<List<NapCatAPIDataFriendCategory>> GetFriendsWithCategory()
        {
            var req = new NapCatAPIRequest("get_friends_with_category");

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            return resp.ParseData<List<NapCatAPIDataFriendCategory>>();
        }

        /// <summary>
        /// 设置头像
        /// </summary>
        /// <param name="file">文件路径或网络链接</param>
        /// <returns>无响应</returns>
        public async Task SetQQAvatar(string file)
        {
            var req = new NapCatAPIRequest("set_qq_avatar");
            req.AddParam("file", file);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 创建收藏
        /// </summary>
        /// <param name="rawData">内容</param>
        /// <param name="brief">标题</param>
        /// <returns>无响应</returns>
        public async Task<NapCatAPIDataCommon> CreateCollection(string rawData, string brief)
        {
            var req = new NapCatAPIRequest("create_collection");
            req.AddParam("rawData", rawData);
            req.AddParam("brief", brief);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            return resp.ParseData<NapCatAPIDataCommon>();
        }

        /// <summary>
        /// 处理好友请求
        /// </summary>
        /// <param name="flag">请求ID</param>
        /// <param name="approve">是否通过</param>
        /// <param name="remark">好友备注</param>
        /// <returns>无响应</returns>
        public async Task SetFriendAddRequest(string flag, bool approve, string remark)
        {
            var req = new NapCatAPIRequest("set_friend_add_request");
            req.AddParam("flag", flag);
            req.AddParam("approve", approve.ToString());
            req.AddParam("remark", remark);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 设置个性签名
        /// </summary>
        /// <param name="longNick">内容</param>
        /// <returns>标准响应</returns>
        public async Task<NapCatAPIDataCommon> SetSelfLongnick(string longNick)
        {
            var req = new NapCatAPIRequest("set_self_longnick");
            req.AddParam("longNick", longNick);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            return resp.ParseData<NapCatAPIDataCommon>();
        }

        /// <summary>
        /// 获取陌生人信息
        /// </summary>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>陌生人信息</returns>
        public async Task<NapCatAPIDataStrangerInfo> GetStrangerInfo(long user_id)
        {
            var req = new NapCatAPIRequest("get_stranger_info");
            req.AddParam("user_id", user_id.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataStrangerInfo>();
        }

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="no_cache">缓存</param>
        /// <returns>好友列表</returns>
        public async Task<List<NapCatAPIDataFriend>> GetFriendList(bool no_cache = false)
        {
            var req = new NapCatAPIRequest("get_friend_list");
            req.AddParam("no_cache", no_cache.ToString());

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<NapCatAPIDataFriend>>();
        }

        /// <summary>
        /// 获取点赞列表
        /// </summary>
        /// <param name="no_cache">缓存</param>
        /// <returns>好友列表</returns>
        public async Task<NapCatAPIDataProfileLike> GetProfileLike()
        {
            var req = new NapCatAPIRequest("get_profile_like");

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataProfileLike>();
        }

        /// <summary>
        /// 获取收藏表情
        /// </summary>
        /// <param name="count">计数</param>
        /// <returns>好友列表</returns>
        public async Task<List<string>> FetchCustomFace(int count)
        {
            var req = new NapCatAPIRequest("fetch_custom_face");
            req.AddParam("count", count.ToString());

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<string>>();
        }

        /// <summary>
        /// 上传私聊文件
        /// </summary>
        /// <param name="user_id">QQ 号</param>
        /// <param name="file">文件</param>
        /// <param name="name">文件名</param>
        /// <returns>无响应</returns>
        public async Task UploadPrivateFile(long user_id, string file, string name)
        {
            var req = new NapCatAPIRequest("upload_private_file");
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("file", file);
            req.AddParam("name", name);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 删除好友
        /// </summary>
        /// <param name="user_id">QQ 号</param>
        /// <param name="friend_id">QQ 号</param>
        /// <param name="temp_block">拉黑</param>
        /// <param name="temp_both_del">双向删除</param>
        /// <returns>无响应</returns>
        public async Task DeleteFriend(long user_id, long friend_id, bool temp_block, bool temp_both_del)
        {
            var req = new NapCatAPIRequest("delete_friend");
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("friend_id", friend_id.ToString());
            req.AddParam("temp_block", temp_block.ToString());
            req.AddParam("temp_both_del", temp_both_del.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取用户状态
        /// </summary>
        /// <param name="user_id">QQ 号</param>
        /// <returns>好友列表</returns>
        public async Task<NapCatAPIDataStatus> GetUserStatus(long user_id)
        {
            var req = new NapCatAPIRequest("nc_get_user_status");
            req.AddParam("user_id", user_id.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataStatus>();
        }

        // 未实现
        // 获取小程序卡片 get_mini_app_ark


        #endregion

        #region Message Operations

        /// <summary>
        /// 发送群聊消息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="message">消息内容</param>
        /// <param name="auto_escape">消息内容是否解析 CQ 码</param>
        /// <returns>发送的消息 ID</returns>
        public async Task<NapCatAPIDataMessageID> SendGroupMessage(long group_id, CqMessageChain message, bool auto_escape = false)
        {
            string msg = message.ToCqQuery();

            var req = new NapCatAPIRequest("send_group_msg");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("message", msg);
            req.AddParam("auto_escape", auto_escape.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null)
            {
                LogAPI?.Invoke($"API 调用失败或获取响应超时：send_group_msg", NapCatLogType.error);
                return null;
            }
            LogAPI?.Invoke($"发送消息至群 ({group_id}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", NapCatLogType.normal);
            return resp.ParseData<NapCatAPIDataMessageID>();
        }

        /// <summary>
        /// 发送私聊消息
        /// </summary>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="group_id">发起私聊的群号</param>
        /// <param name="message">消息内容</param>
        /// <param name="auto_escape">消息内容是否解析 CQ 码</param>
        /// <returns>发送的消息 ID</returns>
        public async Task<NapCatAPIDataMessageID> SendPrivateMessage(long user_id, CqMessageChain message, long group_id = -1, bool auto_escape = false)
        {
            string msg = message.ToCqQuery();

            var req = new NapCatAPIRequest("send_private_msg");
            req.AddParam("user_id", user_id.ToString());
            if (group_id != -1) req.AddParam("group_id", group_id.ToString());
            req.AddParam("message", msg);
            req.AddParam("auto_escape", auto_escape.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null)
            {
                LogAPI?.Invoke($"API 调用失败或获取响应超时：send_private_msg", NapCatLogType.error);
                return null;
            }
            if (group_id == -1)
            {
                LogAPI?.Invoke($"发送消息至 QQ ({user_id}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", NapCatLogType.normal);
            }
            else
            {
                LogAPI?.Invoke($"发送消息至 QQ ({user_id}) 通过群 ({group_id}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", NapCatLogType.normal);
            }
            return resp.ParseData<NapCatAPIDataMessageID>();
        }

        /// <summary>
        /// 设置消息已读
        /// </summary>
        /// <param name="group_id">群号，与user_id二选一</param>
        /// <param name="user_id">QQ号，与group_id二选一</param>
        /// <returns>无响应</returns>
        public async Task MarkMsgAsRead(long group_id, long user_id)
        {
            var req = new NapCatAPIRequest("mark_msg_as_read");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 设置群聊已读
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>无响应</returns>
        public async Task MarkGroupMsgAsRead(long group_id)
        {
            var req = new NapCatAPIRequest("mark_group_msg_as_read");
            req.AddParam("group_id", group_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 设置群聊已读
        /// </summary>
        /// <param name="user_id">QQ 号</param>
        /// <returns>无响应</returns>
        public async Task MarkPrivateMsgAsRead(long user_id)
        {
            var req = new NapCatAPIRequest("mark_private_msg_as_read");
            req.AddParam("user_id", user_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 设置所有消息已读
        /// </summary>
        /// <returns>无响应</returns>
        public async Task MarkAllMsgAsRead()
        {
            var req = new NapCatAPIRequest("mark_all_as_read");

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 撤回消息
        /// </summary>
        /// /// <param name="message_id">消息 ID</param>
        /// <returns>无响应</returns>
        public async Task RecallMessage(long message_id)
        {
            var req = new NapCatAPIRequest("delete_msg");
            req.AddParam("message_id", message_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取消息细节
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns>消息细节</returns>
        public async Task<NapCatAPIDataMessageDetail> GetMessage(int message_id)
        {
            var req = new NapCatAPIRequest("get_msg");
            req.AddParam("message_id", message_id.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            return resp.ParseData<NapCatAPIDataMessageDetail>();
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="file_id">图片缓存文件名称</param>
        /// <returns>图片信息</returns>
        public async Task<NapCatAPIDataFileDetail> GetImage(string file_id)
        {
            var req = new NapCatAPIRequest("get_image");
            req.AddParam("file_id", file_id);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataFileDetail>();
        }

        /// <summary>
        /// 获取语音
        /// </summary>
        /// <param name="file_id">语音文件名</param>
        /// <param name="out_format">输出格式</param>
        /// <returns>转换后的语音文件</returns>
        public async Task<NapCatAPIDataFileDetail> GetRecord(string file_id, NapCatAPIAudioType out_format)
        {
            var req = new NapCatAPIRequest("get_record");
            req.AddParam("file_id", file_id);
            req.AddParam("out_format", out_format.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataFileDetail>();
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="file_id">文件名</param>
        /// <returns>文件信息</returns>
        public async Task<NapCatAPIDataFileDetail> GetFile(string file_id)
        {
            var req = new NapCatAPIRequest("get_file");
            req.AddParam("file_id", file_id);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataFileDetail>();
        }

        /// <summary>
        /// 获取群历史消息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="message_seq">消息 ID，0 为最新</param>
        /// <param name="count">数量</param>
        /// <param name="reverseOrder">倒序</param>
        /// <returns>文件信息</returns>
        public async Task<List<NapCatAPIDataMessageDetail>> GetGroupMsgHistory(long group_id, long message_seq, int count, bool reverseOrder)
        {
            var req = new NapCatAPIRequest("get_group_msg_history");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("message_seq", message_seq.ToString());
            req.AddParam("count", count.ToString());
            req.AddParam("reverseOrder", reverseOrder.ToString());

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<NapCatAPIDataMessageDetail>>();
        }

        /// <summary>
        /// 贴表情
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <param name="emoji_id">表情 ID</param>
        /// <param name="set">未知设置项，可设置为 true</param>
        /// <returns>标准响应</returns>
        public async Task<NapCatAPIDataCommon> SetMsgEmojiLike(long message_id, CqFaceCode emoji_id, bool set)
        {
            var req = new NapCatAPIRequest("set_msg_emoji_like");
            req.AddParam("message_id", message_id.ToString());
            req.AddParam("emoji_id", ((int)emoji_id).ToString());
            req.AddParam("set", set.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            return resp.ParseData<NapCatAPIDataCommon>();
        }

        /// <summary>
        /// 获取好友历史消息
        /// </summary>
        /// <param name="user_id">QQ 号</param>
        /// <param name="message_seq">消息 ID，0 为最新</param>
        /// <param name="count">数量</param>
        /// <param name="reverseOrder">倒序</param>
        /// <returns>文件信息</returns>
        public async Task<List<NapCatAPIDataMessageDetail>> GetFriendMsgHistory(long user_id, long message_seq, int count, bool reverseOrder)
        {
            var req = new NapCatAPIRequest("get_friend_msg_history");
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("message_seq", message_seq.ToString());
            req.AddParam("count", count.ToString());
            req.AddParam("reverseOrder", reverseOrder.ToString());

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<NapCatAPIDataMessageDetail>>();
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="count">会话数量</param>
        /// <returns>文件信息</returns>
        public async Task<List<NapCatAPIDataContact>> GetRecentContact(int count)
        {
            var req = new NapCatAPIRequest("get_recent_contact");
            req.AddParam("count", count.ToString());

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<NapCatAPIDataContact>>();
        }

        /// <summary>
        /// 获取贴表情详情
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <param name="emojiId">表情 ID</param>
        /// <param name="emojiType">表情类型</param>
        /// <returns>表情详情</returns>
        public async Task<NapCatAPIDataEmoji> FetchEmojiLike(long message_id, string emojiId, string emojiType)
        {
            var req = new NapCatAPIRequest("fetch_emoji_like");
            req.AddParam("message_id", message_id.ToString());
            req.AddParam("emoji_id", emojiId);
            req.AddParam("set", emojiType);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            return resp.ParseData<NapCatAPIDataEmoji>();
        }

        /// <summary>
        /// 获取合并转发内容
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns>合并转发消息列表</returns>
        public async Task<NapCatAPIDataForwardMessageChain> GetForwardMessage(int message_id)
        {
            var req = new NapCatAPIRequest("get_forward_msg");
            req.AddParam("message_id", message_id.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataForwardMessageChain>();
        }

        /// <summary>
        /// 发送合并转发消息
        /// </summary>
        /// <param name="group_id">群号，二选一</param>
        /// <param name="user_id">QQ 号，二选一</param>
        /// <param name="message">合并消息链</param>
        /// <param name="news">未知</param>
        /// <param name="prompt">外显</param>
        /// <param name="summary">底下文本</param>
        /// <param name="source">内容</param>
        /// <returns>无响应</returns>
        public async Task SendGroupForwardMessage(long group_id, long user_id, CqMessageChain message, List<string> news, string prompt, string summary, string source)
        {
            string msg = message.ToCqQuery();

            var req = new NapCatAPIRequest("send_forward_msg");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());;
            req.AddParam("messages", message.ToJson());
            req.AddParam("news", news.ToString());
            req.AddParam("prompt", prompt);
            req.AddParam("summary", summary);
            req.AddParam("source", source);

            await SendAPIRequestNoRespAsync(req);

            LogAPI?.Invoke($"发送合并转发消息至群 ({group_id}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", NapCatLogType.normal);
            LogAPI?.Invoke($"合并转发消息 Json : {message.ToJson()}", NapCatLogType.normal);
        }

        #endregion

        #region Group Operations

        /// <summary>
        /// 群聊成员移除
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="reject_add_request">是否拒绝此人加群请求</param>
        /// <returns>无响应</returns>
        public async Task SetGroupKick(long group_id, long user_id, bool reject_add_request = false)
        {
            var req = new NapCatAPIRequest("set_group_kick");
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
            var req = new NapCatAPIRequest("set_group_ban");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("duration", duration.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取群系统消息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>群系统消息</returns>
        public async Task<NapCatAPIDataGroupSystemMessage> GetGroupSystemMessage(long group_id)
        {
            var req = new NapCatAPIRequest("get_group_system_msg");
            req.AddParam("group_id", group_id.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            if (resp.data.ContainsKey("InvitedRequest"))
            {
                foreach (JObject obj in (JArray)resp.data["InvitedRequest"])
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
            return resp.ParseData<NapCatAPIDataGroupSystemMessage>();
        }

        /// <summary>
        /// 获取精华消息列表
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>精华消息列表</returns>
        public async Task<List<NapCatAPIDataEssence>> GetEssenceMessageList(long group_id)
        {
            var req = new NapCatAPIRequest("get_essence_msg_list");
            req.AddParam("group_id", group_id.ToString());

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<NapCatAPIDataEssence>>();
        }

        /// <summary>
        /// 群聊全员禁言
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="enable">是否禁言</param>
        /// <returns>无响应</returns>
        public async Task SetGroupWholeBan(long group_id, bool enable = true)
        {
            var req = new NapCatAPIRequest("set_group_whole_ban");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("enable", enable.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 设置群头像（不稳定，登录后一段时间失效，谨慎使用）
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="file">图片文件名</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>标准响应</returns>
        public async Task<NapCatAPIDataCommon> SetGroupPortrait(long group_id, string file, bool no_cache = false)
        {
            var req = new NapCatAPIRequest("set_group_portrait");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("file", file);
            req.AddParam("cache", no_cache ? "0" : "1");

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            return resp.ParseData<NapCatAPIDataCommon>();
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
            var req = new NapCatAPIRequest("set_group_admin");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("enable", enable.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 设置精华消息
        /// </summary>
        /// <param name="message_id">消息 ID</param>
        /// <returns>无响应</returns>
        public async Task SetEssenceMessage(long message_id)
        {
            var req = new NapCatAPIRequest("set_essence_msg");
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
            var req = new NapCatAPIRequest("delete_essence_msg");
            req.AddParam("message_id", message_id.ToString());

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
            var req = new NapCatAPIRequest("set_group_card");
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
            var req = new NapCatAPIRequest("set_group_name");
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
            var req = new NapCatAPIRequest("set_group_leave");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("is_dismiss", is_dismiss.ToString());

            await SendAPIRequestNoRespAsync(req);
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
            var req = new NapCatAPIRequest("_send_group_notice");
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
        public async Task<List<NapCatAPIDataGroupNotice>> GetGroupNotice(long group_id)
        {
            var req = new NapCatAPIRequest("_get_group_notice");
            req.AddParam("group_id", group_id.ToString());

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<NapCatAPIDataGroupNotice>>();
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
            var req = new NapCatAPIRequest("set_group_special_title");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("special_title", special_title);
            req.AddParam("duration", duration.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 上传群文件
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="file">本地文件路径（网络文件请先下载到本地）</param>
        /// <param name="name">文件名称</param>
        /// <param name="folder_id">父目录 ID，默认根目录</param>
        /// <returns>无响应</returns>
        public async Task UploadGroupFile(long group_id, string file, string name, string folder_id = "")
        {
            var req = new NapCatAPIRequest("upload_group_file");
            req.AddParam("uesr_id", group_id.ToString());
            req.AddParam("file", file);
            req.AddParam("name", name);
            req.AddParam("folder_id", folder_id);

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
        public async Task SetGroupAddRequest(string flag, NapCatRequestGroupSubType sub_type, bool approve = true, string reason = "")
        {
            var req = new NapCatAPIRequest("set_group_add_request");
            req.AddParam("flag", flag);
            req.AddParam("sub_type", sub_type.ToString());
            req.AddParam("approve", approve.ToString());
            req.AddParam("reason", reason);

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取群信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>群信息</returns>
        public async Task<NapCatAPIDataGroupInfo> GetGroupInfo(long group_id, bool no_cache = false)
        {
            var req = new NapCatAPIRequest("get_group_info");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("no_cache", no_cache.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGroupInfo>();
        }

        /// <summary>
        /// 获取群信息EX
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>群信息</returns>
        public async Task<NapCatAPIDataGroupInfoExWrapper> GetGroupInfoEx(long group_id, bool no_cache = false)
        {
            var req = new NapCatAPIRequest("get_group_info_ex");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("no_cache", no_cache.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGroupInfoExWrapper>();
        }

        /// <summary>
        /// 创建群文件文件夹
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="name">文件夹名称</param>
        /// <returns>无响应</returns>
        public async Task CreateGroupFileFolder(long group_id, string name)
        {
            var req = new NapCatAPIRequest("create_group_file_folder");
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
            var req = new NapCatAPIRequest("delete_group_folder");
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
            var req = new NapCatAPIRequest("delete_group_file");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("file_id", file_id);
            req.AddParam("busid", busid.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        /// <summary>
        /// 获取群文件系统信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>群文件系统信息</returns>
        public async Task<NapCatAPIDataGroupFileSystemInfo> GetGroupFileSystemInfo(long group_id)
        {
            var req = new NapCatAPIRequest("get_group_file_system_info");
            req.AddParam("group_id", group_id.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGroupFileSystemInfo>();
        }

        /// <summary>
        /// 获取群根目录文件列表
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>文件结构</returns>
        public async Task<NapCatAPIDataGroupFileSystemDetail> GetGroupRootFiles(long group_id)
        {
            var req = new NapCatAPIRequest("get_group_root_files");
            req.AddParam("group_id", group_id.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGroupFileSystemDetail>();
        }

        /// <summary>
        /// 获取群子目录文件列表
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="folder_id">文件夹 ID</param>
        /// <returns>文件结构</returns>
        public async Task<NapCatAPIDataGroupFileSystemDetail> GetGroupFilesByFolder(long group_id, string folder_id)
        {
            var req = new NapCatAPIRequest("get_group_files_by_folder");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("folder_id", folder_id);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGroupFileSystemDetail>();
        }

        /// <summary>
        /// 获取群文件资源链接
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="file_id">文件 ID</param>
        /// <param name="busid">文件类型</param>
        /// <returns>资源链接</returns>
        public async Task<NapCatAPIDataGroupFileUrl> GetGroupFileUrl(long group_id, string file_id, int busid)
        {
            var req = new NapCatAPIRequest("get_group_file_url");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("file_id", file_id);
            req.AddParam("busid", busid.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGroupFileUrl>();
        }

        /// <summary>
        /// 获取群列表
        /// </summary>
        /// <returns>群信息列表</returns>
        public async Task<List<NapCatAPIDataGroupInfo>> GetGroupList()
        {
            var req = new NapCatAPIRequest("get_group_list");

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<NapCatAPIDataGroupInfo>>();
        }

        /// <summary>
        /// 获取群成员信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="user_id">目标 QQ 号</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>群成员信息</returns>
        public async Task<NapCatAPIDataGroupMember> GetGroupMemberInfo(long group_id, long user_id, bool no_cache = false)
        {
            var req = new NapCatAPIRequest("get_group_member_info");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("user_id", user_id.ToString());
            req.AddParam("no_cache", no_cache.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGroupMember>();
        }

        /// <summary>
        /// 获取群成员列表
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>群成员列表</returns>
        public async Task<List<NapCatAPIDataGroupMember>> GetGroupMemberList(long group_id, bool no_cache = false)
        {
            var req = new NapCatAPIRequest("get_group_member_list");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("no_cache", no_cache.ToString());

            //APILogger?.Invoke("testAPI", NapCatLogType.normal);
            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            string msg = resp.message;
            //APILogger?.Invoke($"获取群成员列表 ({msg}) : {(msg.Length > 128 ? msg.Substring(0, 128) : msg)}", NapCatLogType.normal);
            return resp.ParseData<List<NapCatAPIDataGroupMember>>();
        }

        /// <summary>
        /// 获取群荣誉信息
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <param name="type">获取的群荣誉类型</param>
        /// <returns>群荣誉信息</returns>
        public async Task<NapCatAPIDataGroupHonor> GetGroupHonorInfo(long group_id, NapCatAPIGroupHonorType type)
        {
            var req = new NapCatAPIRequest("get_group_honor_info");
            req.AddParam("group_id", group_id.ToString());
            req.AddParam("type", type.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGroupHonor>();
        }

        /// <summary>
        /// 获取剩余@全体成员次数
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>剩余次数</returns>
        public async Task<NapCatAPIDataGroupAtAllRemain> GetGroupAtAllRemain(long group_id)
        {
            var req = new NapCatAPIRequest("get_group_at_all_remain");
            req.AddParam("group_id", group_id.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataGroupAtAllRemain>();
        }

        /// <summary>
        /// 群聊打卡
        /// </summary>
        /// <param name="group_id">群号</param>
        /// <returns>无响应</returns>
        public async Task SetGroupSign(long group_id)
        {
            var req = new NapCatAPIRequest("set_group_sign");
            req.AddParam("group_id", group_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }

        // 部分 API 仍未支持

        #endregion

        /// <summary>
        /// 获取登录号信息
        /// </summary>
        /// <returns>登录号信息</returns>
        public async Task<NapCatAPIDataLoginInfo> GetLoginInfo()
        {
            var req = new NapCatAPIRequest("get_login_info");

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataLoginInfo>();
        }

        /// <summary>
        /// 获取企点账号信息
        /// </summary>
        /// <returns>企点账号信息</returns>
        public async Task<NapCatAPIDataQidianAccountInfo> GetQidianAccountInfo()
        {
            var req = new NapCatAPIRequest("qidian_get_account_info");

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataQidianAccountInfo>();
        }

        /// <summary>
        /// 获取单向好友列表
        /// </summary>
        /// <returns>单向好友列表</returns>
        public async Task<List<NapCatAPIDataFriendInfo>> GetUnidirectionalFriendList()
        {
            var req = new NapCatAPIRequest("get_unidirectional_friend_list");

            NapCatAPIRespondArray resp = await SendAPIRequestArrayAsync(req);
            if (resp == null) return null;
            return resp.ParseData<List<NapCatAPIDataFriendInfo>>();
        }

        /// <summary>
        /// 获取 Cookies（未被支持）
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>Cookies</returns>
        public async Task<NapCatAPIDataCookies> GetCookies(string domain = "")
        {
            var req = new NapCatAPIRequest("get_cookies");
            req.AddParam("domain", domain);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataCookies>();
        }

        /// <summary>
        /// 获取 CSRF（未被支持）
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>CSRF Token</returns>
        public async Task<NapCatAPIDataCSRF> GetCSRFToken(string domain = "")
        {
            var req = new NapCatAPIRequest("get_csrf_token");
            req.AddParam("domain", domain);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataCSRF>();
        }

        /// <summary>
        /// 获取 QQ 相关接口凭证（未被支持）
        /// </summary>
        /// <param name="domain">域名</param>
        /// <returns>Cookies & CSRF Token</returns>
        public async Task<NapCatAPIDataCredentials> GetCredentials(string domain = "")
        {
            var req = new NapCatAPIRequest("get_credentials");
            req.AddParam("domain", domain);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataCredentials>();
        }

        /// <summary>
        /// 检查是否可以发送图片
        /// </summary>
        /// <returns>是或否</returns>
        public async Task<NapCatAPIDataPermission> CanSendImage()
        {
            var req = new NapCatAPIRequest("can_send_image");

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataPermission>();
        }

        /// <summary>
        /// 检查是否可以发送语音
        /// </summary>
        /// <returns>是或否</returns>
        public async Task<NapCatAPIDataPermission> CanSendRecord()
        {
            var req = new NapCatAPIRequest("can_send_record");

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataPermission>();
        }

        /// <summary>
        /// 获取中文分词（隐藏API, 谨慎使用）
        /// </summary>
        /// <param name="content">内容</param>
        /// <returns>词组</returns>
        public async Task<NapCatAPIDataCNWordSlices> GetCNWordSlices(string content)
        {
            var req = new NapCatAPIRequest(".get_word_slices");
            req.AddParam("content", content);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataCNWordSlices>();
        }

        /// <summary>
        /// 图片 OCR（未经测试）
        /// </summary>
        /// <param name="image">图片 ID</param>
        /// <returns>OCR 结果</returns>
        public async Task<NapCatAPIDataOCR> ImageOCR(string image)
        {
            var req = new NapCatAPIRequest("ocr_image");
            req.AddParam("image", image);

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataOCR>();
        }

        /// <summary>
        /// 获取当前账号在线客户端列表
        /// </summary>
        /// <param name="no_cache">不使用缓存，响应速度可能降低但更新会更及时</param>
        /// <returns>在线列表</returns>
        public async Task<NapCatAPIDataClient> GetOnlineClients(bool no_cache = false)
        {
            var req = new NapCatAPIRequest("get_online_clients");
            req.AddParam("no_cache", no_cache.ToString());

            NapCatAPIRespondObject resp = await SendAPIRequestObjectAsync(req);
            if (resp == null) return null;
            return resp.ParseData<NapCatAPIDataClient>();
        }

        /// <summary>
        /// 删除单向好友
        /// </summary>
        /// <param name="user_id">单向好友 QQ 号</param>
        /// <returns>无响应</returns>
        public async Task DeleteUniDirectionalFriend(long user_id)
        {
            var req = new NapCatAPIRequest("delete_unidirectional_friend");
            req.AddParam("user_id", user_id.ToString());

            await SendAPIRequestNoRespAsync(req);
        }
    }
}
