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
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned by azure mobile services.</value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

		/// <summary>
		/// Gets or sets the ownerid
		/// </summary>
		/// <value>The id string of the quest owner.</value>
        [JsonProperty(PropertyName = "ownerId")]
		public string OwnerId { get; set; }

		/// <summary>
		/// Gets or sets the title
		/// </summary>
		/// <value>The quest title.</value>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the description
		/// </summary>
		/// <value>A textual description of the quest</value>
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the topicid
		/// </summary>
		/// <value>The id string of the topic associated with this quest.</value>
        [JsonProperty(PropertyName = "topicId")]
		public string TopicId { get; set; }

		/// <summary>
		/// Gets or sets the timeout
		/// </summary>
		/// <value>The time remaining to access this quest.</value>
        [JsonProperty(PropertyName = "timeout")]
        public TimeSpan Timeout { get; set; }

		/// <summary>
		/// Gets or sets the activeplayerids
		/// </summary>
		/// <value>JSON serialized List<string> containing the ids of the active players.</value>
		[JsonProperty(PropertyName = "activePlayerIds")]
		public string ActivePlayerIds { get; set; }

		/// <summary>
		/// Gets or sets the passiveplayerids
		/// </summary>
		/// <value>JSON serialized List<string> containing the ids of the passive players.</value>
		[JsonProperty(PropertyName = "passivePlayerIds")]
		public string PassivePlayerIds { get; set; }

    }
}
