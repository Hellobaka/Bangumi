using me.cqp.luohuaming.Bangumi.PublicInfos.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace me.cqp.luohuaming.Bangumi.PublicInfos.BangumiAPI
{
    public class Subject
    {
        public Subject(int subjectID, string token)
        {
            SubjectID = subjectID;
            AccessToken = token;
        }

        #region 成员

        public int SubjectID { get; set; }

        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string SubjectName { get; set; }

        [JsonProperty(PropertyName = "name_cn")]
        public string SubjectName_CN { get; set; }

        [JsonProperty(PropertyName = "type")]
        public SubjectType SubjectType { get; set; }

        [JsonProperty(PropertyName = "summary")]
        public string Summary { get; set; }

        [JsonProperty(PropertyName = "series")]
        public bool Series { get; set; }

        [JsonProperty(PropertyName = "nsfw")]
        public bool NSFW { get; set; }

        [JsonProperty(PropertyName = "locked")]
        public bool Locked { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }

        [JsonProperty(PropertyName = "images")]
        public Subject_Images Image { get; set; }

        [JsonProperty(PropertyName = "volumes")]
        public int Volumes { get; set; }

        [JsonProperty(PropertyName = "eps")]
        public int Eps { get; set; }

        [JsonProperty(PropertyName = "total_episodes")]
        public int TotalEpisodes { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public Subject_Rating Rating { get; set; }

        [JsonProperty(PropertyName = "collection")]
        public Subject_Collection Collection { get; set; }

        [JsonProperty(PropertyName = "tags")]
        public List<Subject_Tag> Tags { get; set; }

        [JsonProperty(PropertyName = "score")]
        public double? Score { get; set; }

        [JsonProperty(PropertyName = "collection_total")]
        public double? CollectionTotal { get; set; }

        [JsonProperty(PropertyName = "rank")]
        public double? Rank { get; set; }

        public List<Subject_Person> Persons { get; set; } = [];

        public List<Subject_Person> Characters { get; set; } = [];

        public List<Subject_Relation> Relations { get; set; } = [];

        public List<Episode> Episodes { get; set; } = [];

        public CollectionType CollectionType { get; set; }

        #endregion 成员

        public bool FetchInfo()
        {
            var json = CommonHelper.Get($"v0/subjects/{SubjectID}", AccessToken);
            if (json == null)
            {
                return false;
            }
            JsonConvert.PopulateObject(json, this);
            return !string.IsNullOrEmpty(SubjectName);
        }

        public bool FetchPersons()
        {
            var json = CommonHelper.Get($"v0/subjects/{SubjectID}/persons", AccessToken);
            if (json == null)
            {
                return false;
            }
            JsonConvert.PopulateObject(json, this.Persons);
            return Persons.Count != 0;
        }

        public bool FetchCharacters()
        {
            var json = CommonHelper.Get($"v0/subjects/{SubjectID}/characters", AccessToken);
            if (json == null)
            {
                return false;
            }
            JsonConvert.PopulateObject(json, this.Characters);
            return Characters.Count != 0;
        }

        public bool FetchRelations()
        {
            var json = CommonHelper.Get($"v0/subjects/{SubjectID}/subjects", AccessToken);
            if (json == null)
            {
                return false;
            }
            JsonConvert.PopulateObject(json, this.Relations);
            return Relations.Count != 0;
        }

        public bool FetchEpisodes(int pageSize = 100, int pageOffset = 0)
        {
            var json = CommonHelper.Get($"v0/episodes?subject_id={SubjectID}&type=0&limit={pageSize}&offset={pageOffset}", AccessToken);
            if (json == null)
            {
                return false;
            }
            JsonConvert.PopulateObject(json, this.Episodes);
            if (Episodes.Count != 0)
            {
                json = CommonHelper.Get($"v0/users/-/collections/{SubjectID}/episodes?episode_type=0&limit={pageSize}&offset={pageOffset}", AccessToken);
                if (json == null)
                {
                    // 无法拉取用户章节收藏信息
                    return true;
                }
                var j = JObject.Parse(json);
                foreach (var item in j["data"] as JArray)
                {
                    var epId = (int)item["episode"]["id"];
                    var ep = Episodes.FirstOrDefault(x => x.Id == epId);
                    if (ep == null)
                    {
                        continue;
                    }
                    ep.CollectionType = (CollectionType)(int)item["type"];
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateEpStatus(List<Episode> eps, CollectionType collectionType = CollectionType.Collect)
        {
            return UpdateEpStatus(eps.Select(x => x.Id).ToList(), collectionType);
        }

        public bool UpdateEpStatus(List<int> eps, CollectionType collectionType = CollectionType.Collect)
        {
            var json = CommonHelper.Post("PATCH", $"v0/users/-/collections/{SubjectID}/episodes"
                , $"{{\"episode_id\":[{string.Join(",", eps)}],\"type\":{(int)collectionType}}}"
                , AccessToken);
            if (json == null)
            {
                return false;
            }
            foreach (var item in eps)
            {
                var ep = Episodes.FirstOrDefault(x => x.Id == item);
                if (ep == null)
                {
                    continue;
                }
                ep.CollectionType = collectionType;
            }
            return true;
        }

        public bool UpdateCollectType(CollectionType collectionType)
        {
            var json = CommonHelper.Post("POST", $"v0/users/-/collections/{SubjectID}"
                , $"{{\"type\":{(int)collectionType}}}"
                , AccessToken);
            if (json == null)
            {
                return false;
            }
            CollectionType = collectionType;
            return true;
        }

        public bool UpdateRate(int score, string comment)
        {
            var json = CommonHelper.Post("POST", $"v0/users/-/collections/{SubjectID}"
                , $"{{\"rate\":{score},\"comment\":{comment}}}"
                , AccessToken);
            if (json == null)
            {
                return false;
            }
            Score = score;
            return true;
        }
    }
}