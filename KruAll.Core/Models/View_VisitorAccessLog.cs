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
    
    public partial class View_VisitorAccessLog
    {
        public Nullable<long> ID { get; set; }
        public long Pers_Nr { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Nullable<System.DateTime> ExitDate { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> AccessEndData { get; set; }
        public int TA_Come { get; set; }
        public int TA_Go { get; set; }
        public System.DateTime BookingTime { get; set; }
        public int BookingCorrection { get; set; }
        public string DynamicField1 { get; set; }
        public long LogID { get; set; }
        public string TerminalSerial { get; set; }
        public string ReaderID { get; set; }
    }
}
