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
    
    public partial class BoroughMem
    {
        public int Key { get; set; }
        public string Type { get; set; }
        public int ID { get; set; }
        public string BoroughID { get; set; }
    
        public virtual Borough Borough { get; set; }
    }
}
