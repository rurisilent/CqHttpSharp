using System;
using System.Collections.Generic;
using System.Text;
using NapCatSharpLib.Data;

namespace NapCatSharpLib.Event
{
    public delegate void EvtNoticeGroupFileUpload(NapCatNoticeGroupFileUpload data);
    public delegate void EvtNoticeGroupAdminChange(NapCatNoticeGroupAdminChange data);
    public delegate void EvtNoticeGroupDecrease(NapCatNoticeGroupDecrease data);
    public delegate void EvtNoticeGroupIncrease(NapCatNoticeGroupIncrease data);
    public delegate void EvtNoticeGroupBan(NapCatNoticeGroupBan data);
    public delegate void EvtNoticeAddFriend(NapCatNoticeAddFriend data);
    public delegate void EvtNoticeGroupRecall(NapCatNoticeGroupRecall data);
    public delegate void EvtNoticePrivateRecall(NapCatNoticePrivateRecall data);
    public delegate void EvtNoticePrivatePoke(NapCatNoticePrivatePoke data);
    public delegate void EvtNoticeGroupPoke(NapCatNoticeGroupPoke data);
    public delegate void EvtNoticeGroupLuckyKing(NapCatNoticeGroupLuckyKing data);
    public delegate void EvtNoticeGroupHonor(NapCatNoticeGroupHonor data);
    public delegate void EvtNoticeGroupCardChange(NapCatNoticeGroupCardChange data);
    public delegate void EvtNoticeOfflineFileReceive(NapCatNoticeOfflineFileReceive data);
    public delegate void EvtNoticeClientStatusChange(NapCatNoticeClientStatusChange data);
    public delegate void EvtNoticeGroupEssence(NapCatNoticeGroupEssence data);
}
