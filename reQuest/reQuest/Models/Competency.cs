using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reQuest.Models
{
    public class Competency
    {
        public Topic Topic { get; set; }
        public double Score { get; set; }
    }
}