using System;
using System.Collections.Generic;
using System.Text;

namespace CqHttpSharp.Data
{
    public class CqHttpFileInfo
    {
        public string id;
        public string name;
        public long size;
        public long busid;
    }

    public class CqHttpFileData
    {
        public string name;
        public long size;
        public string url;
    }

    public class CqHttpDeviceInfo
    {
        public long app_id;
        public string device_name;
        public string device_kind;
    }
}
