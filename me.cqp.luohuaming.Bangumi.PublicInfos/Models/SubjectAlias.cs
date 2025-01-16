using System.Collections.Generic;

namespace me.cqp.luohuaming.Bangumi.PublicInfos.Models
{
    public class SubjectAlias
    {
        public int SubjectID { get; set; }

        public string SubjectName { get; set; }

        public List<string> Aliases { get; set; } = [];
    }
}