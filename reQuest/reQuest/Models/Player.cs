using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using reQuest.Models;

namespace reQuest
{
	public class Player
	{
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
		[JsonProperty(PropertyName = "externalId")]
		public string ExternalId { get; set; }
		[JsonProperty(PropertyName = "competencies")]
		public string Competencies { get; set; }
		[JsonProperty(PropertyName = "score")]
		public double Score { get; set; }
		[JsonProperty(PropertyName = "longitude")]
		public double Longitude { get; set; }
		[JsonProperty(PropertyName = "latitude")]
		public double Latitude { get; set; }

	}
}
