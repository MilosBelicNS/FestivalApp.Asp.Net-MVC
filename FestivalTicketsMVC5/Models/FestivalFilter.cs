using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalTicketsMVC5.Models
{
    public class FestivalFilter
    {

        public string Name { get; set; }
        public string Place { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public double? FromRate { get; set; }
        public double? ToRate { get; set; }
    }
}