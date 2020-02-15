using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecyclingAssistant.Earth911Servies.models
{
    public class RequestResponceDictionary<t>
    {
        public RequestResponceDictionary()
        {
        }

        public string search_time { get; set; }
        public int num_results { get; set; }
        public Dictionary<string, t> result { get; set; }
    }

}
