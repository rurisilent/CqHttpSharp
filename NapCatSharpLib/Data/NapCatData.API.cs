using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using NapCatSharpLib.Enum;

namespace NapCatSharpLib.Data
{
    public class NapCatAPIDataCommon
    {
        public int result;
        public string errMsg;
    }

    public class NapCatAPIDataArkJson
    {
        public int errCode;
        public string errMsg;
        public string arkJson;
    }

    public class NapCatAPIDataFriendCategory
    {
        public int categoryId;
        public int categorySortId;
        public string categoryName;
        public int categoryMbCount;
        public int onlineCount;
        public List<NapCatAPIDataFriend> buddyList;
    }

    public class NapCatAPIDataFriend
    {
        public string qid;
        public string longNick;
        public int birthday_year;
        public int birthday_month;
        public int birthday_day;
        public int age;
        public NapCatSenderSexType sex;
        public string eMail;
        public string phoneNum;
        public int categoryId;
        public int richTime;
        public object richBuffer;
        public string uid;
        public string uin;
        public string nick;
        public string remark;
        public long user_id;
        public string nickname;
        public int level;
    }

    public class NapCatAPIDataProfileLike
    {
        public int total_count;
        public int new_count;
        public int new_nearby_count;
        public long last_visit_time;
        public List<NapCatAPIDataUserInfo> userInfos;
    }

    public class NapCatAPIDataUserInfo
    {
        public string uid;
        public int src;
        public long latestTime;
        public int count;
        public int giftCount;
        public long customId;
        public long lastCharged;
        public int bAvailableCnt;
        public int bTodayVotedCnt;
        public string nick;
        public int gender;
        public int age;
        public bool isFriend;
        public bool isvip;
        public bool isSvip;
        public long uin;
    }

    public class NapCatAPIDataStatus
    {
        public int status;
        public int ext_status;
    }

    public class NapCatAPIDataMessageID
    {
        public int message_id;
    }

    public class NapCatAPIDataMessageDetail
    {
        public long self_id;
        public long user_id;
        public long time;
        public long message_id;
        public long message_seq;
        public long real_id;
        public string message_type;
        public NapCatMessageSender sender;
        public string raw_message;
        public int font;
        public string sub_type;
        public string message;
        public string message_format;
        public string post_type;
        public string message_sent_type;
        public long group_id;
    }

    public class NapCatAPIDataContact
    {
        public NapCatAPIDataMessageDetail lastestMsg; // 有趣的错拼
        public string peerUin;
        public string remark;
        public string msgTime;
        public int chatType;
        public string msgId;
        public string sendNickName;
        public string sendMemberName;
        public string peerName;
    }

    public class NapCatAPIDataEmoji
    {
        public int result;
        public string errMsg;
        public List<NapCatAPIDataEmojiDetail> emojiLikesList;
        public string cookie;
        public bool isLastPage;
        public bool isFirstPage;
    }

    public class NapCatAPIDataEmojiDetail
    {
        public string tinyId;
        public string nickName;
        public string headUrl;
    }

    public class NapCatAPIDataMessageChain
    {
        public List<NapCatAPIDataMessageDetail> messages;
    }

    public class NapCatAPIDataForwardMessage
    {
        public string content;
        public NapCatMessageSender sender;
        public long time;
    }

    public class NapCatAPIDataForwardMessageChain
    {
        public List<NapCatAPIDataForwardMessage> messages;
    }

    public class NapCatAPIDataFileDetail
    {
        public string file;
        public string url;
        public string file_size;
        public string file_name;
        public string base64;
    }

    public class NapCatAPIDataLoginInfo
    {
        public long user_id;
        public string nickname;
    }

    public class NapCatAPIDataQidianAccountInfo
    {
        public long master_id;
        public string ext_name;
        public long create_time;
    }

    public class NapCatAPIDataStrangerInfo
    {
        public long user_id;
        public string uid;
        public string uin;
        public string nickname;
        public int age;
        public string qid;
        public int qqLevel;
        public NapCatSenderSexType sex;
        public string long_nick;
        public long reg_time;
        public bool is_vip;
        public bool is_year_vip;
        public int vip_level;
        public string remark;
        public int status;
        public int login_days;
    }

