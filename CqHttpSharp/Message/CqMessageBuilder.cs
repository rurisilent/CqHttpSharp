using System;
using System.Collections.Generic;
using System.Text;

namespace CqHttpSharp.Message
{
    public class CqMessageBuilder
    {
        CqMessageChain target;

        public CqMessageBuilder (CqMessageChain _target)
        {
            target = _target;
        }

        /// <summary>
        /// 添加文字消息
        /// </summary>
        /// <param name="text">文本</param>
        public void AddText(string text)
        {
            CqMessage msg = new CqMessage(CqCode.text);
            msg.AddData("text", text);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加表情消息
        /// </summary>
        /// <param name="id">表情 ID</param>
        public void AddFace(CqFaceCode id)
        {
            CqMessage msg = new CqMessage(CqCode.face);
            msg.AddData("id", ((int)id).ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加语音消息
        /// </summary>
        /// <param name="file">语音文件名（http / file）</param>
        /// <param name="magic">是否变声</param>
        /// <param name="cache">是否使用缓存文件</param>
        /// <param name="proxy">是否通过代理</param>
        /// <param name="timeout">超时时间（单位：秒）</param>
        public void AddRecord(string file, bool magic = false, bool cache = true, bool proxy = true, long timeout = -1)
        {
            CqMessage msg = new CqMessage(CqCode.record);
            msg.AddData("file", file);

            if (magic) msg.AddData("magic", "1");
            if (!cache) msg.AddData("cache", "0");
            if (!proxy) msg.AddData("proxy", "0");
            if (timeout != -1) msg.AddData("timeout", timeout.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加语音消息（全参数）
        /// </summary>
        /// <param name="file">语音文件名（http / file）</param>
        /// <param name="magic">是否变声</param>
        /// <param name="cache">是否使用缓存文件</param>
        /// <param name="proxy">是否通过代理</param>
        /// <param name="timeout">超时时间（单位：秒）</param>
        public void AddRecordAdvanced(string file, bool magic = false, bool cache = true, bool proxy = true, long timeout = -1)
        {
            CqMessage msg = new CqMessage(CqCode.record);
            msg.AddData("file", file);

            msg.AddData("magic", magic ? "1" : "0");
            msg.AddData("cache", cache ? "1" : "0");
            msg.AddData("proxy", proxy ? "1" : "0");
            if (timeout != -1) msg.AddData("timeout", timeout.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加视频消息
        /// </summary>
        /// <param name="file">视频地址（http / file）</param>
        /// <param name="cover">视频封面（http / file / base64），必须jpg</param>
        /// <param name="c">通过网络下载视频时的线程数量</param>
        public void AddVideo(string file, string cover = "", int c = 1)
        {
            CqMessage msg = new CqMessage(CqCode.video);
            msg.AddData("file", file);

            if (cover != "") msg.AddData("cover", cover);
            if (c > 1 && c <= 3) msg.AddData("c", c.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加@消息
        /// </summary>
        /// <param name="qq">QQ 号，为 -1 时表示全体成员</param>
        /// <param name="name"></param>
        public void AddAt(long qq, string name = "")
        {
            CqMessage msg = new CqMessage(CqCode.at);
            if (qq == -1) msg.AddData("qq", "all");
            else msg.AddData("qq", qq.ToString());

            if (name != "") msg.AddData("name", name);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加猜拳魔法表情消息（未被go-cqhttp支持）
        /// </summary>
        public void AddRps()
        {
            CqMessage msg = new CqMessage(CqCode.rps);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加掷骰子魔法表情消息（未被go-cqhttp支持）
        /// </summary>
        public void AddDice()
        {
            CqMessage msg = new CqMessage(CqCode.dice);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加窗口抖动（戳一戳）消息（未被go-cqhttp支持）
        /// </summary>
        public void AddShake()
        {
            CqMessage msg = new CqMessage(CqCode.shake);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加匿名消息（未被go-cqhttp支持）
        /// </summary>
        public void AddAnonymous()
        {
            CqMessage msg = new CqMessage(CqCode.anonymous);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加分享链接消息
        /// </summary>
        /// <param name="url">链接 URL</param>
        /// <param name="title">标题</param>
        /// <param name="content">内容描述</param>
        /// <param name="image">图片 URL</param>
        public void AddShare(string url, string title, string content = "", string image = "")
        {
            CqMessage msg = new CqMessage(CqCode.share);
            msg.AddData("url", url);
            msg.AddData("title", title);

            if (content != "") msg.AddData("content", content);
            if (image != "") msg.AddData("image", image);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加推荐好友/群消息（未被go-cqhttp支持）
        /// </summary>
        /// <param name="type">推荐类型</param>
        /// <param name="id">被推荐的 QQ 号或群号</param>
        public void AddContact(CqContactType type, long id)
        {

            CqMessage msg = new CqMessage(CqCode.contact);
            msg.AddData("type", type.ToString());
            msg.AddData("id", id.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加位置消息（未被go-cqhttp支持）
        /// </summary>
        /// <param name="lat">纬度</param>
        /// <param name="lon">经度</param>
        /// <param name="title">标题</param>
        /// <param name="content">描述</param>
        public void AddLocation(double lat, double lon, string title = "", string content = "")
        {
            CqMessage msg = new CqMessage(CqCode.location);
            msg.AddData("lat", lat.ToString("0.0000000"));
            msg.AddData("lon", lon.ToString("0.0000000"));

            if (title != "") msg.AddData("title", title);
            if (content != "") msg.AddData("content", content);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加音乐分享消息
        /// </summary>
        /// <param name="type">音乐软件类型</param>
        /// <param name="id">音乐 ID</param>
        public void AddMusic(CqMusicType type, long id)
        {
            CqMessage msg = new CqMessage(CqCode.music);
            msg.AddData("type", type.ToString().Replace("cloudmusic", "163"));
            msg.AddData("id", id.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加音乐分享消息（自定义）
        /// </summary>
        /// <param name="url">点击跳转的 URL</param>
        /// <param name="audio">音频 URL</param>
        /// <param name="title">标题</param>
        /// <param name="content">描述</param>
        /// <param name="image">图片 URL</param>
        public void AddMusicCustom(string url, string audio, string title, string content = "", string image = "")
        {
            CqMessage msg = new CqMessage(CqCode.music);
            msg.AddData("type", "custom");
            msg.AddData("url", url);
            msg.AddData("audio", audio);
            msg.AddData("title", title);

            if (content != "") msg.AddData("content", content);
            if (image != "") msg.AddData("image", image);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加图片消息
        /// </summary>
        /// <param name="file">图片文件 ( http / file / base64 )</param>
        /// <param name="type">图片类型</param>
        /// <param name="subType">图片子类型</param>
        /// <param name="cache">是否使用已缓存文件</param>
        /// <param name="id">秀图特效 ID</param>
        /// <param name="c">通过网络下载的线程数</param>
        public void AddImage(string file, CqImageType type = CqImageType.normal, CqImageSubType subType = CqImageSubType.normal, bool cache = true, CqImageEffectType id = CqImageEffectType.normal, int c = 1)
        {
            CqMessage msg = new CqMessage(CqCode.image);
            msg.AddData("file", file);

            if (type != CqImageType.normal) msg.AddData("type", type.ToString());
            if (subType != CqImageSubType.normal) msg.AddData("subType", ((int)subType).ToString());
            if (!cache) msg.AddData("cache", "0");
            if (id != CqImageEffectType.normal) msg.AddData("id", ((int)id).ToString());
            if (c > 1 && c <= 3) msg.AddData("c", c.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加回复消息
        /// </summary>
        /// <param name="id">引用的消息 ID</param>
        public void AddReply(long id)
        {
            CqMessage msg = new CqMessage(CqCode.reply);
            msg.AddData("id", id.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加回复消息（自定义）
        /// </summary>
        /// <param name="text">回复消息</param>
        /// <param name="qq">回复者</param>
        /// <param name="time">回复时间</param>
        /// <param name="seq">起始消息序号</param>
        public void AddReplyCustom(string text, long qq, long time, long seq)
        {
            CqMessage msg = new CqMessage(CqCode.reply);
            msg.AddData("text", text);
            msg.AddData("qq", qq.ToString());
            msg.AddData("time", time.ToString());
            msg.AddData("seq", seq.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加戳一戳消息
        /// </summary>
        /// <param name="qq">目标 QQ 号</param>
        public void AddPoke(long qq)
        {
            CqMessage msg = new CqMessage(CqCode.poke);
            msg.AddData("qq", qq.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加礼物消息
        /// </summary>
        /// <param name="qq">目标 QQ 号</param>
        /// <param name="id">礼物类型</param>
        public void AddGift(long qq, CqGiftType id)
        {
            CqMessage msg = new CqMessage(CqCode.gift);
            msg.AddData("qq", qq.ToString());
            msg.AddData("id", ((int)id).ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加合并转发消息
        /// </summary>
        /// <param name="id">合并转发 ID</param>
        public void AddForward(string id)
        {
            CqMessage msg = new CqMessage(CqCode.forward);
            msg.AddData("id", id);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加合并转发消息节点
        /// </summary>
        /// <param name="id">引用消息 ID</param>
        public void AddNode(long id)
        {
            CqMessage msg = new CqMessage(CqCode.node);
            msg.AddData("id", id.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加合并转发消息节点（自定义）
        /// </summary>
        /// <param name="name">发送者显示名字</param>
        /// <param name="uin">发送者 QQ 号</param>
        /// <param name="content">消息内容</param>
        /// <param name="seq">起始消息序号</param>
        public void AddNodeCustom(string name, long uin, string content, int seq = -1)
        {
            CqMessage msg = new CqMessage(CqCode.node);
            msg.AddData("name", name);
            msg.AddData("uin", uin.ToString());
            msg.AddData("content", content);
            if (seq != -1) msg.AddData("seq", seq.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加 XML 消息
        /// </summary>
        /// <param name="data">XML 文本，value 部分需要实体化处理</param>
        /// <param name="resid">一般情况不需填写</param>
        public void AddXml(string data, int resid = 0)
        {
            CqMessage msg = new CqMessage(CqCode.xml);
            msg.AddData("data", data);
            if (resid != 0) msg.AddData("resid", resid.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加 JSON 消息
        /// </summary>
        /// <param name="data">JSON 文本，字符串需要实体化处理</param>
        /// <param name="resid">默认0（小程序通道），填写后进入富文本通道</param>
        public void AddJson(string data, int resid = 0)
        {
            CqMessage msg = new CqMessage(CqCode.json);
            msg.AddData("data", data);
            if (resid != 0) msg.AddData("resid", resid.ToString());

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加卡片图片消息
        /// </summary>
        /// <param name="file">图片文件 ( http / file / base64)</param>
        /// <param name="minwidth">最小宽度</param>
        /// <param name="minheight">最小高度</param>
        /// <param name="maxwidth">最大宽度</param>
        /// <param name="maxheight">最大高度</param>
        /// <param name="source">分享来源名称</param>
        /// <param name="icon">分享来源图标 URL</param>
        public void AddCardImage(string file, long minwidth = 400, long minheight = 400, long maxwidth = 500, long maxheight = 1000, string source = "", string icon = "")
        {
            CqMessage msg = new CqMessage(CqCode.cardimage);
            msg.AddData("file", file);

            if (minwidth != 400) msg.AddData("minwidth", minwidth.ToString());
            if (minheight != 400) msg.AddData("minheight", minheight.ToString());
            if (maxwidth != 500) msg.AddData("maxwidth", maxwidth.ToString());
            if (maxheight != 1000) msg.AddData("maxheight", maxheight.ToString());
            if (source != "") msg.AddData("source", source);
            if (icon != "") msg.AddData("icon",icon);

            target.AddMessage(msg);
        }

        /// <summary>
        /// 添加文本转语音消息
        /// </summary>
        /// <param name="text">文本</param>
        public void AddTTS(string text)
        {
            CqMessage msg = new CqMessage(CqCode.tts);
            msg.AddData("text", text);

            target.AddMessage(msg);
        }
    }
}
