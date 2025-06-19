using System;
using System.Collections.Generic;
using System.Text;

namespace NapCatSharpLib.Message
{
    public class CqMessageDataText
    {
        public string text = "";

        public CqMessageDataText() { }

        public CqMessageDataText(CqMessage msg)
        {
            if (msg.data.ContainsKey("text"))
                text = msg.data["text"];
        }
    }

    public class CqMessageDataFace
    {
        public CqFaceCode id = 0;

        public CqMessageDataFace() { }

        public CqMessageDataFace(CqMessage msg)
        {
            if (msg.data.ContainsKey("id") && System.Enum.TryParse<CqFaceCode>(msg.data["id"], out var _id))
                id = _id;
        }
    }

    public class CqMessageDataRecord
    {
        public string file; //default: null
        public bool magic = false;
        public string url; //default: null
        public bool cache = true;
        public bool proxy = true;
        public int timeout = -1;

        public CqMessageDataRecord() { }

        public CqMessageDataRecord(CqMessage msg)
        {
            if (msg.data.ContainsKey("file"))
                file = msg.data["file"];
            if (msg.data.ContainsKey("magic"))
                magic = msg.data["magic"] == "1" ? true : false;
            if (msg.data.ContainsKey("url"))
                url = msg.data["url"];
            if (msg.data.ContainsKey("cache"))
                cache = msg.data["cache"] == "0" ? false : true;
            if (msg.data.ContainsKey("proxy"))
                proxy = msg.data["proxy"] == "0" ? false : true;
            if (msg.data.ContainsKey("timeout") && int.TryParse(msg.data["timeout"], out var _timeout))
                timeout = _timeout;
        }
    }

    public class CqMessageDataVideo
    {
        public string file; //default: null
        public string cover; //default: null
        public int c = 1;

        public CqMessageDataVideo() { }

        public CqMessageDataVideo(CqMessage msg)
        {
            if (msg.data.ContainsKey("file"))
                file = msg.data["file"];
            if (msg.data.ContainsKey("cover"))
                file = msg.data["cover"];
            if (msg.data.ContainsKey("c") && int.TryParse(msg.data["c"], out var _c))
                c = _c;
        }
    }

    public class CqMessageDataAt
    {
        public long qq = -1; //-1 for all
        public string name; //default: null

        public CqMessageDataAt() { }

        public CqMessageDataAt(CqMessage msg)
        {
            if (msg.data.ContainsKey("qq"))
            {
                if (msg.data["qq"] == "all") qq = -1;
                else if (long.TryParse(msg.data["qq"], out var _qq)) qq = _qq;
            }
            if (msg.data.ContainsKey("name"))
                name = msg.data["name"];
        }
    }

    public class CqMessageRps
    {

    }

    public class CqMessageDice
    {

    }

    public class CqMessageShake
    {

    }

    public class CqMessageAnonymous
    {

    }

    public class CqMessageShare
    {
        public string url; //default: null
        public string title; //default: null
        public string content; //default: null
        public string image; //default: null

        public CqMessageShare() { }

        public CqMessageShare(CqMessage msg)
        {
            if (msg.data.ContainsKey("url"))
                url = msg.data["url"];
            if (msg.data.ContainsKey("title"))
                title = msg.data["title"];
            if (msg.data.ContainsKey("content"))
                content = msg.data["content"];
            if (msg.data.ContainsKey("image"))
                image = msg.data["image"];
        }
    }

    public class CqMessageContact
    {
        public CqContactType type = 0;
        public long id = -1;

        public CqMessageContact() { }

        public CqMessageContact(CqMessage msg)
        {
            if (msg.data.ContainsKey("type") && System.Enum.TryParse<CqContactType>(msg.data["type"], out var _type))
                type = _type;
            if (msg.data.ContainsKey("id") && long.TryParse(msg.data["id"], out var _id))
                id = _id;
        }
    }

    public class CqMessageLocation
    {
        public double lat = 0;
        public double lon = 0;
        public string title; //default: null
        public string content; //default: null

