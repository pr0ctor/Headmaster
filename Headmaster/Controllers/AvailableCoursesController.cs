﻿using System;
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
        [HttpPost]
        public JsonResult  GetFilteredCourses(int id)
        {
            List<SelectListItem> items = (from s in db.Courses
                        where s.DepartmentID == id
                        select new SelectListItem
                        {
                            Text = s.CourseName,
                            Value = s.CourseID.ToString()
                        }).ToList();
            ViewBag.CourseNumbers = items;
            return Json(items, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SearchAndRegister()
        {
            if (User.Identity.IsAuthenticated)
            {
                
                    var SemesterYear = (from s in db.SemesterYear.AsEnumerable()
                                        select new SelectListItem
                                        {
                                            Text = s.SemesterYearName,
                                            Value = s.SemesterID.ToString()
                        
                                        }).ToList();

                
                var Dept = (from s in db.Departments.AsEnumerable()
                            select new SelectListItem
                            {
                                Text = s.DepartmentName,
                                Value = s.DepartmentID.ToString()

                            }).ToList();
          

                var Course = (from s in db.Courses.AsEnumerable()
                               select new SelectListItem
                               {
                                   Text = s.CourseName,
                                   Value = s.CourseID.ToString()
                    
                            }).ToList();

                ViewBag.SemesterYearID = new SelectList(SemesterYear, "Value", "Text");
                ViewBag.DepartmentID = new SelectList(Dept, "Value", "Text");
                ViewBag.CourseNumber = new SelectList(Course, "Value", "Text");
                ViewData["Course"]= db.AvailableCourses.ToList();
               
               

                return View();
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
       
       
        [HttpPost]
        public ActionResult SearchAndRegister(AvailableCourses model)
        {
            var search= db.AvailableCourses.ToList();
            if (ModelState.IsValid)
            {
                var courses = from s in db.AvailableCourses
                              where model.SemesterYear.SemesterYearID == s.SemesterYear.SemesterYearID &&
                              model.Courses.DepartmentID == s.Courses.DepartmentID &&
                              model.CourseID == s.CourseID
                              select s;
                search=courses.ToList();

            }
            ViewBag.SemesterYearID = new SelectList(db.SemesterYear);
         
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Department");
            ViewData["Course"]= search;
            return View();
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
