using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Enum;

namespace CqHttpSharp.Data
{
    public class CqHttpRequestFriend : CqHttpDataRequest
    {
        public long user_id;
        public string comment;
        public string flag;
    }

    public class CqHttpRequestGroup : CqHttpDataRequest
    {
        public CqHttpRequestGroupSubType sub_type;
        public long group_id;
        public long user_id;
        public string comment;
        public string flag;
    }
}
