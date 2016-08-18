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
    
    public partial class AccessGroup
    {
        public AccessGroup()
        {
            this.AccessProfileAccessGroupMappings = new HashSet<AccessProfileAccessGroupMapping>();
            this.AccessGroupEmployees = new HashSet<AccessGroupEmployee>();
            this.TbAccessPlans = new HashSet<TbAccessPlan>();
            this.ZuttritProfiles = new HashSet<ZuttritProfile>();
        }
    
        public long Id { get; set; }
        public int AccessGroupNumber { get; set; }
        public string AccessGroupName { get; set; }
        public int AccessGroupType { get; set; }
        public string Memo { get; set; }
    
        public virtual ICollection<AccessProfileAccessGroupMapping> AccessProfileAccessGroupMappings { get; set; }
        public virtual ICollection<AccessGroupEmployee> AccessGroupEmployees { get; set; }
        public virtual ICollection<TbAccessPlan> TbAccessPlans { get; set; }
        public virtual ICollection<ZuttritProfile> ZuttritProfiles { get; set; }
    }
}
