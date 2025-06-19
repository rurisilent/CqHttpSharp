using System;
using System.Collections.Generic;
using System.Text;
using NapCatSharpLib.Event;
using NapCatSharpLib.Log;

namespace NapCatSharpLib.Event.Manager
{
    public class NapCatEventManager
    {
        public EvtNapCatLog EventLogger;

        public EvtMessagePrivate OnEventMessagePrivate;
        public EvtMessageGroup OnEventMessageGroup;

        public EvtNoticeGroupFileUpload OnEventNoticeGroupFileUpload;
        public EvtNoticeGroupAdminChange OnEventNoticeGroupAdminChange;
        public EvtNoticeGroupDecrease OnEventNoticeGroupDecrease;
        public EvtNoticeGroupIncrease OnEventNoticeGroupIncrease;
        public EvtNoticeGroupBan OnEventNoticeGroupBan;
        public EvtNoticeAddFriend OnEventNoticeAddFriend;
        public EvtNoticeGroupRecall OnEventNoticeGroupRecall;
        public EvtNoticePrivateRecall OnEventNoticePrivateRecall;
        public EvtNoticePrivatePoke OnEventNoticePrivatePoke;
        public EvtNoticeGroupPoke OnEventNoticeGroupPoke;
        public EvtNoticeGroupLuckyKing OnEventNoticeGroupLuckyKing;
        public EvtNoticeGroupHonor OnEventNoticeGroupHonor;
        public EvtNoticeGroupCardChange OnEventNoticeGroupCardChange;
        public EvtNoticeOfflineFileReceive OnEventNoticeOfflineFileReceive;
        public EvtNoticeClientStatusChange OnEventNoticeClientStatusChange;
        public EvtNoticeGroupEssence OnEventNoticeGroupEssence;

        public EvtRequestFriend OnEventRequestFriend;
        public EvtRequestGroup OnEventRequestGroup;

        public EvtMetaLifeCycle OnEventMetaLifeCycle;
        public EvtMetaHeartbeat OnEventMetaHeartbeat;
    }
}
