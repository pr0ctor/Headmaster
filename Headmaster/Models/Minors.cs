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
    
    public partial class Minors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Minors()
        {
            this.MinorRequirements = new HashSet<MinorRequirements>();
        }
    
        public int MinorID { get; set; }
        public string MinorName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MinorRequirements> MinorRequirements { get; set; }
        public virtual StudentMinors StudentMinors { get; set; }
    }
}
