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
    public class AvailableCoursesController : Controller
    {
        private headmasterEntities db = new headmasterEntities();

        // GET: AvailableCourses
        public ActionResult Index()
        {
            var availableCourses = db.AvailableCourses.Include(a => a.Buildings).Include(a => a.Courses).Include(a => a.Days).Include(a => a.Professors).Include(a => a.SemesterYear).Include(a => a.Times);
            return View(availableCourses.ToList());
        }

        //Returns the avalible courses to the view after filtering them based on the department
        [Authorize(Roles = "Student")]
        public ActionResult Search()
        {
            if (User.Identity.IsAuthenticated)
            {


                var Department = (from s in db.Departments
                                  select new SelectListItem
                                  {
                                      Text = s.DepartmentName,
                                      Value = s.DepartmentID.ToString()

                                 }).ToList();
                ViewBag.Departments = Department;
                ViewData["Course"] = db.AvailableCourses.OrderByDescending(x => x.SemesterYear.Years.Year).ThenBy(x => x.SemesterYear.SemesterID).ThenBy(x => x.Courses.Departments.DepartmentName).ToList();




                return View();
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        [Authorize(Roles = "Student")]
        public ActionResult Search(AvailableCourses model)
        {
            if(model.Courses.DepartmentID!=0)
                 return RedirectToAction("SearchAndRegister", new { id = model.Courses.DepartmentID });
           else
            {
                if (User.Identity.IsAuthenticated)
                {


                    var Department = (from s in db.Departments
                                      select new SelectListItem
                                      {
                                          Text = s.DepartmentName,
                                          Value = s.DepartmentID.ToString()

                                      }).ToList();
                    ViewBag.Departments = Department;
                    ViewData["Course"] = db.AvailableCourses.OrderByDescending(x => x.SemesterYear.Years.Year).ThenBy(x => x.SemesterYear.SemesterID).ThenBy(x => x.Courses.Departments.DepartmentName).ToList();




                    return View();
                }
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
               

           
        [Authorize(Roles ="Student")]
        public ActionResult SearchAndRegister(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if(id==0)
                {
                    RedirectToAction("Search");
                }
                    var SemesterYear = (from s in db.SemesterYear.OrderByDescending(x=>x.Years.Year).ThenBy(x=>x.SemesterID).AsEnumerable()
                                        select new SelectListItem
                                        {
                                            Text = s.SemesterYearName,
                                            Value = s.SemesterYearID.ToString()
                        
                                        }).ToList();


                var Course = (from s in db.Courses.OrderBy(x => x.Departments.Abbreviation).ThenBy(x => x.CourseNumber).AsEnumerable()
                              where s.DepartmentID == (int)id
                               select new SelectListItem
                               {
                                   Text = s.CourseName,
                                   Value = s.CourseID.ToString()
                    
                            }).ToList(); 

                ViewBag.SemesterYearID = new SelectList(SemesterYear, "Value", "Text");
                ViewBag.CourseID = new SelectList(Course, "Value", "Text");
                ViewData["Course"] = from s in db.AvailableCourses.OrderByDescending(x => x.SemesterYear.Years.Year).ThenByDescending(x => x.SemesterYear.SemesterID).ThenBy(x => x.Courses.Departments.DepartmentName).ToList()
                                     where s.Courses.DepartmentID == id
                                     select s;
                                      



                return View();
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
       
       
        [HttpPost]
        [Authorize(Roles = "Student")]
        public ActionResult SearchAndRegister(AvailableCourses model, int? id)
        {
            if(id==0)
                RedirectToAction("Search");

            var search = (from s in db.AvailableCourses
                          select s);

            if (ModelState.IsValid)
            {
              
                    var courses = (from s in db.AvailableCourses
                                   where (model.SemesterYearID == s.SemesterYearID) && (model.CourseID == s.CourseID) 
                                   select s) ;

                    if (courses != null)
                    {
                        search = courses;
                    }
              
            }

                var SemesterYear = (from s in db.SemesterYear.OrderByDescending(x => x.Years.Year).ThenByDescending(x => x.SemesterID).AsEnumerable()
                                    select new SelectListItem
                                    {
                                        Text = s.SemesterYearName,
                                        Value = s.SemesterYearID.ToString()

                                    }).ToList();


                var Course = (from s in db.Courses.OrderBy(x => x.Departments.Abbreviation).ThenBy(x => x.CourseNumber).AsEnumerable()
                              where s.DepartmentID==id
                              select new SelectListItem
                              {
                                  Text = s.CourseName,
                                  Value = s.CourseID.ToString()

                              }).ToList();
            ViewBag.SemesterYearID = new SelectList(SemesterYear, "Value", "Text");
          
            ViewBag.CourseID = new SelectList(Course, "Value", "Text");
            ViewData["Course"]= search.OrderByDescending(x => x.SemesterYear.Years.Year).ThenByDescending(x => x.SemesterYear.SemesterID).ThenBy(x => x.Courses.Departments.DepartmentName);
            return View();
        }
        
        public ActionResult RegisterStudent(int? id)
        {
            if (User.Identity.IsAuthenticated && id!=null)
            {
                AvailableCourses Course = db.AvailableCourses.Find(id);
                
                return View(Course);

            } else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        public ActionResult RegisterStudent(int id)
        {
            Registrations reg = new Registrations();
            Students user = new StudentsController().QueryStudentID(User.Identity.GetUserId());
            AvailableCourses course = db.AvailableCourses.Find(id);
            reg.AvailableCourseID = id;
            reg.StudentID = user.StudentID;
            db.Registrations.Add(reg);
            db.SaveChanges();
            return RedirectToAction("Search");

        }
        // GET: AvailableCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvailableCourses availableCourses = db.AvailableCourses.Find(id);
            if (availableCourses == null)
            {
                return HttpNotFound();
            }
            return View(availableCourses);
        }

        // GET: AvailableCourses/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.BuildingID = new SelectList(db.Buildings, "BuildingID", "Abbreviaion");
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName");
            ViewBag.DayID = new SelectList(db.Days, "DaysID", "Day");
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName");
            ViewBag.AvailalbeCourseID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID");
            ViewBag.TimeID = new SelectList(db.Times, "TimeID", "Times1");
            return View();
        }

        // POST: AvailableCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "AvailalbeCourseID,CourseID,Section,CRN,TimeID,DayID,BuildingID,ProfessorID,SemesterYearID,RoomNumber")] AvailableCourses availableCourses)
        {
            if (ModelState.IsValid)
            {
                db.AvailableCourses.Add(availableCourses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BuildingID = new SelectList(db.Buildings, "BuildingID", "Abbreviaion", availableCourses.BuildingID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", availableCourses.CourseID);
            ViewBag.DayID = new SelectList(db.Days, "DaysID", "Day", availableCourses.DayID);
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", availableCourses.ProfessorID);
            ViewBag.AvailalbeCourseID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID", availableCourses.AvailalbeCourseID);
            ViewBag.TimeID = new SelectList(db.Times, "TimeID", "Times1", availableCourses.TimeID);
            return View(availableCourses);
        }

        // GET: AvailableCourses/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvailableCourses availableCourses = db.AvailableCourses.Find(id);
            if (availableCourses == null)
            {
                return HttpNotFound();
            }
            ViewBag.BuildingID = new SelectList(db.Buildings, "BuildingID", "Abbreviaion", availableCourses.BuildingID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", availableCourses.CourseID);
            ViewBag.DayID = new SelectList(db.Days, "DaysID", "Day", availableCourses.DayID);
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", availableCourses.ProfessorID);
            ViewBag.AvailalbeCourseID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID", availableCourses.AvailalbeCourseID);
            ViewBag.TimeID = new SelectList(db.Times, "TimeID", "Times1", availableCourses.TimeID);
            return View(availableCourses);
        }
        

        // POST: AvailableCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "AvailalbeCourseID,CourseID,Section,CRN,TimeID,DayID,BuildingID,ProfessorID,SemesterYearID,RoomNumber")] AvailableCourses availableCourses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(availableCourses).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BuildingID = new SelectList(db.Buildings, "BuildingID", "Abbreviaion", availableCourses.BuildingID);
            ViewBag.CourseID = new SelectList(db.Courses, "CourseID", "CourseName", availableCourses.CourseID);
            ViewBag.DayID = new SelectList(db.Days, "DaysID", "Day", availableCourses.DayID);
            ViewBag.ProfessorID = new SelectList(db.Professors, "ProfessorID", "FirstName", availableCourses.ProfessorID);
            ViewBag.AvailalbeCourseID = new SelectList(db.SemesterYear, "SemesterYearID", "SemesterYearID", availableCourses.AvailalbeCourseID);
            ViewBag.TimeID = new SelectList(db.Times, "TimeID", "Times1", availableCourses.TimeID);
            return View(availableCourses);
        }

        // GET: AvailableCourses/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvailableCourses availableCourses = db.AvailableCourses.Find(id);
            if (availableCourses == null)
            {
                return HttpNotFound();
            }
            return View(availableCourses);
        }

        // POST: AvailableCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            AvailableCourses availableCourses = db.AvailableCourses.Find(id);
            db.AvailableCourses.Remove(availableCourses);
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
