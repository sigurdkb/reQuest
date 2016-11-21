using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using reQuest.Models;

namespace reQuest
{
	public class Game
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned by azure mobile services.</value>
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the quest identifier.
		/// </summary>
		/// <value>The quest identifier associated to this game.</value>
		[JsonProperty(PropertyName = "questId")]
		public string QuestId { get; set; }

		/// <summary>
		/// Gets or sets the players string.
		/// </summary>
		/// <value>JSON serialized List<string> containing the ids of active players.</value>
		[JsonProperty(PropertyName = "playerIds")]
		public string PlayersIds { get; set; }

		/// <summary>
		/// Gets or sets the participants string.
		/// </summary>
		/// <value>JSON serialized List<string> containing the ids of passive players</value>
		[JsonProperty(PropertyName = "participantIds")]
		public string ParticipantIds { get; set; }

	}
}