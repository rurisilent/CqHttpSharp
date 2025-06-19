using System;
using System.Collections.Generic;
using System.Text;
using NapCatSharpLib.Enum;

namespace NapCatSharpLib.Data
{
    public class NapCatRequestFriend : NapCatDataRequest
    {
        public long user_id;
        public string comment;
        public string flag;
    }

    public class NapCatRequestGroup : NapCatDataRequest
    {
        public NapCatRequestGroupSubType sub_type;
        public long group_id;
        public long user_id;
        public string comment;
        public string flag;
    }
}
