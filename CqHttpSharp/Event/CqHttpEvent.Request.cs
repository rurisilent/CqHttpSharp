using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Data;

namespace CqHttpSharp.Event
{
    public delegate void EvtRequestFriend(CqHttpRequestFriend data);
    public delegate void EvtRequestGroup(CqHttpRequestGroup data);
}
