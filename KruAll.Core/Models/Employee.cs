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
    
    public partial class Employee
    {
        public Employee()
        {
            this.AccessGroupEmployees = new HashSet<AccessGroupEmployee>();
            this.Addresses = new HashSet<Address>();
            this.DailyAccountTimes = new HashSet<DailyAccountTime>();
            this.EmployeeAbsences = new HashSet<EmployeeAbsence>();
            this.EmployeeTariffs = new HashSet<EmployeeTariff>();
            this.WorkedHoursAccs = new HashSet<WorkedHoursAcc>();
        }
    
        public int ID { get; set; }
        public int EmpNumber { get; set; }
        public string PassportID { get; set; }
        public int Identification { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public bool Status { get; set; }
        public Nullable<int> Identification2 { get; set; }
        public Nullable<int> Identification3 { get; set; }
        public string MiFareID { get; set; }
        public Nullable<System.DateTime> JoiningDate { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public int InfoId { get; set; }
    
        public virtual ICollection<AccessGroupEmployee> AccessGroupEmployees { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<DailyAccountTime> DailyAccountTimes { get; set; }
        public virtual EmployeeInfo EmployeeInfo { get; set; }
        public virtual ICollection<EmployeeAbsence> EmployeeAbsences { get; set; }
        public virtual ICollection<EmployeeTariff> EmployeeTariffs { get; set; }
        public virtual ICollection<WorkedHoursAcc> WorkedHoursAccs { get; set; }
    }
}