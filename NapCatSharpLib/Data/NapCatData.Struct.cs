using System;
using System.Collections.Generic;
using System.Text;

namespace NapCatSharpLib.Data
{
    public class NapCatFileInfo
    {
        public string id;
        public string name;
        public long size;
        public long busid;
    }

    public class NapCatFileData
    {
        public string name;
        public long size;
        public string url;
    }

    public class NapCatDeviceInfo
    {
        public long app_id;
        public string device_name;
        public string device_kind;
    }
}
