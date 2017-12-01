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
            //var semesterYear = db.SemesterYear.Include(s => s.Years).Include(s => s.Semesters).OrderByDescending(x => x.Years.Year).ThenByDescending(x => x.SemesterID);
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
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
                //reader.setSemesterYear(); [RYAN SET THIS VALUE]
                reader.parse();


                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }
}