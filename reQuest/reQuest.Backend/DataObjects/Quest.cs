using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reQuest.Backend.DataObjects
{
    public class Quest : EntityData
    {
        public string OwnerId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TopicId { get; set; }
        public TimeSpan Timeout { get; set; }
        public string AvtivePlayerIds { get; set; }
        public string PassivePlayerIds { get; set; }

    }
}