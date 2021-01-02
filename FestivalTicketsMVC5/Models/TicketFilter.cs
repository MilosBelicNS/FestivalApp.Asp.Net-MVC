using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestivalTicketsMVC5.Models
{
    public class TicketFilter
    {
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}