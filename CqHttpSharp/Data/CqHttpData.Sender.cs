using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Enum;

namespace CqHttpSharp.Data
{
    /// <summary>
    /// 消息发送者的数据，尽最大努力提供，不保证每一项都不为null
    /// </summary>
    public class CqHttpMessageSender
    {
        public long user_id;
        public string nickname;
        public CqHttpSenderSexType sex;
        public int age;

        //以下数据仅在群聊提供
        public string card; //群名片
        public string area;
        public string level;
        public CqHttpSenderRoleType role;
        public string title; //专属头衔
    }
}
