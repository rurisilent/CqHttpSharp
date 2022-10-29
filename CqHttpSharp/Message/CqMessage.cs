using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Utility;

namespace CqHttpSharp.Message
{
    public class CqMessage
    {
        public CqCode type { get; private set; }
        public Dictionary<string, string> data { get; private set; }

        public CqMessage(CqCode _type)
        {
            type = _type;
            data = new Dictionary<string, string>();
        }

        public bool AddData(string _key, string _val)
        {
            if (data.ContainsKey(_key)) return false;

            data.Add(_key, _val);
            return true;
        }

        public void ClearData()
        {
            data = new Dictionary<string, string>();
        }

        public string ToCqCode()
        {
            StringBuilder ret = new StringBuilder();
            if (type == CqCode.text)
            {
                if (data.TryGetValue("text", out var text))
                {
                    return text;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                ret.Append($"[CQ:{type},");
                int count = data.Count;
                int index = 0;
                foreach (var p in data)
                {
                    ret.Append($"{p.Key}={CqHttpCharEncoder.Encode(p.Value)}");
                    if (index < count - 1) ret.Append(",");
                    index++;
                }
                ret.Append("]");
                return ret.ToString();
            }
        }
    }
}
