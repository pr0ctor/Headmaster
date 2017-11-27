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
    public class SemesterYearsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: SemesterYears
        public ActionResult Index()
        {
            var semesterYear = db.SemesterYear.Include(s => s.Years1).Include(s => s.Semesters1);
            return View(semesterYear.ToList());
        }

        // GET: SemesterYears/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterYear semesterYear = db.SemesterYear.Find(id);
            if (semesterYear == null)
            {
                return HttpNotFound();
            }
            return View(semesterYear);
        }

        // GET: SemesterYears/Create
        public ActionResult Create()
        {
           // ViewBag.SemesterYearID = new SelectList(db.AvailableCourses, "AvailalbeCourseID", "Section");
            ViewBag.SemesterID= new SelectList(db.Semesters, "SemesterID", "Semester");
            ViewBag.YearId = new SelectList(db.Years, "YearID", "Year");
            return View();
        }

        // POST: SemesterYears/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SemesterYearID,SemesterID,YearID")] SemesterYear semesterYear)
        {
            if (ModelState.IsValid)
            {
               
                db.SemesterYear.Add(semesterYear);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.SemesterYearID = new SelectList(db.AvailableCourses, "AvailalbeCourseID", "Section", semesterYear.SemesterYearID);
            ViewBag.SemesterYearID = new SelectList(db.Semesters, "SemesterID", "Semester", semesterYear.SemesterYearID);
            ViewBag.SemesterYearID = new SelectList(db.Years, "YearID", "YearID", semesterYear.SemesterYearID);
            return View(semesterYear);
        }

        // GET: SemesterYears/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterYear semesterYear = db.SemesterYear.Find(id);
            if (semesterYear == null)
            {
                return HttpNotFound();
            }
            ViewBag.SemesterYearID = new SelectList(db.AvailableCourses, "AvailalbeCourseID", "Section", semesterYear.SemesterYearID);
            ViewBag.SemesterYearID = new SelectList(db.Semesters, "SemesterID", "Semester", semesterYear.SemesterYearID);
            ViewBag.SemesterYearID = new SelectList(db.Years, "YearID", "YearID", semesterYear.SemesterYearID);
            return View(semesterYear);
        }

        // POST: SemesterYears/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SemesterYearID,SemesterID,YearID")] SemesterYear semesterYear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semesterYear).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SemesterYearID = new SelectList(db.AvailableCourses, "AvailalbeCourseID", "Section", semesterYear.SemesterYearID);
            ViewBag.SemesterYearID = new SelectList(db.Semesters, "SemesterID", "Semester", semesterYear.SemesterYearID);
            ViewBag.SemesterYearID = new SelectList(db.Years, "YearID", "YearID", semesterYear.SemesterYearID);
            return View(semesterYear);
        }

        // GET: SemesterYears/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SemesterYear semesterYear = db.SemesterYear.Find(id);
            if (semesterYear == null)
            {
                return HttpNotFound();
            }
            return View(semesterYear);
        }

        // POST: SemesterYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SemesterYear semesterYear = db.SemesterYear.Find(id);
            db.SemesterYear.Remove(semesterYear);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Add()
        {
           
           ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Semester");
           ViewBag.YearId = new SelectList(db.Years, "YearID", "Year");
           return View();
     
        }
        [HttpPost]
        public ActionResult Add(SemesterYear model)
        {
            if (ModelState.IsValid)
            {
                SemesterYear smy = new SemesterYear();
                smy.SemesterID = model.SemesterID;
                smy.YearID = model.YearID;
                smy.SemesterYearID = 0;
                db.SemesterYear.Add(smy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SemesterID = new SelectList(db.Semesters, "SemesterID", "Semester");
            ViewBag.YearId = new SelectList(db.Years, "YearID", "Year");
            return View();

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
