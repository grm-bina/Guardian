using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace GuardianProj.Core.Models
{
    public class SectionsSearchResponsce
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "userTier")]
        public string UserTier { get; set; }
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "results")]
        public Section[] Sections { get; set; }

    }
}
