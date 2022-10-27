using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CqHttpSharp.API
{
    public class CqHttpAPIRespondBase
    {
        public string status;
        public int retcode;
        public string msg;
        public string wording;
        public string echo;
    }

    public class CqHttpAPIRespondObject : CqHttpAPIRespondBase
    {
        public JObject data;

        public string SerializeData()
        {
            return data.ToString();
        }

        public T ParseData<T>()
        {
            return JsonConvert.DeserializeObject<T>(SerializeData());
        }
    }

    public class CqHttpAPIRespondArray : CqHttpAPIRespondBase
    {
        public JArray data;

        public string SerializeData()
        {
            return data.ToString();
        }

        public T ParseData<T>()
        {
            return JsonConvert.DeserializeObject<T>(SerializeData());
        }
    }
}
