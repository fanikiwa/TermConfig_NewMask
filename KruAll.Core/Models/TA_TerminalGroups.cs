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
    
    public partial class TA_TerminalGroups
    {
        public TA_TerminalGroups()
        {
            this.TA_PersonalGroupMapping = new HashSet<TA_PersonalGroupMapping>();
            this.TA_TerminalGroupMapping = new HashSet<TA_TerminalGroupMapping>();
        }
    
        public long ID { get; set; }
        public long GroupNr { get; set; }
        public string GroupDescription { get; set; }
    
        public virtual ICollection<TA_PersonalGroupMapping> TA_PersonalGroupMapping { get; set; }
        public virtual ICollection<TA_TerminalGroupMapping> TA_TerminalGroupMapping { get; set; }
    }
}
