using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Data;

namespace CqHttpSharp.Event
{
    public delegate void EvtNoticeGroupFileUpload(CqHttpNoticeGroupFileUpload data);
    public delegate void EvtNoticeGroupAdminChange(CqHttpNoticeGroupAdminChange data);
    public delegate void EvtNoticeGroupDecrease(CqHttpNoticeGroupDecrease data);
    public delegate void EvtNoticeGroupIncrease(CqHttpNoticeGroupIncrease data);
    public delegate void EvtNoticeGroupBan(CqHttpNoticeGroupBan data);
    public delegate void EvtNoticeAddFriend(CqHttpNoticeAddFriend data);
    public delegate void EvtNoticeGroupRecall(CqHttpNoticeGroupRecall data);
    public delegate void EvtNoticeCommonRecall(CqHttpNoticeCommonRecall data);
    public delegate void EvtNoticeCommonPoke(CqHttpNoticeCommonPoke data);
    public delegate void EvtNoticeGroupPoke(CqHttpNoticeGroupPoke data);
    public delegate void EvtNoticeGroupLuckyKing(CqHttpNoticeGroupLuckyKing data);
    public delegate void EvtNoticeGroupHonor(CqHttpNoticeGroupHonor data);
    public delegate void EvtNoticeGroupCardChange(CqHttpNoticeGroupCardChange data);
    public delegate void EvtNoticeOfflineFileReceive(CqHttpNoticeOfflineFileReceive data);
    public delegate void EvtNoticeClientStatusChange(CqHttpNoticeClientStatusChange data);
    public delegate void EvtNoticeGroupEssence(CqHttpNoticeGroupEssence data);
}
