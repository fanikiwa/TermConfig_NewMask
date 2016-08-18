using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KruAll.Core.Repositories
{
    public class TerminalInfoRepository : KruAllBaseRepository<TerminalInfo>
    {
        #region Constructors
        public TerminalInfoRepository() { }

        #endregion Constructors

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalInfo> GetAllTerminalInfo()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalInfo GetTerminalInfoByID(int terminfoID)
        {
            return base.FindBy(terminfo => terminfo.ID == terminfoID).FirstOrDefault();
        }
        public enum AccountingUnit
        {
            day,
            week,
            month,
            year
        }
        public string GetTerminalInfoSaldo(string displayCode, int infoLength, int EmployeeID, string EmployeeName)
        {
            string title = string.Empty;
            string result = string.Empty;
            AccountingUnit accountingUnit = AccountingUnit.year;
            double tempValue = 0;
            var subSets = displayCode.Split(' ');
            foreach (var item in subSets)
            {
                switch (item.Substring(0, 2).ToLower())
                {
                    case "k[":
                        switch (item.ToLower().Substring(2, 1))
                        {
                            case "j":
                            case "y":
                                accountingUnit = AccountingUnit.year;
                                break;
                            case "m":
                                accountingUnit = AccountingUnit.month;
                                break;
                            case "w":
                                accountingUnit = AccountingUnit.week;
                                break;
                            case "t":
                            case "d":
                                accountingUnit = AccountingUnit.day;
                                break;
                            default:
                                break;
                        }
                        int kto = 0;
                        if (int.TryParse(item.Replace("]", "").Substring(3), out kto))
                        {
                            bool Stundenkonto = true;

                            var accounts = new KruallPZEBaseRepository<Konten>()._contextPZE.Kontens.FirstOrDefault(x =>
                                x.K_Nr == kto
                                );
                            switch (accounts.K_Bezeichnung.ToLower())
                            {
                                case "urlaubstage genommen":
                                case "resturlaub":
                                case "resturlaub aktuell":
                                case "resturlaub vorjahr":
                                case "kranktage":
                                case "feiertage":
                                    Stundenkonto = false;
                                    break;
                            }

                            tempValue = GetSaldo(EmployeeID, DateTime.Now.AddDays(-1), kto, accountingUnit);

                            if (Stundenkonto)
                            {                               
                                string minutes = TimeSpan.FromHours(tempValue).TotalMinutes.ToString();
                                return (minutes != "00") ? minutes + ":0," : "0:0,";
                            }
                            else
                            {
                                string days = tempValue.ToString();
                                return (days != "00") ? "00:" + days + "," : "0:0,";
                            }
                            
                        }
                        break;
                    default:
                        break;
                }
            }

            // Uwe Jacob, 2015-03-03: return-string (labeltext + accountvalue) should always have the length of title.Length (displayrow in Datafox-Terminal)
            // int fill = infoLength - title.Length - result.Length;
            int fill = infoLength - result.Length;
            if (fill >= 0)
            {
                return title.PadRight(fill, ' ') + result;
            }
            else
            {
                return title.Substring(0, title.Length + fill) + result;
            }
        }

        private string ToRealTimeString(double doubleValue)
        {
            var erg = new StringBuilder();
            erg.Append(((int)doubleValue).ToString());
            erg.Append(":");
            // Uwe Jacob, 2015-01-15: To Prevent two Signs if doublevalue is negative (-12.34 ==>  -12:-34)
            erg.Append((Math.Abs((int)((doubleValue - (int)doubleValue) * 60)).ToString("00")));
            return erg.ToString();
        }

        public double GetSaldo(int employeeId, DateTime targetDate, int accountNo, AccountingUnit accountingUnit)
        {
            var e = new KruallPZEBaseRepository<ERG_Konten>()._contextPZE.ERG_Konten.FirstOrDefault(x =>
               x.Kto_Jahr == targetDate.Year &&
               x.Kto_Monat == targetDate.Month &&
               x.Kto_Pers_Nr == employeeId &&
               x.Kto_K_Nr == accountNo);

            switch (accountingUnit)
            {
                case AccountingUnit.day:
                    switch (targetDate.Day)
                    {
                        case 1: return e.T1.Value;
                        case 2: return e.T2.Value;
                        case 3: return e.T3.Value;
                        case 4: return e.T4.Value;
                        case 5: return e.T5.Value;
                        case 6: return e.T6.Value;
                        case 7: return e.T7.Value;
                        case 8: return e.T8.Value;
                        case 9: return e.T9.Value;
                        case 10: return e.T10.Value;
                        case 11: return e.T11.Value;
                        case 12: return e.T12.Value;
                        case 13: return e.T13.Value;
                        case 14: return e.T14.Value;
                        case 15: return e.T15.Value;
                        case 16: return e.T16.Value;
                        case 17: return e.T17.Value;
                        case 18: return e.T18.Value;
                        case 19: return e.T19.Value;
                        case 20: return e.T20.Value;
                        case 21: return e.T21.Value;
                        case 22: return e.T22.Value;
                        case 23: return e.T23.Value;
                        case 24: return e.T24.Value;
                        case 25: return e.T25.Value;
                        case 26: return e.T26.Value;
                        case 27: return e.T27.Value;
                        case 28: return e.T28.Value;
                        case 29: return e.T29.Value;
                        case 30: return e.T30.Value;
                        case 31: return e.T31.Value;

                    }
                    break;
                case AccountingUnit.week:
                    switch (targetDate.Day)
                    {
                        case 1: return e.W1.Value;
                        case 2: return e.W2.Value;
                        case 3: return e.W3.Value;
                        case 4: return e.W4.Value;
                        case 5: return e.W5.Value;
                        case 6: return e.W6.Value;
                        case 7: return e.W7.Value;
                        case 8: return e.W8.Value;
                        case 9: return e.W9.Value;
                        case 10: return e.W10.Value;
                        case 11: return e.W11.Value;
                        case 12: return e.W12.Value;
                        case 13: return e.W13.Value;
                        case 14: return e.W14.Value;
                        case 15: return e.W15.Value;
                        case 16: return e.W16.Value;
                        case 17: return e.W17.Value;
                        case 18: return e.W18.Value;
                        case 19: return e.W19.Value;
                        case 20: return e.W20.Value;
                        case 21: return e.W21.Value;
                        case 22: return e.W22.Value;
                        case 23: return e.W23.Value;
                        case 24: return e.W24.Value;
                        case 25: return e.W25.Value;
                        case 26: return e.W26.Value;
                        case 27: return e.W27.Value;
                        case 28: return e.W28.Value;
                        case 29: return e.W29.Value;
                        case 30: return e.W30.Value;
                        case 31: return e.W31.Value;
                    }
                    break;
                case AccountingUnit.month:
                    switch (targetDate.Day)
                    {
                        case 1: return e.M1.Value;
                        case 2: return e.M2.Value;
                        case 3: return e.M3.Value;
                        case 4: return e.M4.Value;
                        case 5: return e.M5.Value;
                        case 6: return e.M6.Value;
                        case 7: return e.M7.Value;
                        case 8: return e.M8.Value;
                        case 9: return e.M9.Value;
                        case 10: return e.M10.Value;
                        case 11: return e.M11.Value;
                        case 12: return e.M12.Value;
                        case 13: return e.M13.Value;
                        case 14: return e.M14.Value;
                        case 15: return e.M15.Value;
                        case 16: return e.M16.Value;
                        case 17: return e.M17.Value;
                        case 18: return e.M18.Value;
                        case 19: return e.M19.Value;
                        case 20: return e.M20.Value;
                        case 21: return e.M21.Value;
                        case 22: return e.M22.Value;
                        case 23: return e.M23.Value;
                        case 24: return e.M24.Value;
                        case 25: return e.M25.Value;
                        case 26: return e.M26.Value;
                        case 27: return e.M27.Value;
                        case 28: return e.M28.Value;
                        case 29: return e.M29.Value;
                        case 30: return e.M30.Value;
                        case 31: return e.M31.Value;
                    }
                    break;
                case AccountingUnit.year:
                    switch (targetDate.Day)
                    {
                        case 1: return e.J1.Value;
                        case 2: return e.J2.Value;
                        case 3: return e.J3.Value;
                        case 4: return e.J4.Value;
                        case 5: return e.J5.Value;
                        case 6: return e.J6.Value;
                        case 7: return e.J7.Value;
                        case 8: return e.J8.Value;
                        case 9: return e.J9.Value;
                        case 10: return e.J10.Value;
                        case 11: return e.J11.Value;
                        case 12: return e.J12.Value;
                        case 13: return e.J13.Value;
                        case 14: return e.J14.Value;
                        case 15: return e.J15.Value;
                        case 16: return e.J16.Value;
                        case 17: return e.J17.Value;
                        case 18: return e.J18.Value;
                        case 19: return e.J19.Value;
                        case 20: return e.J20.Value;
                        case 21: return e.J21.Value;
                        case 22: return e.J22.Value;
                        case 23: return e.J23.Value;
                        case 24: return e.J24.Value;
                        case 25: return e.J25.Value;
                        case 26: return e.J26.Value;
                        case 27: return e.J27.Value;
                        case 28: return e.J28.Value;
                        case 29: return e.J29.Value;
                        case 30: return e.J30.Value;
                        case 31: return e.J31.Value;
                    }
                    break;
                default:
                    switch (targetDate.Day)
                    {
                        case 1: return e.J1.Value;
                        case 2: return e.J2.Value;
                        case 3: return e.J3.Value;
                        case 4: return e.J4.Value;
                        case 5: return e.J5.Value;
                        case 6: return e.J6.Value;
                        case 7: return e.J7.Value;
                        case 8: return e.J8.Value;
                        case 9: return e.J9.Value;
                        case 10: return e.J10.Value;
                        case 11: return e.J11.Value;
                        case 12: return e.J12.Value;
                        case 13: return e.J13.Value;
                        case 14: return e.J14.Value;
                        case 15: return e.J15.Value;
                        case 16: return e.J16.Value;
                        case 17: return e.J17.Value;
                        case 18: return e.J18.Value;
                        case 19: return e.J19.Value;
                        case 20: return e.J20.Value;
                        case 21: return e.J21.Value;
                        case 22: return e.J22.Value;
                        case 23: return e.J23.Value;
                        case 24: return e.J24.Value;
                        case 25: return e.J25.Value;
                        case 26: return e.J26.Value;
                        case 27: return e.J27.Value;
                        case 28: return e.J28.Value;
                        case 29: return e.J29.Value;
                        case 30: return e.J30.Value;
                        case 31: return e.J31.Value;
                    }
                    break;
            }
            return 0;
        }

        public string GetTerminalInfoText(string displayCode)
        {
            foreach (var item in displayCode.Split(' '))
            {
                switch (item.Substring(0, 2).ToLower())
                {
                    case "t[":
                        return item.Replace("]", "").Substring(2);
                    default:
                        break;
                }
            }
            return "";
        }
        #endregion
    }
}
