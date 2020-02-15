using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecyclingAssistant.Earth911Servies.models
{
    public class Provider
    {
        public Provider()
        {
        }

        public bool curbside { get; set; }
        public string description { get; set; }
        public double distnce { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public int location_type_id { get; set; }
        public bool municipal { get; set; }

    }
}
