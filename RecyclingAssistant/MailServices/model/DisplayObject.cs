using System;
using System.Collections.Generic;
using RecyclingAssistant.Earth911Servies.models;

namespace RecyclingAssistant.MailServices.model
{
    public class DisplayObject
    {
        public DisplayObject()
        {
        }

        public List<RecycProgramDetails> Programs { get; set; }
        public List<string> ReceiptItems { get; set; }
    }
}
