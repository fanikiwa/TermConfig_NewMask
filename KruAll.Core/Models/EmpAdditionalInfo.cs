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
    
    public partial class EmpAdditionalInfo
    {
        public int AdditionalInfoID { get; set; }
        public Nullable<int> EmpNumber { get; set; }
        public int ColumnID { get; set; }
        public string EntryValue { get; set; }
    
        public virtual DynamicColumn DynamicColumn { get; set; }
    }
}