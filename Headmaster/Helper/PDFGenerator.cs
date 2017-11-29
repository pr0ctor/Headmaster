using System;
using System.Collections;
using System.Text;
using iTextSharp.text.pdf;
using System.IO;

namespace Headmaster.Helper
{
    public class PDFGenerator
    {

        private void FillForm()
        {
            string pdfTemplate = @"C:\Users\jdfishtorn\Downloads\Advisement Form  final corected.pdf";
            string newFile = @"C:\Users\jdfishtorn\Downloads\CompletedAdvisement Form  final corected.pdf";

            PdfReader pdfReader = new PdfReader(pdfTemplate);
            PdfStamper pdfStamper = new PdfStamper(pdfReader, new FileStream(
                        newFile, FileMode.Create));

            AcroFields pdfFormFields = pdfStamper.AcroFields;

            // set form pdfFormFields
            //First Name
            pdfFormFields.SetField("First Name", "TEST");
            //Last Name
            pdfFormFields.SetField("Last Name", "TEST");
            //middle initial 
            pdfFormFields.SetField("MI", "T");
            //VIP ID
            pdfFormFields.SetField("VIP ID", "");
            //Major
            pdfFormFields.SetField("Major", "");
            //Minor
            pdfFormFields.SetField("Minor", "");
            //year
            pdfFormFields.SetField("Year", "");
            //Semester term
            pdfFormFields.SetField("Term", "");
            //Days of the Week for courses 1-8
            pdfFormFields.SetField("DW1", "");
            pdfFormFields.SetField("DW2", "");
            pdfFormFields.SetField("DW3", "");
            pdfFormFields.SetField("DW4", "");
            pdfFormFields.SetField("DW5", "");
            pdfFormFields.SetField("DW6", "");
            pdfFormFields.SetField("DW7", "");
            pdfFormFields.SetField("DW8", "");
            //Course Credit for Courses 1-8 
            pdfFormFields.SetField("CR1", "");
            pdfFormFields.SetField("CR2", "");
            pdfFormFields.SetField("CR3", "");
            pdfFormFields.SetField("CR4", "");
            pdfFormFields.SetField("CR5", "");
            pdfFormFields.SetField("CR6", "");
            pdfFormFields.SetField("CR7", "");
            pdfFormFields.SetField("CR8", "");
            //Course Number for Courses 1-8
            pdfFormFields.SetField("C1", "");
            pdfFormFields.SetField("C2", "");
            pdfFormFields.SetField("C3", "");
            pdfFormFields.SetField("C4", "");
            pdfFormFields.SetField("C5", "");
            pdfFormFields.SetField("C6", "");
            pdfFormFields.SetField("C7", "");
            pdfFormFields.SetField("C8", "");
            //These fields are the fields for the first alternative courses after the CRN fields 
            pdfFormFields.SetField("A1.1", "");
            pdfFormFields.SetField("A1.2", "");
            pdfFormFields.SetField("A1.3", "");
            pdfFormFields.SetField("A1.4", "");
            pdfFormFields.SetField("A1.5", "");
            pdfFormFields.SetField("A1.6", "");
            pdfFormFields.SetField("A1.7", "");
            //These fields are the fields for the Second alternative courses after the CRN fields 
            pdfFormFields.SetField("A2.1", "");
            pdfFormFields.SetField("A2.2", "");
            pdfFormFields.SetField("A2.3", "");
            pdfFormFields.SetField("A2.4", "");
            pdfFormFields.SetField("A2.5", "");
            pdfFormFields.SetField("A2.6", "");
            pdfFormFields.SetField("A2.7", "");
            //These are the section number fields for courses 1-8
            pdfFormFields.SetField("SE1", "");
            pdfFormFields.SetField("SE2", "");
            pdfFormFields.SetField("SE3", "");
            pdfFormFields.SetField("SE4", "");
            pdfFormFields.SetField("SE5", "");
            pdfFormFields.SetField("SE6", "");
            pdfFormFields.SetField("SE7", "");
            pdfFormFields.SetField("SE8", "");
            //These are the CRN's for each course. Each course has a CRN consisting of 5 numbers mapped to 5 seperate fields 
            pdfFormFields.SetField("1.1", "");
            pdfFormFields.SetField("1.2", "");
            pdfFormFields.SetField("1.3", "");
            pdfFormFields.SetField("1.4", "");
            pdfFormFields.SetField("1.5", "");
            //Course 2 CRN
            pdfFormFields.SetField("2.1", "");
            pdfFormFields.SetField("2.2", "");
            pdfFormFields.SetField("2.3", "");
            pdfFormFields.SetField("2.4", "");
            pdfFormFields.SetField("2.5", "");
            //Course 3 CRN
            pdfFormFields.SetField("3.1", "");
            pdfFormFields.SetField("3.2", "");
            pdfFormFields.SetField("3.3", "");
            pdfFormFields.SetField("3.4", "");
            pdfFormFields.SetField("3.5", "");
            //Course 4 CRN
            pdfFormFields.SetField("4.1", "");
            pdfFormFields.SetField("4.2", "");
            pdfFormFields.SetField("4.3", "");
            pdfFormFields.SetField("4.4", "");
            pdfFormFields.SetField("4.5", "");
            //Course 5 CRN
            pdfFormFields.SetField("5.1", "");
            pdfFormFields.SetField("5.2", "");
            pdfFormFields.SetField("5.3", "");
            pdfFormFields.SetField("5.4", "");
            pdfFormFields.SetField("5.5", "");
            //Course 6 CRN
            pdfFormFields.SetField("6.1", "");
            pdfFormFields.SetField("6.2", "");
            pdfFormFields.SetField("6.3", "");
            pdfFormFields.SetField("6.4", "");
            pdfFormFields.SetField("6.5", "");
            //Course 7 CRN
            pdfFormFields.SetField("7.1", "");
            pdfFormFields.SetField("7.2", "");
            pdfFormFields.SetField("7.3", "");
            pdfFormFields.SetField("7.4", "");
            pdfFormFields.SetField("7.5", "");
            //Course 8 CRN
            pdfFormFields.SetField("8.1", "");
            pdfFormFields.SetField("8.2", "");
            pdfFormFields.SetField("8.3", "");
            pdfFormFields.SetField("8.4", "");
            pdfFormFields.SetField("8.5", "");
            //Alternative course 1 CRN
            pdfFormFields.SetField("9.1", "");
            pdfFormFields.SetField("9.2", "");
            pdfFormFields.SetField("9.3", "");
            pdfFormFields.SetField("9.4", "");
            pdfFormFields.SetField("9.5", "");
            //Alternative course 2 CRN
            pdfFormFields.SetField("10.1", "");
            pdfFormFields.SetField("10.2", "");
            pdfFormFields.SetField("10.3", "");
            pdfFormFields.SetField("10.4", "");
            pdfFormFields.SetField("10.5", "");
            //Subject fields for courses 1-8
            pdfFormFields.SetField("S1", "");
            pdfFormFields.SetField("S2", "");
            pdfFormFields.SetField("S3", "");
            pdfFormFields.SetField("S4", "");
            pdfFormFields.SetField("S5", "");
            pdfFormFields.SetField("S6", "");
            pdfFormFields.SetField("S7", "");
            pdfFormFields.SetField("S8", "");
            //These are the time fields for courses 1-8
            pdfFormFields.SetField("T1", "");
            pdfFormFields.SetField("T2", "");
            pdfFormFields.SetField("T3", "");
            pdfFormFields.SetField("T4", "");
            pdfFormFields.SetField("T5", "");
            pdfFormFields.SetField("T6", "");
            pdfFormFields.SetField("T7", "");
            pdfFormFields.SetField("T8", "");
            //These are the action codes for courses 1-8
            pdfFormFields.SetField("CODE1", "");
            pdfFormFields.SetField("CODE2", "");
            pdfFormFields.SetField("CODE3", "");
            pdfFormFields.SetField("CODE4", "");
            pdfFormFields.SetField("CODE5", "");
            pdfFormFields.SetField("CODE6", "");
            pdfFormFields.SetField("CODE7", "");
            pdfFormFields.SetField("CODE8", "");

            // flatten the form to remove editting options, set it to false
            // to leave the form open to subsequent manual edits
            pdfStamper.FormFlattening = false;

            // close the pdf
            pdfStamper.Close();
        }

    }
}