using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reQuest.Backend.DataObjects
{
    public class Team : EntityData
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public double Score { get; set; }
        public string PlayerIds { get; set; }

    }
}