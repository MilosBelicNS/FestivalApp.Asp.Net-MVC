using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestivalTicketsMVC5.Models
{
    public class TicketType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Ticket type")]
        public string Name { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}