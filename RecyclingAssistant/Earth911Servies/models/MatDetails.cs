using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecyclingAssistant.Earth911Servies.models
{
    public class MatDetails
    {
        public MatDetails()
        {
            Programs = new List<RecycProgramDetails>();
        }

        public RecycProgramMaterials Material { get; set; }
        public List<RecycProgramDetails> Programs { get; set; }
    }
}
