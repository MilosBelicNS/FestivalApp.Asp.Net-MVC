using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FestivalTicketsMVC5.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        [Range(500, 35000)]
        public decimal Price { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [CurrentValidation]
        [Display(Name="Purchase date")]
        public DateTime PurchaseDate { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Customer name")]
        public string CustomerName { get; set; }
        [Required]
        [Display(Name = "Picked up?")]
        public bool PickedUp { get; set; }

        [ForeignKey("Festival")]
        public int FestivalId { get; set; }
        
        public  Festival Festival { get; set; }
     

        [ForeignKey("TicketType")]
        public int TicketTypeId { get; set; }
       
        public TicketType TicketType { get; set; }
       

    }
}