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
    
    public partial class Attendance
    {
        public int ID { get; set; }
        public int StaffID { get; set; }
        public int SessID { get; set; }
        public Nullable<short> Outcome { get; set; }
        public string Comments { get; set; }
        public Nullable<int> BookedBy { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public Nullable<int> CancelledBy { get; set; }
    
        public virtual Staff Booker { get; set; }
        public virtual Staff Canceller { get; set; }
        public virtual Sess Sess { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
