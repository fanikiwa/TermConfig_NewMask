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
    
    public partial class AuditHist
    {
        public long usr_id { get; set; }
        public string u_action { get; set; }
        public string comment { get; set; }
        public System.DateTime audit_date { get; set; }
        public Nullable<long> ID { get; set; }
    }
}