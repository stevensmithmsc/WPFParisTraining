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
    
    public partial class search_session_Result
    {
        public int ID { get; set; }
        public int Course { get; set; }
        public Nullable<int> Trainer { get; set; }
        public Nullable<short> Location { get; set; }
        public Nullable<System.DateTime> Strt { get; set; }
        public Nullable<System.DateTime> Endt { get; set; }
        public short MaxP { get; set; }
        public string Notes { get; set; }
    }
}
