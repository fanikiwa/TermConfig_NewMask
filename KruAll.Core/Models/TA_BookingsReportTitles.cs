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
    
    public partial class TA_BookingsReportTitles
    {
        public int ID { get; set; }
        public int ReportID { get; set; }
        public int PositionNr { get; set; }
        public string PositionTitle { get; set; }
    
        public virtual TA_BookingsReport TA_BookingsReport { get; set; }
    }
}
