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
    
    public partial class vwSwitchProfile
    {
        public long BuildingPlanID { get; set; }
        public int TermID { get; set; }
        public long TerminalID { get; set; }
        public string TermType { get; set; }
        public string TerminalDescription { get; set; }
        public long CalendarID { get; set; }
        public int SwitchProfileID { get; set; }
        public string Profile_Id { get; set; }
        public long Profile_Nr { get; set; }
        public string ProfileDescription { get; set; }
        public int Number { get; set; }
        public int DayFrom { get; set; }
        public int DayTo { get; set; }
        public System.DateTime TimeFrom { get; set; }
        public System.DateTime TimeTo { get; set; }
        public string Memo { get; set; }
    }
}