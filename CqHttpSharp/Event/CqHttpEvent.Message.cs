using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Data;

namespace CqHttpSharp.Event
{
    public delegate void EvtMessagePrivate(CqHttpMessagePrivate data);
    public delegate void EvtMessageGroup(CqHttpMessageGroup data);
}
