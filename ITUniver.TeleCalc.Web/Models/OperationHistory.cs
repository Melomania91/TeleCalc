using ITUniver.TeleCalc.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Models
{
    public class OperationHistory : IEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Initiator { get; set; }

        public int Operation { get; set; }

        public string Args { get; set; }

        public double Result { get; set; }

        public DateTime CalcDate { get; set; }

        public int Time { get; set; }
    }
}