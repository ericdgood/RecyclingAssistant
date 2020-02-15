using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecyclingAssistant.Earth911Servies.models
{
    public class RecycProgramDetails
    {
        public RecycProgramDetails()
        {
        }

        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postal_code { get; set; }
        public string hours { get; set; }
        public string url { get; set; }
        public string notes { get; set; }
        public string phone { get; set; }
        public string description { get; set; }
        public List<RecycProgramMaterials> materials { get; set; }
    }
}
