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
    
    public partial class Handy
    {
        public int Id { get; set; }
        public long CellID { get; set; }
        public string HandyName { get; set; }
        public string HandyIMEI { get; set; }
        public bool Licenced { get; set; }
        public int LicenceLevel { get; set; }
        public string MobileNumber { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string Username { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public System.DateTime OutputOn { get; set; }
        public string ContractingParty { get; set; }
        public System.DateTime Inception { get; set; }
        public System.DateTime ContractEnd { get; set; }
        public string LeaseTerm { get; set; }
        public string Tariff { get; set; }
        public string TariffPricePerMonth { get; set; }
        public bool TimeTracking { get; set; }
        public bool CostCenter { get; set; }
        public bool CostandFlexCostCenter { get; set; }
        public bool ProjectTracking { get; set; }
        public bool Customer { get; set; }
        public bool Project { get; set; }
        public bool IsOrder { get; set; }
    
        public virtual Abteilungen Abteilungen { get; set; }
        public virtual Personalstamm Personalstamm { get; set; }
    }
}
