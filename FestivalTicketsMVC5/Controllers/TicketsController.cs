using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FestivalTicketsMVC5.Models;
using PagedList;

namespace FestivalTicketsMVC5.Controllers
{
    public class TicketsController : Controller
    {
        private FestivalDbContext db = new FestivalDbContext();

        public enum SortTypes
        {
            
           
            Price,
            PriceDesc,
            Date,
            DateDesc

        }

        public static Dictionary<string, SortTypes> SortTypesDict = new Dictionary<string, SortTypes>
        {
            
            {"Price", SortTypes.Price},
            {"PriceDesc", SortTypes.PriceDesc },
            {"Date", SortTypes.Date },
            {"DateDesc", SortTypes.DateDesc }
        };

        private int TicketsPerPage = 2;
        // GET: Festivals
        public ActionResult Index(TicketFilter ticketFilter, string sort = "Price", int page = 1)
        {
            IQueryable<Ticket> tickets = db.Tickets.Include(x => x.Festival).Include(x=>x.TicketType);

            //sort

            if (sort == "")
            {
                sort = "Price";
            }
            SortTypes sortType = SortTypesDict[sort];

            switch (sortType)
            {
                case SortTypes.Price:
                    tickets = tickets.OrderBy(x => x.Price);
                    break;
                case SortTypes.PriceDesc:
                    tickets = tickets.OrderByDescending(x => x.Price);
                    break;
                case SortTypes.Date:
                    tickets = tickets.OrderBy(x => x.PurchaseDate);
                    break;
                case SortTypes.DateDesc:
                    tickets = tickets.OrderByDescending(x => x.PurchaseDate);
                    break;
                
            }

            //filter

            if (ticketFilter.PriceFrom != null)
            {
                tickets = tickets.Where(f => f.Price >= ticketFilter.PriceFrom);

            }
            if (ticketFilter.PriceTo != null)
            {
                tickets = tickets.Where(f => f.Price <= ticketFilter.PriceTo);

            }
            if (ticketFilter.DateFrom != null)
            {
                tickets = tickets.Where(f => f.PurchaseDate >= ticketFilter.DateFrom);

            }
            if (ticketFilter.DateTo != null)
            {
                tickets = tickets.Where(f => f.PurchaseDate <= ticketFilter.DateTo);

            }


            ViewBag.selectionList = new SelectList(SortTypesDict, "Key", "Key", sort);
            ViewBag.chosenSort = sort;
            ViewBag.filter = ticketFilter;


            return View(tickets.ToPagedList(page, TicketsPerPage));
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.FestivalId = new SelectList(db.Festivals, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,PurchaseDate,CustomerName,PickedUp,FestivalId,TicketTypeId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FestivalId = new SelectList(db.Festivals, "Id", "Name", ticket.FestivalId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.FestivalId = new SelectList(db.Festivals, "Id", "Name", ticket.FestivalId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,PurchaseDate,CustomerName,PickedUp,FestivalId,TicketTypeId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FestivalId = new SelectList(db.Festivals, "Id", "Name", ticket.FestivalId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
