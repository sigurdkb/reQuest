using Newtonsoft.Json;

namespace reQuest.Models
{
    public class Topic
    {
        [JsonProperty(PropertyName = "shortname")]
        public string ShortName { get; set; }

        [JsonProperty(PropertyName = "longname")]
        public string LongName { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "locked")]
        public bool Locked { get; set; }
    }
}