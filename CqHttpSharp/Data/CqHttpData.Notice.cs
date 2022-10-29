using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Enum;

namespace CqHttpSharp.Data
{
    public class CqHttpNoticeGroupFileUpload : CqHttpDataNotice
    {
        public long group_id;
        public long user_id;
        public CqHttpFileInfo file;
    }

    public class CqHttpNoticeGroupAdminChange : CqHttpDataNotice
    {
        public CqHttpNoticeGroupAdminSubType sub_type;
        public long group_id;
        public long user_id;
    }

    public class CqHttpNoticeGroupDecrease : CqHttpDataNotice
    {
        public CqHttpNoticeGroupDecreaseSubType sub_type;
        public long group_id;
        public long operator_id;
        public long user_id;
    }

    public class CqHttpNoticeGroupIncrease : CqHttpDataNotice
    {
        public CqHttpNoticeGroupIncreaseSubType sub_type;
        public long group_id;
        public long operator_id;
        public long user_id;
    }

    public class CqHttpNoticeGroupBan : CqHttpDataNotice
    {
        public CqHttpNoticeGroupBanSubType sub_type;
        public long group_id;
        public long operator_id;
        public long user_id;
        public long duration; //单位：秒
    }

    public class CqHttpNoticeAddFriend : CqHttpDataNotice
    {
        public long user_id;
    }

    public class CqHttpNoticeGroupRecall : CqHttpDataNotice
    {
        public long group_id;
        public long operator_id;
        public long user_id;
        public long message_id;
    }

    public class CqHttpNoticePrivateRecall : CqHttpDataNotice
    {
        public long user_id;
        public long message_id;
    }

    public class CqHttpNoticePrivatePoke : CqHttpDataNotice
    {
        public CqHttpNoticeNotifySubType sub_type;
        public long sender_id;
        public long user_id;
        public long target_id;
    }

    public class CqHttpNoticeGroupPoke : CqHttpDataNotice
    {
        public CqHttpNoticeNotifySubType sub_type;
        public long group_id;
        public long user_id;
        public long target_id;
    }

    public class CqHttpNoticeGroupLuckyKing : CqHttpDataNotice
    {
        public CqHttpNoticeNotifySubType sub_type;
        public long group_id;
        public long user_id;
        public long target_id;
    }

    public class CqHttpNoticeGroupHonor : CqHttpDataNotice
    {
        public CqHttpNoticeNotifySubType sub_type;
        public long group_id;
        public long user_id;
        public CqHttpNoticeGroupHonorSubType honor_type;
    }

    public class CqHttpNoticeGroupCardChange : CqHttpDataNotice
    {
        public long group_id;
        public long user_id;
        public string card_new;
        public string card_old;
    }

    public class CqHttpNoticeOfflineFileReceive : CqHttpDataNotice
    {
        public long user_id;
        public CqHttpFileData file;
    }

    public class CqHttpNoticeClientStatusChange : CqHttpDataNotice
    {
        public CqHttpDeviceInfo client;
        public bool online;
    }

    public class CqHttpNoticeGroupEssence : CqHttpDataNotice
    {
        public CqHttpNoticeGroupEssenceSubType sub_type;
        public long sender_id;
        public long operator_id;
        public long message_id;
    }
}
