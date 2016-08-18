using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace KruAll.Core.Models
{
    public static class PZEEx
    {
        public static string ToRealTimeString(this double doubleValue)
        {
            var erg = new StringBuilder();
            erg.Append(((int)doubleValue).ToString());
            erg.Append(":");
            // Uwe Jacob, 2015-01-15: To Prevent two Signs if doublevalue is negative (-12.34 ==>  -12:-34)
            erg.Append((Math.Abs((int)((doubleValue - (int)doubleValue) * 60)).ToString("00")));
            return erg.ToString();
        }


        public static System.Data.DataTable ToDataTable(List<List<string>> liste)
        {
            System.Data.DataTable dt = new DataTable();
            dt.Columns.Add("Ausweis", typeof(string));
            dt.Columns.Add("Hinweis", typeof(string));
            foreach (List<string> item in liste)
            {
                dt.Rows.Add(item[0], item[1]);
            }
            try
            {
                return dt;
            }
            catch (Exception)
            {

                return dt;
            }
        }
        
        public static double ToIndTime(this string stringValue)
        {
            double erg = 0;
            var splittedString = stringValue.Split(':');
            if (splittedString.GetUpperBound(0) >= 1)
            {
                double.TryParse(splittedString[0], out erg);
                double minutesPart = 0;
                double.TryParse(splittedString[1], out minutesPart);
            }
            return erg;
        }

        public static double GetSaldo(int employeeId, DateTime targetDate, int accountNo, AccountingUnit accountingUnit, ttxTools.ClsDB dbConnection)
        {
            var sel = new StringBuilder();
            sel.Append("SELECT ");
            switch (accountingUnit)
            {
                case PZE.Accounting.AccountingUnit.day:
                    sel.Append("T");
                    break;
                case PZE.Accounting.AccountingUnit.week:
                    sel.Append("W");
                    break;
                case PZE.Accounting.AccountingUnit.month:
                    sel.Append("M");
                    break;
                case PZE.Accounting.AccountingUnit.year:
                    sel.Append("J");
                    break;
                default:
                    sel.Append("J");
                    break;
            }

            sel.Append(targetDate.Day.ToString() + " FROM Erg_Konten WHERE KTO_Jahr = " + targetDate.Year.ToString());
            sel.Append(" AND kto_Monat =" + targetDate.Month.ToString());
            sel.Append(" AND kto_Pers_Nr = " + employeeId.ToString());
            sel.Append(" AND kto_K_nr = " + accountNo.ToString());
            try
            {
                return Convert.ToDouble("" + dbConnection.DBLookUP(sel.ToString()));
            }
            catch (Exception)
            {

                return 0;
            }

        }
        

        // Uwe Jacob, 2015-03-10: fill column quelle with "TermConfig" instead of Datafox
        public static void SetDirty(int employeeId, DateTime targetDate, ttxTools.ClsDB dbConnection)
        {
            var par = new SortedList<string, object>();
            par.Add("inde", employeeId.ToString("0000000") + targetDate.ToString("yyyyMMdd"));
            par.Add("Pers_Nr", employeeId);
            par.Add("datum", targetDate);
            par.Add("quelle", "TermConfig");
            dbConnection.DBExecuteWithParameters("INSERT INTO dirty_Data (inde,pers_nr,[datum],Quelle) Values (@inde,@pers_nr,@datum,@quelle)", par);
        }

    }
}
