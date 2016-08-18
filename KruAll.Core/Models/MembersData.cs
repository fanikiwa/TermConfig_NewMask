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
    
    public partial class MembersData
    {
        public MembersData()
        {
            this.MemberAccessGroups = new HashSet<MemberAccessGroup>();
            this.MemberCommonInfoes = new HashSet<MemberCommonInfo>();
            this.MemberDrivingLicenses = new HashSet<MemberDrivingLicense>();
            this.MemberDynamicFields = new HashSet<MemberDynamicField>();
            this.MemberHealthCards = new HashSet<MemberHealthCard>();
            this.MemberIdentityCards = new HashSet<MemberIdentityCard>();
            this.MemberPassports = new HashSet<MemberPassport>();
            this.MembersAccessPlanMappings = new HashSet<MembersAccessPlanMapping>();
            this.MembersTransponders = new HashSet<MembersTransponder>();
            this.TbAccessPlanMembersMappings = new HashSet<TbAccessPlanMembersMapping>();
        }
    
        public long ID { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<long> MemberGroupId { get; set; }
        public Nullable<int> Salutation { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string Place { get; set; }
        public Nullable<long> MemberNumber { get; set; }
        public string AgreementNr { get; set; }
        public Nullable<long> ContractDuration { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Profession { get; set; }
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string MemberPhoto { get; set; }
        public string Memo { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> ExitDate { get; set; }
        public Nullable<int> ActiveCard { get; set; }
        public Nullable<bool> Active { get; set; }
    
        public virtual ERP_Anrede ERP_Anrede { get; set; }
        public virtual ICollection<MemberAccessGroup> MemberAccessGroups { get; set; }
        public virtual ICollection<MemberCommonInfo> MemberCommonInfoes { get; set; }
        public virtual ICollection<MemberDrivingLicense> MemberDrivingLicenses { get; set; }
        public virtual MemberDuration MemberDuration { get; set; }
        public virtual ICollection<MemberDynamicField> MemberDynamicFields { get; set; }
        public virtual ICollection<MemberHealthCard> MemberHealthCards { get; set; }
        public virtual ICollection<MemberIdentityCard> MemberIdentityCards { get; set; }
        public virtual ICollection<MemberPassport> MemberPassports { get; set; }
        public virtual ICollection<MembersAccessPlanMapping> MembersAccessPlanMappings { get; set; }
        public virtual ICollection<MembersTransponder> MembersTransponders { get; set; }
        public virtual ICollection<TbAccessPlanMembersMapping> TbAccessPlanMembersMappings { get; set; }
    }
}