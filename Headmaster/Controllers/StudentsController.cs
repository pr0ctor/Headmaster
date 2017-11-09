using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Headmaster.Models;
using Microsoft.AspNet.Identity;

namespace Headmaster.Controllers
{
    public class StudentsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentMinors).Include(s => s.StudentUsers);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        public ActionResult StudentDashBoard()
        {
            String userID=User.Identity.GetUserId();
          
            //Students student= db.Students.Include("StudentUsers").Where(userID=StudentUser.Id)

            return View();
        }

        // GET: Students/Create
        public ActionResult Create()
        {
           ViewBag.StudentID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID");
           ViewBag.StudentID = new SelectList(db.StudentUsers, "StudentUserID", "UserID");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,MiddleName,VIPID")] Students students)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                students=db.Students.Add(students);

                db.SaveChanges();
                return RedirectToAction("StudentDashBoard");
            }

            ViewBag.StudentID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID", students.StudentID);
           // ViewBag.StudentID = new SelectList(db.StudentUsers, "StudentUserID", "UserID", students.StudentID);
            return View(students);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID", students.StudentID);
            ViewBag.StudentID = new SelectList(db.StudentUsers, "StudentUserID", "UserID", students.StudentID);
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,MiddleName,VIPID")] Students students)
        {
            if (ModelState.IsValid)
            {
                db.Entry(students).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID", students.StudentID);
            ViewBag.StudentID = new SelectList(db.StudentUsers, "StudentUserID", "UserID", students.StudentID);
            return View(students);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Students students = db.Students.Find(id);
            db.Students.Remove(students);
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
