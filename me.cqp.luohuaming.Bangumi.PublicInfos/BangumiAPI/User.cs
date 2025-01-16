using me.cqp.luohuaming.Bangumi.PublicInfos.Models;
using Newtonsoft.Json;
using System;

namespace me.cqp.luohuaming.Bangumi.PublicInfos.BangumiAPI
{
    public class User
    {
        public User(int userId, string token)
        {
            UserID = userId;
            AccessToken = token;
        }

        public int UserID { get; set; }

        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "avatar")]
        public Subject_Images Avatar { get; set; }

        [JsonProperty(PropertyName = "sign")]
        public string Sign { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [JsonProperty(PropertyName = "nickname")]
        public string Nickname { get; set; }

        [JsonProperty(PropertyName = "user_group")]
        public int UserGroup { get; set; }

        [JsonProperty(PropertyName = "reg_time")]
        public DateTime RegTime { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "time_offset")]
        public int TimeOffset { get; set; }

        public UserSubject[] WishSubjects { get; set; } = [];

        public UserSubject[] CollectSubjects { get; set; } = [];

        public UserSubject[] DoingSubjects { get; set; } = [];

        public UserSubject[] HoldOnSubjects { get; set; } = [];

        public UserSubject[] DroppedSubjects { get; set; } = [];

        public bool FetchUserInfo()
        {
            var json = CommonHelper.Get($"v0/me", AccessToken);
            if (json == null)
            {
                return false;
            }
            JsonConvert.PopulateObject(json, this);
            return !string.IsNullOrEmpty(Username);
        }

        public bool FetchUserSubjects(SubjectType subjectType = SubjectType.Anime, CollectionType collectionType = CollectionType.Doing, int pageSize = 100, int pageOffset = 0)
        {
            var json = CommonHelper.Get($"v0/users/{UserID}/collections?subject_type={(int)subjectType}&type={(int)collectionType}&limit={pageSize}&offset={pageOffset}", AccessToken);
            if (json == null)
            {
                return false;
            }
            switch (collectionType)
            {
                case CollectionType.Wish:
                    JsonConvert.PopulateObject(json, this.WishSubjects);
                    Array.ForEach(WishSubjects, x => x.Subject.CollectionType = CollectionType.Wish);
                    return WishSubjects.Length > 0;

                case CollectionType.Collect:
                    JsonConvert.PopulateObject(json, this.CollectSubjects);
                    Array.ForEach(CollectSubjects, x => x.Subject.CollectionType = CollectionType.Collect);
                    return CollectSubjects.Length > 0;

                case CollectionType.Doing:
                    JsonConvert.PopulateObject(json, this.DoingSubjects);
                    Array.ForEach(DoingSubjects, x => x.Subject.CollectionType = CollectionType.Doing);
                    return DoingSubjects.Length > 0;

                case CollectionType.OnHold:
                    JsonConvert.PopulateObject(json, this.HoldOnSubjects);
                    Array.ForEach(HoldOnSubjects, x => x.Subject.CollectionType = CollectionType.OnHold);
                    return HoldOnSubjects.Length > 0;

                case CollectionType.Dropped:
                    JsonConvert.PopulateObject(json, this.DroppedSubjects);
                    Array.ForEach(DroppedSubjects, x => x.Subject.CollectionType = CollectionType.Dropped);
                    return DroppedSubjects.Length > 0;

                default:
                    return false;
            }
        }

        public UserSubject? FetchUserSubjectByID(int id)
        {
            var json = CommonHelper.Get($"v0/users/{UserID}/collections/{id}", AccessToken);
            if (json == null)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UserSubject>(json);
        }
    }
}