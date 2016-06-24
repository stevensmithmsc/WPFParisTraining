//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFParisTraining.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Course
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Course()
        {
            this.ReqFor = new HashSet<CourseReq>();
            this.PreReqs = new HashSet<CourseReq>();
            this.StaffReqs = new HashSet<Req>();
            this.Sesses = new HashSet<Sess>();
        }
    
        public int ID { get; set; }
        public string CourseName { get; set; }
        public Nullable<short> Length { get; set; }
        public string Notes { get; set; }
        public bool External { get; set; }
        public Nullable<bool> Paris { get; set; }
        public short Template { get; set; }
        public Nullable<int> FullColour { get; set; }
        public Nullable<int> BookedColour { get; set; }
        public Nullable<int> EmptyColour { get; set; }
        public bool Obselete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseReq> ReqFor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseReq> PreReqs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Req> StaffReqs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sess> Sesses { get; set; }
    }
}