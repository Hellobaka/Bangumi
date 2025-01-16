using me.cqp.luohuaming.Bangumi.PublicInfos.Models;
using System.Collections.Generic;

namespace me.cqp.luohuaming.Bangumi.PublicInfos
{
    public class AppConfig : ConfigBase
    {
        public AppConfig(string path)
            : base(path)
        {
            LoadConfig();
            Instance = this;
        }

        public static AppConfig Instance { get; private set; }

        public static List<UserAPIKey> APIKeys { get; set; } = [];
        
        public static List<SubjectAlias> SubjectAliases { get; set; } = [];

        public static bool EnableNSFW { get; set; }

        public static List<int> FilterSubjects { get; set; } = [];

        public static string CommandUpdateEpisode { get; set; } = "#番剧看过";

        public static string CommandUpdateEpisodeUpTo { get; set; } = "#番剧看到";

        public static string CommandSubjectDetail { get; set; } = "#番剧详情";

        public static string CommandSearch { get; set; } = "#番剧搜索";

        public static string CommandCollectSubject { get; set; } = "#番剧收藏";

        public static string CommandMenu { get; set; } = "#番剧菜单";

        public static string CommandCalendar { get; set; } = "#每日番剧";

        public static string CommandMyCollect { get; set; } = "#我的追番";

        public static string CommandMenuContent { get; set; } = "";

        public override void LoadConfig()
        {
            APIKeys = GetConfig("APIKeys", new List<UserAPIKey>());
            SubjectAliases = GetConfig("SubjectAliases", new List<SubjectAlias>());
            FilterSubjects = GetConfig("FilterSubjects", new List<int>());
            EnableNSFW = GetConfig("EnableNSFW", false);
            CommandUpdateEpisode = GetConfig("CommandUpdateEpisode", "#番剧看过");
            CommandUpdateEpisodeUpTo = GetConfig("CommandUpdateEpisodeUpTo", "#番剧看到");
            CommandSubjectDetail = GetConfig("CommandSubjectDetail", "#番剧详情");
            CommandSearch = GetConfig("CommandSearch", "#番剧搜索");
            CommandCollectSubject = GetConfig("CommandCollectSubject", "#番剧收藏");
            CommandMenu = GetConfig("CommandMenu", "#番剧菜单");
            CommandCalendar = GetConfig("CommandCalendar", "#每日番剧");
            CommandMyCollect = GetConfig("CommandMyCollect", "#我的追番");
            CommandMenuContent = GetConfig("CommandMenuContent", "");
        }
    }
}