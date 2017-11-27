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
    public class MajorsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Majors
        public ActionResult Index()
        {
            return View(db.Majors.ToList());
        }

        // GET: Majors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Majors majors = db.Majors.Find(id);
            if (majors == null)
            {
                return HttpNotFound();
            }
            return View(majors);
        }

        // GET: Majors/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Majors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "MajorID,MajorName")] Majors majors)
        {
            if (ModelState.IsValid)
            {
                db.Majors.Add(majors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(majors);
        }

        // GET: Majors/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Majors majors = db.Majors.Find(id);
            if (majors == null)
            {
                return HttpNotFound();
            }
            return View(majors);
        }

        // POST: Majors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "MajorID,MajorName")] Majors majors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(majors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(majors);
        }

        // GET: Majors/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Majors majors = db.Majors.Find(id);
            if (majors == null)
            {
                return HttpNotFound();
            }
            return View(majors);
        }

        // POST: Majors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Majors majors = db.Majors.Find(id);
            db.Majors.Remove(majors);
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
