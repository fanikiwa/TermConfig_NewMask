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
    
    public partial class MemberHealthCard
    {
        public long ID { get; set; }
        public Nullable<long> MemberID { get; set; }
        public string BoxNumber { get; set; }
        public string CreatedIn { get; set; }
        public string ItemNumber { get; set; }
        public string SecurityNumber { get; set; }
        public string CardNumber { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Memo { get; set; }
    
        public virtual MembersData MembersData { get; set; }
    }
}
