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
    
    public partial class GetPersonalStammByLocationId_Result
    {
        public int Pers_Nr { get; set; }
        public Nullable<int> Pers_Ausweis_Nr { get; set; }
        public string Pers_Name1 { get; set; }
        public string Pers_Name2 { get; set; }
        public string Pers_Str { get; set; }
        public Nullable<int> Pers_Plz { get; set; }
        public string Pers_Ort { get; set; }
        public string Pers_Tel { get; set; }
        public string Pers_Fax { get; set; }
        public string Pers_Mobil { get; set; }
        public string Pers_Email { get; set; }
        public string Pers_Info { get; set; }
        public string Pers_Bank { get; set; }
        public string Pers_Blz { get; set; }
        public string Pers_Kto { get; set; }
        public Nullable<System.DateTime> Pers_Geburtsdatum { get; set; }
        public Nullable<int> Pers_Werk { get; set; }
        public Nullable<int> Pers_Kostenstelle { get; set; }
        public Nullable<int> Pers_Abteilung { get; set; }
        public Nullable<int> Pers_Lohngruppe { get; set; }
        public Nullable<int> Pers_Tarif { get; set; }
        public Nullable<System.DateTime> Pers_Eintritt { get; set; }
        public Nullable<System.DateTime> Pers_Austritt { get; set; }
        public string Pers_Finanzamt { get; set; }
        public string Pers_Nation { get; set; }
        public string Pers_Familienstand { get; set; }
        public string Pers_Religion { get; set; }
        public string Pers_SozVersNr { get; set; }
        public string Pers_Qualifikation { get; set; }
        public string Pers_Foto { get; set; }
        public Nullable<int> Pers_Wabr { get; set; }
        public Nullable<short> Pers_Wabr_tag { get; set; }
        public Nullable<int> Pers_Mabr { get; set; }
        public Nullable<short> Pers_Mabr_tag { get; set; }
        public Nullable<double> Pers_Jahresurlaub { get; set; }
        public string Pers_Krankenkasse { get; set; }
        public string Pers_KassenVersNr { get; set; }
        public Nullable<int> Pers_Standardkalender { get; set; }
        public string Pers_Geschlecht { get; set; }
        public Nullable<System.DateTime> Pers_Tageswechsel { get; set; }
        public string Pers_Status { get; set; }
        public Nullable<System.DateTime> Pers_DatumStartStatus { get; set; }
        public Nullable<bool> Pers_Ohne_Abrechnung { get; set; }
        public Nullable<bool> Pers_Fahrer { get; set; }
        public string Pers_PinCode { get; set; }
        public Nullable<int> Pers_Zutritt { get; set; }
        public Nullable<bool> Pers_Anzug { get; set; }
        public Nullable<int> Pers_PremLohnart { get; set; }
        public Nullable<int> Pers_ZulagLohnart { get; set; }
        public Nullable<System.DateTime> LastAccess { get; set; }
        public Nullable<bool> Datev { get; set; }
        public Nullable<int> Pers_QualNr { get; set; }
        public Nullable<int> Pers_QualGrpNr { get; set; }
        public Nullable<int> Pers_PCTGruppe { get; set; }
        public Nullable<int> Pers_PCTKonto { get; set; }
        public Nullable<int> LetzterAuftrag { get; set; }
        public Nullable<int> LetzteTaetigkeit { get; set; }
        public Nullable<int> Pers_Sollzeitkalender { get; set; }
        public string Pers_TagesschieneNr { get; set; }
        public Nullable<int> Pers_Wbelast_Tag { get; set; }
        public Nullable<int> Pers_Mbelast_Tag { get; set; }
        public Nullable<double> Pers_SonderUrlaub1 { get; set; }
        public Nullable<double> Pers_SonderUrlaub2 { get; set; }
        public Nullable<double> Pers_SonderUrlaub3 { get; set; }
        public Nullable<double> Pers_SonderUrlaub4 { get; set; }
        public Nullable<double> Pers_SonderUrlaub5 { get; set; }
        public Nullable<double> Pers_SonderUrlaub6 { get; set; }
        public Nullable<double> Pers_SonderUrlaub7 { get; set; }
        public Nullable<double> Pers_SonderUrlaub8 { get; set; }
        public Nullable<int> Pers_Ausweis_nr2 { get; set; }
        public Nullable<int> NoPZE { get; set; }
        public Nullable<int> TabStatus { get; set; }
        public string TabAbw { get; set; }
        public Nullable<System.DateTime> TabAbwEnd { get; set; }
        public Nullable<double> Pers_BerechnungsFaktor { get; set; }
        public string PERS_Z1 { get; set; }
        public string PERS_Z2 { get; set; }
        public string PERS_Z3 { get; set; }
        public string PERS_Z4 { get; set; }
        public string PERS_Z5 { get; set; }
        public string PERS_Z6 { get; set; }
        public string PERS_Z7 { get; set; }
        public string PERS_Z8 { get; set; }
        public string PERS_Z9 { get; set; }
        public string PERS_Z10 { get; set; }
        public string INFO1 { get; set; }
        public string INFO2 { get; set; }
        public string INFO3 { get; set; }
        public string INFO4 { get; set; }
        public Nullable<bool> PERS_WPP_GETMAIL { get; set; }
        public Nullable<bool> PERS_WPP_KORR { get; set; }
        public string PERS_TEMPLATE { get; set; }
        public Nullable<int> PERS_VISNR { get; set; }
        public Nullable<int> PERS_COUNTER { get; set; }
        public string pers_position { get; set; }
        public Nullable<System.DateTime> pers_ablauf { get; set; }
        public string MES_Flag { get; set; }
        public string MES_ANNummer { get; set; }
        public Nullable<double> PERS_MAXGZ { get; set; }
        public Nullable<int> PERS_MONATSWECHSEL { get; set; }
        public Nullable<int> PERS_MOWEGUTMODUS { get; set; }
        public Nullable<double> PERS_MOWEGUTSTD { get; set; }
        public string PERS_HZO2 { get; set; }
        public string Pers_Anrede { get; set; }
        public string Pers_GebOrt { get; set; }
        public string Pers_Kinder { get; set; }
        public string Pers_Beruf { get; set; }
        public Nullable<System.DateTime> Pers_VertrBis { get; set; }
        public string Pers_StdZahl { get; set; }
        public Nullable<System.DateTime> Pers_VertrAb { get; set; }
        public string Pers_Z11 { get; set; }
        public string Pers_Z12 { get; set; }
        public string Pers_Z13 { get; set; }
        public string Pers_Z14 { get; set; }
        public string Pers_Z15 { get; set; }
        public string Pers_Z16 { get; set; }
        public string Pers_Z17 { get; set; }
        public string Pers_Z18 { get; set; }
        public string Pers_Z19 { get; set; }
        public string Pers_Z20 { get; set; }
        public string Pers_FSchein { get; set; }
        public string Pers_StKlasse { get; set; }
        public Nullable<System.DateTime> Pers_AngelegtAm { get; set; }
        public Nullable<System.DateTime> Pers_AusgeschiedenAm { get; set; }
        public Nullable<int> Pers_AutoAusb { get; set; }
        public Nullable<int> Pers_Durchschnitt { get; set; }
        public string Pers_Pre_Ausweis_Nr { get; set; }
        public string Pers_Pre_Ausweis_Nr2 { get; set; }
        public Nullable<int> Pers_WF { get; set; }
        public Nullable<int> Pers_WF_ITerm { get; set; }
        public Nullable<bool> Pers_WF_Admin { get; set; }
        public Nullable<int> Pers_KstAnzeigeGrp { get; set; }
        public string Pers_Prefix { get; set; }
        public Nullable<bool> Pers_BDEPlanung { get; set; }
        public Nullable<int> Pers_ZutrittsKalender { get; set; }
        public Nullable<int> Pers_Bereich { get; set; }
        public Nullable<bool> Pers_PEPMB { get; set; }
        public Nullable<int> Pers_FlexKstGr { get; set; }
        public Nullable<bool> Pers_Ausw_Gesperrt { get; set; }
        public string Pers_ParkplatzFirma { get; set; }
        public string Pers_ParkplatzKennzeichen { get; set; }
        public string Pers_ParkplatzAutomarke { get; set; }
        public string Pers_ParkplatzTyp { get; set; }
        public string Pers_ParkplatzFarbe { get; set; }
        public Nullable<System.DateTime> Pers_ParkplatzVon { get; set; }
        public Nullable<System.DateTime> Pers_ParkplatzBis { get; set; }
        public Nullable<int> Pers_TaetGrpNr { get; set; }
        public Nullable<int> Pers_Mandant { get; set; }
        public string Pers_WoProg { get; set; }
        public Nullable<System.DateTime> Pers_WoProg_Start { get; set; }
        public string Pers_LKenner { get; set; }
        public Nullable<int> Pers_MAStatus { get; set; }
        public Nullable<int> Pers_Zuordnung1 { get; set; }
        public Nullable<int> Pers_Zuordnung2 { get; set; }
        public string Pers_Titel { get; set; }
        public string Pers_Name12 { get; set; }
        public string Pers_Bic { get; set; }
        public string Pers_Iban { get; set; }
        public Nullable<int> Pers_Zutritt2 { get; set; }
        public Nullable<int> Pers_ZutrittAktiv { get; set; }
        public string Berufsschule { get; set; }
        public Nullable<bool> Df_Message_anzeigen { get; set; }
    }
}
