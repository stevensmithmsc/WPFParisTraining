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
    
    public partial class CourseReq
    {
        public int ID { get; set; }
        public int CourseID { get; set; }
        public int PreReqID { get; set; }
    
        public virtual Course PreReq { get; set; }
        public virtual Course Course { get; set; }
    }
}
