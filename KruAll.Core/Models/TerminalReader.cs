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
    
    public partial class TerminalReader
    {
        public TerminalReader()
        {
            this.ReaderAccessPlans = new HashSet<ReaderAccessPlan>();
            this.ReaderAssigneds = new HashSet<ReaderAssigned>();
            this.ReaderVisitorPlans = new HashSet<ReaderVisitorPlan>();
        }
    
        public long ID { get; set; }
        public Nullable<long> TermID { get; set; }
        public int ReaderID { get; set; }
        public Nullable<int> ReaderNr { get; set; }
        public Nullable<int> Direction { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> RelayTime { get; set; }
        public string ReaderInfo { get; set; }
        public string Category { get; set; }
        public Nullable<long> BeforeAlarm { get; set; }
        public Nullable<long> AlarmFrom { get; set; }
        public string ReaderType { get; set; }
        public string Name { get; set; }
        public string Memo { get; set; }
        public Nullable<int> Lock { get; set; }
        public Nullable<int> Delay { get; set; }
        public string ReaderImage { get; set; }
        public string TerminalReaderID { get; set; }
    
        public virtual ICollection<ReaderAccessPlan> ReaderAccessPlans { get; set; }
        public virtual ICollection<ReaderAssigned> ReaderAssigneds { get; set; }
        public virtual ICollection<ReaderVisitorPlan> ReaderVisitorPlans { get; set; }
        public virtual TerminalConfig TerminalConfig { get; set; }
        public virtual TerminalReadersStatic TerminalReadersStatic { get; set; }
    }
}