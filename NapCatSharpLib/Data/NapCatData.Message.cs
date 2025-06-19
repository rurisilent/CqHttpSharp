using System;
using System.Collections.Generic;
using System.Text;

namespace NapCatSharpLib.Data
{
    public class NapCatMessagePrivate : NapCatDataMessage
    {
        public int temp_source;
    }

    public class NapCatMessageGroup : NapCatDataMessage
    {
        public long group_id;
        public NapCatAnonymous anonymous;
    }
}
