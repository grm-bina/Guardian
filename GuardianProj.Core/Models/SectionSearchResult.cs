using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace GuardianProj.Core.Models
{
    class SectionSearchResult
    {
        [JsonProperty(PropertyName = "response")]
        public SectionsSearchResponsce SearchResponse { get; set; }

    }
}
