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
    
    public partial class Portal_PermissionProfile
    {
        public Portal_PermissionProfile()
        {
            this.Portal_PermissionMapping = new HashSet<Portal_PermissionMapping>();
            this.Portal_ProfileUSerMapping = new HashSet<Portal_ProfileUSerMapping>();
        }
    
        public long ID { get; set; }
        public int ProfileNr { get; set; }
        public string ProfileDescription { get; set; }
        public string Memo { get; set; }
    
        public virtual ICollection<Portal_PermissionMapping> Portal_PermissionMapping { get; set; }
        public virtual ICollection<Portal_ProfileUSerMapping> Portal_ProfileUSerMapping { get; set; }
    }
}