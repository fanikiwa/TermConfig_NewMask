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
    
    public partial class Portal_Users
    {
        public Portal_Users()
        {
            this.Portal_ProfileUSerMapping = new HashSet<Portal_ProfileUSerMapping>();
        }
    
        public long ID { get; set; }
        public long PortalNo { get; set; }
        public string Salutation { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string MobileNo { get; set; }
        public string TelephoneNo { get; set; }
        public string TaxId { get; set; }
        public string CompanyName { get; set; }
        public string StreetNo { get; set; }
        public string PostalCode { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string StreetName { get; set; }
        public string Code { get; set; }
        public bool Isactive { get; set; }
        public bool FirstPassChanged { get; set; }
    
        public virtual ICollection<Portal_ProfileUSerMapping> Portal_ProfileUSerMapping { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
