using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NapCatSharpLib.Message
{
    public class CqMessageChain
    {
        public CqMessageBuilder Builder { get; private set; }
        public List<CqMessage> Messages { get; private set; }

        public int Count
        {
            get
            {
                return Messages.Count;
            }
        }

        public CqMessageChain()
        {
            Builder = new CqMessageBuilder(this);

            Messages = new List<CqMessage>();
        }

        public CqMessageChain(string cqMessage)
        {
            Messages = CqMessageLexer.Parse(cqMessage).Messages;

            Builder = new CqMessageBuilder(this);
        }

        public void AddMessage(CqMessage msg)
        {
            Messages.Add(msg);
        }

        public void ClearMessage()
        {
            Messages.Clear();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(Messages);
        }

        public string ToCqQuery()
        {
            StringBuilder ret = new StringBuilder();
            foreach (var msg in Messages)
            {
                ret.Append(msg.ToCqCode());
            }
            return ret.ToString();
        }
    }
}
