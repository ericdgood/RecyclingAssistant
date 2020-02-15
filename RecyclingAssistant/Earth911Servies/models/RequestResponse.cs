using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecyclingAssistant.Earth911Servies.models
{
    public class RequestResponse<t>
    {
        public RequestResponse()
        {
        }

        public string search_time { get; set; }
        public int num_results { get; set; }
        public t result { get; set; }
    }
}
