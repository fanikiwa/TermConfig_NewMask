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
    
    public partial class LocationsFederalState
    {
        public LocationsFederalState()
        {
            this.Clients = new HashSet<Client>();
        }
    
        public long ID { get; set; }
        public Nullable<int> StateNo { get; set; }
        public string StateName { get; set; }
        public string Memo { get; set; }
    
        public virtual ICollection<Client> Clients { get; set; }
    }
}