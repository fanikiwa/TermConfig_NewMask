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
    
    public partial class ERP_KAnsprechp
    {
        public int KAnsprechpCode { get; set; }
        public Nullable<int> KundenCode { get; set; }
        public Nullable<int> AnredeCode { get; set; }
        public string Vorname { get; set; }
        public string Name { get; set; }
        public string Telefon { get; set; }
        public string Telefon2 { get; set; }
        public string Telefon3 { get; set; }
        public string Telefax { get; set; }
        public string Briefanrede { get; set; }
        public string Funktion { get; set; }
        public Nullable<int> AbteilungCode { get; set; }
        public string Straße { get; set; }
        public string Staat { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Mobilfunk { get; set; }
        public string AdreßErweiterung { get; set; }
        public string Notiz { get; set; }
        public string E_Mail { get; set; }
        public Nullable<int> MailanPrivat { get; set; }
        public string TelPrivat { get; set; }
        public string FaxPrivat { get; set; }
        public Nullable<System.DateTime> Geburtsdatum { get; set; }
        public Nullable<int> OutlookAdresse { get; set; }
        public string SenderName { get; set; }
        public Nullable<int> Entlassen { get; set; }
        public Nullable<System.DateTime> LetzteÄnderung { get; set; }
        public string eMailPrivat { get; set; }
        public Nullable<int> BCodeErstkontakt { get; set; }
        public Nullable<int> BCodeLetzteÄnderung { get; set; }
        public string I_LogName { get; set; }
        public Nullable<int> GeburtstagTag { get; set; }
        public Nullable<int> GeburtstagMonat { get; set; }
        public Nullable<int> GeburtstagJahr { get; set; }
        public Nullable<int> VIP { get; set; }
        public Nullable<int> Serienbriefsperre { get; set; }
        public Nullable<int> Mailsperre { get; set; }
        public string Titelerweiterung { get; set; }
        public string Namenserweiterung { get; set; }
        public Nullable<System.DateTime> Erstkontakt { get; set; }
        public Nullable<int> PrimäreAdresse { get; set; }
        public Nullable<int> FirmenAdresse { get; set; }
        public Nullable<int> AbteilungInAdresseZeigen { get; set; }
        public Nullable<int> FunktionInAdresseZeigen { get; set; }
        public string Skypename { get; set; }
        public string MobilPrivat { get; set; }
        public string NotizRTF { get; set; }
        public string Memo { get; set; }
    
        public virtual ERP_Anrede ERP_Anrede { get; set; }
    }
}
