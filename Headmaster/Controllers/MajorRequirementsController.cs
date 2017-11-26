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
using Headmaster.Helper;
namespace Headmaster.Controllers
{
    public class MajorRequirementsController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: MajorRequirements
        public ActionResult Index()
        {
            var majorRequirements = db.MajorRequirements.Include(m => m.Courses).Include(m => m.Majors);
            return View(majorRequirements.ToList());
        }

        // GET: MajorRequirements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorRequirements majorRequirements = db.MajorRequirements.Find(id);
            if (majorRequirements == null)
            {
                return HttpNotFound();
            }
            return View(majorRequirements);
        }

        // GET: MajorRequirements/Create
        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        // POST: MajorRequirements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MajorRequirementsID,MajorID,CourseID")] MajorRequirements majorRequirements)
        {
            if (ModelState.IsValid)
            {
                db.MajorRequirements.Add(majorRequirements);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", majorRequirements.CourseID);
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorRequirements.MajorID);
            return View(majorRequirements);
        }

        // GET: MajorRequirements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorRequirements majorRequirements = db.MajorRequirements.Find(id);
            if (majorRequirements == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", majorRequirements.CourseID);
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorRequirements.MajorID);
            return View(majorRequirements);
        }
        

        // POST: MajorRequirements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MajorRequirementsID,MajorID,CourseID")] MajorRequirements majorRequirements)
        {
            if (ModelState.IsValid)
            {
                db.Entry(majorRequirements).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", majorRequirements.CourseID);
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", majorRequirements.MajorID);
            return View(majorRequirements);
        }

        // GET: MajorRequirements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MajorRequirements majorRequirements = db.MajorRequirements.Find(id);
            if (majorRequirements == null)
            {
                return HttpNotFound();
            }
            return View(majorRequirements);
        }

        // POST: MajorRequirements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MajorRequirements majorRequirements = db.MajorRequirements.Find(id);
            db.MajorRequirements.Remove(majorRequirements);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Creates a view showing what courses the user still needs towards their degree
        public ActionResult RequiredCourses()
        {
            if (User.Identity.IsAuthenticated)
            {
                Students user = new StudentsController().QueryStudentID(User.Identity.GetUserId());
                var taken = from s in db.Registrations
                            where s.StudentID == user.StudentID 
                            select s.AvailableCourses.CourseID;

                var needs = from s in db.MajorRequirements
                            where !taken.Contains(s.CourseID)
                            select s;
                var genED = from s in needs
                            where s.CoursePriority.PriorityLevel == 1
                            select s;
                var Supple = from s in needs
                            where s.CoursePriority.PriorityLevel == 2
                            select s;
                var core = from s in needs
                            where s.CoursePriority.PriorityLevel == 3
                            select s;

                ViewBag.Core = core.OrderBy(x=>x.Courses.CourseNumber);
                ViewBag.GenED = genED.OrderBy(x => x.Courses.CourseNumber); ;
                ViewBag.Supplemental = Supple.OrderBy(x => x.Courses.CourseNumber); 
             
                return View();
            }else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        public ActionResult RecommendCourse()
        {
            if (User.Identity.IsAuthenticated)
            {
                //Taken Courses
                Students user = new StudentsController().QueryStudentID(User.Identity.GetUserId());
                var taken = from s in db.Registrations
                            where s.StudentID == user.StudentID
                            select s.AvailableCourses.CourseID;
                //Has taken pre-reqs for
                var Aval = from s in db.Prerequisites
                           where taken.Contains(s.RequiredCourseID)
                           select s.CourseID;
                //Remove duplicates part 1
                var RemFalses= from s in db.Prerequisites
                               where !taken.Contains(s.RequiredCourseID)
                               select s.CourseID;
                var ClassWithPreq= from s in db.Prerequisites
                                   select s.CourseID;
                //Remove duplicates part 2
                Aval = from s in Aval
                      where !RemFalses.Contains(s)
                      select s;
                
                var major = from s in db.MajorRequirements
                            select s;
                //Pull needed class from avalible 
                var Recommend = (from s in major
                                 where Aval.Contains(s.CourseID) || (!ClassWithPreq.Contains(s.CourseID) && (!taken.Contains(s.CourseID)))
                                 select s);

                //Sort class based on priority
                var Queue = new PriorityQueue<MajorRequirements>();
                
                foreach(var item in Recommend)
                {
                    if (item.CoursePriority.PriorityLevel == 1)
                        Queue.Enqueue(QueuePriorityEnum.Medium, item);
                    if (item.CoursePriority.PriorityLevel == 2)
                        Queue.Enqueue(QueuePriorityEnum.Low, item);
                    if (item.CoursePriority.PriorityLevel == 3)
                        Queue.Enqueue(QueuePriorityEnum.High, item);
                }
                List<MajorRequirements> Recommendation = new List<MajorRequirements>();
                try
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Recommendation.Add(Queue.Dequeue());
                    }

                } catch(InvalidOperationException)
                {

                }
                ViewBag.Recommendation = Recommendation;
                return View();
            }else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
