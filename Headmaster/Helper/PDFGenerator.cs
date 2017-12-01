using System;
using System.Collections;
using System.Text;
using iTextSharp.text.pdf;
using System.IO;
using Headmaster.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Headmaster.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
//This method creates a new PDF from the existing PDF in resources and then populates the PDF with the current Registration
namespace Headmaster.Helper
{
    public class PDFGenerator
    {   
        
        private headmasterEntities db = new headmasterEntities();
        String fileName {get;set;}

        public String getFileName()
        {
            return fileName;
        }
        public void setFileName(String name)
        {
            fileName = name;
        }

        public PDFGenerator()
        {
        }
            
        public String FillForm()
        {       

                
            Students student = new StudentsController().QueryStudentID(HttpContext.Current.User.Identity.GetUserId());
            int Year = new StudentsController().getLastYear(student);
            int Semester = new StudentsController().GetLastSemester(Year, student);

            //Pulls most recent Registration
                var course = from s in db.Registrations
                             where s.StudentID == student.StudentID && s.AvailableCourses.SemesterYear.Years.Year == Year
                             && s.AvailableCourses.SemesterYear.SemesterID == Semester
                             select s;
            // String studentFirstName = student.FirstName
            Registrations currentRegistration = course.FirstOrDefault();
            Random rand = new Random();
            int randomNumber = rand.Next(Int32.MaxValue);

            String url = student.FirstName + student.LastName + 
                         (currentRegistration.AvailableCourses.SemesterYear.Years.Year) +
                         (currentRegistration.AvailableCourses.SemesterYear.Semesters.Semester) + 
                         randomNumber.ToString();

                        
            string pdfTemplate = HttpContext.Current.Server.MapPath("~/Resources/AdvisementForm.pdf");
            string newFile = HttpContext.Current.Server.MapPath("~/temp/"+url+".pdf");
            setFileName(url+".pdf");
            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(newFile, FileMode.Create));
            AcroFields pdfFormFields = pdfStamper.AcroFields;

                pdfFormFields.SetField("First Name",student.FirstName);
                pdfFormFields.SetField("Last Name",student.LastName);
                pdfFormFields.SetField("VIP ID",student.VIPID.ToString());
                pdfFormFields.SetField("Year",currentRegistration.AvailableCourses.SemesterYear.Years.Year.ToString());
                pdfFormFields.SetField("Term",currentRegistration.AvailableCourses.SemesterYear.Semesters.Semester);
                pdfFormFields.SetField("Major","Computer Science");

                
            String S = "S", C="C", SE="SE", CR="CR", DW="DW", T="T";
            int counter = 1;
            foreach(Registrations x in course )
            {
               pdfFormFields.SetField(S + counter, x.AvailableCourses.Courses.Departments.Abbreviation);
               pdfFormFields.SetField (C + counter, x.AvailableCourses.Courses.CourseNumber);
               pdfFormFields.SetField (SE + counter , x.AvailableCourses.Section);
               pdfFormFields.SetField (CR + counter,x.AvailableCourses.Courses.Credits.ToString());
               pdfFormFields.SetField  (DW + counter, x.AvailableCourses.Days.Day);
               pdfFormFields.SetField (T + counter,  x.AvailableCourses.Times.Times1);
               pdfFormFields.SetField("CODE"+counter, "A");
                for(int i = 1; i<=5;i++)
                {
                    pdfFormFields.SetField(counter + "." + i, x.AvailableCourses.CRN.Substring(i-1));
                }
                counter++;
            };


            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
            return newFile;
        }

    }
}