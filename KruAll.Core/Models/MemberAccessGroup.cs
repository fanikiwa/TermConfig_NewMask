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
    
    public partial class MemberAccessGroup
    {
        public long ID { get; set; }
        public long MemberID { get; set; }
        public long GroupID { get; set; }
        public bool Active { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
    
        public virtual TbAccessPlanGroup TbAccessPlanGroup { get; set; }
        public virtual MembersData MembersData { get; set; }
    }
}
