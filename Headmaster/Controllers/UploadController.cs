using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Headmaster.Models;
using Headmaster.Helper;
using System.IO;

namespace Headmaster.Controllers
{
    public class UploadController : Controller
    {
        private headmasterEntities db = new headmasterEntities();
        // GET: Upload  
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UploadFile()
        {
            var SemesterYear = (from s in db.SemesterYear.OrderByDescending(x => x.Years.Year).ThenBy(x => x.SemesterID).AsEnumerable()
                                select new SelectListItem
                                {
                                    Text = s.SemesterYearName,
                                    Value = s.SemesterYearID.ToString()

                                }).ToList();
            ViewBag.SemesterYear = SemesterYear;
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file, SemesterYear model)
        {

            /*
             * Check is if semesteryearId!=0 if it does refresh view 
             * 
             */
            if (model.SemesterYearID != 0)
            {
                String path = "";
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Uploads"), _FileName);
                        path = _path;
                        file.SaveAs(_path);
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";

                    CSVReader reader = new CSVReader();
                    reader.setPath(path);
                    reader.setSemesterYear(model.SemesterYearID);
                    reader.parse();

                    var SemesterYear = (from s in db.SemesterYear.OrderByDescending(x => x.Years.Year).ThenBy(x => x.SemesterID).AsEnumerable()
                                        select new SelectListItem
                                        {
                                            Text = s.SemesterYearName,
                                            Value = s.SemesterYearID.ToString()

                                        }).ToList();
                    ViewBag.SemesterYear = SemesterYear;
                    return View();
                }
                catch
                {
                    var SemesterYear = (from s in db.SemesterYear.OrderByDescending(x => x.Years.Year).ThenBy(x => x.SemesterID).AsEnumerable()
                                        select new SelectListItem
                                        {
                                            Text = s.SemesterYearName,
                                            Value = s.SemesterYearID.ToString()

                                        }).ToList();
                    ViewBag.SemesterYear = SemesterYear;
                    ViewBag.Message = "File upload failed!!";
                    return View();
                }
            }
            else
            {
                var SemesterYear = (from s in db.SemesterYear.OrderByDescending(x => x.Years.Year).ThenBy(x => x.SemesterID).AsEnumerable()
                                    select new SelectListItem
                                    {
                                        Text = s.SemesterYearName,
                                        Value = s.SemesterYearID.ToString()

                                    }).ToList();
                ViewBag.SemesterYear = SemesterYear;
                return View();
            }
        }
    }
}