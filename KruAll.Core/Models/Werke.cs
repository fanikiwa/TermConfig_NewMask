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
    
    public partial class Werke
    {
        public int W_Nr { get; set; }
        public string W_Bezeichnung { get; set; }
        public string W_Str { get; set; }
        public Nullable<int> W_Plz { get; set; }
        public string W_Ort { get; set; }
        public string W_Memo { get; set; }
        public Nullable<System.DateTime> LastAccess { get; set; }
        public string W_Bundesland { get; set; }
    }
}
