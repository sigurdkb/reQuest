using Newtonsoft.Json;

namespace reQuest.Models
{
    public class Topic
    {
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier assigned by azure mobile services.</value>
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the short name.
		/// </summary>
		/// <value>The short name of the topic, typically the course code.</value>
        [JsonProperty(PropertyName = "shortName")]
        public string ShortName { get; set; }

		/// <summary>
		/// Gets or sets the long name.
		/// </summary>
		/// <value>The long name of the topic.</value>
        [JsonProperty(PropertyName = "longName")]
        public string LongName { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>A description of the topic.</value>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:reQuest.Models.Topic"/> is locked.
		/// Indicates whether or not the course is imported from external source.
		/// </summary>
		/// <value><c>true</c> if imported; otherwise, <c>false</c>.</value>
        [JsonProperty(PropertyName = "locked")]
        public bool Locked { get; set; }
    }
}