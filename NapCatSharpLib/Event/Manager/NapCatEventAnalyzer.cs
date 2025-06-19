using System;
using System.Collections.Generic;
using System.Text;
using NapCatSharpLib.Data;
using NapCatSharpLib.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NapCatSharpLib.Event.Manager
{
    public class NapCatEventAnalyzer
    {
        NapCatEventManager evtManager;

        public NapCatEventAnalyzer(NapCatEventManager _evtManager)
        {
            evtManager = _evtManager;
        }

        public void AnalyzeEvent(string data)
        {
            JObject jsonData = JObject.Parse(data);

            if (jsonData.ContainsKey("post_type"))
            {
                if (System.Enum.TryParse<NapCatPostType>(jsonData["post_type"].ToString(), out var postType))
                {
                    if (postType == NapCatPostType.message)
                    {
                        if (jsonData.ContainsKey("message_type"))
                        {
                            var messageType = jsonData["message_type"].ToString();
                            if (messageType == "private")
                            {
                                var ret = JsonConvert.DeserializeObject<NapCatMessagePrivate>(data);
                                evtManager.EventLogger?.Invoke($"收到来自{ret.sender.nickname} ({ret.sender.user_id}) 的消息 : {(ret.message.Length > 50 ? ret.message.Substring(0, 50) + "..." : ret.message)}", Log.NapCatLogType.normal);
                                evtManager.OnEventMessagePrivate?.Invoke(ret);
                            }
                            else
                            {
                                var ret = JsonConvert.DeserializeObject<NapCatMessageGroup>(data);
                                evtManager.EventLogger?.Invoke($"收到来自群 ({ret.group_id}) 成员{ret.sender.nickname} ({ret.sender.user_id}) 的消息 : {(ret.message.Length > 50 ? ret.message.Substring(0, 50) + "..." : ret.message)}", Log.NapCatLogType.normal);
                                evtManager.OnEventMessageGroup?.Invoke(ret);
                            }
                        }
                    }
                    else if (postType == NapCatPostType.notice)
                    {
                        if (jsonData.ContainsKey("notice_type"))
                        {
                            if (System.Enum.TryParse<NapCatNoticeType>(jsonData["notice_type"].ToString(), out var noticeType))
                            {
                                if (noticeType == NapCatNoticeType.group_upload)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupFileUpload>(data);
                                    evtManager.EventLogger?.Invoke($"通知：群 ({ret.group_id}) 成员 ({ret.user_id}) 上传群文件 : {ret.file.name}", Log.NapCatLogType.normal);
                                    evtManager.OnEventNoticeGroupFileUpload?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.group_admin)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupAdminChange>(data);
                                    evtManager.OnEventNoticeGroupAdminChange?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.group_decrease)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupDecrease>(data);
                                    evtManager.OnEventNoticeGroupDecrease?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.group_increase)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupIncrease>(data);
                                    evtManager.OnEventNoticeGroupIncrease?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.group_ban)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupBan>(data);
                                    evtManager.OnEventNoticeGroupBan?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.friend_add)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeAddFriend>(data);
                                    evtManager.OnEventNoticeAddFriend?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.group_recall)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupRecall>(data);
                                    evtManager.OnEventNoticeGroupRecall?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.friend_recall)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticePrivateRecall>(data);
                                    evtManager.OnEventNoticePrivateRecall?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.notify)
                                {
                                    if (jsonData.ContainsKey("sub_type"))
                                    {
                                        if (System.Enum.TryParse<NapCatNoticeNotifySubType>(jsonData["sub_type"].ToString(), out var subType))
                                        {
                                            if (subType == NapCatNoticeNotifySubType.poke)
                                            {
                                                if (jsonData.ContainsKey("group_id"))
                                                {
                                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupPoke>(data);
                                                    evtManager.OnEventNoticeGroupPoke?.Invoke(ret);
                                                }
                                                else
                                                {
                                                    var ret = JsonConvert.DeserializeObject<NapCatNoticePrivatePoke>(data);
                                                    evtManager.OnEventNoticePrivatePoke?.Invoke(ret);
                                                }
                                            }
                                            else if (subType == NapCatNoticeNotifySubType.lucky_king)
                                            {
                                                var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupLuckyKing>(data);
                                                evtManager.OnEventNoticeGroupLuckyKing?.Invoke(ret);
                                            }
                                            else if (subType == NapCatNoticeNotifySubType.honor)
                                            {
                                                var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupHonor>(data);
                                                evtManager.OnEventNoticeGroupHonor?.Invoke(ret);
                                            }
                                        }
                                    }
                                }
                                else if (noticeType == NapCatNoticeType.group_card)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupCardChange>(data);
                                    evtManager.OnEventNoticeGroupCardChange?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.offline_file)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeOfflineFileReceive>(data);
                                    evtManager.OnEventNoticeOfflineFileReceive?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.client_status)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeClientStatusChange>(data);
                                    evtManager.OnEventNoticeClientStatusChange?.Invoke(ret);
                                }
                                else if (noticeType == NapCatNoticeType.essence)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatNoticeGroupEssence>(data);
                                    evtManager.OnEventNoticeGroupEssence?.Invoke(ret);
                                }
                            }
                        }
                    }
                    else if (postType == NapCatPostType.request)
                    {
                        if (jsonData.ContainsKey("request_type"))
                        {
                            if (System.Enum.TryParse<NapCatRequestType>(jsonData["request_type"].ToString(), out var requestType))
                            {
                                if (requestType == NapCatRequestType.friend)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatRequestFriend>(data);
                                    evtManager.OnEventRequestFriend?.Invoke(ret);
                                }
                                else if (requestType == NapCatRequestType.group)
                                {
                                    var ret = JsonConvert.DeserializeObject<NapCatRequestGroup>(data);
                                    evtManager.OnEventRequestGroup?.Invoke(ret);
                                }

                            }
                        }
                    }
                    else if (postType == NapCatPostType.meta_event)
                    {

                    }
                }
            }
        }
    }
}
