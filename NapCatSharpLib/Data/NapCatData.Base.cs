using System;
using System.Collections.Generic;
using System.Text;
using NapCatSharpLib.Enum;

namespace NapCatSharpLib.Data
{
    public class NapCatDataBase
    {
        public long time;
        public long self_id;
        public NapCatPostType post_type;
    }

    public class NapCatDataMessage : NapCatDataBase
    {
        public string sub_type;
        public int message_id;
        public long user_id;
        public string message;
        public string raw_message;
        public int font;
        public NapCatMessageSender sender;
    }

    public class NapCatDataRequest : NapCatDataBase
    {
        public NapCatRequestType request_type;
    }

    public class NapCatDataNotice : NapCatDataBase
    {
        public NapCatNoticeType notice_type;
    }

    public class NapCatDataMetaEvent : NapCatDataBase
    {
        public NapCatPostMetaEventType meta_event_type;
    }

    public class NapCatMessageChain
    {
        public List<NapCatMessageBlock> messages;
    }

    public class NapCatMessageBlock
    {
        public string type;
        public Dictionary<string, string> data;
    }
}
