using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Data;

namespace CqHttpSharp.Event
{
    public delegate void EvtMetaLifeCycle(CqHttpMetaEventLifeCycle data);
    public delegate void EvtMetaHeartbeat(CqHttpMetaEventHeartbeat data);
}
