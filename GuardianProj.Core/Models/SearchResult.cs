using MvvmCross.ViewModels;
using Newtonsoft.Json;

namespace GuardianProj.Core.Models
{

    public class SearchResult
    {
        [JsonProperty(PropertyName = "response")]
        public SearchResponse SearchResponse { get; set; }
    }

}
