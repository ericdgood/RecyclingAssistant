using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecyclingAssistant.Earth911Servies.models
{
    public class RecycProgramMaterials
    {
        public RecycProgramMaterials()
        {
        }

        public bool dropoff { get; set; }
        public string description { get; set; }
        public bool business { get; set; }
        public bool residential { get; set; }
        public string notes { get; set; }
        public int material_id { get; set; }
        public bool pickup { get; set; }
    }
}
