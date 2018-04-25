using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Models
{
    public class OperationHistory
    {
        public int Id { get; set; }

        public string Initiator { get; set; }

        public string Operation { get; set; }

        public string Args { get; set; }

        public double Result { get; set; }

        public DateTime CalcDate { get; set; }

        public int Time { get; set; }
    }
}