        public CqMessageLocation() { }

        public CqMessageLocation(CqMessage msg)
        {
            if (msg.data.ContainsKey("lat") && double.TryParse(msg.data["lat"], out var _lat))
                lat = _lat;
            if (msg.data.ContainsKey("lon") && double.TryParse(msg.data["lon"], out var _lon))
                lon = _lon;
            if (msg.data.ContainsKey("title"))
                title = msg.data["title"];
            if (msg.data.ContainsKey("content"))
                content = msg.data["content"];
        }
    }

    public class CqMessageMusic
    {
        public CqMusicType type = 0;
        public int id = -1;

        public CqMessageMusic() { }

        public CqMessageMusic(CqMessage msg)
        {
            if (msg.data.ContainsKey("type") && System.Enum.TryParse<CqMusicType>(msg.data["type"], out var _type))
                type = _type;
            if (msg.data.ContainsKey("id") && int.TryParse(msg.data["id"], out var _id))
                id = _id;
        }
    }

    public class CqMessageMusicCustom
    {
        public string url; //default:null
        public string audio; //default:null
        public string title; //default:null
        public string content; //default:null

        public CqMessageMusicCustom() { }

        public CqMessageMusicCustom(CqMessage msg)
        {
            if (msg.data.ContainsKey("url"))
                url = msg.data["url"];
            if (msg.data.ContainsKey("audio"))
                audio = msg.data["audio"];
            if (msg.data.ContainsKey("title"))
                title = msg.data["title"];
            if (msg.data.ContainsKey("content"))
                content = msg.data["content"];
        }
    }

    public class CqMessageImage
    {
        public string file; //default: null
        public CqImageType type = 0;
        public CqImageSubType subType = 0;
        public string url; //default: null
        public bool cache = true;
        public CqImageEffectType id = 0;
        public int c = 1;

        public CqMessageImage() { }

        public CqMessageImage(CqMessage msg)
        {
            if (msg.data.ContainsKey("file"))
                file = msg.data["file"];
            if (msg.data.ContainsKey("type") && System.Enum.TryParse<CqImageType>(msg.data["type"], out var _type))
                type = _type;
            if (msg.data.ContainsKey("subType") && System.Enum.TryParse<CqImageSubType>(msg.data["subType"], out var _subType))
                subType = _subType;
            if (msg.data.ContainsKey("url"))
                url = msg.data["url"];
            if (msg.data.ContainsKey("cache"))
                cache = msg.data["cache"] == "0" ? false : true;
            if (msg.data.ContainsKey("id") && System.Enum.TryParse<CqImageEffectType>(msg.data["id"], out var _id))
                id = _id;
            if (msg.data.ContainsKey("c") && int.TryParse(msg.data["c"], out var _c))
                c = _c;
        }
    }

    public class CqMessageReply
    {
        public int id = -1;
        public string text; //default: null
        public long qq = -1;
        public long time = -1;
        public long seq = -1;

        public CqMessageReply() { }

        public CqMessageReply(CqMessage msg)
        {
            if (msg.data.ContainsKey("id") && int.TryParse(msg.data["id"], out var _id))
                id = _id;
            if (msg.data.ContainsKey("text"))
                text = msg.data["text"];
            if (msg.data.ContainsKey("qq") && long.TryParse(msg.data["qq"], out var _qq))
                qq = _qq;
            if (msg.data.ContainsKey("time") && long.TryParse(msg.data["time"], out var _time))
                time = _time;
            if (msg.data.ContainsKey("seq") && long.TryParse(msg.data["seq"], out var _seq))
                seq = _seq;
        }
    }

    public class CqMessageRedBag
    {
        public string title; //default: null

        public CqMessageRedBag() { }

        public CqMessageRedBag(CqMessage msg)
        {
            if (msg.data.ContainsKey("title"))
                title = msg.data["title"];
        }
    }

    public class CqMessagePoke
    {
        public long qq = -1;

