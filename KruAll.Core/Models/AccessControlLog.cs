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
    
    public partial class AccessControlLog
    {
        public long ID { get; set; }
        public string TerminalSerial { get; set; }
        public string ReaderID { get; set; }
        public long Card_Nr { get; set; }
        public long Pers_Nr { get; set; }
        public int Status { get; set; }
        public System.DateTime AccessTime { get; set; }
        public Nullable<long> VisitorID { get; set; }
        public Nullable<long> MemberID { get; set; }
    }
}
