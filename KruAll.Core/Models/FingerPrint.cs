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
    
    public partial class FingerPrint
    {
        public long ID { get; set; }
        public int PersType { get; set; }
        public long PersIDNr { get; set; }
        public Nullable<int> FingerNr { get; set; }
        public string Template9 { get; set; }
        public string Template10 { get; set; }
        public bool Valid { get; set; }
        public string Description { get; set; }
        public Nullable<int> Quality1 { get; set; }
        public Nullable<int> Quality2 { get; set; }
        public Nullable<int> Quality3 { get; set; }
    }
}
