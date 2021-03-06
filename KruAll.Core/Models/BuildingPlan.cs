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
    
    public partial class BuildingPlan
    {
        public BuildingPlan()
        {
            this.AccessPlanGroupDoorMappings = new HashSet<AccessPlanGroupDoorMapping>();
            this.ReaderAssigneds = new HashSet<ReaderAssigned>();
            this.SwitchPlans = new HashSet<SwitchPlan>();
            this.TbAccessPlans = new HashSet<TbAccessPlan>();
            this.TbAccessPlanGroups = new HashSet<TbAccessPlanGroup>();
            this.TbVisitorPlans = new HashSet<TbVisitorPlan>();
        }
    
        public long ID { get; set; }
        public int PlanNr { get; set; }
        public string PlanName { get; set; }
        public string PlanDefinition { get; set; }
        public string Memo { get; set; }
        public int LastNodeKey { get; set; }
    
        public virtual ICollection<AccessPlanGroupDoorMapping> AccessPlanGroupDoorMappings { get; set; }
        public virtual ICollection<ReaderAssigned> ReaderAssigneds { get; set; }
        public virtual ICollection<SwitchPlan> SwitchPlans { get; set; }
        public virtual ICollection<TbAccessPlan> TbAccessPlans { get; set; }
        public virtual ICollection<TbAccessPlanGroup> TbAccessPlanGroups { get; set; }
        public virtual ICollection<TbVisitorPlan> TbVisitorPlans { get; set; }
    }
}
