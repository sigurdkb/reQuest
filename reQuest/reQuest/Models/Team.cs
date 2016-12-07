using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using reQuest.Models;

namespace reQuest
{
	public class Team
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned by azure mobile services.</value>
		[JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name of the team.</value>
		[JsonProperty(PropertyName = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>A description of the team</value>
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the playerids.
		/// </summary>
		/// <value>JSON serialized List<string> containing the ids of the team's players.</value>
		[JsonProperty(PropertyName = "playerIds")]
		public string PlayerIds { get; set; } = "";

	}
}
