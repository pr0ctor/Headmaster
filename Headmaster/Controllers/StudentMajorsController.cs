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
    public class StudentMajorsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: StudentMajors
        public ActionResult Index()
        {
            var studentMajors = db.StudentMajors.Include(s => s.Majors).Include(s => s.Students);
            return View(studentMajors.ToList());
        }

        // GET: StudentMajors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentMajors studentMajors = db.StudentMajors.Find(id);
            if (studentMajors == null)
            {
                return HttpNotFound();
            }
            return View(studentMajors);
        }

        // GET: StudentMajors/Create
        public ActionResult Create()
        {
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: StudentMajors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentMajorID,StudentID,MajorID")] StudentMajors studentMajors)
        {
            if (ModelState.IsValid)
            {
                db.StudentMajors.Add(studentMajors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", studentMajors.MajorID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentMajors.StudentID);
            return View(studentMajors);
        }

        // GET: StudentMajors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentMajors studentMajors = db.StudentMajors.Find(id);
            if (studentMajors == null)
            {
                return HttpNotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", studentMajors.MajorID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentMajors.StudentID);
            return View(studentMajors);
        }

        // POST: StudentMajors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentMajorID,StudentID,MajorID")] StudentMajors studentMajors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentMajors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", studentMajors.MajorID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", studentMajors.StudentID);
            return View(studentMajors);
        }

        // GET: StudentMajors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentMajors studentMajors = db.StudentMajors.Find(id);
            if (studentMajors == null)
            {
                return HttpNotFound();
            }
            return View(studentMajors);
        }

        // POST: StudentMajors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentMajors studentMajors = db.StudentMajors.Find(id);
            db.StudentMajors.Remove(studentMajors);
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
