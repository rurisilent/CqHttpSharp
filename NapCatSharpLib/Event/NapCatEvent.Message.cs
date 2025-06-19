using System;
using System.Collections.Generic;
using System.Text;
using NapCatSharpLib.Data;

namespace NapCatSharpLib.Event
{
    public delegate void EvtMessagePrivate(NapCatMessagePrivate data);
    public delegate void EvtMessageGroup(NapCatMessageGroup data);
}
