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
    
    public partial class AccessCalendar
    {
        public AccessCalendar()
        {
            this.AccessCalendarMonths = new HashSet<AccessCalendarMonth>();
            this.TbAccessPlanGroups = new HashSet<TbAccessPlanGroup>();
            this.TbVisitorPlans = new HashSet<TbVisitorPlan>();
        }
    
        public long ID { get; set; }
        public int Calendar_Nr { get; set; }
        public string Calendar_Name { get; set; }
        public long AccessProfileID { get; set; }
        public string Memo { get; set; }
        public bool CheckMon { get; set; }
        public bool CheckTue { get; set; }
        public bool CheckWed { get; set; }
        public bool CheckThur { get; set; }
        public bool CheckFri { get; set; }
        public bool CheckSat { get; set; }
        public bool CheckSun { get; set; }
        public System.DateTime CalendarDate { get; set; }
    
        public virtual ICollection<AccessCalendarMonth> AccessCalendarMonths { get; set; }
        public virtual ICollection<TbAccessPlanGroup> TbAccessPlanGroups { get; set; }
        public virtual ICollection<TbVisitorPlan> TbVisitorPlans { get; set; }
    }
}