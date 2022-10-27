using System;
using System.Collections.Generic;
using System.Text;

namespace CqHttpSharp.Utility
{
    public static class CqHttpCharEncoder
    {
        public static string Encode(string text)
        {
            text = text.Replace("&", "&amp;");
            text = text.Replace("[", "&#91;");
            text = text.Replace("]", "&#93;");
            text = text.Replace(",", "&#44;");

            return text;
        }

        public static string Decode(string text)
        {
            text = text.Replace("&#91;", "[");
            text = text.Replace("&#93;", "]");
            text = text.Replace("&#44;", ",");
            text = text.Replace("&amp;", "&");

            return text;
        }
    }
}
