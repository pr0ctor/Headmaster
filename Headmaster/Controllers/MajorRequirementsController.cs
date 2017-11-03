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
    public class MajorRequirementsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: MajorRequirements
        public ActionResult Index()
        {
            var majorRequirements = db.MajorRequirements.Include(m => m.Courses).Include(m => m.Majors);
            return View(majorRequirements.ToList());
        }

        // GET: MajorRequirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorRequirements majorRequirements = db.MajorRequirements.Find(id);
            if (majorRequirements == null)
            {
                return HttpNotFound();
            }
            return View(majorRequirements);
        }

        // GET: MajorRequirements/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        // POST: MajorRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MajorRequirementsID,MajorID,CourseID")] MajorRequirements majorRequirements)
        {
            if (ModelState.IsValid)
            {
                db.MajorRequirements.Add(majorRequirements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", majorRequirements.CourseID);
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorRequirements.MajorID);
            return View(majorRequirements);
        }

        // GET: MajorRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorRequirements majorRequirements = db.MajorRequirements.Find(id);
            if (majorRequirements == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", majorRequirements.CourseID);
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorRequirements.MajorID);
            return View(majorRequirements);
        }

        // POST: MajorRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MajorRequirementsID,MajorID,CourseID")] MajorRequirements majorRequirements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(majorRequirements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", majorRequirements.CourseID);
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorRequirements.MajorID);
            return View(majorRequirements);
        }

        // GET: MajorRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorRequirements majorRequirements = db.MajorRequirements.Find(id);
            if (majorRequirements == null)
            {
                return HttpNotFound();
            }
            return View(majorRequirements);
        }

        // POST: MajorRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MajorRequirements majorRequirements = db.MajorRequirements.Find(id);
            db.MajorRequirements.Remove(majorRequirements);
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
