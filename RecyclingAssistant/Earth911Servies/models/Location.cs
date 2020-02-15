using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecyclingAssistant.Earth911Servies.models
{
    public class Location
    {
        public Location()
        {
        }

        public string provence { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string longitude { get; set; }
        public string postal_code { get; set; }
        public string latitude { get; set; }
    }
}
