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
    public class CorequisitesController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Corequisites
        public ActionResult Index()
        {
            var corequisites = db.Corequisites.Include(c => c.Courses).Include(c => c.Courses1);
            return View(corequisites.ToList());
        }

        // GET: Corequisites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corequisites corequisites = db.Corequisites.Find(id);
            if (corequisites == null)
            {
                return HttpNotFound();
            }
            return View(corequisites);
        }

        // GET: Corequisites/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.RequiredCourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return View();
        }

        // POST: Corequisites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "CorequisiteID,CourseID,RequiredCourseID")] Corequisites corequisites)
        {
            if (ModelState.IsValid)
            {
                db.Corequisites.Add(corequisites);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", corequisites.CourseID);
            ViewBag.RequiredCourseID = new SelectList(db.Courses, "CourseID", "CourseName", corequisites.RequiredCourseID);
            return View(corequisites);
        }

        // GET: Corequisites/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corequisites corequisites = db.Corequisites.Find(id);
            if (corequisites == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", corequisites.CourseID);
            ViewBag.RequiredCourseID = new SelectList(db.Courses, "CourseID", "CourseName", corequisites.RequiredCourseID);
            return View(corequisites);
        }

        // POST: Corequisites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "CorequisiteID,CourseID,RequiredCourseID")] Corequisites corequisites)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corequisites).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", corequisites.CourseID);
            ViewBag.RequiredCourseID = new SelectList(db.Courses, "CourseID", "CourseName", corequisites.RequiredCourseID);
            return View(corequisites);
        }

        // GET: Corequisites/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corequisites corequisites = db.Corequisites.Find(id);
            if (corequisites == null)
            {
                return HttpNotFound();
            }
            return View(corequisites);
        }

        // POST: Corequisites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Corequisites corequisites = db.Corequisites.Find(id);
            db.Corequisites.Remove(corequisites);
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
