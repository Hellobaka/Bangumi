using me.cqp.luohuaming.Bangumi.PublicInfos.BangumiAPI;
using Newtonsoft.Json;
using System;

namespace me.cqp.luohuaming.Bangumi.PublicInfos.Models
{
    public enum SubjectType
    {
        Book = 1,

        Anime = 2,

        Music = 3,

        Game = 4,

        Sanjigen = 6,
    }

    public enum CollectionType
    {
        Wish = 1,

        Collect = 2,

        Doing = 3,

        OnHold = 4,

        Dropped = 5,
    }

    public class Subject_Images
    {
        [JsonProperty(PropertyName = "large")]
        public string Large { get; set; }

        [JsonProperty(PropertyName = "common")]
        public string? Common { get; set; }

        [JsonProperty(PropertyName = "medium")]
        public string Medium { get; set; }

        [JsonProperty(PropertyName = "small")]
        public string Small { get; set; }

        [JsonProperty(PropertyName = "grid")]
        public string Grid { get; set; }
    }

    public class Subject_Rating
    {
        [JsonProperty(PropertyName = "rank")]
        public int Rank { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int[] Count { get; set; } = new int[10];

        [JsonProperty(PropertyName = "score")]
        public double Score { get; set; }
    }

    public class Subject_Collection
    {
        [JsonProperty(PropertyName = "wish")]
        public int Wish { get; set; }

        [JsonProperty(PropertyName = "collect")]
        public int Collect { get; set; }

        [JsonProperty(PropertyName = "doing")]
        public int Doing { get; set; }

        [JsonProperty(PropertyName = "on_hold")]
        public int OnHold { get; set; }

        [JsonProperty(PropertyName = "dropped")]
        public int Dropped { get; set; }
    }

    public class Subject_Tag
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }

    public class Subject_Person
    {
        [JsonProperty(PropertyName = "images")]
        public Subject_Images Images { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "relation")]
        public string Relation { get; set; }

        [JsonProperty(PropertyName = "short_summary")]
        public string ShortSummary { get; set; }

        [JsonProperty(PropertyName = "career")]
        public string[] Career { get; set; }

        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "eps")]
        public string Eps { get; set; }

        [JsonProperty(PropertyName = "locked")]
        public bool Locked { get; set; }

        [JsonProperty(PropertyName = "actors")]
        public Subject_Person[] Actors { get; set; }
    }

    public class Subject_Relation
    {
        public Subject_Images Images { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "name_cn")]
        public string NameCN { get; set; }

        [JsonProperty(PropertyName = "relation")]
        public string Relation { get; set; }

        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
    }

    public class Episode
    {
        [JsonProperty(PropertyName = "airdate")]
        public string AirDate { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "name_cn")]
        public string NameCn { get; set; }

        [JsonProperty(PropertyName = "duration")]
        public string Duration { get; set; }

        [JsonProperty(PropertyName = "desc")]
        public string Desc { get; set; }

        /// <summary>
        /// 条目内的集数, 从1开始。非本篇剧集的此字段无意义
        /// </summary>
        [JsonProperty(PropertyName = "ep")]
        public int Ep { get; set; }

        /// <summary>
        /// 同类条目的排序和集数
        /// </summary>
        [JsonProperty(PropertyName = "sort")]
        public int Sort { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public int Comment { get; set; }

        [JsonProperty(PropertyName = "type")]
        public int Type { get; set; }

        [JsonProperty(PropertyName = "disc")]
        public int Disc { get; set; }

        [JsonProperty(PropertyName = "duration_seconds")]
        public int DurationSeconds { get; set; }

        public CollectionType CollectionType { get; set; }
    }

    public class UserSubject
    {
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime updated_at { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string? Comment { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public string[] Tags { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public Subject Subject { get; set; }

        [JsonProperty(PropertyName = "subject_id")]
        public int SubjectID { get; set; }

        [JsonProperty(PropertyName = "vol_status")]
        public int VolStatus { get; set; }

        [JsonProperty(PropertyName = "ep_status")]
        public int EpStatus { get; set; }

        [JsonProperty(PropertyName = "subject_type")]
        public SubjectType SubjectType { get; set; }

        [JsonProperty(PropertyName = "type")]
        public CollectionType Type { get; set; }

        [JsonProperty(PropertyName = "rate")]
        public double Rate { get; set; }

        [JsonProperty(PropertyName = "private")]
        public int Private { get; set; }
    }
}