    public class NapCatAPIDataFriendInfo
    {
        public long user_id;
        public string nickname;
        public string remark;
    }

    public class NapCatAPIDataGroupInfo
    {
        public long group_id;
        public string group_name;
        public string group_memo;
        public uint group_create_time;
        public uint group_level;
        public int member_count;
        public int max_member_count;
    }

    public class NapCatAPIDataGroupInfoExWrapper
    {
        public string groupCode;
        public int resultCode;
        public NapCatAPIDataGroupInfoEx extInfo;
    }

    public class NapCatAPIDataGroupInfoEx
    {
        public int groupInfoExtSeq;
        public int reserve;
        public string luckyWordId;
        public int lightCharNum;
        public string luckyWord;
        public int starId;
        public int essentialMsgSwitch;
        public int todoSeq;
        public int blacklistExpireTime;
        public int isLimitGroupRtc;
        public int companyId;
        public int hasGroupCustomPortrait;
        public string bindGuildId;
        public NapCatAPIDataGroupOwnerId groupOwnerId;
        public int essentialMsgPrivilege;
        public string msgEventSeq;
        public string inviteRobotSwitch;
        public string gangUpId;
        public int qqMusicMedalSwitch;
        public int showPlayTogetherSwitch;
        public string groupFlagPro1;
        public NapCatAPIDataGroupGuildIds groupBindGuildIds;
        public string viewedMsgDisapperTime;
        public NapCatAPIDataGroupExtFlame groupExtFlameData;
        public int gorupBindGuildSwitch;
        public string groupAioBindGuildId;
        public NapCatAPIDataGroupGuildIds groupExcludeGuildIds;
        public int fullGroupExpansionSwitch;
        public int fullGroupExpansionSeq;
        public int inviteRobotMemberSwitch;
        public int inviteRobotMemberExamine;
        public int groupSquareSwitch;
    }

    public class NapCatAPIDataGroupOwnerId
    {
        public string memberUin;
        public string memberUid;
        public string memberQid;
    }

    public class NapCatAPIDataGroupGuildIds
    {
        public List<string> guildIds;
    }

    public class NapCatAPIDataGroupExtFlame
    {
        public int switchState;
        public int state;
        public List<string> dayNums;
        public int version;
        public string updateTime;
        public bool isDisplayDayNum;
    }

    public class NapCatAPIDataGroupMember
    {
        public long group_id;
        public long user_id;
        public string nickname;
        public string card;
        public NapCatSenderSexType sex;
        public int age;
        public string area;
        public long join_time;
        public long last_sent_time;
        public string level;
        public NapCatSenderRoleType role;
        public bool unfriendly;
        public string title;
        public long title_expire_time;
        public bool card_changable;
        public long shut_up_timestamp;
    }

    public class NapCatAPIDataGroupHonor
    {
        public long group_id;
        public NapCatAPIDataGroupHonorTalkative current_talkative;
        public List<NapCatAPIDataGroupHonorObject> talkative_list;
        public List<NapCatAPIDataGroupHonorObject> performer_list;
        public List<NapCatAPIDataGroupHonorObject> legend_list;
        public List<NapCatAPIDataGroupHonorObject> strong_newbie_list;
        public List<NapCatAPIDataGroupHonorObject> emotion_list;
    }

    public class NapCatAPIDataGroupHonorTalkative
    {
        public long user_id;
        public string nickname;
        public string avatar;
        public int day_count;
    }

    public class NapCatAPIDataGroupHonorObject
    {
        public long user_id;
        public string nickname;
        public string avatar;
        public string description;
    }

    public class NapCatAPIDataCookies
    {
        public string cookies;
    }

    public class NapCatAPIDataCSRF
    {
        public string token;
    }

    public class NapCatAPIDataCredentials
    {
        public string cookies;
        public string csrf_token;
    }

    public class NapCatAPIDataPermission
    {
        public bool yes;
    }

