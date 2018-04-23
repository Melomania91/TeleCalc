using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Models
{
    public class CalcModel
    {
        public CalcModel()
        {
            InputData = new List<double>();
        }

        [DisplayName("Операция")]
        public string OperName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public SelectList OperationList { get; set; }

        [DisplayName("Входные данные")]       
        public IEnumerable<double> InputData { get; set; }

        [Obsolete("Заменён на InputData", true)]
        [ReadOnly(true)]
        public double X { get; set; }

        [Obsolete("Заменён на InputData", true)]
        [ReadOnly(true)]
        public double Y { get; set; }

        [ReadOnly(true)]
        [DisplayName("Результат")]
        public double Result { get; set; }
    }
}