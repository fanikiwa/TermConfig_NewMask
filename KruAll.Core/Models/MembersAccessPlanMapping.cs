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
    
    public partial class MembersAccessPlanMapping
    {
        public long ID { get; set; }
        public Nullable<long> AccessPlan_Nr { get; set; }
        public Nullable<long> MemberID { get; set; }
        public Nullable<System.DateTime> DateFrom { get; set; }
        public Nullable<System.DateTime> DateTo { get; set; }
        public string AutomaticLogout { get; set; }
    
        public virtual MembersData MembersData { get; set; }
        public virtual TbAccessPlan TbAccessPlan { get; set; }
    }
}
