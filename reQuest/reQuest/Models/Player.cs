using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using reQuest.Models;

namespace reQuest
{
	public class Player
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned by azure mobile services.</value>
		[JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

		/// <summary>
		/// Gets or sets the external identifier.
		/// </summary>
		/// <value>The identifier from an external authentication source.</value>
		[JsonProperty(PropertyName = "externalId")]
		public string ExternalId { get; set; }

		/// <summary>
		/// Gets or sets the team identifier.
		/// </summary>
		/// <value>The id string of the players team.</value>
		[JsonProperty(PropertyName = "teamId")]
		public string TeamId { get; set; }

		/// <summary>
		/// Gets or sets the competencies.
		/// </summary>
		/// <value>JSON serialized List<string> containing the ids of the players competencies.</value>
		[JsonProperty(PropertyName = "competencies")]
		public string Competencies { get; set; }

		/// <summary>
		/// Gets or sets the longitude.
		/// </summary>
		/// <value>The longitude part of the players position.</value>
		[JsonProperty(PropertyName = "longitude")]
		public double Longitude { get; set; }

		/// <summary>
		/// Gets or sets the latitude.
		/// </summary>
		/// <value>The latitude part of the players position.</value>
		[JsonProperty(PropertyName = "latitude")]
		public double Latitude { get; set; }

	}
}
