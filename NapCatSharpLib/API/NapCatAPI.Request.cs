using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NapCatSharpLib.API
{
    public class NapCatAPIRequest
    {
        public string action;
        public Dictionary<string, string> parameters;
        public string echo;

        public NapCatAPIRequest(string _action)
        {
            action = _action;

            parameters = new Dictionary<string, string>();
        }

        public bool AddParam(string _key, string _val)
        {
            if (parameters.ContainsKey(_key)) return false;

            parameters.Add(_key, _val);
            return true;
        }

        public void ClearParam()
        {
            parameters = new Dictionary<string, string>();
        }

        public string Serialize()
        {
            JObject data = new JObject();
            data["action"] = action;
            data["params"] = new JObject();
            foreach (var p in parameters)
            {
                data["params"][p.Key] = p.Value;
            }
            if (echo != null)
                data["echo"] = echo;

            return data.ToString();
        }
    }
}
