//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Headmaster.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MinorRequirements
    {
        public int MinorRequirementsID { get; set; }
        public int MinorID { get; set; }
        public int CourseID { get; set; }
    
        public virtual Courses Courses { get; set; }
        public virtual Minors Minors { get; set; }
    }
}
