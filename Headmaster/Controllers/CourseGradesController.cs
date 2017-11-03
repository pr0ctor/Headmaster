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
    public class CourseGradesController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: CourseGrades
        public ActionResult Index()
        {
            var courseGrades = db.CourseGrades.Include(c => c.Grades).Include(c => c.Registrations).Include(c => c.Students);
            return View(courseGrades.ToList());
        }

        // GET: CourseGrades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGrades courseGrades = db.CourseGrades.Find(id);
            if (courseGrades == null)
            {
                return HttpNotFound();
            }
            return View(courseGrades);
        }

        // GET: CourseGrades/Create
        public ActionResult Create()
        {
            ViewBag.GradeID = new SelectList(db.Grades, "GradeID", "Grade");
            ViewBag.RegistrationID = new SelectList(db.Registrations, "RegistrationID", "RegistrationID");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: CourseGrades/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseGradeID,RegistrationID,GradeID,StudentID")] CourseGrades courseGrades)
        {
            if (ModelState.IsValid)
            {
                db.CourseGrades.Add(courseGrades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GradeID = new SelectList(db.Grades, "GradeID", "Grade", courseGrades.GradeID);
            ViewBag.RegistrationID = new SelectList(db.Registrations, "RegistrationID", "RegistrationID", courseGrades.RegistrationID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", courseGrades.StudentID);
            return View(courseGrades);
        }

        // GET: CourseGrades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGrades courseGrades = db.CourseGrades.Find(id);
            if (courseGrades == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradeID = new SelectList(db.Grades, "GradeID", "Grade", courseGrades.GradeID);
            ViewBag.RegistrationID = new SelectList(db.Registrations, "RegistrationID", "RegistrationID", courseGrades.RegistrationID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", courseGrades.StudentID);
            return View(courseGrades);
        }

        // POST: CourseGrades/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseGradeID,RegistrationID,GradeID,StudentID")] CourseGrades courseGrades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseGrades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GradeID = new SelectList(db.Grades, "GradeID", "Grade", courseGrades.GradeID);
            ViewBag.RegistrationID = new SelectList(db.Registrations, "RegistrationID", "RegistrationID", courseGrades.RegistrationID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", courseGrades.StudentID);
            return View(courseGrades);
        }

        // GET: CourseGrades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseGrades courseGrades = db.CourseGrades.Find(id);
            if (courseGrades == null)
            {
                return HttpNotFound();
            }
            return View(courseGrades);
        }

        // POST: CourseGrades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseGrades courseGrades = db.CourseGrades.Find(id);
            db.CourseGrades.Remove(courseGrades);
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
