using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestivalTicketsMVC5.Models
{
    public class Festival
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3)]
        [Display(Name = "Festival name")]
        public string Name { get; set; }
        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string Place { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date ")]
        [CurrentValidation(ErrorMessage = "You can only select the future!")]
        public DateTime DateF { get; set; }
        [Required]
        [Range(1, 5)]
        public double Rate { get; set; }
        [Required]
        [Range(1, 300000)]
        [Display(Name="Ticket capacity")]
        public int EventCapacity { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}