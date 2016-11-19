using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reQuest.Models
{
    public class Quest
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "ownerId")]
		public string OwnerId { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "topicId")]
		public string TopicId { get; set; }
        [JsonProperty(PropertyName = "timeLimit")]
        public TimeSpan TimeLimit { get; set; }

		//public string ImagePath { get; set; }


    }
}
