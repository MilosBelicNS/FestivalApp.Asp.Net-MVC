namespace FestivalTicketsMVC5.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FestivalTicketsMVC5.Models.FestivalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FestivalTicketsMVC5.Models.FestivalDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            Models.Festival festival1 = new Models.Festival()
            {
                Id = 1,
                Name = "Exit 2021",
                Place = "Petrovaradinska tvrdjava, Novi Sad",
                DateF = new DateTime(2021, 7, 8).Date,
                Rate = 4.8,
                EventCapacity = 250000


            };

            context.Festivals.AddOrUpdate(festival1);
            context.SaveChanges();

            Models.TicketType ticketType1 = new Models.TicketType()
            {
                Id = 1,
                Name = "One-day pass"


            };

            Models.TicketType ticketType2 = new Models.TicketType()
            {
                Id = 2,
                Name = "VIP-One-day pass"
            };
            Models.TicketType ticketType3 = new Models.TicketType()
            {
                Id = 3,
                Name = "Multi-day pass"


            };

            context.TicketTypes.AddOrUpdate(ticketType1, ticketType2, ticketType3);
            context.SaveChanges();

            Models.Ticket ticket1 = new Models.Ticket()
            {
                Id = 1,
                Price = 3200,
                PurchaseDate = new DateTime(2021, 01, 04),
                CustomerName = "Milos Belic",
                PickedUp = true,
                FestivalId = 1,
                TicketTypeId = 1

            };

            Models.Ticket ticket2 = new Models.Ticket()
            {
                Id = 2,
                Price = 10000,
                PurchaseDate = new DateTime(2021, 01, 04),
                CustomerName = "Milos Belic",
                PickedUp = true,
                FestivalId = 1,
                TicketTypeId = 2

            };
            Models.Ticket ticket3 = new Models.Ticket()
            {
                Id = 3,
                Price = 9000,
                PurchaseDate = new DateTime(2021, 01, 03).Date,
                CustomerName = "Milos Belic",
                PickedUp = true,
                FestivalId = 1,
                TicketTypeId = 2

            };

            context.Tickets.AddOrUpdate(ticket1, ticket2, ticket3);
            context.SaveChanges();


            


            Models.TicketType ticketType1M = context.TicketTypes.FirstOrDefault(m => m.Id == 1);
            Models.TicketType ticketType2M = context.TicketTypes.FirstOrDefault(m => m.Id == 2);
            Models.TicketType ticketType3M = context.TicketTypes.FirstOrDefault(m => m.Id == 2);
            Models.Ticket ticket1M = context.Tickets.FirstOrDefault(m => m.Id == 1);
            Models.Ticket ticket2M = context.Tickets.FirstOrDefault(m => m.Id == 2);
            Models.Ticket ticket3M = context.Tickets.FirstOrDefault(m => m.Id == 3);
            Models.Festival festivalM = context.Festivals.FirstOrDefault(m => m.Id == 1);       


        }
    }
}
