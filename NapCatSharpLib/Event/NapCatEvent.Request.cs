using System;
using System.Collections.Generic;
using System.Text;
using NapCatSharpLib.Data;

namespace NapCatSharpLib.Event
{
    public delegate void EvtRequestFriend(NapCatRequestFriend data);
    public delegate void EvtRequestGroup(NapCatRequestGroup data);
}
