using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FestivalTicketsMVC5.Models;
using Microsoft.Ajax.Utilities;
using PagedList;

namespace FestivalTicketsMVC5.Controllers
{
    public class TicketTypesController : Controller
    {
        private FestivalDbContext db = new FestivalDbContext();

        // GET: TicketTypes

        public enum SortTypes
        {
            Name,
            NameDesc,
           

        }

        public static Dictionary<string, SortTypes> SortTypesDict = new Dictionary<string, SortTypes>
        {
            {"Name", SortTypes.Name },
            {"NameDesc", SortTypes.NameDesc}
           
        };

        private int TypesPerPage = 2;
        // GET: Festivals
        public ActionResult Index(TicketTypesFilter typesFilter, string sort = "Name", int page = 1)
        {
            IQueryable<TicketType> tTypes = db.TicketTypes;

            //sort

            if (sort == "")
            {
                sort = "Name";
            }
            SortTypes sortType = SortTypesDict[sort];

            switch (sortType)
            {
                case SortTypes.Name:
                    tTypes = tTypes.OrderBy(x => x.Name);
                    break;
                case SortTypes.NameDesc:
                    tTypes = tTypes.OrderByDescending(x => x.Name);
                    break;
                
            }

            //filter

            if (!typesFilter.Name.IsNullOrWhiteSpace())
            {
                tTypes = tTypes.Where(f => f.Name.Contains(typesFilter.Name));

            }
            

            ViewBag.selectionList = new SelectList(SortTypesDict, "Key", "Key", sort);
            ViewBag.chosenSort = sort;
            ViewBag.filter = typesFilter;


            return View(tTypes.ToPagedList(page, TypesPerPage));
        }
         

        // GET: TicketTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketType ticketType = db.TicketTypes.Find(id);
            if (ticketType == null)
            {
                return HttpNotFound();
            }
            return View(ticketType);
        }

        // GET: TicketTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] TicketType ticketType)
        {
            if (ModelState.IsValid)
            {
                db.TicketTypes.Add(ticketType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticketType);
        }

        // GET: TicketTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketType ticketType = db.TicketTypes.Find(id);
            if (ticketType == null)
            {
                return HttpNotFound();
            }
            return View(ticketType);
        }

        // POST: TicketTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] TicketType ticketType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticketType);
        }

        // GET: TicketTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketType ticketType = db.TicketTypes.Find(id);
            if (ticketType == null)
            {
                return HttpNotFound();
            }
            return View(ticketType);
        }

        // POST: TicketTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketType ticketType = db.TicketTypes.Find(id);
            db.TicketTypes.Remove(ticketType);
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
