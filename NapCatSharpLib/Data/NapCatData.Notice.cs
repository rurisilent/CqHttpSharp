using System;
using System.Collections.Generic;
using System.Text;
using NapCatSharpLib.Enum;

namespace NapCatSharpLib.Data
{
    public class NapCatNoticeGroupFileUpload : NapCatDataNotice
    {
        public long group_id;
        public long user_id;
        public NapCatFileInfo file;
    }

    public class NapCatNoticeGroupAdminChange : NapCatDataNotice
    {
        public NapCatNoticeGroupAdminSubType sub_type;
        public long group_id;
        public long user_id;
    }

    public class NapCatNoticeGroupDecrease : NapCatDataNotice
    {
        public NapCatNoticeGroupDecreaseSubType sub_type;
        public long group_id;
        public long operator_id;
        public long user_id;
    }

    public class NapCatNoticeGroupIncrease : NapCatDataNotice
    {
        public NapCatNoticeGroupIncreaseSubType sub_type;
        public long group_id;
        public long operator_id;
        public long user_id;
    }

    public class NapCatNoticeGroupBan : NapCatDataNotice
    {
        public NapCatNoticeGroupBanSubType sub_type;
        public long group_id;
        public long operator_id;
        public long user_id;
        public long duration; //单位：秒
    }

    public class NapCatNoticeAddFriend : NapCatDataNotice
    {
        public long user_id;
    }

    public class NapCatNoticeGroupRecall : NapCatDataNotice
    {
        public long group_id;
        public long operator_id;
        public long user_id;
        public long message_id;
    }

    public class NapCatNoticePrivateRecall : NapCatDataNotice
    {
        public long user_id;
        public long message_id;
    }

    public class NapCatNoticePrivatePoke : NapCatDataNotice
    {
        public NapCatNoticeNotifySubType sub_type;
        public long sender_id;
        public long user_id;
        public long target_id;
    }

    public class NapCatNoticeGroupPoke : NapCatDataNotice
    {
        public NapCatNoticeNotifySubType sub_type;
        public long group_id;
        public long user_id;
        public long target_id;
    }

    public class NapCatNoticeGroupLuckyKing : NapCatDataNotice
    {
        public NapCatNoticeNotifySubType sub_type;
        public long group_id;
        public long user_id;
        public long target_id;
    }

    public class NapCatNoticeGroupHonor : NapCatDataNotice
    {
        public NapCatNoticeNotifySubType sub_type;
        public long group_id;
        public long user_id;
        public NapCatNoticeGroupHonorSubType honor_type;
    }

    public class NapCatNoticeGroupCardChange : NapCatDataNotice
    {
        public long group_id;
        public long user_id;
        public string card_new;
        public string card_old;
    }

    public class NapCatNoticeOfflineFileReceive : NapCatDataNotice
    {
        public long user_id;
        public NapCatFileData file;
    }

    public class NapCatNoticeClientStatusChange : NapCatDataNotice
    {
        public NapCatDeviceInfo client;
        public bool online;
    }

    public class NapCatNoticeGroupEssence : NapCatDataNotice
    {
        public NapCatNoticeGroupEssenceSubType sub_type;
        public long group_id;
        public long sender_id;
        public long operator_id;
        public long message_id;
    }
}
