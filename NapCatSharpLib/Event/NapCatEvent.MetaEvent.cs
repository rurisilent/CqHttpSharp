using System;
using System.Collections.Generic;
using System.Text;
using NapCatSharpLib.Data;

namespace NapCatSharpLib.Event
{
    public delegate void EvtMetaLifeCycle(NapCatMetaEventLifeCycle data);
    public delegate void EvtMetaHeartbeat(NapCatMetaEventHeartbeat data);
}
