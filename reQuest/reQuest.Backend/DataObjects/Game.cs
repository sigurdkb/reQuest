using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reQuest.Backend.DataObjects
{
    public class Game : EntityData
    {
        public IList<Player> Players { get; set; } = new List<Player>();
        public string Target { get; set; }
        public string Player { get; set; }

    }
}