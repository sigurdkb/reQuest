using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reQuest.iOS
{
    public class LocationData : EventArgs, ILocationData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }
        public string BeaconUUID { get; set; }
        public string BeaconID { get; set; }

    }
}
