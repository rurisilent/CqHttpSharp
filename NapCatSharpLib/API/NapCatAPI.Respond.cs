using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NapCatSharpLib.API
{
    public class NapCatAPIRespondBase
    {
        public string status;
        public int retcode;
        public string message;
        public string wording;
        public string echo;
    }

    public class NapCatAPIRespondObject : NapCatAPIRespondBase
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

    public class NapCatAPIRespondArray : NapCatAPIRespondBase
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
