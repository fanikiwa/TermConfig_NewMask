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
    
    public partial class PersonnelTariff
    {
        public long ID { get; set; }
        public long PersonnelID { get; set; }
        public long TariffID { get; set; }
    
        public virtual Personal Personal { get; set; }
        public virtual Tariff Tariff { get; set; }
    }
}
