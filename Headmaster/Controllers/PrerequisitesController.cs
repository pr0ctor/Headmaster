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
    public class PrerequisitesController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Prerequisites
        public ActionResult Index()
        {
            var prerequisites = db.Prerequisites.Include(p => p.Courses).Include(p => p.Courses1);
            return View(prerequisites.ToList());
        }

        // GET: Prerequisites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prerequisites prerequisites = db.Prerequisites.Find(id);
            if (prerequisites == null)
            {
                return HttpNotFound();
            }
            return View(prerequisites);
        }

        // GET: Prerequisites/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.RequiredCourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            return View();
        }

        // POST: Prerequisites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PrerequisiteID,CourseID,RequiredCourseID")] Prerequisites prerequisites)
        {
            if (ModelState.IsValid)
            {
                db.Prerequisites.Add(prerequisites);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", prerequisites.CourseID);
            ViewBag.RequiredCourseID = new SelectList(db.Courses, "CourseID", "CourseName", prerequisites.RequiredCourseID);
            return View(prerequisites);
        }

        // GET: Prerequisites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prerequisites prerequisites = db.Prerequisites.Find(id);
            if (prerequisites == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", prerequisites.CourseID);
            ViewBag.RequiredCourseID = new SelectList(db.Courses, "CourseID", "CourseName", prerequisites.RequiredCourseID);
            return View(prerequisites);
        }

        // POST: Prerequisites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PrerequisiteID,CourseID,RequiredCourseID")] Prerequisites prerequisites)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prerequisites).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", prerequisites.CourseID);
            ViewBag.RequiredCourseID = new SelectList(db.Courses, "CourseID", "CourseName", prerequisites.RequiredCourseID);
            return View(prerequisites);
        }

        // GET: Prerequisites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prerequisites prerequisites = db.Prerequisites.Find(id);
            if (prerequisites == null)
            {
                return HttpNotFound();
            }
            return View(prerequisites);
        }

        // POST: Prerequisites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prerequisites prerequisites = db.Prerequisites.Find(id);
            db.Prerequisites.Remove(prerequisites);
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
