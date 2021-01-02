using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestivalTicketsMVC5.Models
{
    public class CurrentValidation : ValidationAttribute
    {


        public CurrentValidation()
        {

     
        }


        public override bool IsValid(object value)
        {
          var date =  (DateTime)value;
            if (date >= DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}