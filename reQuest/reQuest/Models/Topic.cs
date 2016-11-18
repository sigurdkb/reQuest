using Newtonsoft.Json;

namespace reQuest.Models
{
    public class Topic
    {
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }
        [JsonProperty(PropertyName = "shortName")]
        public string ShortName { get; set; }
        [JsonProperty(PropertyName = "longName")]
        public string LongName { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "locked")]
        public bool Locked { get; set; }
    }
}