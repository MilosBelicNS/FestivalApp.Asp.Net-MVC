using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FestivalTicketsMVC5.Models
{
    public class FestivalDbContext : DbContext
    {

        public DbSet<Festival> Festivals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }


        public FestivalDbContext() : base("name= FestivalsMvc5")
        {

        }

    }
}