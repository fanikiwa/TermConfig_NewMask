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
    
    public partial class TerminalBookingRawData
    {
        public long ID { get; set; }
        public int EmpNumber { get; set; }
        public System.DateTime BookingDate { get; set; }
        public Nullable<int> StatusNr { get; set; }
        public Nullable<int> ProcessingStatus { get; set; }
        public string Comment { get; set; }
        public string Source { get; set; }
    }
}
