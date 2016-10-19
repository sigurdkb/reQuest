using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reQuest.Models
{
    class LocationData
    {
        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }
        [JsonProperty(PropertyName = "logitude")]
        public double Longitude { get; set; }
        [JsonProperty(PropertyName = "distance")]
        public double Distance { get; set; }
        [JsonProperty(PropertyName = "beaconUUID")]
        public string BeaconUUID { get; set; }
        [JsonProperty(PropertyName = "beaconID")]
        public string BeaconID { get; set; }

    }
}
