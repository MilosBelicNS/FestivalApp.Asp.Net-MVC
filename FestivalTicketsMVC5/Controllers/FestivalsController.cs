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
    public class FestivalsController : Controller
    {
        private FestivalDbContext db = new FestivalDbContext();

        // GET: Festivals
        public enum SortTypes
        {
            Name,
            NameDesc,
            Capacity,
            CapacityDesc,
            Rate,
            RateDesc,
            Date,
            DateDesc

        }

        public static Dictionary<string, SortTypes> SortTypesDict = new Dictionary<string, SortTypes>
        {
            {"Name", SortTypes.Name },
            {"NameDesc", SortTypes.NameDesc},
            {"Capacity", SortTypes.Capacity },
            {"CapacityDesc", SortTypes.CapacityDesc },
            {"Rate", SortTypes.Rate},
            {"RateDesc", SortTypes.RateDesc },
            {"Date", SortTypes.Date },
            {"DateDesc", SortTypes.DateDesc }
        };

        private int FestivalPerPage = 2;
        // GET: Festivals
        public ActionResult Index(FestivalFilter festivalFilter, string sort = "Name", int page = 1)
        {
            IQueryable<Festival> festivals = db.Festivals;

            //sort

            if (sort == "")
            {
                sort = "Name";
            }
            SortTypes sortType = SortTypesDict[sort];

            switch (sortType)
            {
                case SortTypes.Name:
                    festivals = festivals.OrderBy(x => x.Name);
                    break;
                case SortTypes.NameDesc:
                    festivals = festivals.OrderByDescending(x => x.Name);
                    break;
                case SortTypes.Capacity:
                    festivals = festivals.OrderBy(x => x.EventCapacity);
                    break;
                case SortTypes.CapacityDesc:
                    festivals = festivals.OrderByDescending(x => x.EventCapacity);
                    break;
                case SortTypes.Rate:
                    festivals = festivals.OrderByDescending(x => x.Rate);
                    break;
                case SortTypes.RateDesc:
                    festivals = festivals.OrderByDescending(x => x.Rate);
                    break;
                case SortTypes.Date:
                    festivals = festivals.OrderByDescending(x => x.DateF);
                    break;
                case SortTypes.DateDesc:
                    festivals = festivals.OrderByDescending(x => x.DateF);
                    break;
            }

            //filter

            if (!festivalFilter.Name.IsNullOrWhiteSpace())
            {
                festivals = festivals.Where(f => f.Name.Contains(festivalFilter.Name));

            }
            if (!festivalFilter.Place.IsNullOrWhiteSpace())
            {
                festivals = festivals.Where(f => f.Place.Contains(festivalFilter.Place));
            }
            if (festivalFilter.FromDate != null)
            {
                festivals = festivals.Where(f => f.DateF >= festivalFilter.FromDate);
            }
            if (festivalFilter.ToDate != null)
            {
                festivals = festivals.Where(f => f.DateF <= festivalFilter.ToDate);
            }
            if (festivalFilter.FromRate != null)
            {
                festivals = festivals.Where(f => f.Rate >= festivalFilter.FromRate);
            }
            if (festivalFilter.ToRate != null)
            {
                festivals = festivals.Where(f => f.Rate <= festivalFilter.ToRate);
            }

            ViewBag.selectionList = new SelectList(SortTypesDict, "Key", "Key", sort);
            ViewBag.chosenSort = sort;
            ViewBag.filter = festivalFilter;


            return View(festivals.ToPagedList(page, FestivalPerPage));
        }

        // GET: Festivals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Festival festival = db.Festivals.Find(id);
            if (festival == null)
            {
                return HttpNotFound();
            }
            return View(festival);
        }

        // GET: Festivals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Festivals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Place,DateF,Rate,EventCapacity")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                db.Festivals.Add(festival);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(festival);
        }

        // GET: Festivals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Festival festival = db.Festivals.Find(id);
            if (festival == null)
            {
                return HttpNotFound();
            }
            return View(festival);
        }

        // POST: Festivals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Place,DateF,Rate,EventCapacity")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                db.Entry(festival).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(festival);
        }

        // GET: Festivals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Festival festival = db.Festivals.Find(id);
            if (festival == null)
            {
                return HttpNotFound();
            }
            return View(festival);
        }

        // POST: Festivals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Festival festival = db.Festivals.Find(id);
            db.Festivals.Remove(festival);
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
