using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reQuest.Backend.DataObjects
{
    public class Player : EntityData
    {
        public Player()
        {
            Competencies = new List<Competency>();
        }
        public string ExternalId { get; set; }
        public IList<Competency> Competencies { get; set; }
        public double Score { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

    }
}