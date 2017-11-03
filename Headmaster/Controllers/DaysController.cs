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
    public class DaysController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Days
        public ActionResult Index()
        {
            return View(db.Days.ToList());
        }

        // GET: Days/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Days days = db.Days.Find(id);
            if (days == null)
            {
                return HttpNotFound();
            }
            return View(days);
        }

        // GET: Days/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Days/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DaysID,Day")] Days days)
        {
            if (ModelState.IsValid)
            {
                db.Days.Add(days);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(days);
        }

        // GET: Days/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Days days = db.Days.Find(id);
            if (days == null)
            {
                return HttpNotFound();
            }
            return View(days);
        }

        // POST: Days/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DaysID,Day")] Days days)
        {
            if (ModelState.IsValid)
            {
                db.Entry(days).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(days);
        }

        // GET: Days/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Days days = db.Days.Find(id);
            if (days == null)
            {
                return HttpNotFound();
            }
            return View(days);
        }

        // POST: Days/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Days days = db.Days.Find(id);
            db.Days.Remove(days);
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
