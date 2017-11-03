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
    public class MinorRequirementsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: MinorRequirements
        public ActionResult Index()
        {
            var minorRequirements = db.MinorRequirements.Include(m => m.Courses).Include(m => m.Minors);
            return View(minorRequirements.ToList());
        }

        // GET: MinorRequirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MinorRequirements minorRequirements = db.MinorRequirements.Find(id);
            if (minorRequirements == null)
            {
                return HttpNotFound();
            }
            return View(minorRequirements);
        }

        // GET: MinorRequirements/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.MinorID = new SelectList(db.Minors, "MinorID", "MinorName");
            return View();
        }

        // POST: MinorRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MinorRequirementsID,MinorID,CourseID")] MinorRequirements minorRequirements)
        {
            if (ModelState.IsValid)
            {
                db.MinorRequirements.Add(minorRequirements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", minorRequirements.CourseID);
            ViewBag.MinorID = new SelectList(db.Minors, "MinorID", "MinorName", minorRequirements.MinorID);
            return View(minorRequirements);
        }

        // GET: MinorRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MinorRequirements minorRequirements = db.MinorRequirements.Find(id);
            if (minorRequirements == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", minorRequirements.CourseID);
            ViewBag.MinorID = new SelectList(db.Minors, "MinorID", "MinorName", minorRequirements.MinorID);
            return View(minorRequirements);
        }

        // POST: MinorRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MinorRequirementsID,MinorID,CourseID")] MinorRequirements minorRequirements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(minorRequirements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", minorRequirements.CourseID);
            ViewBag.MinorID = new SelectList(db.Minors, "MinorID", "MinorName", minorRequirements.MinorID);
            return View(minorRequirements);
        }

        // GET: MinorRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MinorRequirements minorRequirements = db.MinorRequirements.Find(id);
            if (minorRequirements == null)
            {
                return HttpNotFound();
            }
            return View(minorRequirements);
        }

        // POST: MinorRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MinorRequirements minorRequirements = db.MinorRequirements.Find(id);
            db.MinorRequirements.Remove(minorRequirements);
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
