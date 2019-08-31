using Newtonsoft.Json;
using System;
using System.Text;

namespace GuardianProj.Core.Models
{
    public class StoryHeaderAdditionalFields
    {
        [JsonProperty(PropertyName = "trailText")]
        public string TrailText { get; set; }
        [JsonProperty(PropertyName = "thumbnail")]
        public string Thumbnail { get; set; }


        public void CleanTrailTextFromHtmlTags(string tagName)
        {
            TrailText=TrailText.Replace(ConvertTagNameToOpenTag(tagName), String.Empty);
            TrailText = TrailText.Replace(ConvertTagNameToCloseTag(tagName), String.Empty);

        }

        private string ConvertTagNameToOpenTag(string tagName)
        {
            StringBuilder t = new StringBuilder();
            t.Append("<");
            t.Append(tagName);
            t.Append(">");
            return t.ToString();
        }

        private string ConvertTagNameToCloseTag(string tagName)
        {
            StringBuilder t = new StringBuilder();
            t.Append("</");
            t.Append(tagName);
            t.Append(">");
            return t.ToString();

        }

    }
}

