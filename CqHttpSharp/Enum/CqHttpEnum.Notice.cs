using System;
using System.Collections.Generic;
using System.Text;

namespace CqHttpSharp.Enum
{
    public enum CqHttpNoticeType
    {
        group_upload, //群文件上传
        group_admin, //群管理员变更
        group_decrease, //群成员减少
        group_increase, //群成员增加
        group_ban, //群成员禁言
        friend_add, //好友添加
        group_recall, //群消息撤回
        friend_recall, //好友消息撤回
        group_card, //群名片变更
        offline_file, //离线文件上传
        client_status, //客户端状态变更
        essence, //精华消息
        notify //系统通知
    }

    public enum CqHttpNoticeNotifySubType
    {
        honor, //群荣誉变更
        poke, //戳一戳
        lucky_king //群红包幸运王
    }
    public enum CqHttpNoticeGroupAdminSubType
    {
        set,
        unset
    }

    public enum CqHttpNoticeGroupDecreaseSubType
    {
        leave, //退群
        kick, //踢
        kick_me //Bot被踢
    }

    public enum CqHttpNoticeGroupIncreaseSubType
    {
        approve, //主动进群
        invite //邀请进群
    }

    public enum CqHttpNoticeGroupBanSubType
    {
        ban, //禁言
        lift_ban //解除禁言
    }

    public enum CqHttpNoticeGroupHonorSubType
    {
        talkative, //龙王
        performer, //群聊之火
        emotion //快乐源泉
    }

    public enum CqHttpNoticeGroupEssenceSubType
    { 
        add,
        delete
    }
}
