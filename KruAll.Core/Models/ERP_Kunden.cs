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
    
    public partial class ERP_Kunden
    {
        public int ID { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public Nullable<int> GrCode { get; set; }
        public Nullable<int> Nummer { get; set; }
        public string Firma1 { get; set; }
        public string Firma2 { get; set; }
        public string Firma3 { get; set; }
        public string Straße { get; set; }
        public string Staat { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Telefon { get; set; }
        public string Telefax { get; set; }
        public Nullable<int> Privatkunde { get; set; }
        public Nullable<int> Bruttorechnung { get; set; }
        public Nullable<int> Gesperrt { get; set; }
        public Nullable<int> Zahlungsfrist { get; set; }
        public Nullable<float> Skonto { get; set; }
        public Nullable<float> Skontofrist { get; set; }
        public Nullable<double> Skonto2 { get; set; }
        public Nullable<int> Skonto2Frist { get; set; }
        public Nullable<double> Mahntoleranz { get; set; }
        public Nullable<float> Rabattvorschlag { get; set; }
        public Nullable<int> Preisgruppe { get; set; }
        public string Notiz { get; set; }
        public Nullable<int> KAnsprechpCode { get; set; }
        public Nullable<int> NebenAdrCode1 { get; set; }
        public Nullable<int> NebenAdrCode2 { get; set; }
        public Nullable<int> NebenAdrCode3 { get; set; }
        public Nullable<int> NebenAdrType1 { get; set; }
        public Nullable<int> NebenAdrType2 { get; set; }
        public Nullable<int> NebenAdrType3 { get; set; }
        public Nullable<int> KKontaktCode { get; set; }
        public Nullable<System.DateTime> Erstkontakt { get; set; }
        public Nullable<System.DateTime> Letzterkontakt { get; set; }
        public string PersonErstkontakt { get; set; }
        public string PersonLetzterkontakt { get; set; }
        public string Waswurdezuletztgetan { get; set; }
        public Nullable<float> Entfernung { get; set; }
        public string Postfach { get; set; }
        public string PLZPostfach { get; set; }
        public string OrtPostfach { get; set; }
        public string Vorwahl { get; set; }
        public string AnsprechPartner { get; set; }
        public string BriefAnrede { get; set; }
        public Nullable<int> AnredeCode { get; set; }
        public string Autotelefon { get; set; }
        public string InterNet { get; set; }
        public Nullable<int> VertreterCode { get; set; }
        public Nullable<double> Provision { get; set; }
        public string Mark { get; set; }
        public Nullable<int> Standardkonto { get; set; }
        public Nullable<int> Steuer { get; set; }
        public string Kontonummer { get; set; }
        public string Bankverbindung { get; set; }
        public string Bankleitzahl { get; set; }
        public string Kontoinhaber { get; set; }
        public Nullable<int> Bankeinzug { get; set; }
        public string USTIDNR { get; set; }
        public string Kundennr { get; set; }
        public string Kürzel { get; set; }
        public Nullable<int> HausbankCode { get; set; }
        public Nullable<int> SprachCode { get; set; }
        public string E_Mail { get; set; }
        public Nullable<int> WährungCode { get; set; }
        public Nullable<double> Kreditlimit { get; set; }
        public Nullable<int> ZahlungsCode { get; set; }
        public Nullable<int> DB { get; set; }
        public Nullable<int> SteuerschluesselCode { get; set; }
        public string SenderName { get; set; }
        public Nullable<int> OutlookAdresse { get; set; }
        public Nullable<System.DateTime> Geburtsdatum { get; set; }
        public Nullable<int> Vertreter2Code { get; set; }
        public Nullable<System.DateTime> LetzteÄnderung { get; set; }
        public string Titelerweiterung { get; set; }
        public Nullable<int> GeburtstagTag { get; set; }
        public Nullable<int> GeburtstagMonat { get; set; }
        public Nullable<int> GeburtstagJahr { get; set; }
        public string Namenserweiterung { get; set; }
        public Nullable<int> Erloschen { get; set; }
        public string Funktion { get; set; }
        public string FirmenAnrede { get; set; }
        public Nullable<int> Intern { get; set; }
        public Nullable<int> DoublettenCheck_NichtMehrAnzeigen { get; set; }
        public string Adreßerweiterung { get; set; }
        public string E_Mail2 { get; set; }
        public string NotizRTF { get; set; }
        public string IBAN { get; set; }
        public string BIC { get; set; }
        public string Telefon2 { get; set; }
        public Nullable<int> Lieferadresse { get; set; }
        public Nullable<int> DTANichtZusammenfassen { get; set; }
        public Nullable<int> MailSperre { get; set; }
        public Nullable<int> SerienbriefSperre { get; set; }
        public Nullable<int> LieferungsArtCode { get; set; }
        public Nullable<int> LieferungsArtZiel { get; set; }
        public string MiteID { get; set; }
        public string Konzernkennzeichen { get; set; }
        public Nullable<int> Mahnsperre { get; set; }
        public Nullable<int> TeilrechnungslogikCode { get; set; }
        public string Ordner { get; set; }
        public Nullable<int> VertreterSDObjMemberCode { get; set; }
        public Nullable<int> VertreterSDObjType { get; set; }
        public Nullable<int> NebenadrAPCode1 { get; set; }
        public Nullable<int> NebenadrAPCode2 { get; set; }
        public Nullable<int> NebenadrAPCode3 { get; set; }
        public Nullable<int> ERPFreigabepflichtDeaktivieren { get; set; }
        public Nullable<int> AdresseWirdGepflegtBeiLieferantCode { get; set; }
        public Nullable<double> Rabatt2 { get; set; }
        public Nullable<double> Rabatt3 { get; set; }
        public Nullable<double> Rabatt4 { get; set; }
        public Nullable<int> AdresseWirdGepflegtBeiKundeCode { get; set; }
        public Nullable<int> KeineStaffelrabatte { get; set; }
        public string Memo { get; set; }
        public Nullable<int> KundenType { get; set; }
    }
}
