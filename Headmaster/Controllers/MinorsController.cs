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
    public class MinorsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Minors
        public ActionResult Index()
        {
            var minors = db.Minors.Include(m => m.StudentMinors);
            return View(minors.ToList());
        }

        // GET: Minors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Minors minors = db.Minors.Find(id);
            if (minors == null)
            {
                return HttpNotFound();
            }
            return View(minors);
        }

        // GET: Minors/Create
        public ActionResult Create()
        {
            ViewBag.MinorID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID");
            return View();
        }

        // POST: Minors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MinorID,MinorName")] Minors minors)
        {
            if (ModelState.IsValid)
            {
                db.Minors.Add(minors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MinorID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID", minors.MinorID);
            return View(minors);
        }

        // GET: Minors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Minors minors = db.Minors.Find(id);
            if (minors == null)
            {
                return HttpNotFound();
            }
            ViewBag.MinorID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID", minors.MinorID);
            return View(minors);
        }

        // POST: Minors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MinorID,MinorName")] Minors minors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(minors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MinorID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID", minors.MinorID);
            return View(minors);
        }

        // GET: Minors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Minors minors = db.Minors.Find(id);
            if (minors == null)
            {
                return HttpNotFound();
            }
            return View(minors);
        }

        // POST: Minors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Minors minors = db.Minors.Find(id);
            db.Minors.Remove(minors);
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
