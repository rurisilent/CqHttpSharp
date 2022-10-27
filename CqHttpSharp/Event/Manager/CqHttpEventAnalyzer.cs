using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Data;
using CqHttpSharp.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CqHttpSharp.Event.Manager
{
    public class CqHttpEventAnalyzer
    {
        CqHttpEventManager evtManager;

        public CqHttpEventAnalyzer(CqHttpEventManager _evtManager)
        {
            evtManager = _evtManager;
        }

        public void AnalyzeEvent(string data)
        {
            JObject jsonData = JObject.Parse(data);

            if (jsonData.ContainsKey("post_type"))
            {
                if (System.Enum.TryParse<CqHttpPostType>(jsonData["post_type"].ToString(), out var postType))
                {
                    if (postType == CqHttpPostType.message)
                    {
                        if (jsonData.ContainsKey("message_type"))
                        {
                            var messageType = jsonData["message_type"].ToString();
                            if (messageType == "private")
                            {
                                var ret = JsonConvert.DeserializeObject<CqHttpMessagePrivate>(data);
                                evtManager.OnEventPrivateCommon?.Invoke(ret);
                            }
                            else
                            {
                                var ret = JsonConvert.DeserializeObject<CqHttpMessageGroup>(data);
                                evtManager.OnEventMessageGroup?.Invoke(ret);
                            }
                        }
                    }
                    else if (postType == CqHttpPostType.notice)
                    {
                        if (jsonData.ContainsKey("notice_type"))
                        {
                            if (System.Enum.TryParse<CqHttpNoticeType>(jsonData["notice_type"].ToString(), out var noticeType))
                            {
                                if (noticeType == CqHttpNoticeType.group_upload)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupFileUpload>(data);
                                    evtManager.OnEventNoticeGroupFileUpload?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.group_admin)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupAdminChange>(data);
                                    evtManager.OnEventNoticeGroupAdminChange?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.group_decrease)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupDecrease>(data);
                                    evtManager.OnEventNoticeGroupDecrease?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.group_increase)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupIncrease>(data);
                                    evtManager.OnEventNoticeGroupIncrease?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.group_ban)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupBan>(data);
                                    evtManager.OnEventNoticeGroupBan?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.friend_add)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeAddFriend>(data);
                                    evtManager.OnEventNoticeAddFriend?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.group_recall)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupRecall>(data);
                                    evtManager.OnEventNoticeGroupRecall?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.friend_recall)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticePrivateRecall>(data);
                                    evtManager.OnEventNoticePrivateRecall?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.notify)
                                {
                                    if (jsonData.ContainsKey("sub_type"))
                                    {
                                        if (System.Enum.TryParse<CqHttpNoticeNotifySubType>(jsonData["sub_type"].ToString(), out var subType))
                                        {
                                            if (subType == CqHttpNoticeNotifySubType.poke)
                                            {
                                                if (jsonData.ContainsKey("group_id"))
                                                {
                                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupPoke>(data);
                                                    evtManager.OnEventNoticeGroupPoke?.Invoke(ret);
                                                }
                                                else
                                                {
                                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticePrivatePoke>(data);
                                                    evtManager.OnEventNoticePrivatePoke?.Invoke(ret);
                                                }
                                            }
                                            else if (subType == CqHttpNoticeNotifySubType.lucky_king)
                                            {
                                                var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupLuckyKing>(data);
                                                evtManager.OnEventNoticeGroupLuckyKing?.Invoke(ret);
                                            }
                                            else if (subType == CqHttpNoticeNotifySubType.honor)
                                            {
                                                var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupHonor>(data);
                                                evtManager.OnEventNoticeGroupHonor?.Invoke(ret);
                                            }
                                        }
                                    }
                                }
                                else if (noticeType == CqHttpNoticeType.group_card)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupCardChange>(data);
                                    evtManager.OnEventNoticeGroupCardChange?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.offline_file)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeOfflineFileReceive>(data);
                                    evtManager.OnEventNoticeOfflineFileReceive?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.client_status)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeClientStatusChange>(data);
                                    evtManager.OnEventNoticeClientStatusChange?.Invoke(ret);
                                }
                                else if (noticeType == CqHttpNoticeType.essence)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpNoticeGroupEssence>(data);
                                    evtManager.OnEventNoticeGroupEssence?.Invoke(ret);
                                }
                            }
                        }
                    }
                    else if (postType == CqHttpPostType.request)
                    {
                        if (jsonData.ContainsKey("request_type"))
                        {
                            if (System.Enum.TryParse<CqHttpRequestType>(jsonData["request_type"].ToString(), out var requestType))
                            {
                                if (requestType == CqHttpRequestType.friend)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpRequestFriend>(data);
                                    evtManager.OnEventRequestFriend?.Invoke(ret);
                                }
                                else if (requestType == CqHttpRequestType.group)
                                {
                                    var ret = JsonConvert.DeserializeObject<CqHttpRequestGroup>(data);
                                    evtManager.OnEventRequestGroup?.Invoke(ret);
                                }

                            }
                        }
                    }
                    else if (postType == CqHttpPostType.meta_event)
                    {

                    }
                }
            }
        }
    }
}
