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
    
    public partial class Konten
    {
        public int K_Nr { get; set; }
        public string K_Bezeichnung { get; set; }
        public string K_Anmerkungen { get; set; }
        public string K_Anzeige { get; set; }
        public Nullable<bool> K_Standard { get; set; }
        public Nullable<bool> K_Jahresuebertrag { get; set; }
        public Nullable<int> K_Zielkonto { get; set; }
        public Nullable<bool> K_Absolut { get; set; }
        public Nullable<System.DateTime> LastAccess { get; set; }
        public Nullable<int> K_Makro { get; set; }
        public Nullable<System.DateTime> K_Datum { get; set; }
        public string K_TYP { get; set; }
    }
}
