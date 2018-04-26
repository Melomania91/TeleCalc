using ITUniver.TeleCalc.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Models
{
    public class OperationModel :IEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Owner { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

  
    }

    
}