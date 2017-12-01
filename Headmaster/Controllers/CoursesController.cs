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
    public class CoursesController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Courses
        public ActionResult Index()
        {
            ViewBag.Department = (from s in db.Departments
                                  select new SelectListItem
                                  {
                                      Text = s.DepartmentName,
                                      Value = s.DepartmentID.ToString()
                                  }).ToList();

            var courses = db.Courses.Include(c => c.Departments);
            ViewBag.Course = courses.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Index(Courses model)
        {
            ViewBag.Department = (from s in db.Departments
                                  select new SelectListItem
                                  {
                                      Text = s.DepartmentName,
                                      Value = s.DepartmentID.ToString()
                                  }).ToList();

            var courses = from s in db.Courses.Include(c => c.Departments)
                          where s.DepartmentID == model.DepartmentID
                          select s ;
            ViewBag.Course = courses.ToList();
            return View();
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
              return HttpNotFound();
            }
            return View(courses);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Abbreviation");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "CourseID,DepartmentID,CourseName,CourseNumber,Description,Credits")] Courses courses)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(courses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Abbreviation", courses.DepartmentID);
            return View(courses);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Abbreviation", courses.DepartmentID);
            return View(courses);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "CourseID,DepartmentID,CourseName,Description,Credits")] Courses courses)
        {
            if (ModelState.IsValid)
            {
               
                    db.Entry(courses).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                
               
            }
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Abbreviation", courses.DepartmentID);
            return View(courses);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses courses = db.Courses.Find(id);
            if (courses == null)
            {
                return HttpNotFound();
            }
            return View(courses);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Courses courses = db.Courses.Find(id);
            db.Courses.Remove(courses);
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