        public CqMessagePoke() { }

        public CqMessagePoke(CqMessage msg)
        {
            if (msg.data.ContainsKey("qq") && long.TryParse(msg.data["qq"], out var _qq))
                qq = _qq;
        }
    }

    public class CqMessageGift
    {
        public long qq = -1;
        public CqGiftType id = 0;

        public CqMessageGift() { }

        public CqMessageGift(CqMessage msg)
        {
            if (msg.data.ContainsKey("qq") && long.TryParse(msg.data["qq"], out var _qq))
                qq = _qq;
            if (msg.data.ContainsKey("id") && System.Enum.TryParse<CqGiftType>(msg.data["id"], out var _id))
                id = _id;
        }
    }

    public class CqMessageForward
    {
        public string id; //default: null

        public CqMessageForward() { }

        public CqMessageForward(CqMessage msg)
        {
            if (msg.data.ContainsKey("id"))
                id = msg.data["id"];
        }
    }

    public class CqMessageNode
    {
        public long id = -1;
        public string name; //default: null
        public long uin = -1;
        public string message; //default: null
        public int seq = -1;

        public CqMessageNode() { }

        public CqMessageNode(CqMessage msg)
        {
            if (msg.data.ContainsKey("id") && int.TryParse(msg.data["id"], out var _id))
                id = _id;
            if (msg.data.ContainsKey("name"))
                name = msg.data["name"];
            if (msg.data.ContainsKey("uin") && int.TryParse(msg.data["uin"], out var _uin))
                uin = _uin;
            if (msg.data.ContainsKey("message"))
                name = msg.data["message"];
            if (msg.data.ContainsKey("seq") && int.TryParse(msg.data["seq"], out var _seq))
                seq = _seq;
        }
    }

    public class CqMessageXML
    {
        public string data;
        public int resid = 0;

        public CqMessageXML() { }

        public CqMessageXML(CqMessage msg)
        {
            if (msg.data.ContainsKey("data"))
                data = msg.data["data"];
            if (msg.data.ContainsKey("resid") && int.TryParse(msg.data["resid"], out var _resid))
                resid = _resid;
        }
    }

    public class CqMessageJSON
    {
        public string data;
        public int resid = 0;

        public CqMessageJSON() { }

        public CqMessageJSON(CqMessage msg)
        {
            if (msg.data.ContainsKey("data"))
                data = msg.data["data"];
            if (msg.data.ContainsKey("resid") && int.TryParse(msg.data["resid"], out var _resid))
                resid = _resid;
        }
    }

    public class CqMessageCardImage
    {
        public string file; //default: null
        public long minwidth = 400;
        public long minheight = 400;
        public long maxwidth = 500;
        public long maxheight = 1000;
        public string source; //default: null
        public string icon; //default: null

        public CqMessageCardImage() { }

        public CqMessageCardImage(CqMessage msg)
        {
            if (msg.data.ContainsKey("file"))
                file = msg.data["file"];
            if (msg.data.ContainsKey("minwidth") && long.TryParse(msg.data["minwidth"], out var _minwidth))
                minwidth = _minwidth;
            if (msg.data.ContainsKey("minheight") && long.TryParse(msg.data["minheight"], out var _minheight))
                minheight = _minheight;
            if (msg.data.ContainsKey("maxwidth") && long.TryParse(msg.data["maxwidth"], out var _maxwidth))
                maxwidth = _maxwidth;
            if (msg.data.ContainsKey("maxheight") && long.TryParse(msg.data["maxheight"], out var _maxheight))
                maxheight = _maxheight;
            if (msg.data.ContainsKey("source"))
                source = msg.data["source"];
            if (msg.data.ContainsKey("icon"))
                icon = msg.data["icon"];
        }
    }

    public class CqMessageTTS
    {
        public string text;

        public CqMessageTTS() { }

        public CqMessageTTS(CqMessage msg)
        {
            if (msg.data.ContainsKey("text"))
                text = msg.data["text"];
        }
    }
}
