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
    
    public partial class HolidayAccessPlam_with_DateTime
    {
        public Nullable<System.DateTime> Datum { get; set; }
        public long AccessProfileID { get; set; }
        public long ID { get; set; }
        public long HolidayPlanCalendarId { get; set; }
        public long ZPID { get; set; }
        public Nullable<long> ZPGroupID { get; set; }
        public string ZPAccessProfileID { get; set; }
        public int ZPAccessProfileNo { get; set; }
        public string ZPAccessDescription { get; set; }
        public long HPCId { get; set; }
        public long HPCHolidayCalenderId { get; set; }
        public long HPCHolidayPlanCalendarNumber { get; set; }
        public string HPCHolidayPlanCalendarName { get; set; }
    }
}
