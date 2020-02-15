using System.Collections.Generic;
using RecyclingAssistant.Earth911Servies.models;

namespace RecyclingAssistant.Controllers
{
    public class ViewModel {
        public ViewModel()
        {
            ReceiptItems = new List<string>();
        }

        public RecycProgramDetails Program { get; set; }
        public List<string> ReceiptItems { get; set; }
    }
}