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
    
    public partial class HolidayPlanCalendarMapped
    {
        public HolidayPlanCalendarMapped()
        {
            this.HolidayPlanCalendarMonthMappeds = new HashSet<HolidayPlanCalendarMonthMapped>();
        }
    
        public long Id { get; set; }
        public int CalendarYear { get; set; }
        public long HolidayPlanCalendarNumber { get; set; }
        public string HolidayPlanCalendarName { get; set; }
        public string Memo { get; set; }
    
        public virtual ICollection<HolidayPlanCalendarMonthMapped> HolidayPlanCalendarMonthMappeds { get; set; }
    }
}
