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
    public class SemestersController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Semesters
        public ActionResult Index()
        {
            var semesters = db.Semesters.Include(s => s.SemesterYear);
            return View(semesters.ToList());
        }

        // GET: Semesters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semesters semesters = db.Semesters.Find(id);
            if (semesters == null)
            {
                return HttpNotFound();
            }
            return View(semesters);
        }

        // GET: Semesters/Create
        public ActionResult Create()
        {
            ViewBag.SemesterID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID");
            return View();
        }

        // POST: Semesters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SemesterID,Semester")] Semesters semesters)
        {
            if (ModelState.IsValid)
            {
                db.Semesters.Add(semesters);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SemesterID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID", semesters.SemesterID);
            return View(semesters);
        }

        // GET: Semesters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semesters semesters = db.Semesters.Find(id);
            if (semesters == null)
            {
                return HttpNotFound();
            }
            ViewBag.SemesterID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID", semesters.SemesterID);
            return View(semesters);
        }

        // POST: Semesters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SemesterID,Semester")] Semesters semesters)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semesters).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SemesterID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID", semesters.SemesterID);
            return View(semesters);
        }

        // GET: Semesters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semesters semesters = db.Semesters.Find(id);
            if (semesters == null)
            {
                return HttpNotFound();
            }
            return View(semesters);
        }

        // POST: Semesters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Semesters semesters = db.Semesters.Find(id);
            db.Semesters.Remove(semesters);
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
