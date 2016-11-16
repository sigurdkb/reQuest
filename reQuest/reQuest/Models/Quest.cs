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
        [JsonProperty(PropertyName = "owner")]
        public Player Owner { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "topic")]
        public Topic Topic { get; set; }
        [JsonProperty(PropertyName = "timeLimit")]
        public TimeSpan TimeLimit { get; set; }

		//public string ImagePath { get; set; }


    }
}
