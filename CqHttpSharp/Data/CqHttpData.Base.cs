using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Enum;

namespace CqHttpSharp.Data
{
    public class CqHttpDataBase
    {
        public long time;
        public long self_id;
        public CqHttpPostType post_type;
    }

    public class CqHttpDataMessage : CqHttpDataBase
    {
        public string sub_type;
        public int message_id;
        public long user_id;
        public string message;
        public string raw_message;
        public int font;
        public CqHttpMessageSender sender;
    }

    public class CqHttpDataRequest : CqHttpDataBase
    {
        public CqHttpRequestType request_type;
    }

    public class CqHttpDataNotice : CqHttpDataBase
    {
        public CqHttpNoticeType notice_type;
    }

    public class CqHttpDataMetaEvent : CqHttpDataBase
    {
        public CqHttpPostMetaEventType meta_event_type;
    }

    public class CqHttpMessageChain
    {
        public List<CqHttpMessageBlock> messages;
    }

    public class CqHttpMessageBlock
    {
        public string type;
        public Dictionary<string, string> data;
    }
}
