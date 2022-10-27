using System;
using System.Collections.Generic;
using System.Text;

namespace CqHttpSharp.Enum
{
    public enum CqHttpMessageSubType
    {
        friend, //好友消息
        group, //临时消息
        group_self, //自身发送消息
        normal, //正常群聊消息
        anonymous, //匿名消息
        notice //通知消息
    }

    public enum CqHttpMessageTempSource
    {
        group = 0, //群聊
        qq_inquiry = 1, //QQ咨询
        search = 2, //查找
        qq_movie = 3, //QQ电影
        hot_chat = 4, //热聊
        verification = 6, //验证消息
        multiple_chat = 7, //多人聊天
        dating = 8, //约会
        contact = 9, //通讯录
    }
}
