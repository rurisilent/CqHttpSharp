using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Utility;

namespace CqHttpSharp.Message
{
    public static class CqMessageLexer
    {
        public static CqMessageChain Parse(string data)
        {
            CqMessageChain ret = new CqMessageChain();

            int len = data.Length;
            StringBuilder tempMsg = new StringBuilder();

            int idx = 0;

            while (idx < len)
            {
                if (data[idx] == '[')
                {
                    if (tempMsg.Length > 0)
                    {
                        CqMessage msg = new CqMessage(CqCode.text);
                        msg.AddData("text", CqHttpCharEncoder.Decode(tempMsg.ToString()));
                        ret.AddMessage(msg);
                    }

                    tempMsg.Clear();
                }
                else if (data[idx] == ']')
                {
                    string[] cqData = tempMsg.ToString().Split(',');
                    if (cqData.Length > 0)
                    {
                        if (System.Enum.TryParse<CqCode>(cqData[0].Replace("CQ:", ""), out var type))
                        {
                            CqMessage msg = new CqMessage(type);
                            for (int i = 1; i < cqData.Length; i++)
                            {
                                int equalIndex = cqData[i].IndexOf('=');
                                if (equalIndex != -1) 
                                    msg.AddData(cqData[i].Substring(0, equalIndex), CqHttpCharEncoder.Decode(cqData[i].Substring(equalIndex + 1, cqData[i].Length - equalIndex + 2)));
                            }
                            ret.AddMessage(msg);
                        }
                    }

                    tempMsg.Clear();
                }
                else
                {
                    tempMsg.Append(data[idx]);
                }

                idx++;
            }

            if (tempMsg.Length > 0)
            {
                CqMessage msg = new CqMessage(CqCode.text);
                msg.AddData("text", tempMsg.ToString());
            }

            return ret;
        }
    }
}
