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
    [Authorize]
    public class StudentMinorsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: StudentMinors
        public ActionResult Index()
        {
            var studentMinors = db.StudentMinors.Include(s => s.Minors).Include(s => s.Students);
            return View(studentMinors.ToList());
        }

        // GET: StudentMinors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentMinors studentMinors = db.StudentMinors.Find(id);
            if (studentMinors == null)
            {
                return HttpNotFound();
            }
            return View(studentMinors);
        }

        // GET: StudentMinors/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.StudentMinorID = new SelectList(db.Minors, "MinorID", "MinorName");
            ViewBag.StudentMinorID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: StudentMinors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "StudentMinorID,StudentID,MinorID")] StudentMinors studentMinors)
        {
            if (ModelState.IsValid)
            {
                db.StudentMinors.Add(studentMinors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentMinorID = new SelectList(db.Minors, "MinorID", "MinorName", studentMinors.StudentMinorID);
            ViewBag.StudentMinorID = new SelectList(db.Students, "StudentID", "FirstName", studentMinors.StudentMinorID);
            return View(studentMinors);
        }

        // GET: StudentMinors/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentMinors studentMinors = db.StudentMinors.Find(id);
            if (studentMinors == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentMinorID = new SelectList(db.Minors, "MinorID", "MinorName", studentMinors.StudentMinorID);
            ViewBag.StudentMinorID = new SelectList(db.Students, "StudentID", "FirstName", studentMinors.StudentMinorID);
            return View(studentMinors);
        }

        // POST: StudentMinors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "StudentMinorID,StudentID,MinorID")] StudentMinors studentMinors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentMinors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentMinorID = new SelectList(db.Minors, "MinorID", "MinorName", studentMinors.StudentMinorID);
            ViewBag.StudentMinorID = new SelectList(db.Students, "StudentID", "FirstName", studentMinors.StudentMinorID);
            return View(studentMinors);
        }

        // GET: StudentMinors/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentMinors studentMinors = db.StudentMinors.Find(id);
            if (studentMinors == null)
            {
                return HttpNotFound();
            }
            return View(studentMinors);
        }

        // POST: StudentMinors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentMinors studentMinors = db.StudentMinors.Find(id);
            db.StudentMinors.Remove(studentMinors);
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
