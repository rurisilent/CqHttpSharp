using System;
using System.Collections.Generic;
using System.Text;

namespace CqHttpSharp.Log
{
    public enum CqHttpLogType
    {
        normal,
        warning,
        error
    }

    public delegate void EvtCqHttpLog(string content, CqHttpLogType type);
}
