using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reQuest.Backend.DataObjects
{
    public class Competency : EntityData
    {
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string Description { get; set; }
        public bool Locked { get; set; }
    }
}