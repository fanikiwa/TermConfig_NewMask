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
    
    public partial class HolidayPlanCalendar
    {
        public HolidayPlanCalendar()
        {
            this.HolidayPlanAccessProfileMonths = new HashSet<HolidayPlanAccessProfileMonth>();
            this.HolidayPlanCalendarMonths = new HashSet<HolidayPlanCalendarMonth>();
            this.SwitchPlans = new HashSet<SwitchPlan>();
            this.TbAccessPlans = new HashSet<TbAccessPlan>();
            this.TbAccessPlanGroups = new HashSet<TbAccessPlanGroup>();
            this.TbVisitorPlans = new HashSet<TbVisitorPlan>();
        }
    
        public long Id { get; set; }
        public long HolidayCalenderId { get; set; }
        public int CalendarYear { get; set; }
        public long HolidayPlanCalendarNumber { get; set; }
        public string HolidayPlanCalendarName { get; set; }
        public string Memo { get; set; }
        public Nullable<int> AllowAccess { get; set; }
    
        public virtual HolidayCalendar HolidayCalendar { get; set; }
        public virtual ICollection<HolidayPlanAccessProfileMonth> HolidayPlanAccessProfileMonths { get; set; }
        public virtual ICollection<HolidayPlanCalendarMonth> HolidayPlanCalendarMonths { get; set; }
        public virtual ICollection<SwitchPlan> SwitchPlans { get; set; }
        public virtual ICollection<TbAccessPlan> TbAccessPlans { get; set; }
        public virtual ICollection<TbAccessPlanGroup> TbAccessPlanGroups { get; set; }
        public virtual ICollection<TbVisitorPlan> TbVisitorPlans { get; set; }
    }
}