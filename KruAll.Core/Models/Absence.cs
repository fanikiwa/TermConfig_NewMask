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
    
    public partial class Absence
    {
        public Absence()
        {
            this.EmployeeAbsences = new HashSet<EmployeeAbsence>();
            this.Specialdays = new HashSet<Specialday>();
        }
    
        public int ID { get; set; }
        public string AbsenceNo { get; set; }
        public string Indicator { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Comment { get; set; }
        public Nullable<bool> BookingStatus { get; set; }
        public Nullable<int> ForeColor { get; set; }
        public Nullable<int> BackColor { get; set; }
        public Nullable<double> Priority { get; set; }
        public Nullable<double> Factor { get; set; }
        public string DistComment { get; set; }
        public Nullable<double> DistFactor { get; set; }
    
        public virtual ICollection<EmployeeAbsence> EmployeeAbsences { get; set; }
        public virtual ICollection<Specialday> Specialdays { get; set; }
    }
}
