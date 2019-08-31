using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace GuardianProj.Core.Models
{
    public class Section
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "webTitle")]
        public string WebTitle { get; set; }
        public string WebUrl { get; set; }
        [JsonProperty(PropertyName = "apiUrl")]
        public string ApiUrl { get; set; }

    }
}
