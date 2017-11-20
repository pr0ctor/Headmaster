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
    public class RegistrationsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Registrations
        public ActionResult Index()
        {
            var registrations = db.Registrations.Include(r => r.AvailableCourses).Include(r => r.Students);
            return View(registrations.ToList());
        }

        // GET: Registrations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registrations registrations = db.Registrations.Find(id);
            if (registrations == null)
            {
                return HttpNotFound();
            }
            return View(registrations);
        }

        // GET: Registrations/Create
        public ActionResult Create()
        {
            ViewBag.AvailableCourseID = new SelectList(db.AvailableCourses, "AvailalbeCourseID", "Section");
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName");
            return View();
        }

        // POST: Registrations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RegistrationID,StudentID,AvailableCourseID")] Registrations registrations)
        {
            if (ModelState.IsValid)
            {
                db.Registrations.Add(registrations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AvailableCourseID = new SelectList(db.AvailableCourses, "AvailalbeCourseID", "Section", registrations.AvailableCourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", registrations.StudentID);
            return View(registrations);
        }

        // GET: Registrations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registrations registrations = db.Registrations.Find(id);
            if (registrations == null)
            {
                return HttpNotFound();
            }
            ViewBag.AvailableCourseID = new SelectList(db.AvailableCourses, "AvailalbeCourseID", "Section", registrations.AvailableCourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", registrations.StudentID);
         
            return View(registrations);
        }

        // POST: Registrations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RegistrationID,StudentID,AvailableCourseID")] Registrations registrations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registrations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AvailableCourseID = new SelectList(db.AvailableCourses, "AvailalbeCourseID", "Section", registrations.AvailableCourseID);
            ViewBag.StudentID = new SelectList(db.Students, "StudentID", "FirstName", registrations.StudentID);
            return View(registrations);
        }

        // GET: Registrations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registrations registrations = db.Registrations.Find(id);
            if (registrations == null)
            {
                return HttpNotFound();
            }
            return View(registrations);
        }

        // POST: Registrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registrations registrations = db.Registrations.Find(id);
            db.Registrations.Remove(registrations);
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
