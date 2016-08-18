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
    
    public partial class VehicleType
    {
        public VehicleType()
        {
            this.Pers_Vehicles = new HashSet<Pers_Vehicles>();
            this.Visitor_Vehicle = new HashSet<Visitor_Vehicle>();
            this.Visitors = new HashSet<Visitor>();
        }
    
        public long ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Memo { get; set; }
        public Nullable<int> VehicleNr { get; set; }
        public byte[] VehiclePhoto { get; set; }
    
        public virtual ICollection<Pers_Vehicles> Pers_Vehicles { get; set; }
        public virtual ICollection<Visitor_Vehicle> Visitor_Vehicle { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
    }
}