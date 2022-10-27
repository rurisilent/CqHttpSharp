using System;
using System.Collections.Generic;
using System.Text;
using CqHttpSharp.Enum;

namespace CqHttpSharp.Data
{
    public class CqHttpAPIDataMessageID
    {
        public int message_id;
    }

    public class CqHttpAPIDataMessageDetail
    {
        public int message_id;
        public int real_id;
        public CqHttpMessageSender sender;
        public long time;
        public string message;
        public string raw_message;
    }

    public class CqHttpAPIDataMessageChain
    {
        public List<CqHttpAPIDataMessageDetail> messages;
    }

    public class CqHttpAPIDataForwardMessage
    {
        public string content;
        public CqHttpMessageSender sender;
        public long time;
    }

    public class CqHttpAPIDataForwardMessageChain
    {
        public List<CqHttpAPIDataForwardMessage> messages;
    }

    public class CqHttpAPIDataImageDetail
    {
        public int size;
        public string filename;
        public string url;
    }

    public class CqHttpAPIDataLoginInfo
    {
        public long user_id;
        public string nickname;
    }

    public class CqHttpAPIDataQidianAccountInfo
    {
        public long master_id;
        public string ext_name;
        public long create_time;
    }

    public class CqHttpAPIDataStrangerInfo
    {
        public long user_id;
        public string nickname;
        public CqHttpSenderSexType sex;
        public int age;
        public string qid;
        public int level;
        public int login_days;
    }

    public class CqHttpAPIDataFriendInfo
    {
        public long user_id;
        public string nickname;
        public string remark;
    }

    public class CqHttpAPIDataGroupInfo
    {
        public long group_id;
        public string group_name;
        public string group_memo;
        public uint group_create_time;
        public uint group_level;
        public int member_count;
        public int max_member_count;
    }

    public class CqHttpAPIDataGroupMember
    {
        public long group_id;
        public long user_id;
        public string nickname;
        public string card;
        public CqHttpSenderSexType sex;
        public int age;
        public string area;
        public long join_time;
        public long last_sent_time;
        public string level;
        public CqHttpSenderRoleType role;
        public bool unfriendly;
        public string title;
        public long title_expire_time;
        public bool card_changable;
        public long shut_up_timestamp;
    }

    public class CqHttpAPIDataGroupHonor
    {
        public long group_id;
        public CqHttpAPIDataGroupHonorTalkative current_talkative;
        public List<CqHttpAPIDataGroupHonorObject> talkative_list;
        public List<CqHttpAPIDataGroupHonorObject> performer_list;
        public List<CqHttpAPIDataGroupHonorObject> legend_list;
        public List<CqHttpAPIDataGroupHonorObject> strong_newbie_list;
        public List<CqHttpAPIDataGroupHonorObject> emotion_list;
    }

    public class CqHttpAPIDataGroupHonorTalkative
    {
        public long user_id;
        public string nickname;
        public string avatar;
        public int day_count;
    }

    public class CqHttpAPIDataGroupHonorObject
    {
        public long user_id;
        public string nickname;
        public string avatar;
        public string description;
    }

    public class CqHttpAPIDataCookies
    {
        public string cookies;
    }

    public class CqHttpAPIDataCSRF
    {
        public string token;
    }

    public class CqHttpAPIDataCredentials
    {
        public string cookies;
        public string csrf_token;
    }

    public class CqHttpAPIDataRecordFile
    {
        public string file;
    }

    public class CqHttpAPIDataPermission
    {
        public bool yes;
    }

    public class CqHttpAPIDataGOCQVersionInfo
    {
        public string app_name;
        public string app_version;
        public string app_full_name;
        public string protocol_version;
        public string coolq_edition;
        public string coolq_directory;
        public bool go_cqhttp = true;
        public string plugin_version;
        public string plugin_build_configuration;
        public string runtime_version;
        public string runtime_os;
        public string version;
        public int protocol;
    }

    public class CqHttpAPIDataGOCQStatus
    {
        public bool app_initialized;
        public bool app_enabled;
        public bool plugins_good;
        public bool app_good;
        public bool online;
        public bool good;
        public CqHttpAPIDataGOCQStatistics stat;
    }

    public class CqHttpAPIDataGOCQStatistics
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

    public class CqHttpAPIDataCNWordSlices
    {
        public List<string> slices;
    }

    public class CqHttpAPIDataOCR
    {
        public List<CqHttpAPIDataOCRText> texts;
        public string language;
    }

    public class CqHttpAPIDataOCRText
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

    public class CqHttpAPIDataGroupSystemMessage
    {
        public List<CqHttpAPIDataInviteRequest> invited_requests;
        public List<CqHttpAPIDataJoinRequest> join_requests;
    }

    public class CqHttpAPIDataInviteRequest
    {
        public long request_id;
        public long invitor_uin;
        public string invitor_nick;
        public long group_id;
        public string group_name;
        public bool if_checked;
        public long actor;
    }

    public class CqHttpAPIDataJoinRequest
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

    public class CqHttpAPIDataGroupFileSystemInfo
    {
        public int file_count;
        public int limit_count;
        public long used_space;
        public long total_space;
    }
    
    public class CqHttpAPIDataGroupFile
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

    public class CqHttpAPIDataGroupFolder
    {
        public long group_id;
        public string folder_id;
        public string folder_name;
        public long create_time;
        public long creator;
        public string creator_name;
        public int total_file_count;
    }

    public class CqHttpAPIDataGroupFileSystemDetail
    {
        public List<CqHttpAPIDataGroupFile> files;
        public List<CqHttpAPIDataGroupFolder> folders;
    }

    public class CqHttpAPIDataGroupFileUrl
    {
        public string url;
    }

    public class CqHttpAPIDataGroupAtAllRemain
    {
        public bool can_at_all;
        public short remain_at_all_count_for_group;
        public short remain_at_all_count_for_uin;
    }

    public class CqHttpAPIDataGroupNotice
    {
        public long sender_id;
        public long publish_time;
        public CqHttpAPIDataGroupNoticeMessage message;
    }

    public class CqHttpAPIDataGroupNoticeMessage
    {
        public string text;
        public CqHttpAPIDataGroupNoticeImage image;
    }

    public class CqHttpAPIDataGroupNoticeImage
    {
        public uint height;
        public uint width;
        public string id;
    }

    public class CqHttpAPIDataClient
    {
        public List<CqHttpAPIDataDevice> clients;
    }

    public class CqHttpAPIDataDevice
    {
        public long app_id;
        public string device_name;
        public string device_kind;
    }

    public class CqHttpAPIDataEssence
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