    public class NapCatAPIDataGOCQVersionInfo
    {
        public string app_name;
        public string app_version;
        public string app_full_name;
        public string protocol_version;
        public string coolq_edition;
        public string coolq_directory;
        public bool go_NapCat = true;
        public string plugin_version;
        public string plugin_build_configuration;
        public string runtime_version;
        public string runtime_os;
        public string version;
        public int protocol;
    }

    public class NapCatAPIDataGOCQStatus
    {
        public bool app_initialized;
        public bool app_enabled;
        public bool plugins_good;
        public bool app_good;
        public bool online;
        public bool good;
        public NapCatAPIDataGOCQStatistics stat;
    }

    public class NapCatAPIDataGOCQStatistics
    {
        public ulong PacketReceived;
        public ulong PacketSent;
        public uint PacketLost;
        public ulong MessageReceived;
        public ulong MessageSent;
        public uint DisconnectTimes;
        public uint LostTimes;
        public long LastMessageTime;
    }

    public class NapCatAPIDataCNWordSlices
    {
        public List<string> slices;
    }

    public class NapCatAPIDataOCR
    {
        public List<NapCatAPIDataOCRText> texts;
        public string language;
    }

    public class NapCatAPIDataOCRText
    {
        public string text;
        public int confidence;
        public string coordinates; //Vector2

        public (double, double) Coordinates
        {
            get
            {
                string[] coord = coordinates.Replace("(", "").Replace(")", "").Split(',');
                return (double.Parse(coord[0]), double.Parse(coord[1]));
            }
        }
    }

    public class NapCatAPIDataGroupSystemMessage
    {
        public List<NapCatAPIDataInviteRequest> invited_requests;
        public List<NapCatAPIDataJoinRequest> join_requests;
    }

    public class NapCatAPIDataInviteRequest
    {
        public long request_id;
        public long invitor_uin;
        public string invitor_nick;
        public long group_id;
        public string group_name;
        public bool if_checked;
        public long actor;
    }

    public class NapCatAPIDataJoinRequest
    {
        public long request_id;
        public long requester_uin;
        public string requester_nick;
        public string message;
        public long group_id;
        public string group_name;
        public bool if_checked;
        public long actor;
    }

    public class NapCatAPIDataGroupFileSystemInfo
    {
        public int file_count;
        public int limit_count;
        public long used_space;
        public long total_space;
    }
    
    public class NapCatAPIDataGroupFile
    {
        public long group_id;
        public string file_id;
        public string file_name;
        public int busid;
        public long file_size;
        public long upload_time;
        public long dead_time;
        public long modify_time;
        public int download_times;
        public long uploader;
        public string uploader_name;
    }

    public class NapCatAPIDataGroupFolder
    {
        public long group_id;
        public string folder_id;
        public string folder_name;
        public long create_time;
        public long creator;
        public string creator_name;
        public int total_file_count;
    }

    public class NapCatAPIDataGroupFileSystemDetail
    {
        public List<NapCatAPIDataGroupFile> files;
        public List<NapCatAPIDataGroupFolder> folders;
    }

    public class NapCatAPIDataGroupFileUrl
    {
        public string url;
    }

    public class NapCatAPIDataGroupAtAllRemain
    {
        public bool can_at_all;
        public short remain_at_all_count_for_group;
        public short remain_at_all_count_for_uin;
    }

    public class NapCatAPIDataGroupNotice
    {
        public long sender_id;
        public long publish_time;
        public NapCatAPIDataGroupNoticeMessage message;
    }

    public class NapCatAPIDataGroupNoticeMessage
    {
        public string text;
        public NapCatAPIDataGroupNoticeImage image;
    }

    public class NapCatAPIDataGroupNoticeImage
    {
        public uint height;
        public uint width;
        public string id;
    }

    public class NapCatAPIDataClient
    {
        public List<NapCatAPIDataDevice> clients;
    }

    public class NapCatAPIDataDevice
    {
        public long app_id;
        public string device_name;
        public string device_kind;
    }

    public class NapCatAPIDataEssence
    {
        public long sender_id;
        public string sender_nick;
        public long sender_time;
        public long operator_id;
        public string operator_nick;
        public long operator_time;
        public long message_id;
    }
}
