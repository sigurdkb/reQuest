using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reQuest.Backend.DataObjects
{
    public class Player : EntityData
    {
        public string ExternalId { get; set; }
        public string Competencies { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

    }
}