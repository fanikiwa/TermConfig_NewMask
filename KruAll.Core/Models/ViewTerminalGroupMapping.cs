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
    
    public partial class ViewTerminalGroupMapping
    {
        public int TermID { get; set; }
        public string TermType { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string SerialNumber { get; set; }
        public string ConnectionType { get; set; }
        public string IpAddress { get; set; }
        public Nullable<int> Port { get; set; }
        public long TerminalGroupId { get; set; }
        public long TerminalInstanceId { get; set; }
    }
}