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
    
    public partial class DynamicColumn
    {
        public DynamicColumn()
        {
            this.EmpAdditionalInfoes = new HashSet<EmpAdditionalInfo>();
        }
    
        public int ColumnID { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
    
        public virtual ICollection<EmpAdditionalInfo> EmpAdditionalInfoes { get; set; }
    }
}