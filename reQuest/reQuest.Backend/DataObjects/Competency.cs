using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reQuest.Backend.DataObjects
{
    public class Competency : EntityData
    {
        public string TopicId { get; set; }
        public double Score { get; set; }
    }
}