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
    
    public partial class Shift
    {
        public Shift()
        {
            this.ResourceEvents = new HashSet<ResourceEvent>();
            this.ResourceEventMappeds = new HashSet<ResourceEventMapped>();
        }
    
        public long ID { get; set; }
        public string ShiftName { get; set; }
        public string ShiftDescription { get; set; }
        public int DailyProgramId { get; set; }
    
        public virtual DailyProgram DailyProgram { get; set; }
        public virtual ICollection<ResourceEvent> ResourceEvents { get; set; }
        public virtual ICollection<ResourceEventMapped> ResourceEventMappeds { get; set; }
    }
}
