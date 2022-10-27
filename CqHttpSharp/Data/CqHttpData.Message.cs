using System;
using System.Collections.Generic;
using System.Text;

namespace CqHttpSharp.Data
{
    public class CqHttpMessagePrivate : CqHttpDataMessage
    {
        public int temp_source;
    }

    public class CqHttpMessageGroup : CqHttpDataMessage
    {
        public long group_id;
        public CqHttpAnonymous anonymous;
    }
}
