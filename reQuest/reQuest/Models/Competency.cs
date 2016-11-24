using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reQuest.Models
{
    public class Competency
    {
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned by azure mobile services.</value>
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the topic identifier.
		/// </summary>
		/// <value>The id string of the associated topic.</value>
		[JsonProperty(PropertyName = "topicId")]
		public string TopicId { get; set; }

		/// <summary>
		/// Gets or sets the score.
		/// </summary>
		/// <value>The score optained in this topic</value>
		[JsonProperty(PropertyName = "score")]
        public double Score { get; set; }
    }
}