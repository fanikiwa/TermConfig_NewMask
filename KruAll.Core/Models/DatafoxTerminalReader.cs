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
    
    public partial class DatafoxTerminalReader
    {
        public long ID { get; set; }
        public Nullable<long> DatafoxTerminalID { get; set; }
        public int ReaderID { get; set; }
        public string ReaderType { get; set; }
        public string ReaderDescription { get; set; }
        public string Direction { get; set; }
        public string Status { get; set; }
        public Nullable<int> RelayTime { get; set; }
        public string ReaderMemo { get; set; }
    
        public virtual DatafoxTerminalInstance DatafoxTerminalInstance { get; set; }
    }
}