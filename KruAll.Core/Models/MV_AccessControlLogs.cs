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
    
    public partial class MV_AccessControlLogs
    {
        public long Pers_Nr { get; set; }
        public string Name { get; set; }
        public long Card_Nr { get; set; }
        public System.DateTime AccessTime { get; set; }
        public Nullable<int> LocationID { get; set; }
        public Nullable<int> BuildingID { get; set; }
        public Nullable<int> FloorID { get; set; }
        public Nullable<int> RoomID { get; set; }
        public int DoorID { get; set; }
        public string LocationName { get; set; }
        public string DepartmentName { get; set; }
        public string PlanDefinition { get; set; }
    }
}