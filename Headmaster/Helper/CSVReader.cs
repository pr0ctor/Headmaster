﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic.FileIO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Headmaster.Helper
{
    class CSVReader
    {
        public String path { get; set; }
        public int SemesterYear { get; set; }

        public void setSemesterYear(int semesterYear)
        {
            SemesterYear = semesterYear;
        }

        public void setPath(String newPath)
        {
            path = newPath;
        }


        public void parse()
        {
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();
                int counter = 0;
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    //string Name = fields[0];
                    //string Address = fields[1];
                    CSVCourse course = new CSVCourse();

                    course.setCVSCourse(fields[0], fields[1], fields[2], fields[3], fields[4], fields[5], fields[6], fields[7], fields[8], fields[9], fields[10]);

                    course.dbInsert(SemesterYear);


                    Console.WriteLine(counter + " " + course.toString());
                    counter++;
                }
            }
        }
    }


    class CSVCourse
    {
        public String CourseReferenceNumber { get; set; }
        public String Subject { get; set; }
        public String ActualCourseNumber { get; set; }
        public String Section { get; set; }
        public String CourseTitle { get; set; }
        public String BuildingAbbr { get; set; }
        public String BuildingLongName { get; set; }
        public String RoomNumber { get; set; }
        public String Instructor { get; set; }
        public String Days { get; set; }
        public String Times { get; set; }
        public String path { get; set; }
        public int SemesterYearID { get; set; }

        public void setCVSCourse(String CRN, String Sub, String CN, String Sec, String CrsTtle, String BdAbbr, String BdLN, String Room, String Prof, String Day, String Time)
        {
            CourseReferenceNumber = CRN;
            Subject = Sub; //Department
            ActualCourseNumber = calculateActualCourseNumber(CN);
            Section = Sec;
            CourseTitle = CrsTtle;
            BuildingAbbr = BdAbbr;
            BuildingLongName = BdLN;
            RoomNumber = Room;
            Instructor = Prof;
            Days = Day.Trim();
            Times = Time.Trim();

        }

        public void setSemesterYear(int semesterYear)
        {
            SemesterYearID = semesterYear;
        }

        //Gets rid of the begining 'U' before the actual course number.
        private String calculateActualCourseNumber(String value)
        {
            return value.Substring(1);
        }

        //Insert this class into the databse.
        public void dbInsert(int SemesterYear)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertAvailableCourse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CourseReferenceNumber", CourseReferenceNumber);
                    cmd.Parameters.AddWithValue("@Department", Subject);
                    cmd.Parameters.AddWithValue("@CourseNumber", ActualCourseNumber);
                    cmd.Parameters.AddWithValue("@Section", Section);
                    cmd.Parameters.AddWithValue("@CourseName", CourseTitle);
                    cmd.Parameters.AddWithValue("@Abbreviation", BuildingAbbr);
                    cmd.Parameters.AddWithValue("@BuildingName", BuildingLongName);
                    cmd.Parameters.AddWithValue("@RoomNumber", RoomNumber);
                    cmd.Parameters.AddWithValue("@ProfessorName", Instructor);
                    cmd.Parameters.AddWithValue("@Days", Days);
                    cmd.Parameters.AddWithValue("@Times", Times);
                    cmd.Parameters.AddWithValue("@SemesterYearID", SemesterYear);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public String toString()
        {
            String result = CourseReferenceNumber + " " + Subject + " " + ActualCourseNumber + " " + Section +
                " " + CourseTitle + " " + BuildingAbbr + " " + BuildingLongName + " " + RoomNumber + " " +
                Instructor + " " + Days + " " + Times;
            return result;
        }

    }
}