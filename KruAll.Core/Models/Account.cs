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
    
    public partial class Account
    {
        public Account()
        {
            this.DailyAccountTimes = new HashSet<DailyAccountTime>();
            this.SurchargesAccountsMappings = new HashSet<SurchargesAccountsMapping>();
        }
    
        public long ID { get; set; }
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public string Abbr { get; set; }
        public Nullable<bool> StandardAccount { get; set; }
        public string TransferAcc { get; set; }
        public string DisplayFormat { get; set; }
        public string BillingMacro { get; set; }
        public Nullable<System.DateTime> BIllingDate { get; set; }
        public string AccInfo { get; set; }
        public Nullable<bool> Day_Booking_Mask { get; set; }
        public Nullable<bool> Workflow { get; set; }
        public Nullable<bool> Project_Management { get; set; }
        public Nullable<bool> Main_Account { get; set; }
        public Nullable<bool> Clearing_Account { get; set; }
    
        public virtual ICollection<DailyAccountTime> DailyAccountTimes { get; set; }
        public virtual ICollection<SurchargesAccountsMapping> SurchargesAccountsMappings { get; set; }
    }
}