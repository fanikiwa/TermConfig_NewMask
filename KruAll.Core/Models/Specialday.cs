//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KruAll.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Specialday
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
        public Nullable<int> Absence_Reason { get; set; }
        public Nullable<int> LocationID { get; set; }
    
        public virtual Absence Absence { get; set; }
    }
}
