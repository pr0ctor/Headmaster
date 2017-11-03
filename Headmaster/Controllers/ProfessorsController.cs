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
    public class ProfessorsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Professors
        public ActionResult Index()
        {
            return View(db.Professors.ToList());
        }

        // GET: Professors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professors professors = db.Professors.Find(id);
            if (professors == null)
            {
                return HttpNotFound();
            }
            return View(professors);
        }

        // GET: Professors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Professors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfessorID,FirstName,LastName")] Professors professors)
        {
            if (ModelState.IsValid)
            {
                db.Professors.Add(professors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(professors);
        }

        // GET: Professors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professors professors = db.Professors.Find(id);
            if (professors == null)
            {
                return HttpNotFound();
            }
            return View(professors);
        }

        // POST: Professors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfessorID,FirstName,LastName")] Professors professors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professors);
        }

        // GET: Professors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professors professors = db.Professors.Find(id);
            if (professors == null)
            {
                return HttpNotFound();
            }
            return View(professors);
        }

        // POST: Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professors professors = db.Professors.Find(id);
            db.Professors.Remove(professors);
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
