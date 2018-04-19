using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITUniver.TeleCalc.Web.Models
{
    public class CalcModel
    {
        [ReadOnly(true)]
        public string OperName { get; set; }

        public double X { get; set; }

        [Required(ErrorMessage = "Пропустил!")]
        public double Y { get; set; }

        [ReadOnly(true)]
        public double Result { get; set; }
    }
}