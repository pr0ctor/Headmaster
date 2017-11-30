using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Headmaster.Models;

namespace Headmaster.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class YearsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Years
        public ActionResult Index()
        {
            var years = db.Years;
            return View(years.ToList());
        }

        // GET: Years/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Years years = db.Years.Find(id);
            if (years == null)
            {
                return HttpNotFound();
            }
            return View(years);
        }

        // GET: Years/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Years/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "YearID,Year")] Years years)
        {
            if (ModelState.IsValid)
            {
                db.Years.Add(years);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.YearID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID", years.YearID);
            return View(years);
        }

        // GET: Years/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Years years = db.Years.Find(id);
            if (years == null)
            {
                return HttpNotFound();
            }
            ViewBag.YearID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID", years.YearID);
            return View(years);
        }

        // POST: Years/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "YearID,Year")] Years years)
        {
            if (ModelState.IsValid)
            {
                db.Entry(years).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.YearID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID", years.YearID);
            return View(years);
        }

        // GET: Years/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Years years = db.Years.Find(id);
            if (years == null)
            {
                return HttpNotFound();
            }
            return View(years);
        }

        // POST: Years/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Years years = db.Years.Find(id);
            db.Years.Remove(years);
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
