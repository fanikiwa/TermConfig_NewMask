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
    
    public partial class Department
    {
        public Department()
        {
            this.Areas = new HashSet<Area>();
            this.Pers_Departments = new HashSet<Pers_Departments>();
        }
    
        public long ID { get; set; }
        public string Name { get; set; }
        public long Department_Nr { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public Nullable<long> LocationId { get; set; }
        public string LocationHeadName { get; set; }
        public string LocationHeadFunction { get; set; }
        public string LocationHeadPhone { get; set; }
        public string LocationHeadMobile { get; set; }
        public string LocationHeadEmail { get; set; }
        public string InfoText { get; set; }
        public string Place { get; set; }
    
        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Pers_Departments> Pers_Departments { get; set; }
    }
}
