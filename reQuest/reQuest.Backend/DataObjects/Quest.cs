using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reQuest.Backend.DataObjects
{
    public class Quest : EntityData
    {
        public Player Owner { get; set; }
        public string Title { get; set; }
        public Topic Topic { get; set; }
        public TimeSpan TimeLimit { get; set; }
    }
}