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
    [Authorize]
    public class StudentsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentMinors);
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

        //Gets the student ID from the userID 
        public Students QueryStudentID(String UserId)
        {
            var list= from s in db.Students
                          where s.UserId.Equals(UserId)
                          select s;
            Students student = list.First();
            return student;
        }
       

        //Returns the total number of credits taken by a student
        public int TotalCredits(Students student)
        {
            int credits = 0;
            try
            {
                 credits = (from s in db.Registrations
                               where s.StudentID == student.StudentID
                               select s.AvailableCourses.Courses.Credits).Sum();
            }catch(System.InvalidOperationException e)
            {
                credits = 0;
            }


            return credits;
                        
        }
        //Returns last Year student registered for classes
        public int getLastYear(Students student)
        {
            int Year=0;
            try
            {
                Year = (from s in db.Registrations
                        where s.StudentID == student.StudentID
                        select s.AvailableCourses.SemesterYear.Years1.Year).Max();
            }
            catch(System.InvalidOperationException e)
            {
                try
                {
                    Year = (from s in db.Years
                            select s.Year).Max();
                }catch(System.InvalidOperationException x)
                {
                    Year = 2017;
                }
            }
            return Year;
        }
        // Returns last SemesterID of that year for that student in defualts to spring
        //Also sorry did realize table was already filled so now 
        /*Semester | SemesterID
         * Spring   4
         * Summer   5
         * Fall     6
         */
        public int GetLastSemester(int year, Students student)
        {
            int id = 4;
            try
            {
             id = (from s in db.Registrations
                            where s.StudentID == student.StudentID && year == s.AvailableCourses.SemesterYear.Years1.Year
                            select s.AvailableCourses.SemesterYear.Semesters1.SemesterID).Max();
                

            }
            catch (System.InvalidOperationException)
            {
                
            }
            return id;
        }
        
        public ActionResult StudentDashBoard()
        {
            if (User.Identity.IsAuthenticated)
            {
                Students student = QueryStudentID(User.Identity.GetUserId());
                int Year = getLastYear(student);
                int Semester = GetLastSemester(Year, student);

                //Pulls most recent Registration
                var course = from s in db.Registrations
                             where s.StudentID == student.StudentID && s.AvailableCourses.SemesterYear.Years1.Year == Year
                             && s.AvailableCourses.SemesterYear.SemesterID == Semester
                             select s;

                ViewData["Courses"] = course;
                student.TotalCredits = TotalCredits(student);
                
                return View(student);
            }
           
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        public ActionResult AdminDashBoard()
        {
            return View();
        }

        // GET: Students/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
           ViewBag.StudentID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID");
          
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,MiddleName,VIPID")] Students students)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                students=db.Students.Add(students);

                db.SaveChanges();
                return RedirectToAction("StudentDashBoard");
            }

            ViewBag.StudentID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID", students.StudentID);
           
            return View(students);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit()
        {
            
            if (!User.Identity.IsAuthenticated)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Students students = QueryStudentID(User.Identity.GetUserId());
                
            if (students == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID", students.StudentID);
           
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,MiddleName,VIPID")] Students students)
        {
            if (ModelState.IsValid)
            {
                students.UserId = User.Identity.GetUserId();
                db.Entry(students).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("StudentDashBoard");
            }
            ViewBag.StudentID = new SelectList(db.StudentMinors, "StudentMinorID", "StudentMinorID", students.StudentID);
          
            return View(students);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
