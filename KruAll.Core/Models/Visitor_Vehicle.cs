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
    
    public partial class Visitor_Vehicle
    {
        public long ID { get; set; }
        public Nullable<long> VisitorID { get; set; }
        public Nullable<long> VehicleID { get; set; }
    
        public virtual VehicleType VehicleType { get; set; }
    }
}
