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
    
    public partial class Portal_Permissions
    {
        public Portal_Permissions()
        {
            this.Portal_PermissionMapping = new HashSet<Portal_PermissionMapping>();
        }
    
        public long ID { get; set; }
        public string Permission { get; set; }
        public string Memo { get; set; }
    
        public virtual ICollection<Portal_PermissionMapping> Portal_PermissionMapping { get; set; }
    }
}
