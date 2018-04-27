using ITUniver.TeleCalc.Web.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITUniver.TeleCalc.Web.Models
{
    public class User :IEntity
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// true - мужской, false - женский
        /// </summary>
        public bool Sex { get; set; }

        public int Status { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

    }

    
}