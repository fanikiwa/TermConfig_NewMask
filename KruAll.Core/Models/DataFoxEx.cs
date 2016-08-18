using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using KruAll.Core.Models;
using ttxTools.DataBase.Extensions;
using System.Data.EntityClient;

namespace KruAll.Core.Models
{
    public static class DataFoxEx
    {
        public static DataRow newTermRecord(string deviceName, string IP, ttxTools.ClsDB dbConnection)
        {
            var table = new DataTable();
            table.Columns.Add("TermBezeichnung", typeof(string));
            table.Columns.Add("TelNr", typeof(string));
            for (int i = 0; i < 8; i++)
            {
                table.Columns.Add("TermInfo" + i.ToString(), typeof(string));
            }
            table.Rows.Add(deviceName, IP);


            //var termInfo = dbConnection.DBLookUP("SELECT TERMINFO" + i + " FROM TERMINALTAB WHERE TERMBEZEICHNUNG = '" + deviceName + "'")
            //for (int i = 0; i < 4; i++)
            //{
            //    //Fill TermInfo List and the TermInfo in termConfig Row. Plus 2, because 1 and 2 are reserved for TermBezeichnung and TelNr
            //}


            return table.Rows[0];
        }

        public static DataTable GetEmployeeList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            //english Db
            //StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT COALESCE(CONVERT(VARCHAR(20), CASE  WHEN CONVERT(BIGINT,Card_Nr)>99999999 THEN Card_Nr END ) ,REPLACE(STR(CONVERT(BIGINT,Card_Nr),9,0),' ','0')) as AWN,FirstName + ', ' + LastName AS DisplayName , Pers_Nr AS EmployeeID FROM Personal WHERE Card_Nr > 0 ORDER BY 1");
            //return dbConnection.DBDataTable(sb.ToString());

            return dbConnection.DBDataTable("SELECT COALESCE(CONVERT(VARCHAR(20),CASE WHEN CONVERT(BIGINT,[Pers_Pre_Ausweis_Nr])>99999999 THEN [Pers_Pre_Ausweis_Nr] END ) ,REPLACE(STR(CONVERT(BIGINT,[Pers_Ausweis_Nr]),9,0),' ','0')) as AWN,Pers_Name1 + ', ' + Pers_Name2 AS DisplayName , Pers_Nr AS EmployeeID FROM [Personalstamm] WHERE Pers_Ausweis_Nr > 0 ORDER BY 1");
        }
        public static DataTable GetEmployeeList(ttxTools.ClsDB dbConnection, int EmployeeID)
        {
            return dbConnection.DBDataTable("SELECT COALESCE(CONVERT(VARCHAR(20),CASE WHEN CONVERT(BIGINT,[Pers_Pre_Ausweis_Nr])>99999999 THEN [Pers_Pre_Ausweis_Nr] END ) ,REPLACE(STR(CONVERT(BIGINT,[Pers_Ausweis_Nr]),9,0),' ','0')) as AWN,Pers_Name1 + ', ' + Pers_Name2 AS DisplayName /* , Pers_Nr AS EmployeeID */ FROM [Personalstamm] WHERE Pers_Ausweis_Nr = " + EmployeeID);
        }

        private static List<string> getInfoTextFromDb(ttxTools.ClsDB dbConnection, string IP)
        {
            List<string> erg = new List<string>();
            for (int i = 1; i < 4; i++)
            {
                string terminfo = dbConnection.DBLookUP("SELECT TERMINFO" + i + " FROM TERMINALTAB WHERE TELNR = '" + IP + "'");
                if (terminfo != string.Empty)
                    erg.Add(dbConnection.DBLookUP("SELECT TERMINFO" + i + " FROM TERMINALTAB WHERE TELNR = '" + IP + "'"));
            }
            return erg;
        }
        public static List<List<string>> getInfoTable(int EmployeeID, ttxTools.ClsDB dbConnection, string IP)
        {
            var erg = new List<List<string>>();
            if (IP == string.Empty)
                return erg;

            List<string> InfoText = getInfoTextFromDb(dbConnection, IP);
            if (InfoText.Count > 0)
            {
                var Employees = DataFoxEx.GetEmployeeList(dbConnection, EmployeeID);
                //PersSchleife
                foreach (var item in Employees.Select("", "AWN"))
                {
                    foreach (var itext in InfoText)
                    {
                        var newRow = new List<string>();
                        newRow.Add(item["awn"].ToString());
                        //newRow.Add(GetTerminalInfoText(itext, 25, item.ttxInt("EmployeeID"), item, dbConnection));
                        erg.Add(newRow);
                    }
                }
            }
            return erg;
        }
        private static string GetTerminalInfoText(string displayCode, int infoLength, int EmployeeID, DataRow employeeRow, ttxTools.ClsDB dbConnection)
        {
            string title = string.Empty;
            string result = string.Empty;
            KruAll.Core.Models.Accounting.AccountingUnit accountingUnit = Accounting.AccountingUnit.year;
            double tempValue = 0;
            var subSets = displayCode.Split(' ');
            foreach (var item in subSets)
            {
                switch (item.Substring(0, 2).ToLower())
                {
                    case "v[":
                        if (item.ToLower().Contains("name"))
                        {
                            title = employeeRow["DisplayName"].ToString();
                        }
                        break;
                    case "k[":
                        switch (item.ToLower().Substring(2, 1))
                        {
                            case "j":
                            case "y":
                                accountingUnit = Accounting.AccountingUnit.year;
                                break;
                            case "m":
                                accountingUnit = Accounting.AccountingUnit.month;
                                break;
                            case "w":
                                accountingUnit = Accounting.AccountingUnit.week;
                                break;
                            case "t":
                            case "d":
                                accountingUnit = Accounting.AccountingUnit.day;
                                break;
                            default:
                                break;
                        }
                        int kto = 0;
                        if (int.TryParse(item.Replace("]", "").Substring(3), out kto))
                        {
                            tempValue = KruAll.Core.Models.PZEEx.GetSaldo(EmployeeID, DateTime.Now.AddDays(-1), kto, accountingUnit, dbConnection);
                        }
                        break;
                    case "f[":
                        if (item.ToLower().Contains("[r]"))
                        {
                            result = tempValue.ToRealTimeString();
                        }
                        else
                        {
                            result = tempValue.ToString("0.00");
                        }
                        break;
                    case "t[":
                        title = item.Replace("]", "").Substring(2);
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

        public static DataTable GetCustomStausList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            return dbConnection.DBDataTable("SELECT V_Nr, V_Bezeichnung FROM [ZITERM_V20_Vorlauftasten] WHERE ISNULL(V_NichtAnTerminalSenden,0) = 0 ORDER BY V_Bezeichnung");
        }



        public static DataTable GetHolidayList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("DECLARE @TerminalDesc VARCHAR(300); ");
            sb.Append("SET @TerminalDesc = '" + terminalName + "'; ");
            sb.Append("SELECT\n");
            sb.Append("res.Day\n");
            sb.Append(",res.RefGroup\n");
            sb.Append(",ISNULL(CONVERT(INT,1000 + res.ProfilNo * 10 + ZPTF.Level),0) as RefTime\n");
            sb.Append("FROM (\n");
            sb.Append("SELECT \n");
            sb.Append("HolCal.Datum as 'Day'\n");
            sb.Append(",TbAP.AccessPlanNr as RefGroup\n");
            sb.Append(",CASE WHEN EXISTS \n");
            sb.Append("(SELECT ZPAccessProfileNo FROM HolidayAccessPlam_with_DateTime WHERE Datum = HolCal.Datum AND HolCal.CalendarId = HolidayAccessPlam_with_DateTime.HPCHolidayCalenderId) \n");
            sb.Append("THEN HolAcc.AccessProfileID ELSE 0 END as RefTime\n");
            sb.Append(",CASE WHEN EXISTS \n");
            sb.Append("(SELECT ZPAccessProfileNo FROM HolidayAccessPlam_with_DateTime WHERE Datum = HolCal.Datum AND HolCal.CalendarId = HolidayAccessPlam_with_DateTime.HPCHolidayCalenderId) \n");
            sb.Append("THEN HolAcc.AccessProfileID ELSE 0 END as newRefTime\n");
            sb.Append(",HolAcc.ZPAccessProfileNo as ProfilNo\n");
            sb.Append("from \n");
            sb.Append("ReaderAssigned RA \n");
            sb.Append("INNER JOIN TerminalConfig TC ON TC.ID = RA.TerminalID\n");
            sb.Append("INNER JOIN TbAccessPlan TbAP ON TbAP.BuildingPlanID = RA.BuildingPlanID\n");
            sb.Append("INNER JOIN HolidayAccessPlam_with_DateTime HolAcc ON HolAcc.HPCId = TbAP.HolidayPlanCalendarId\n");
            sb.Append("INNER JOIN HolidayCalendar_with_DateTime HolCal ON HolCal.CalendarId = HolAcc.HPCHolidayCalenderId\n");
            sb.Append("WHERE \n");
            sb.Append("TC.Description = @TerminalDesc\n");
            sb.Append("AND HolCal.HolidayId > 0\n");
            sb.Append(") res\n");
            sb.Append("LEFT JOIN ZuttritProfilesTimeFrames ZPTF ON res.RefTime = ZPTF.AccessProfID\n");
            sb.Append("WHERE (ZPTF.ProfilAktiv = 1 OR res.RefTime = 0)\n");

            // Only days in future 
            return dbConnection.DBDataTable(sb.ToString());
        }


        public static DataTable GetTerminalList(ttxTools.ClsDB dbConnection)
        {
            return dbConnection.DBDataTable("SELECT * FROM TerminalTab AS te INNER JOIN TerminalTypen AS ty ON te.TERMTYP = ty.TERMTYP WHERE te.TermStatus > 0 AND ty.HerstellerID = 5");


        }
        public static bool SaveAccessControlTransaction(Int64 CardId, DateTime eventTime, string terminalId, string readerId, int status, ttxTools.ClsDB dbConnection)
        {
            int idNumber = 0;
            int personalType = 0;
            string  terminalDescription = string.Empty;
            int persNumber = 0;
            int visNumber = 0;
            int memNumber = 0;

            const int PERSONAL_TYPE = 1;
            const int VISITOR_TYPE = 2;
            const int MEMBER_TYPE = 3;


            idNumber = Convert.ToInt32(dbConnection.DBLookUP("SELECT IDNr FROM View_Transponders WHERE TransponderNr = " + CardId + "; "));
            personalType = Convert.ToInt32(dbConnection.DBLookUP("SELECT Pers_Type FROM View_Transponders WHERE TransponderNr = " + CardId + "; "));
            terminalDescription = terminalId;

            string terminalIDSQL = " SELECT ISNULL(ReaderID, 0) AS ReaderID FROM "
                                    + "  "
                                    + " (SELECT REPLACE(STR((ROW_NUMBER() Over(Order by @@rowcount ASC) - 1) * 10, 3, 0), ' ', '0') AS TM, TerminalReaders.ReaderID FROM TerminalReaders "
                                    + " LEFT JOIN TerminalConfig ON TerminalConfig.ID = TerminalReaders.TermID "
                                    + " WHERE TerminalConfig.Description = '" + terminalId + "') t "
                                    + " WHERE TM = '" + readerId + "' ";

            string terminalReaderID = dbConnection.DBLookUP(terminalIDSQL);

            switch (personalType)
            {
                case PERSONAL_TYPE:
                    persNumber = idNumber;
                    break;
                case VISITOR_TYPE:
                    visNumber = idNumber;
                    break;
                case MEMBER_TYPE:
                    memNumber = idNumber;
                    break;
            }

            var sqlInsertRawBookingData = "INSERT INTO AccessControlLogs (TerminalSerial ,ReaderID ,Card_Nr ,Pers_Nr ,Status ,AccessTime ,VisitorID ,MemberID) "
                                        + "VALUES('" + terminalDescription + "','" + terminalReaderID + "' ," + CardId + "," + persNumber + " ," + status + " , "
                                        + string.Format("CONVERT(DATETIME, '{0:yyyy-MM-dd HH:mm}', 20)", eventTime) + " , " + visNumber + " , " + memNumber + ")";

            dbConnection.DBExecute(sqlInsertRawBookingData);


            //Process PZE Data

            //string PZE_BookingStatusSQL = "";
            //string PZE_BookingStatus = "";

            //PZE_BookingStatusSQL = "SELECT CASE WHEN TA_Come = 1 THEN '-1' "
            //                     + "WHEN TA_Go = 1 THEN '-2' "
            //                     + "           ELSE '' "
            //                     + "END AS PZEStatus "
            //                     + "FROM TerminalReaders "
            //                     + "INNER JOIN TerminalConfig ON TerminalConfig.ID = TerminalReaders.TermID "
            //                     + "INNER JOIN ReaderAssigned ON ReaderAssigned.TerminalID = TerminalConfig.ID AND ReaderAssigned.ReaderID = TerminalReaders.ID "
            //                     + "WHERE TerminalConfig.Description = '" + terminalId + "' AND TerminalReaders.ReaderID = " + terminalReaderID + "";

            //PZE_BookingStatus = dbConnection.DBLookUP(PZE_BookingStatusSQL);

            //if(PZE_BookingStatus != string.Empty)
            //{
            //    string entityConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["KrutecPZEEntities"].ConnectionString;
            //    string _connectionString = new EntityConnectionStringBuilder(entityConnectionString).ProviderConnectionString;

            //    ttxTools.ClsDB dd = new ttxTools.ClsDB();
            //    dd.ConnectionString = _connectionString;

            //    StringBuilder sqlBuilder = new StringBuilder();
            //    sqlBuilder.Append(String.Format("DECLARE @newBooking datetime = CONVERT(DATETIME, '{0:yyyy-MM-dd HH:mm}', 20); ", eventTime));
            //    sqlBuilder.Append("EXEC sp_InsertBooking ");
            //    sqlBuilder.Append("@datetime =  ");
            //    sqlBuilder.Append(" @newBooking ");
            //    sqlBuilder.Append(", @persNr = ");
            //    sqlBuilder.Append(employeeNumber);
            //    sqlBuilder.Append(",@status = ");
            //    sqlBuilder.Append(PZE_BookingStatus);
            //    sqlBuilder.Append(",@termDesc = ");
            //    sqlBuilder.Append("'" + terminalId.Replace("'", "''") + "'");

            //    dd.DBExecute(sqlBuilder.ToString());

            //}
            

            return true;
        }

        //Write the PZE Transaction into the Zeitbuchungen Table -- without BDE Part
        public static string CutTo20Chars(string input)
        {
            if (input.Length > 20)
                return input.Substring(0, 20);
            else
                return input;
        }

        public static string simpleInsertString(SortedList<string, object> par)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" INSERT INTO Zeitbuchungen (Z_Pers_Nr,z_zeit, z_zdatum,z_status,z_terminal,Z_ANr,Z_TNr) VALUES (");
            sb.Append(par["PersNr"]);
            sb.Append(",'" + par["z_Zeit"] + "'");
            sb.Append(",'" + par["Z_ZDatum"] + "'");
            sb.Append("," + par["z_status"]);
            sb.Append(",'" + par["z_terminal"] + "'");
            sb.Append("," + par["Z_ANr"]);
            sb.Append("," + par["Z_TNr"]);
            sb.Append(")");
            return sb.ToString();
        }
        private static string defaultInsertString(SortedList<string, object> par,int BookingIntervall)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" IF NOT EXISTS (SELECT Z_ZEIT FROM Zeitbuchungen WHERE Z_ZEIT > ");
            sb.Append("DATEADD(minute,-" + BookingIntervall + ",'" + par["z_Zeit"] + "')");
            sb.Append(" AND Z_ZEIT < ");
            sb.Append("DATEADD(minute," + BookingIntervall + ",'" + par["z_Zeit"] + "')");
            sb.Append(" AND Z_Pers_Nr = " + par["PersNr"]);
            sb.Append(")  BEGIN");
            sb.Append(" INSERT INTO Zeitbuchungen (Z_Pers_Nr,z_zeit, z_zdatum,z_status,z_terminal,Z_ANr,Z_TNr) VALUES (");
            sb.Append(par["PersNr"]);
            sb.Append(",'" + par["z_Zeit"] + "'");
            sb.Append(",'" + par["Z_ZDatum"] + "'");
            sb.Append("," + par["z_status"]);
            sb.Append(",'" + par["z_terminal"] + "'");
            sb.Append("," + par["Z_ANr"]);
            sb.Append("," + par["Z_TNr"]);
            sb.Append(")");
            sb.Append(" END");

            return sb.ToString();
        }

        public static bool SavePzeTransaction(Int64 CardId, DateTime eventTime, string terminalId, int status, ttxTools.ClsDB dbConnection, int BookingIntervall = 0, int Auftrag = 0, int Taetigkeiten = 0, int Stueckzahl = 0, ttxTools.ClsDB AzeDbConnection = null)
        {


            var par = new SortedList<string, object>();
            par.Add("Z_ANr", Auftrag);
            par.Add("Z_TNr", Taetigkeiten);
            par.Add("PersNr", Convert.ToInt32("0" + dbConnection.DBLookUP("(SELECT TOP 1 Pers_Nr FROM Personalstamm WHERE CONVERT(BIGINT,Pers_Ausweis_Nr) = " + CardId.ToString() + " OR CONVERT(BIGINT,Pers_Pre_Ausweis_Nr) = " + CardId.ToString() + ")")));
            par.Add("z_Zeit", eventTime);
            par.Add("Z_ZDatum", eventTime.Date);
            par.Add("z_status", status);
            par.Add("z_terminal", CutTo20Chars(terminalId));

            if (Auftrag == 0 && Taetigkeiten == 0)
            {

                //string insert = MuehlbachInsertString(par);
                string insert = (BookingIntervall == 0) ? simpleInsertString(par) : defaultInsertString(par,BookingIntervall);
                var res = dbConnection.DBExecute(insert);


                // Uwe Jacob, 2015-03-10: always fill table 'dirty_data'
                //if (eventTime < DateTime.Now.Date)
                if (res != -1)
                {
                    PZEEx.SetDirty((int) par["PersNr"], eventTime.Date.AddDays(-1), dbConnection);
                }
                return (true);
            }
            else
            {
                bool res = SaveBDETransaction(CardId, eventTime, terminalId, status, dbConnection, Auftrag, Taetigkeiten,
                    Stueckzahl, AzeDbConnection);
                return res;
            }


        }

        public static bool SaveBDETransaction(Int64 CardId, DateTime eventTime, string terminalId, int status, ttxTools.ClsDB dbConnection, int Auftrag = 0, int Taetigkeiten = 0, int Stueckzahl = 0, ttxTools.ClsDB AzeDbConnection = null)
        {


            var par = new SortedList<string, object>();
            par.Add("Z_ANr", Auftrag);
            par.Add("Z_TNr", Taetigkeiten);
            par.Add("PersNr", Convert.ToInt32("0" + dbConnection.DBLookUP("(SELECT TOP 1 Pers_Nr FROM Personalstamm WHERE CONVERT(BIGINT,Pers_Ausweis_Nr) = " + CardId.ToString() + " OR CONVERT(BIGINT,Pers_Pre_Ausweis_Nr) = " + CardId.ToString() + ")")));
            par.Add("z_Zeit", eventTime);
            par.Add("Z_ZDatum", eventTime.Date);
            par.Add("z_status", status);
            par.Add("z_terminal", CutTo20Chars(terminalId));

            //Write in PZE Database
            if (Stueckzahl == 0)
            {
                dbConnection.DBExecuteWithParameters("INSERT INTO Zeitbuchungen (Z_Pers_Nr,z_zeit, z_zdatum,z_status,z_terminal,Z_ANr,Z_TNr) VALUES (@PersNr,@z_Zeit,@Z_ZDatum,@z_status,@z_terminal,@Z_ANr,@Z_TNr)", par);

                // Uwe Jacob, 2015-03-10: always fill table 'dirty_data'
                // if (eventTime < DateTime.Now.Date)
                PZEEx.SetDirty((int)par["PersNr"], eventTime.Date.AddDays(-1), dbConnection);
            }
            else
            {
                par.Add("z_stueckzahl", Stueckzahl);
                dbConnection.DBExecuteWithParameters("INSERT INTO Zeitbuchungen (Z_Pers_Nr,z_zeit, z_zdatum,z_status,z_terminal,Z_ANr,Z_TNr,z_stueckzahl) VALUES (@PersNr,@z_Zeit,@Z_ZDatum,@z_status,@z_terminal,@Z_ANr,@Z_TNr,@z_stueckzahl)", par);

                // Uwe Jacob, 2015-03-10: always fill table 'dirty_data'
                //if (eventTime < DateTime.Now.Date)
                PZEEx.SetDirty((int)par["PersNr"], eventTime.Date.AddDays(-1), dbConnection);
            }
            //Write in AZE Database
            if (AzeDbConnection != null)
            {
                AzeDbConnection.DBExecuteWithParameters("INSERT INTO Stueckzahlbuchungen (AuftragsNr,TaetigkeitsNr,Stueck,PersoNr,Datum) VALUES (@Z_ANr,@Z_TNr,@z_stueckzahl,@PersNr,@z_Zeit)", par);
                //AzeDbConnection.DBExecuteWithParameters("INSERT INTO Ergebnistabelle (Erg_A_Nr,Erg_T_Nr,Stueck,PersoNr,Datum) VALUES (@Z_ANr,@Z_TNr,@z_stueckzahl,@PersNr,@z_Zeit)", par); 
            }
            return true;

        }

        //Identification - List
        public static DataTable GetIdentificationList(ttxTools.ClsDB dbConnection, string terminalName)
        {

            var sb = new StringBuilder();
            #region A.Seibel & Kenneth 2015-08-06

            //sb.Append("SELECT DISTINCT ");
            //sb.Append("COALESCE(CONVERT(VARCHAR(20), CASE WHEN CONVERT(BIGINT,[Card_Nr]) > 99999999 THEN[Card_Nr] END), REPLACE(STR(CONVERT(BIGINT,[Card_Nr]), 9, 0), ' ', '0')) as AWN, \n");
            //sb.Append("ISNULL(TbAccessPlan.AccessPlanNr, 0) AS 'Group', \n");
            //sb.Append("ISNULL((SELECT DISTINCT PinCode FROM Pers_PinCode WHERE Pers_PinCode.Pers_Nr = Personal.Pers_Nr AND(PinCodeType = 2)), 0)  AS Pin, \n");
            //sb.Append("ISNULL((SELECT DISTINCT PinCode FROM Pers_PinCode WHERE Pers_PinCode.Pers_Nr = Personal.Pers_Nr AND PinCodeType = 3)  , 0)  AS Menace, \n");
            //sb.Append("ISNULL(CONVERT(VARCHAR(10), TbAccessPlanPersMapping.DateFrom, 120), '1900-01-01')  AS ActiveStart, \n");
            //sb.Append("ISNULL(CONVERT(VARCHAR(10), TbAccessPlanPersMapping.DateTo, 120), '2099-01-01')   AS ActiveEnd, \n");
            //sb.Append("ISNULL(CONVERT(INT, Personal.CardActive), 0) AS ActiveGeneral FROM Personal \n");
            //sb.Append("LEFT OUTER JOIN TbAccessPlanPersMapping ON Personal.Pers_Nr = TbAccessPlanPersMapping.Pers_Nr \n");
            //sb.Append("LEFT OUTER JOIN TbAccessPlan ON TbAccessPlanPersMapping.AccessPlan_Nr = TbAccessPlan.ID \n");
            //sb.Append("ORDER BY[Group], AWN ");
            #endregion
            #region A.Seibel & Kenneth 2016-03-31
            //sb.Append("SELECT DISTINCT \n");
            //sb.Append("ISNULL(\n");
            //sb.Append("		CASE WHEN Personal.ActiveCardType = 1 THEN\n");
            //sb.Append("				(SELECT ISNULL((SELECT  REPLACE(STR(CONVERT(BIGINT, TransponderNr), 9, 0), ' ', '0') FROM Pers_Transponders WHERE PersNr = Personal.Pers_Nr  AND ISNULL(ValidTo, GETDATE()) >= CONVERT(date, GETDATE()) AND TransponderStatus = 1   AND TransponderType = 1), '000000000'))\n");
            //sb.Append("			 WHEN Personal.ActiveCardType = 2 THEN\n");
            //sb.Append("				(SELECT ISNULL((SELECT  REPLACE(STR(CONVERT(BIGINT, TransponderNr), 9, 0), ' ', '0') FROM Pers_Transponders WHERE PersNr = Personal.Pers_Nr  AND ISNULL(ValidTo, GETDATE()) >= CONVERT(date, GETDATE()) AND TransponderStatus = 1   AND TransponderType = 2), '000000000'))\n");
            //sb.Append("			ELSE\n");
            //sb.Append("				'000000000'\n");
            //sb.Append("		 END, '000000000') AS AWN,\n");
            ////sb.Append("ISNULL(TbAccessPlan.AccessPlanNr, 0) AS 'Group', \n");
            //sb.Append("CONVERT(VARCHAR(10),1) AS 'Group', \n");
            //sb.Append("CONVERT(VARCHAR(10),0)  AS Pin, \n");
            ////sb.Append("ISNULL((SELECT DISTINCT PinCode FROM Pers_PinCode WHERE Pers_PinCode.Pers_Nr = Personal.Pers_Nr AND(PinCodeType = 1)), 0)  AS Pin, \n");
            //sb.Append("ISNULL((SELECT DISTINCT PinCode FROM Pers_PinCode WHERE Pers_PinCode.Pers_Nr = Personal.Pers_Nr AND PinCodeType = 3)  , 0)  AS Menace, \n");
            //sb.Append("ISNULL(CASE \n");
            //sb.Append("	WHEN Personal.ActiveCardType = 1 THEN \n");
            //sb.Append("		CONVERT(VARCHAR(10),(SELECT MIN(ValidFrom) FROM (\n");
            //sb.Append("SELECT DateIssued as ValidFrom FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            //sb.Append("UNION \n");
            //sb.Append("SELECT ValidTo as ValidFrom FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            //sb.Append(") ValidFrom))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 2 THEN \n");
            //sb.Append("	CONVERT(VARCHAR(10),(SELECT MIN(ValidFrom) FROM (\n");
            //sb.Append("SELECT DateIssued as ValidFrom FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("UNION \n");
            //sb.Append("SELECT ValidTo as ValidFrom FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append(") ValidFrom))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 0 THEN  \n");
            //sb.Append("		CONVERT(VARCHAR(10), '2099-01-01') \n");
            //sb.Append("END,'1900-01-01')\n");
            //sb.Append("AS ActiveStart, \n");
            //sb.Append("ISNULL(CASE \n");
            //sb.Append("	WHEN Personal.ActiveCardType = 1 THEN \n");
            //sb.Append("		CONVERT(VARCHAR(10),(SELECT MAX(ValidTo) FROM (\n");
            //sb.Append("														SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            //sb.Append("													UNION \n");
            //sb.Append("														SELECT TransponderDeactivatedOn as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            //sb.Append("														) ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 2 THEN \n");
            //sb.Append("	CONVERT(VARCHAR(10),(SELECT MAX(ValidTo) FROM (\n");
            //sb.Append("														SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("													UNION \n");
            //sb.Append("														SELECT TransponderDeactivatedOn as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("														) ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 2 THEN \n");
            //sb.Append("		CONVERT(VARCHAR(10),(SELECT ISNULL(MIN(DateFrom.ValidFrom),'01-01-2099') as ValidFrom FROM \n");
            //sb.Append("			(\n");
            //sb.Append("				SELECT DateIssued as ValidFrom FROM [Pers_Transponders] WHERE TransponderStatus = 2\n");
            //sb.Append("			UNION \n");
            //sb.Append("				SELECT ValidFrom FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("			) DateFrom) , 120)   \n");
            //sb.Append("	WHEN Personal.ActiveCardType = 0 THEN  \n");
            //sb.Append("		CONVERT(VARCHAR(10), '1900-01-01') \n");
            //sb.Append("END,'2099-01-01')\n");
            //sb.Append("AS ActiveEnd,  \n");
            //sb.Append("CASE WHEN \n");
            //sb.Append("	(CASE WHEN ISNULL(ActiveCardType,0) > 0 THEN 1 ELSE 0 END) = 1\n");
            //sb.Append(" AND\n");
            //sb.Append("	(SELECT CASE WHEN \n");

            //sb.Append("	CASE WHEN (SELECT \n");
            //sb.Append("				CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            //sb.Append("				THEN CONVERT(datetime2,'01-01-2099')  \n");
            //sb.Append("				ELSE MAX(ISNULL(ExpiryDate,'01-01-2099')) END 	\n");
            //sb.Append("			   FROM dbo.Pers_IdentityCard WHERE  Pers_Nr = Personal.Pers_Nr) >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            //sb.Append("	= 1 AND\n");

            //sb.Append("	CASE WHEN (SELECT \n");
            //sb.Append("				CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            //sb.Append("				THEN CONVERT(datetime2,'01-01-2099')  \n");
            //sb.Append("				ELSE MAX(ISNULL(ExpiryDate,'01-01-2099')) END \n");
            //sb.Append("			   FROM dbo.Pers_Passport WHERE Pers_Nr = Personal.Pers_Nr)  >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            //sb.Append("	= 1 AND\n");

            //sb.Append("	CASE WHEN (SELECT \n");
            //sb.Append("	CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            //sb.Append("				THEN CONVERT(datetime2,'01-01-2099')  \n");
            //sb.Append(" 				ELSE MAX(ISNULL(ExpiryDate,'01-01-2099')) END 	\n");
            //sb.Append("	          FROM dbo.Pers_HealthCard WHERE Pers_Nr = Personal.Pers_Nr)  >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");

            //sb.Append("	= 1 \n");
            //sb.Append("THEN 1 ELSE 0\n");
            //sb.Append("END) = 1 THEN 1 ELSE 0 END\n");
            //sb.Append("AS ActiveGeneral \n");
            //sb.Append("FROM Personal \n");
            //sb.Append("LEFT OUTER JOIN TbAccessPlanPersMapping ON Personal.Pers_Nr = TbAccessPlanPersMapping.Pers_Nr \n");
            //sb.Append("LEFT OUTER JOIN TbAccessPlan ON TbAccessPlanPersMapping.AccessPlan_Nr = TbAccessPlan.ID \n");
            #endregion
            #region A.Seibel & Kenneth 2016-03-31
            //sb.Append("SELECT DISTINCT \n");
            //sb.Append("ISNULL(\n");
            //sb.Append("		CASE WHEN Personal.ActiveCardType = 1 THEN\n");
            //sb.Append("				(SELECT ISNULL((SELECT TOP 1 REPLACE(STR(CONVERT(BIGINT, TransponderNr), 9, 0), ' ', '0') FROM Pers_Transponders WHERE PersNr = Personal.Pers_Nr  AND ISNULL(ValidTo, GETDATE()) >= CONVERT(date, GETDATE()) AND TransponderStatus = 1   AND TransponderType = 1), '000000000'))\n");
            //sb.Append("			 WHEN Personal.ActiveCardType = 2 THEN\n");
            //sb.Append("				(SELECT ISNULL((SELECT TOP 1 REPLACE(STR(CONVERT(BIGINT, TransponderNr), 9, 0), ' ', '0') FROM Pers_Transponders WHERE PersNr = Personal.Pers_Nr  AND ISNULL(ValidTo, GETDATE()) >= CONVERT(date, GETDATE()) AND TransponderStatus = 1   AND TransponderType = 2), '000000000'))\n");
            //sb.Append("			ELSE\n");
            //sb.Append("				'000000000'\n");
            //sb.Append("		 END, '000000000') AS AWN,\n");
            //sb.Append("CONVERT(VARCHAR(10),1) AS 'Group', \n");
            //sb.Append("CONVERT(VARCHAR(10),0)  AS Pin, \n");
            //sb.Append("ISNULL((SELECT DISTINCT PinCode FROM Pers_PinCode WHERE Pers_PinCode.Pers_Nr = Personal.Pers_Nr AND PinCodeType = 3)  , 0)  AS Menace, \n");
            //sb.Append("ISNULL(CASE \n");
            //sb.Append("	WHEN Personal.ActiveCardType = 1 THEN \n");
            //sb.Append("		CONVERT(VARCHAR(10),(SELECT MIN(ValidTo) FROM (\n");
            //sb.Append("SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1 AND TransponderStatus = 1\n");
            //sb.Append("UNION \n");
            //sb.Append("SELECT ExtendedTo as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1 AND TransponderStatus = 1\n");
            //sb.Append(") ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 2 THEN \n");
            //sb.Append("	CONVERT(VARCHAR(10),(SELECT MIN(ValidTo) FROM (\n");
            //sb.Append("SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2 AND TransponderStatus = 1\n");
            //sb.Append("UNION \n");
            //sb.Append("SELECT ExtendedTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2 AND TransponderStatus = 1\n");
            //sb.Append(") ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 0 THEN  \n");
            //sb.Append("		CONVERT(VARCHAR(10), '2099-01-01') \n");
            //sb.Append("END,'1900-01-01')\n");
            //sb.Append("AS ActiveStart, \n");
            //sb.Append("ISNULL(CASE \n");
            //sb.Append("	WHEN Personal.ActiveCardType = 1 THEN \n");
            //sb.Append("		CONVERT(VARCHAR(10),(SELECT MAX(ValidTo) FROM (\n");
            //sb.Append("														SELECT ValidTo as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            //sb.Append("													UNION \n");
            //sb.Append("														SELECT TransponderDeactivatedOn as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            //sb.Append("														) ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 2 THEN \n");
            //sb.Append("	CONVERT(VARCHAR(10),(SELECT MAX(ValidTo) FROM (\n");
            //sb.Append("														SELECT ValidTo as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("													UNION \n");
            //sb.Append("														SELECT TransponderDeactivatedOn as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("														) ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 2 THEN \n");
            //sb.Append("		CONVERT(VARCHAR(10),(SELECT ISNULL(MIN(DateFrom.ValidTo),'01-01-2099') as ValidTo FROM \n");
            //sb.Append("			(\n");
            //sb.Append("				SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE TransponderStatus = 2\n");
            //sb.Append("			UNION \n");
            //sb.Append("				SELECT ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("			) DateFrom) , 120)   \n");
            //sb.Append("	WHEN Personal.ActiveCardType = 0 THEN  \n");
            //sb.Append("		CONVERT(VARCHAR(10), '1900-01-01') \n");
            //sb.Append("END,'2099-01-01')\n");
            //sb.Append("AS ActiveEnd, \n");
            //sb.Append(" \n");
            //sb.Append("CASE WHEN \n");
            //sb.Append("	(CASE WHEN ISNULL(ActiveCardType,0) > 0 THEN 1 ELSE 0 END) = 1\n");
            //sb.Append(" AND\n");
            //sb.Append("	(SELECT CASE WHEN \n");
            //sb.Append("	CASE WHEN (SELECT \n");
            //sb.Append("				CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            //sb.Append("				THEN CONVERT(datetime2,'01-01-2099')  \n");
            //sb.Append("				ELSE ISNULL(MAX(ISNULL(ExpiryDate,'01-01-2099')),'01-01-2099') END 	\n");
            //sb.Append("			   FROM dbo.Pers_IdentityCard WHERE  Pers_Nr = Personal.Pers_Nr) >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            //sb.Append("	= 1 AND\n");
            //sb.Append("	CASE WHEN (SELECT \n");
            //sb.Append("				CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            //sb.Append("				THEN CONVERT(datetime2,'01-01-2099')  \n");
            //sb.Append("				ELSE ISNULL(MAX(ISNULL(ExpiryDate,'01-01-2099')),'01-01-2099') END \n");
            //sb.Append("			   FROM dbo.Pers_Passport WHERE Pers_Nr = Personal.Pers_Nr)  >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            //sb.Append("	= 1 AND\n");
            //sb.Append("	CASE WHEN (SELECT \n");
            //sb.Append("	CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            //sb.Append("				THEN CONVERT(datetime2,'01-01-2099')  \n");
            //sb.Append(" 				ELSE ISNULL(MAX(ISNULL(ExpiryDate,'01-01-2099')),'01-01-2099') END 	\n");
            //sb.Append("	          FROM dbo.Pers_HealthCard WHERE Pers_Nr = Personal.Pers_Nr)  >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            //sb.Append("	= 1 \n");
            //sb.Append("THEN 1 ELSE 0\n");
            //sb.Append("END) = 1 THEN 1 ELSE 0 END\n");
            //sb.Append("AS ActiveGeneral \n");
            //sb.Append("FROM Personal \n");
            //sb.Append("LEFT OUTER JOIN TbAccessPlanPersMapping ON Personal.Pers_Nr = TbAccessPlanPersMapping.Pers_Nr \n");
            //sb.Append("LEFT OUTER JOIN TbAccessPlan ON TbAccessPlanPersMapping.AccessPlan_Nr = TbAccessPlan.ID \n");

            #endregion

            #region A. Seibel Visitor Data
            //sb.Append(" ;with cte as (\n");
            //sb.Append("select \n");
            //sb.Append("T.TransponderNr as AWN\n");
            //sb.Append(",CASE WHEN T.ValidFrom <= AT.StartDate THEN T.ValidFrom ELSE AT.StartDate END as ValidMin\n");
            //sb.Append(",CASE WHEN T.ValidTo >= AT.EndDate THEN T.ValidTo ELSE AT.EndDate END as ValidMax\n");
            //sb.Append("FROM Visitors V\n");
            //sb.Append("LEFT JOIN VisitorTransponders T ON V.ID = T.VisitorID\n");
            //sb.Append("LEFT JOIN VisitorAccessTime AT ON V.ID = AT.VisitorID\n");
            //sb.Append("WHERE TransponderStatus = 1\n");
            //sb.Append(")\n");
            //sb.Append("SELECT DISTINCT \n");
            //sb.Append("ISNULL(\n");
            //sb.Append("		CASE WHEN Personal.ActiveCardType = 1 THEN\n");
            //sb.Append("				(SELECT ISNULL((SELECT TOP 1 REPLACE(STR(CONVERT(BIGINT, TransponderNr), 10, 0), ' ', '0') FROM Pers_Transponders WHERE PersNr = Personal.Pers_Nr  AND ISNULL(ValidTo, GETDATE()) >= CONVERT(date, GETDATE()) AND TransponderStatus = 1   AND TransponderType = 1), '000000000'))\n");
            //sb.Append("			 WHEN Personal.ActiveCardType = 2 THEN\n");
            //sb.Append("				(SELECT ISNULL((SELECT TOP 1 REPLACE(STR(CONVERT(BIGINT, TransponderNr), 10, 0), ' ', '0') FROM Pers_Transponders WHERE PersNr = Personal.Pers_Nr  AND ISNULL(ValidTo, GETDATE()) >= CONVERT(date, GETDATE()) AND TransponderStatus = 1   AND TransponderType = 2), '000000000'))\n");
            //sb.Append("			ELSE\n");
            //sb.Append("				'000000000'\n");
            //sb.Append("		 END, '000000000') AS AWN,\n");
            //sb.Append("CONVERT(VARCHAR(10),1) AS 'Group', \n");
            //sb.Append("CONVERT(VARCHAR(10),0)  AS Pin, \n");
            //sb.Append("ISNULL((SELECT DISTINCT PinCode FROM Pers_PinCode WHERE Pers_PinCode.Pers_Nr = Personal.Pers_Nr AND PinCodeType = 3)  , 0)  AS Menace, \n");
            //sb.Append("ISNULL(CASE \n");
            //sb.Append("	WHEN Personal.ActiveCardType = 1 THEN \n");
            //sb.Append("		CONVERT(VARCHAR(10),(SELECT MIN(ValidTo) FROM (\n");
            //sb.Append("SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1 AND TransponderStatus = 1\n");
            //sb.Append("UNION \n");
            //sb.Append("SELECT ExtendedTo as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1 AND TransponderStatus = 1\n");
            //sb.Append(") ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 2 THEN \n");
            //sb.Append("	CONVERT(VARCHAR(10),(SELECT MIN(ValidTo) FROM (\n");
            //sb.Append("SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2 AND TransponderStatus = 1\n");
            //sb.Append("UNION \n");
            //sb.Append("SELECT ExtendedTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2 AND TransponderStatus = 1\n");
            //sb.Append(") ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 0 THEN  \n");
            //sb.Append("		CONVERT(VARCHAR(10), '2099-01-01') \n");
            //sb.Append("END,'1900-01-01')\n");
            //sb.Append("AS ActiveStart, \n");
            //sb.Append("ISNULL(CASE \n");
            //sb.Append("	WHEN Personal.ActiveCardType = 1 THEN \n");
            //sb.Append("		CONVERT(VARCHAR(10),(SELECT MAX(ValidTo) FROM (\n");
            //sb.Append("														SELECT ValidTo as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            //sb.Append("													UNION \n");
            //sb.Append("														SELECT TransponderDeactivatedOn as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            //sb.Append("														) ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 2 THEN \n");
            //sb.Append("	CONVERT(VARCHAR(10),(SELECT MAX(ValidTo) FROM (\n");
            //sb.Append("														SELECT ValidTo as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("													UNION \n");
            //sb.Append("														SELECT TransponderDeactivatedOn as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("														) ValidTo))\n");
            //sb.Append("	WHEN Personal.ActiveCardType = 2 THEN \n");
            //sb.Append("		CONVERT(VARCHAR(10),(SELECT ISNULL(MIN(DateFrom.ValidTo),'01-01-2099') as ValidTo FROM \n");
            //sb.Append("			(\n");
            //sb.Append("				SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE TransponderStatus = 2\n");
            //sb.Append("			UNION \n");
            //sb.Append("				SELECT ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            //sb.Append("			) DateFrom) , 120)   \n");
            //sb.Append("	WHEN Personal.ActiveCardType = 0 THEN  \n");
            //sb.Append("		CONVERT(VARCHAR(10), '1900-01-01') \n");
            //sb.Append("END,'2099-01-01')\n");
            //sb.Append("AS ActiveEnd, \n");
            //sb.Append(" \n");
            //sb.Append("CASE WHEN \n");
            //sb.Append("	(CASE WHEN ISNULL(ActiveCardType,0) > 0 THEN 1 ELSE 0 END) = 1\n");
            //sb.Append(" AND\n");
            //sb.Append("	(SELECT CASE WHEN \n");
            //sb.Append("	CASE WHEN (SELECT \n");
            //sb.Append("				CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            //sb.Append("				THEN CONVERT(datetime2,'01-01-2099')  \n");
            //sb.Append("				ELSE ISNULL(MAX(ISNULL(ExpiryDate,'01-01-2099')),'01-01-2099') END 	\n");
            //sb.Append("			   FROM dbo.Pers_IdentityCard WHERE  Pers_Nr = Personal.Pers_Nr) >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            //sb.Append("	= 1 AND\n");
            //sb.Append("	CASE WHEN (SELECT \n");
            //sb.Append("				CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            //sb.Append("				THEN CONVERT(datetime2,'01-01-2099')  \n");
            //sb.Append("				ELSE ISNULL(MAX(ISNULL(ExpiryDate,'01-01-2099')),'01-01-2099') END \n");
            //sb.Append("			   FROM dbo.Pers_Passport WHERE Pers_Nr = Personal.Pers_Nr)  >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            //sb.Append("	= 1 AND\n");
            //sb.Append("	CASE WHEN (SELECT \n");
            //sb.Append("	CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            //sb.Append("				THEN CONVERT(datetime2,'01-01-2099')  \n");
            //sb.Append(" 				ELSE ISNULL(MAX(ISNULL(ExpiryDate,'01-01-2099')),'01-01-2099') END 	\n");
            //sb.Append("	          FROM dbo.Pers_HealthCard WHERE Pers_Nr = Personal.Pers_Nr)  >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            //sb.Append("	= 1 \n");
            //sb.Append("THEN 1 ELSE 0\n");
            //sb.Append("END) = 1 THEN 1 ELSE 0 END\n");
            //sb.Append("AS ActiveGeneral \n");
            //sb.Append("FROM Personal \n");
            //sb.Append("LEFT OUTER JOIN TbAccessPlanPersMapping ON Personal.Pers_Nr = TbAccessPlanPersMapping.Pers_Nr \n");
            //sb.Append("LEFT OUTER JOIN TbAccessPlan ON TbAccessPlanPersMapping.AccessPlan_Nr = TbAccessPlan.ID \n");
            //sb.Append(" UNION \n");
            //sb.Append("select \n");
            //sb.Append("ISNULL(REPLACE(STR(CONVERT(BIGINT, AWN), 9, 0), ' ', '0'),'0') as AWN \n");
            //sb.Append(",CONVERT(VARCHAR(10),1) AS 'Group'\n");
            //sb.Append(",CONVERT(VARCHAR(10),0)  AS Pin\n");
            //sb.Append(",CONVERT(VARCHAR(10),0)  AS Menace\n");
            //sb.Append(",CONVERT(VARCHAR(10), ValidMin) as ActiveStart\n");
            //sb.Append(",CONVERT(VARCHAR(10), ValidMax) as ActiveEnd\n");
            //sb.Append(",CONVERT(VARCHAR(10),1)  AS ActiveGeneral\n");
            //sb.Append("from cte\n");
            #endregion

            #region A. Seibel 20160523

            sb.Append("DECLARE @desc VARCHAR(300) = '" + terminalName + "'; ");
            sb.Append("\n");
            sb.Append("DECLARE @ReaderTyp int = (select TOP 1 TR.ReaderNr from TerminalReaders TR \n");
            sb.Append("            								INNER JOIN TerminalReadersStatic TRStatic ON TRStatic.ID = TR.ReaderNr \n");
            sb.Append("            								WHERE Name = @desc)\n");
            sb.Append("            		 \n");
            sb.Append("            ;with cte as (\n");
            sb.Append("                        select \n");
            sb.Append("                        T.TransponderNr as AWN\n");
            sb.Append("                        ,CASE WHEN T.ValidFrom <= AT.StartDate THEN T.ValidFrom ELSE AT.StartDate END as ValidMin\n");
            sb.Append("                        ,CASE WHEN T.ValidTo >= AT.EndDate THEN T.ValidTo ELSE AT.EndDate END as ValidMax\n");
            sb.Append("                        FROM Visitors V\n");
            sb.Append("                        LEFT JOIN VisitorTransponders T ON V.ID = T.VisitorID\n");
            sb.Append("                        LEFT JOIN VisitorAccessTime AT ON V.ID = AT.VisitorID\n");
            sb.Append("                        WHERE TransponderStatus = 1\n");
            sb.Append("                        )\n");
            sb.Append("                        SELECT DISTINCT \n");
            sb.Append("                        ISNULL(\n");
            sb.Append("                        		CASE WHEN Personal.ActiveCardType = 1 THEN\n");
            sb.Append("                        				(SELECT ISNULL((SELECT TOP 1 REPLACE(STR(CONVERT(BIGINT, TransponderNr), 9, 0), ' ', '0') FROM Pers_Transponders WHERE PersNr = Personal.Pers_Nr  AND ISNULL(ValidTo, GETDATE()) >= CONVERT(date, GETDATE()) AND TransponderStatus = 1   AND TransponderType = 1), '000000000'))\n");
            sb.Append("                        			 WHEN Personal.ActiveCardType = 2 THEN\n");
            sb.Append("                        				(SELECT ISNULL((SELECT TOP 1 REPLACE(STR(CONVERT(BIGINT, TransponderNr), 9, 0), ' ', '0') FROM Pers_Transponders WHERE PersNr = Personal.Pers_Nr  AND ISNULL(ValidTo, GETDATE()) >= CONVERT(date, GETDATE()) AND TransponderStatus = 1   AND TransponderType = 2), '000000000'))\n");
            sb.Append("                        			ELSE\n");
            sb.Append("                        				'000000000'\n");
            sb.Append("                        		 END, '000000000') AS AWN,\n");
            sb.Append("            \n");
            sb.Append("            			CASE WHEN ISNULL(TbAccessPlan.AccessPlanNr, 0) IN (	 SELECT TP.AccessPlanNr FROM TerminalConfig TC\n");
            sb.Append("            	 INNER JOIN ReaderAssigned RA ON RA.TerminalID = TC.ID\n");
            sb.Append("            	 LEFT JOIN ReaderAccessPlan RAP ON RAP.ReaderId = RA.ReaderID\n");
            sb.Append("            	 INNER JOIN TbAccessPlan TP ON TP.ID = RAP.AccessPlanId \n");
            sb.Append("            	 WHERE  Description = @desc AND RAP.AccessPlanReaderStatus = 1) THEN ISNULL(TbAccessPlan.AccessPlanNr, 0) ELSE 0 END\n");
            sb.Append("                         AS 'Group', \n");
            sb.Append("            			\n");
            sb.Append("            			CASE @ReaderTyp\n");
            sb.Append("            				WHEN 1008  THEN \n");
            sb.Append("                        ISNULL((SELECT DISTINCT CONVERT(VARCHAR(50),PinCode) FROM Pers_PinCode WHERE Pers_PinCode.Pers_Nr = Personal.Pers_Nr AND PinCodeType = 3)  , '0')  \n");
            sb.Append("            			ELSE '0' END AS Pin, \n");
            sb.Append("            \n");
            sb.Append("                        CONVERT(VARCHAR(10),0)  AS Menace,\n");
            sb.Append("                        ISNULL(CASE \n");
            sb.Append("                        	WHEN Personal.ActiveCardType = 1 THEN \n");
            sb.Append("                        		CONVERT(VARCHAR(10),(SELECT MIN(ValidTo) FROM (\n");
            sb.Append("                        SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1 AND TransponderStatus = 1\n");
            sb.Append("                        UNION \n");
            sb.Append("                        SELECT ExtendedTo as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1 AND TransponderStatus = 1\n");
            sb.Append("                        ) ValidTo))\n");
            sb.Append("                        	WHEN Personal.ActiveCardType = 2 THEN \n");
            sb.Append("                        	CONVERT(VARCHAR(10),(SELECT MIN(ValidTo) FROM (\n");
            sb.Append("                        SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2 AND TransponderStatus = 1\n");
            sb.Append("                        UNION \n");
            sb.Append("                        SELECT ExtendedTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2 AND TransponderStatus = 1\n");
            sb.Append("                        ) ValidTo))\n");
            sb.Append("                        	WHEN Personal.ActiveCardType = 0 THEN  \n");
            sb.Append("                        		CONVERT(VARCHAR(10), '2099-01-01') \n");
            sb.Append("                        END,'1900-01-01')\n");
            sb.Append("                        AS ActiveStart, \n");
            sb.Append("                        ISNULL(CASE \n");
            sb.Append("                        	WHEN Personal.ActiveCardType = 1 THEN \n");
            sb.Append("                        		CONVERT(VARCHAR(10),(SELECT MAX(ValidTo) FROM (\n");
            sb.Append("                        														SELECT ValidTo as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            sb.Append("                        													UNION \n");
            sb.Append("                        														SELECT TransponderDeactivatedOn as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 1\n");
            sb.Append("                        														) ValidTo))\n");
            sb.Append("                        	WHEN Personal.ActiveCardType = 2 THEN \n");
            sb.Append("                        	CONVERT(VARCHAR(10),(SELECT MAX(ValidTo) FROM (\n");
            sb.Append("                        														SELECT ValidTo as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            sb.Append("                        													UNION \n");
            sb.Append("                        														SELECT TransponderDeactivatedOn as ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            sb.Append("                        														) ValidTo))\n");
            sb.Append("                        	WHEN Personal.ActiveCardType = 2 THEN \n");
            sb.Append("                        		CONVERT(VARCHAR(10),(SELECT ISNULL(MIN(DateFrom.ValidTo),'01-01-2099') as ValidTo FROM \n");
            sb.Append("                        			(\n");
            sb.Append("                        				SELECT ValidFrom as ValidTo FROM [Pers_Transponders] WHERE TransponderStatus = 2\n");
            sb.Append("                        			UNION \n");
            sb.Append("                        				SELECT ValidTo FROM [Pers_Transponders] WHERE PersNr = Personal.Pers_Nr AND TransponderType = 2\n");
            sb.Append("                        			) DateFrom) , 120)   \n");
            sb.Append("                        	WHEN Personal.ActiveCardType = 0 THEN  \n");
            sb.Append("                        		CONVERT(VARCHAR(10), '1900-01-01') \n");
            sb.Append("                        END,'2099-01-01')\n");
            sb.Append("                        AS ActiveEnd ,\n");
            sb.Append("                         \n");
            sb.Append("                        CASE WHEN \n");
            sb.Append("                        	(CASE WHEN ISNULL(ActiveCardType,0) > 0 THEN 1 ELSE 0 END) = 1\n");
            sb.Append("                         AND\n");
            sb.Append("                        	(SELECT CASE WHEN \n");
            sb.Append("                        	CASE WHEN (SELECT \n");
            sb.Append("                        				CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            sb.Append("                        				THEN CONVERT(datetime2,'01-01-2099')  \n");
            sb.Append("                        				ELSE ISNULL(MAX(ISNULL(ExpiryDate,'01-01-2099')),'01-01-2099') END 	\n");
            sb.Append("                        			   FROM dbo.Pers_IdentityCard WHERE  Pers_Nr = Personal.Pers_Nr) >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            sb.Append("                        	= 1 AND\n");
            sb.Append("                        	CASE WHEN (SELECT \n");
            sb.Append("                        				CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            sb.Append("                        				THEN CONVERT(datetime2,'01-01-2099')  \n");
            sb.Append("                        				ELSE ISNULL(MAX(ISNULL(ExpiryDate,'01-01-2099')),'01-01-2099') END \n");
            sb.Append("                        			   FROM dbo.Pers_Passport WHERE Pers_Nr = Personal.Pers_Nr)  >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            sb.Append("                        	= 1 AND\n");
            sb.Append("                        	CASE WHEN (SELECT \n");
            sb.Append("                        	CASE WHEN MAX(ISNULL(ExpiryDate,'01-01-2099')) = CONVERT(datetime2,'0001-01-01 00:00:00.0000000') \n");
            sb.Append("                        				THEN CONVERT(datetime2,'01-01-2099')  \n");
            sb.Append("                         				ELSE ISNULL(MAX(ISNULL(ExpiryDate,'01-01-2099')),'01-01-2099') END 	\n");
            sb.Append("                        	          FROM dbo.Pers_HealthCard WHERE Pers_Nr = Personal.Pers_Nr)  >= DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE())) THEN 1 ELSE 0 END \n");
            sb.Append("                        	= 1 \n");
            sb.Append("                        THEN 1 ELSE 0\n");
            sb.Append("                        END) = 1 THEN \n");
            sb.Append("            			\n");
            sb.Append("            			CASE @ReaderTyp\n");
            sb.Append("            				WHEN 1008 THEN \n");
            sb.Append("            							CASE WHEN ISNULL((SELECT DISTINCT CONVERT(VARCHAR(50),ISNULL(PinCode,'')) FROM Pers_PinCode WHERE Pers_PinCode.Pers_Nr = Personal.Pers_Nr AND PinCodeType = 3)  , '0') = '' THEN '0' ELSE '8' END\n");
            sb.Append("            			ELSE '1'\n");
            sb.Append("            			END\n");
            sb.Append("            						\n");
            sb.Append("            			ELSE '0' END\n");
            sb.Append("                        \n");
            sb.Append("            			\n");
            sb.Append("            			AS ActiveGeneral \n");
            sb.Append("                        \n");
            sb.Append("            			\n");
            sb.Append("            			\n");
            sb.Append("            			FROM Personal \n");
            sb.Append("                        LEFT OUTER JOIN TbAccessPlanPersMapping ON Personal.Pers_Nr = TbAccessPlanPersMapping.Pers_Nr \n");
            sb.Append("                        LEFT OUTER JOIN TbAccessPlan ON TbAccessPlanPersMapping.AccessPlan_Nr = TbAccessPlan.ID \n");
            sb.Append("            \n");
            sb.Append("            \n");
            sb.Append("                         UNION \n");
            sb.Append("                        select \n");
            sb.Append("                        ISNULL(REPLACE(STR(CONVERT(BIGINT, AWN), 9, 0), ' ', '0'),'0') as AWN \n");
            sb.Append("                        ,CONVERT(VARCHAR(10),1) AS 'Group'\n");
            sb.Append("                        ,CONVERT(VARCHAR(10),0)  AS Pin\n");
            sb.Append("                        ,CONVERT(VARCHAR(10),0)  AS Menace\n");
            sb.Append("                        ,CONVERT(VARCHAR(10), ValidMin) as ActiveStart\n");
            sb.Append("                        ,CONVERT(VARCHAR(10), ValidMax) as ActiveEnd\n");
            sb.Append("                        ,CONVERT(VARCHAR(10),1)  AS ActiveGeneral\n");
            sb.Append("                        from cte\n");

            #endregion

            string qq = sb.ToString();
            return dbConnection.DBDataTable(sb.ToString());
        }
        //Reader - List
        public static DataTable GetReaderList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            //A.Seibel & Kenneth 2015-08-06
            StringBuilder sb = new StringBuilder();

            #region A.Seibel commented out: 2015-08-20
            //sb.Append("DECLARE @desc VarChar(300);  ");
            //sb.Append("SET @desc = '" + terminalName + "';  ");
            //sb.Append("  ");
            //sb.Append("SELECT TOP 1 		  ");
            //sb.Append("TB.TermID * 100 + ROW_NUMBER() Over (Order by @@rowcount ASC )  - 1 AS ID 		  ");
            //sb.Append(",0 AS ZM 		  ");
            //sb.Append(",REPLACE(STR(320,3,0),' ','0') AS TM 		  ");
            //sb.Append(",0 AS RefLocation  		  ");
            //sb.Append(",0 AS RefAction 		  ");
            //sb.Append(",0 AS PinGeneral 		  ");
            //sb.Append("FROM TerminalReaders AS  TR INNER JOIN TerminalConfig AS TB ON TR.TermId  = TB.ID 	WHERE TB.Description = @desc  ");
            //sb.Append("  ");
            //sb.Append("UNION 		  ");
            //sb.Append("  ");
            //sb.Append("SELECT  ");
            //sb.Append("TB.TermID * 100 + ROW_NUMBER() Over (Order by @@rowcount ASC )   AS ID,   ");
            //sb.Append("1 AS ZM,  ");
            //sb.Append("REPLACE(STR((ROW_NUMBER() Over (Order by @@rowcount ASC )  ) * 10 ,3,0),' ','0') AS TM,  ");
            //sb.Append("ROW_NUMBER() Over (Order by @@rowcount ASC )  AS RefLocation,  ");
            //sb.Append("ROW_NUMBER() Over (Order by @@rowcount ASC )   AS RefAction,  ");
            //sb.Append("0 AS PinGeneral  ");
            //sb.Append("FROM TerminalReaders AS  TR INNER JOIN TerminalConfig AS TB ON TR.TermId  = TB.ID  WHERE TB.Description = @desc  ");
            #endregion
            sb.Append("DECLARE @desc VarChar(300) = '" + terminalName + "'; ");
            sb.Append("SELECT TOP 1 		 ");
            sb.Append("TB.TermID * 100 + ROW_NUMBER() Over (Order by @@rowcount ASC )  - 1 AS ID 		 ");
            sb.Append(",0 AS ZM 		 ");
            sb.Append(",REPLACE(STR(320,3,0),' ','0') AS TM 		 ");
            sb.Append(",0 AS RefLocation  		 ");
            sb.Append(",0 AS RefAction 		 ");
            sb.Append(",0 AS PinGeneral 		 ");
            sb.Append("FROM TerminalReaders AS  TR INNER JOIN TerminalConfig AS TB ON TR.TermId  = TB.ID 	WHERE TB.Description = @desc ");
            sb.Append(" ");
            sb.Append("UNION 		 ");
            sb.Append(" ");
            sb.Append("SELECT ");
            sb.Append("TB.TermID * 100 + ROW_NUMBER() Over (Order by @@rowcount ASC )   AS ID,  ");
            sb.Append("1 AS ZM, ");
            sb.Append("REPLACE(STR((ROW_NUMBER() Over (Order by @@rowcount ASC ) - 1)* 10  ,3,0),' ','0') AS TM, ");
            sb.Append("ROW_NUMBER() Over (Order by @@rowcount ASC )  AS RefLocation, ");
            sb.Append("ROW_NUMBER() Over (Order by @@rowcount ASC )   AS RefAction, ");
            sb.Append("0 AS PinGeneral ");
            sb.Append("FROM TerminalReaders AS  TR INNER JOIN TerminalConfig AS TB ON TR.TermId  = TB.ID  WHERE TB.Description = @desc ");


            return dbConnection.DBDataTable(sb.ToString());
        }

        //Location - List
        public static DataTable GetLocationList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            StringBuilder sb = new StringBuilder();
            #region A.Seibel commented out: 2015-08-20
            //sb.Append("DECLARE @desc VarChar(300);  ");
            //sb.Append("SET @desc = '" + terminalName + "';  ");
            //sb.Append("SELECT DISTINCT  ");
            //sb.Append("ReaderID AS ID  ");
            //sb.Append(",APRM.AcessPlanID As 'RefGroup'  ");
            //sb.Append(",1000 + (AccessProfile - 20000) * 10 + Level AS RefTime  ");
            //sb.Append(",0 AS RefTimenoPin  ");
            //sb.Append(",TerminalConfig.Description AS TerminalBezeichnung  ");
            //sb.Append("FROM TerminalConfig   ");
            //sb.Append("--ReaderConfig -> Reader   ");
            //sb.Append("RIGHT OUTER JOIN   ");
            //sb.Append("ReaderAssigned AS RA   ");
            //sb.Append("ON RA.TerminalID = TerminalConfig.TermID  ");
            //sb.Append("--Reader -> BuildingPlanBuild, BuildingPlan -> Zutrittsplan  ");
            //sb.Append("RIGHT OUTER JOIN   ");
            //sb.Append("TbAcessPlanReaderMapping AS APRM  ");
            //sb.Append("ON APRM.BuildingID = RA.BuildingPlanID  ");
            //sb.Append("  ");
            //sb.Append("-- ZutrittsPlan -> Zutrittscalendar  ");
            //sb.Append("RIGHT OUTER JOIN TbAccessPlan  ");
            //sb.Append("ON AccessPlanNr = AcessPlanID  ");
            //sb.Append("  ");
            //sb.Append("--Zutrittscalendar -> Zutrittsprofil (AccessProfile), Days (7 Days)  ");
            //sb.Append("RIGHT  JOIN (  ");
            //sb.Append("select *   ");
            //sb.Append("FROM AccessCalendarProfiles  ");
            //sb.Append("Where Date BETWEEN DATEADD(d,-7,GETDATE()) AND DATEADD(d,0,GETDATE())  ");
            //sb.Append(") ACP ON ACP.AccessCalendarID = TbAccessPlan.AccessCalendarId  ");
            //sb.Append("  ");
            //sb.Append("--Zutrittsprofil -> Zutrittsprofil Level  ");
            //sb.Append("RIGHT JOIN [ZuttritProfilesTimeFrames] ZPT   ");
            //sb.Append("ON ZPT.AccessProfID = ACP.AccessProfile  ");
            //sb.Append("where   ");
            //sb.Append("--TerminalConfig.Description = @desc  ");
            //sb.Append("ZPT.ProfilAktiv = 1  ");
            #endregion
            sb.Append("DECLARE @desc VARCHAR(300) = '" + terminalName + "'; ");
            sb.Append(" ");
            sb.Append("SELECT ROW_NUMBER() Over (Order by @@rowcount ASC ) AS ID, RefGroup, RefTime, RefTimenoPin FROM ");
            sb.Append("( ");
            sb.Append(" ");
            sb.Append("select DISTINCT ");
            sb.Append(" ");
            sb.Append("ReaderID ");
            sb.Append(", TA.AccessPlanNr AS RefGroup ");
            sb.Append(", 1000 + ZP.AccessProfileNo * 10 + ZPT.Level AS RefTime ");
            sb.Append(", 0 AS RefTimenoPin ");
            sb.Append(" ");
            sb.Append("FROM ReaderAssigned AS RA ");
            sb.Append("LEFT JOIN TbAccessPlan TA ON TA.BuildingPlanID = RA.BuildingPlanID ");
            sb.Append(" ");
            sb.Append("RIGHT  JOIN( ");
            sb.Append("select AccessCalendarID, DateTime, ProfileID FROM View_AccessCalendarProfiles Where DateTime BETWEEN DATEADD(d, -1, GETDATE()) AND DATEADD(d, 7, GETDATE()) ");
            sb.Append(") ACP ON ACP.AccessCalendarID = TA.AccessCalendarId ");
            sb.Append(" ");
            sb.Append("RIGHT JOIN( ");
            sb.Append("select AccessProfID, Level from[ZuttritProfilesTimeFrames] WHERE [ZuttritProfilesTimeFrames].ProfilAktiv = 1 ");
            sb.Append(") ZPT ON ZPT.AccessProfID = ACP.ProfileID ");
            sb.Append(" ");
            sb.Append("INNER JOIN ZuttritProfiles ZP ON ZP.ID = ACP.ProfileID ");
            sb.Append("WHERE TerminalID = (SELECT ID FROM TerminalConfig WHERE Description = @desc) ");
            sb.Append("AND RA.Assigned = 1 ");
            sb.Append(") t");

            return dbConnection.DBDataTable(sb.ToString());
        }

        //Action - List
        public static DataTable GetActionList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            StringBuilder sb = new StringBuilder();
            #region 1. Model
            //sb.Append("	 SELECT 	");
            //sb.Append(" LT.LeserID AS ID 	");
            //sb.Append(" ,TT.TermDID * 100 + + LT.LeserID AS RefReader 	");
            //sb.Append(" ,1 AS PortOut 	");
            //sb.Append(" , LT.Opentime * 5 AS Elapse 	");
            //sb.Append(" ,0 AS RefTime 	");
            //sb.Append(" FROM lesertab AS LT 	");
            //sb.Append(" INNER JOIN TERMINALTAB AS TT 	");
            //sb.Append(" ON LT.TermId = TT.TERMDID 	");
            //sb.Append(" AND TT.TERMBEZEICHNUNG = 'zkBox'	");
            #endregion
            //sb.Append(" /* ORDER BY LT.LeserID */ 	");
            #region Uwes erstes Konzept
            //sb.Append(" /* Daueröffnung der Türen*/ 	");
            //sb.Append(" UNION SELECT DISTINCT 	");
            //sb.Append("ID AS ID 	");
            //sb.Append(" , TT.TermDID * 100 + LT.LeserID AS RefReader 	");
            //sb.Append("	 , 1 AS PortOut 	");
            //sb.Append(" -- , LT.LeserID AS PortOut 	");
            //sb.Append(" , LT.Opentime * 5 AS Elapse 	");
            //sb.Append(" , ID AS RefTime 	");
            //sb.Append("FROM view_Datafox_Timelist AS TL 	");
            //sb.Append(" ,lesertab AS LT 	");
            //sb.Append(" INNER JOIN TERMINALTAB AS TT 	");
            //sb.Append(" ON LT.TermId = TT.TERMDID 	");

            //sb.Append(" INNER JOIN ( 	");
            //sb.Append(" SELECT TERMBEZEICHNUNG,1 AS LESERID,SUBSTRING(PARAM, 1,1) AS ZP FROM terminaltab 	");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,2 AS LESERID,SUBSTRING(PARAM, 3,1) AS ZP FROM terminaltab 	");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,3 AS LESERID,SUBSTRING(PARAM, 5,1) AS ZP FROM terminaltab 	");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,4 AS LESERID,SUBSTRING(PARAM, 7,1) AS ZP FROM terminaltab 	");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,5 AS LESERID,SUBSTRING(PARAM, 9,1) AS ZP FROM terminaltab 	");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,6 AS LESERID,SUBSTRING(PARAM,11,1) AS ZP FROM terminaltab 	");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,7 AS LESERID,SUBSTRING(PARAM,13,1) AS ZP FROM terminaltab 	");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,8 AS LESERID,SUBSTRING(PARAM,15,1) AS ZP FROM terminaltab 	");
            //sb.Append(" ) AS DAUER 	");
            //sb.Append(" ON DAUER.TERMBEZEICHNUNG = TT.TERMBEZEICHNUNG 	");
            //sb.Append(" AND DAUER.LESERID = LT.LeserID 	");
            //sb.Append("		");
            //sb.Append(" WHERE 1=1 	");
            //sb.Append(" AND Substring(STR(TL.ID,4,0),3,1)=DAUER.ZP 	");
            //sb.Append(" AND TT.TERMBEZEICHNUNG = 'zkBox'	");


            //sb.Append(" /* Daueröffnung der Türen aus Zutrittskalender für heute und die nächsten 6 Tage, wenn die Bezeichnung mit Sternchen (*) beginnt*/ 	");
            //sb.Append(" UNION SELECT DISTINCT 	");
            //sb.Append(" ID AS ID 	");
            //sb.Append(" , TT.TermDID * 100 + LT.LeserID AS RefReader 	");
            //sb.Append(" , LT.LeserID AS PortOut 	");
            //sb.Append(" , 0 AS Elapse 	");
            //sb.Append(" , ID AS RefTime 	");
            //sb.Append("FROM view_Datafox_Timelist AS TL 	");
            //sb.Append(" , [view_Kalender_Zutritt_per_day] AS KAL 	");
            //sb.Append(" INNER JOIN TerminalGruppen AS TG 	");
            //sb.Append(" ON TG.TermGrpID = KAL.TG 	");
            //sb.Append(" INNER JOIN TerminalZuordnung AS TZ	");
            //sb.Append(" ON TZ.TermGrpID = KAL.TG 	");
            //sb.Append(" INNER JOIN lesertab AS LT 	");
            //sb.Append(" ON LT.LeserID = TZ.LeserId 	");
            //sb.Append(" INNER JOIN TERMINALTAB AS TT 	");
            //sb.Append(" ON LT.TermId = TT.TERMDID 	");
            //sb.Append("		");
            //sb.Append(" WHERE 1=1 	");
            //sb.Append(" AND KAL.Pers_Nr < 0 	");
            //sb.Append(" AND KAL.TG > '' 	");
            //sb.Append(" AND ISDATE(KAL.Datum)=1 	");
            //sb.Append(" AND DATEDIFF(d,GETDATE(),KAL.DATUM) BETWEEN 0 AND 6 	");
            //sb.Append(" AND LEFT(TG.TermGrpBez,1)='*' 	");
            //sb.Append(" AND Substring(STR(TL.ID,4,0),3,1)=TZ.ZLProfil 	");
            //sb.Append(" AND TT.TERMBEZEICHNUNG = 'zkBox' 	");
            #endregion

            #region A.Seibel commented out: 2015-08-20
            //sb.Append("	DECLARE @bez VARCHAR(500);	");
            //sb.Append("	SET @bez = '" + terminalName + "';	");
            //sb.Append("	 	");
            //sb.Append("SELECT LT.LeserID AS ID 	");
            //sb.Append(" ,TT.TermDID * 100 + + LT.LeserID AS RefReader 	");
            //sb.Append(" ,1 AS PortOut 	");
            //sb.Append(" , LT.Opentime * 5 AS Elapse 	");
            //sb.Append(" ,0 AS RefTime 	");
            //sb.Append(" FROM lesertab AS LT 	");
            //sb.Append(" INNER JOIN TERMINALTAB AS TT 	");
            //sb.Append(" ON LT.TermId = TT.TERMDID 	");
            //sb.Append(" AND TT.TERMBEZEICHNUNG = @bez	");

            ///* Daueröffnung der Türen*/
            //sb.Append(" UNION SELECT DISTINCT 	");
            //sb.Append("ID AS ID 	");
            //sb.Append(" , TT.TermDID * 100 + LT.LeserID AS RefReader 	");
            //sb.Append("	 , 1 AS PortOut 	");
            //// sb.Append(" -- , LT.LeserID AS PortOut 	");
            //sb.Append(" , LT.Opentime * 5 AS Elapse 	");
            //sb.Append(" , ID AS RefTime 	");
            //sb.Append("FROM view_Datafox_Timelist AS TL 	");
            //sb.Append(" ,lesertab AS LT 	");
            //sb.Append(" INNER JOIN TERMINALTAB AS TT 	");
            //sb.Append(" ON LT.TermId = TT.TERMDID 	");

            //sb.Append(" INNER JOIN ( 	");
            //sb.Append(" SELECT TERMBEZEICHNUNG,1 AS LESERID,SUBSTRING(PARAM, 1,1) AS ZP FROM terminaltab 	");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,2 AS LESERID,SUBSTRING(PARAM, 3,1) AS ZP FROM terminaltab ");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,3 AS LESERID,SUBSTRING(PARAM, 5,1) AS ZP FROM terminaltab ");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,4 AS LESERID,SUBSTRING(PARAM, 7,1) AS ZP FROM terminaltab ");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,5 AS LESERID,SUBSTRING(PARAM, 9,1) AS ZP FROM terminaltab ");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,6 AS LESERID,SUBSTRING(PARAM,11,1) AS ZP FROM terminaltab ");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,7 AS LESERID,SUBSTRING(PARAM,13,1) AS ZP FROM terminaltab ");
            //sb.Append(" UNION SELECT TERMBEZEICHNUNG,8 AS LESERID,SUBSTRING(PARAM,15,1) AS ZP FROM terminaltab ");
            //sb.Append(" ) AS DAUER 	");
            //sb.Append(" ON DAUER.TERMBEZEICHNUNG = TT.TERMBEZEICHNUNG 	");
            //sb.Append(" AND DAUER.LESERID = LT.LeserID 	");

            //sb.Append(" WHERE 1=1 	");
            //sb.Append(" AND Substring(STR(TL.ID,4,0),3,1)=DAUER.ZP 	");
            //sb.Append(" AND TT.TERMBEZEICHNUNG = @bez	");


            ///* Daueröffnung der Türen aus Zutrittskalender für heute und die nächsten 6 Tage, wenn die Bezeichnung mit Sternchen (*) beginnt*/
            //sb.Append(" UNION SELECT DISTINCT 	");
            //sb.Append(" ID AS ID 	");
            //sb.Append(" , TT.TermDID * 100 + LT.LeserID AS RefReader 	");
            //sb.Append(" , LT.LeserID AS PortOut 	");
            //sb.Append(" , 0 AS Elapse 	");
            //sb.Append(" , ID AS RefTime 	");
            //sb.Append("FROM view_Datafox_Timelist AS TL 	");
            //sb.Append(" , [view_Kalender_Zutritt_per_day] AS KAL 	");
            //sb.Append(" INNER JOIN TerminalGruppen AS TG 	");
            //sb.Append(" ON TG.TermGrpID = KAL.TG 	");
            //sb.Append(" INNER JOIN TerminalZuordnung AS TZ	");
            //sb.Append(" ON TZ.TermGrpID = KAL.TG 	");
            //sb.Append(" INNER JOIN lesertab AS LT 	");
            //sb.Append(" ON LT.LeserID = TZ.LeserId 	");
            //sb.Append(" INNER JOIN TERMINALTAB AS TT 	");
            //sb.Append(" ON LT.TermId = TT.TERMDID 	");

            //sb.Append(" WHERE 1=1 	");
            //sb.Append(" AND KAL.Pers_Nr < 0 	");
            //sb.Append(" AND KAL.TG > '' 	");
            //sb.Append(" AND ISDATE(KAL.Datum)=1 	");
            //sb.Append(" AND DATEDIFF(d,GETDATE(),KAL.DATUM) BETWEEN 0 AND 6 	");
            //sb.Append(" AND LEFT(TG.TermGrpBez,1)='*' 	");
            //sb.Append(" AND Substring(STR(TL.ID,4,0),3,1)=TZ.ZLProfil 	");
            //sb.Append(" AND TT.TERMBEZEICHNUNG = @bez 	");
            #endregion
            sb.Append("DECLARE @desc VARCHAR(300) = '" + terminalName + "'; ");
            sb.Append(" ");
            sb.Append("SELECT   ");
            sb.Append("DISTINCT ");
            sb.Append("ROW_NUMBER() Over(Order by @@rowcount ASC)  AS ID, ");
            sb.Append("(SELECT TermID FROM TerminalConfig WHERE ID = ReaderAssigned.TerminalID) * 100 + ROW_NUMBER() Over(Order by @@rowcount ASC) AS RefReader, ");
            sb.Append("1 AS PortOut, ");
            sb.Append("ISNULL((SELECT ISNULL(RelayTime, 0) FROM TerminalReaders WHERE TermID = ReaderAssigned.TerminalID AND ID = ReaderAssigned.ReaderID), 0) *5 AS Elapse, ");
            sb.Append("ISNULL(STC.RefTime + 7000,0) AS RefTime ");
            sb.Append("from ReaderAssigned ");
            sb.Append("LEFT JOIN SwitchPlan AS SP ON SP.BuidingPlanID = ReaderAssigned.BuildingPlanID ");
            sb.Append(" ");
            sb.Append("LEFT JOIN ( ");
            sb.Append("select DISTINCT ");
            sb.Append("SwitchCalendarID, ");
            sb.Append("CalendarDate, ");
            sb.Append("ProfileId, ");
            sb.Append("ST.RefTime ");
            sb.Append("from View_SwitchTimesCalendarProfiles ");
            sb.Append("RIGHT JOIN( ");
            sb.Append("select ");
            sb.Append("1000 + Schaltprofil * 10 + Level AS RefTime ");
            sb.Append(", SchaltprofilID ");
            sb.Append("from View_SwitchTimes ");
            sb.Append(") ST ON ST.SchaltprofilID = ProfileId ");
            sb.Append("Where CalendarDate BETWEEN DATEADD(d,-1, GETDATE()) AND DATEADD(d,7, GETDATE())  ");
            sb.Append(") STC ON STC.SwitchCalendarID = SP.SwitchCalendarId ");
            sb.Append("WHERE TerminalID = (SELECT ID FROM TerminalConfig WHERE Description = @desc)  ");



            return dbConnection.DBDataTable(sb.ToString());
        }

        //Event - List
        public static DataTable GetEventList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            var sb = new StringBuilder();

            sb.Append("DECLARE @desc VarChar(300);  ");
            sb.Append("SET @desc = '" + terminalName + "';  ");
            sb.Append("  ");
            sb.Append("SELECT DISTINCT  ");
            sb.Append("TR.TermID AS RefReader  ");
            sb.Append(",1 AS PortIn  ");
            sb.Append(", TR.TermID AS RefAction  ");
            sb.Append(",0 AS RefTime  ");
            sb.Append("FROM TerminalReaders AS  TR   ");
            sb.Append("INNER JOIN TerminalConfig AS TB   ");
            sb.Append("ON TR.TermId  = TB.ID    ");
            sb.Append("WHERE TB.Description = @desc  ");

            return dbConnection.DBDataTable(sb.ToString());
        }
        //Time - List
        public static DataTable GetTimeList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            var sb = new StringBuilder();
            #region A.Seibel commented out: 2015-08-20
            //sb.Append("select   ");
            //sb.Append("1000 + (AccessProfID - 20000) * 10 + Level AS ID  ");
            //sb.Append(",1 AS Weekdays  ");
            //sb.Append(",CONVERT(VARCHAR(5), MonFrom, 114) as TimeFrom  ");
            //sb.Append(",CONVERT(VARCHAR(5), MonTo, 114) as TimeTo --, AccessProfID  ");
            //sb.Append("  ");
            //sb.Append("from  [ZuttritProfilesTimeFrames]  ");
            //sb.Append("Where ProfilAktiv = 1  ");
            //sb.Append("  ");
            //sb.Append("UNION ALL  ");
            //sb.Append("  ");
            //sb.Append("select   ");
            //sb.Append("1000 + (AccessProfID - 20000) * 10 + Level AS ID  ");
            //sb.Append(",2 AS Weekdays  ");
            //sb.Append(",CONVERT(VARCHAR(5), TueFrom, 114) as TimeFrom  ");
            //sb.Append(",CONVERT(VARCHAR(5), TueTo, 114) as TimeTo --, AccessProfID  ");
            //sb.Append("  ");
            //sb.Append("from  [ZuttritProfilesTimeFrames]  ");
            //sb.Append("Where ProfilAktiv = 1  ");
            //sb.Append("  ");
            //sb.Append("UNION ALL  ");
            //sb.Append("  ");
            //sb.Append("select   ");
            //sb.Append("1000 + (AccessProfID - 20000) * 10 + Level AS ID  ");
            //sb.Append(",3 AS Weekdays  ");
            //sb.Append(",CONVERT(VARCHAR(5), WedFrom, 114) as TimeFrom  ");
            //sb.Append(",CONVERT(VARCHAR(5), WedTo, 114) as TimeTo --, AccessProfID  ");
            //sb.Append("  ");
            //sb.Append("from  [ZuttritProfilesTimeFrames]  ");
            //sb.Append("Where ProfilAktiv = 1  ");
            //sb.Append("  ");
            //sb.Append("UNION ALL  ");
            //sb.Append("  ");
            //sb.Append("select   ");
            //sb.Append("1000 + (AccessProfID - 20000) * 10 + Level AS ID  ");
            //sb.Append(",4 AS Weekdays  ");
            //sb.Append(",CONVERT(VARCHAR(5), ThurFrom, 114) as TimeFrom  ");
            //sb.Append(",CONVERT(VARCHAR(5), ThurTo, 114) as TimeTo --, AccessProfID  ");
            //sb.Append("  ");
            //sb.Append("from  [ZuttritProfilesTimeFrames]  ");
            //sb.Append("Where ProfilAktiv = 1  ");
            //sb.Append("  ");
            //sb.Append("UNION ALL  ");
            //sb.Append("  ");
            //sb.Append("select   ");
            //sb.Append("1000 + (AccessProfID - 20000) * 10 + Level AS ID  ");
            //sb.Append(",5 AS Weekdays  ");
            //sb.Append(",CONVERT(VARCHAR(5), FriFrom, 114) as TimeFrom  ");
            //sb.Append(",CONVERT(VARCHAR(5), FriTo, 114) as TimeTo --, AccessProfID  ");
            //sb.Append("  ");
            //sb.Append("from  [ZuttritProfilesTimeFrames]  ");
            //sb.Append("Where ProfilAktiv = 1  ");
            //sb.Append("  ");
            //sb.Append("UNION ALL  ");
            //sb.Append("  ");
            //sb.Append("select   ");
            //sb.Append("1000 + (AccessProfID - 20000) * 10 + Level AS ID  ");
            //sb.Append(",6 AS Weekdays  ");
            //sb.Append(",CONVERT(VARCHAR(5), SatFrom, 114) as TimeFrom  ");
            //sb.Append(",CONVERT(VARCHAR(5), SatTo, 114) as TimeTo --, AccessProfID  ");
            //sb.Append("from  [ZuttritProfilesTimeFrames]  ");
            //sb.Append("Where ProfilAktiv = 1  ");
            //sb.Append("  ");
            //sb.Append("UNION ALL  ");
            //sb.Append("  ");
            //sb.Append("select   ");
            //sb.Append("1000 + (AccessProfID - 20000) * 10 + Level AS ID  ");
            //sb.Append(",7 AS Weekdays  ");
            //sb.Append(",CONVERT(VARCHAR(5), SunFrom, 114) as TimeFrom  ");
            //sb.Append(",CONVERT(VARCHAR(5), SunTo, 114) as TimeTo --, AccessProfID  ");
            //sb.Append("from  [ZuttritProfilesTimeFrames]  ");
            //sb.Append("Where ProfilAktiv = 1  ");
            //sb.Append("  ");
            //sb.Append("--ORDER BY ID,Weekdays  ");
            //sb.Append("  ");
            //sb.Append("UNION ALL  ");
            //sb.Append("  ");
            //sb.Append("--CREATE VIEW DF_TimeList_SwitchTimes AS  ");
            //sb.Append("select 6600 + (SwitchProfiles.Profile_Nr - 2000)  AS ID, --ProfileID, SwitchProfiles.Profile_Nr,  ");
            //sb.Append("SUBSTRING('123456789', CONVERT(int, LEFT(DayFrom,1)), CONVERT(int, RIGHT(DayTo, 1)) - convert(int, LEFT(DayFrom,1))  + 1) Weekdays,  ");
            //sb.Append("CONVERT(VARCHAR(5), TimeFrom, 114) AS TimeFrom,  ");
            //sb.Append("CONVERT(VARCHAR(5), TimeTo, 114)  AS TimeTo  ");
            //sb.Append("from SwitchProfilePairs   ");
            //sb.Append("  ");
            //sb.Append("INNER JOIN SwitchProfiles On SwitchProfilePairs.ProfileID = SwitchProfiles.ID  ");
            //sb.Append("  ");
            //sb.Append("where DayFrom <= DayTo  ");
            #endregion
            sb.Append("DECLARE @desc VARCHAR(300) = '" + terminalName + "'; ");
            sb.Append(" ");
            sb.Append("select ");
            sb.Append("CONVERT(INT,8000 + SP.Profile_Nr * 10 + ROW_NUMBER() Over (Order by @@rowcount ASC )) AS ID, ");
            sb.Append("CONVERT(INT,SUBSTRING('123456789', CONVERT(int, LEFT(DayFrom,1)), CONVERT(int, RIGHT(DayTo, 1)) - convert(int, LEFT(DayFrom,1))  + 1)) Weekdays, ");
            sb.Append("CONVERT(VARCHAR(5), TimeFrom, 114) AS TimeStart, ");
            sb.Append("CONVERT(VARCHAR(5), TimeTo, 114)  AS TimeEnd ");
            sb.Append("from SwitchProfilePairs ");
            sb.Append("INNER JOIN SwitchProfiles AS SP ON SP.ID = SwitchProfilePairs.ProfileID ");
            sb.Append("where  ");
            sb.Append("DayFrom <= DayTo ");
            sb.Append("AND TimeFrom <= TimeTo ");
            sb.Append(" ");
            sb.Append("AND SP.Profile_Nr = ( ");
            sb.Append(" ");
            sb.Append("select  ");
            sb.Append("DISTINCT ");
            sb.Append("STC.Switch_Profile AS Switch_Profile ");
            sb.Append(" ");
            sb.Append("from ReaderAssigned  ");
            sb.Append("LEFT JOIN SwitchPlan AS SP ON SP.BuidingPlanID =  ReaderAssigned.BuildingPlanID ");
            sb.Append(" ");
            sb.Append("RIGHT JOIN ( ");
            sb.Append("select DISTINCT ");
            sb.Append("SwitchCalendarID,  ");
            sb.Append("CalendarDate, ");
            sb.Append("ProfileId, ");
            sb.Append("ST.Switch_Profile  ");
            sb.Append("from  View_SwitchTimesCalendarProfiles  ");
            sb.Append("RIGHT JOIN ( ");
            sb.Append("select  ");
            sb.Append("Schaltprofil AS Switch_Profile ");
            sb.Append(",SchaltprofilID ");
            sb.Append("from View_SwitchTimes ");
            sb.Append(") ST ON ST.SchaltprofilID = ProfileId ");
            sb.Append(" ");
            sb.Append("Where CalendarDate BETWEEN DATEADD(d,-1,GETDATE()) AND DATEADD(d,7,GETDATE()) ");
            sb.Append(") STC ON STC.SwitchCalendarID = SP.SwitchCalendarId ");
            sb.Append("WHERE TerminalID = (SELECT ID FROM TerminalConfig WHERE Description = @desc) ");
            sb.Append(") ");
            sb.Append(" ");
            sb.Append("UNION ALL ");
            sb.Append(" ");
            sb.Append("select  ");
            sb.Append("1000 + ZP.AccessProfileNo * 10 + Level AS ID ");
            sb.Append(",1 AS Weekdays ");
            sb.Append(",CONVERT(VARCHAR(5), MonFrom, 114) as TimeStart ");
            sb.Append(",CONVERT(VARCHAR(5), MonTo, 114) as TimeEnd ");
            sb.Append("from  [ZuttritProfilesTimeFrames] ");
            sb.Append("INNER JOIN ZuttritProfiles AS ZP ");
            sb.Append("ON [ZuttritProfilesTimeFrames].AccessProfID = ZP.ID ");
            sb.Append("Where ProfilAktiv = 1 ");
            sb.Append(" ");
            sb.Append("UNION ALL ");
            sb.Append(" ");
            sb.Append("select  ");
            sb.Append("1000 + ZP.AccessProfileNo * 10 + Level AS ID ");
            sb.Append(",2 AS Weekdays ");
            sb.Append(",CONVERT(VARCHAR(5), TueFrom, 114) as TimeStart ");
            sb.Append(",CONVERT(VARCHAR(5), TueTo, 114) as TimeEnd ");
            sb.Append("from  [ZuttritProfilesTimeFrames] ");
            sb.Append("INNER JOIN ZuttritProfiles AS ZP ");
            sb.Append("ON [ZuttritProfilesTimeFrames].AccessProfID = ZP.ID ");
            sb.Append("Where ProfilAktiv = 1 ");
            sb.Append(" ");
            sb.Append("UNION ALL ");
            sb.Append(" ");
            sb.Append("select  ");
            sb.Append("1000 + ZP.AccessProfileNo * 10 + Level AS ID ");
            sb.Append(",3 AS Weekdays ");
            sb.Append(",CONVERT(VARCHAR(5), WedFrom, 114) as TimeStart ");
            sb.Append(",CONVERT(VARCHAR(5), WedTo, 114) as TimeEnd ");
            sb.Append("from  [ZuttritProfilesTimeFrames] ");
            sb.Append("INNER JOIN ZuttritProfiles AS ZP ");
            sb.Append("ON [ZuttritProfilesTimeFrames].AccessProfID = ZP.ID ");
            sb.Append("Where ProfilAktiv = 1 ");
            sb.Append(" ");
            sb.Append("UNION ALL ");
            sb.Append(" ");
            sb.Append("select  ");
            sb.Append("1000 + ZP.AccessProfileNo * 10 + Level AS ID ");
            sb.Append(",4 AS Weekdays ");
            sb.Append(",CONVERT(VARCHAR(5), ThurFrom, 114) as TimeStart ");
            sb.Append(",CONVERT(VARCHAR(5), ThurTo, 114) as TimeEnd ");
            sb.Append("from  [ZuttritProfilesTimeFrames] ");
            sb.Append("INNER JOIN ZuttritProfiles AS ZP ");
            sb.Append("ON [ZuttritProfilesTimeFrames].AccessProfID = ZP.ID ");
            sb.Append("Where ProfilAktiv = 1 ");
            sb.Append(" ");
            sb.Append("UNION ALL ");
            sb.Append(" ");
            sb.Append("select  ");
            sb.Append("1000 + ZP.AccessProfileNo * 10 + Level AS ID ");
            sb.Append(",5 AS Weekdays ");
            sb.Append(",CONVERT(VARCHAR(5), FriFrom, 114) as TimeStart ");
            sb.Append(",CONVERT(VARCHAR(5), FriTo, 114) as TimeEnd ");
            sb.Append("from  [ZuttritProfilesTimeFrames] ");
            sb.Append("INNER JOIN ZuttritProfiles AS ZP ");
            sb.Append("ON [ZuttritProfilesTimeFrames].AccessProfID = ZP.ID ");
            sb.Append("Where ProfilAktiv = 1 ");
            sb.Append(" ");
            sb.Append("UNION ALL ");
            sb.Append(" ");
            sb.Append("select  ");
            sb.Append("1000 + ZP.AccessProfileNo * 10 + Level AS ID ");
            sb.Append(",6 AS Weekdays ");
            sb.Append(",CONVERT(VARCHAR(5), SatFrom, 114) as TimeStart ");
            sb.Append(",CONVERT(VARCHAR(5), SatTo, 114) as TimeEnd ");
            sb.Append("from  [ZuttritProfilesTimeFrames] ");
            sb.Append("INNER JOIN ZuttritProfiles AS ZP ");
            sb.Append("ON [ZuttritProfilesTimeFrames].AccessProfID = ZP.ID ");
            sb.Append("Where ProfilAktiv = 1 ");
            sb.Append(" ");
            sb.Append("UNION ALL ");
            sb.Append(" ");
            sb.Append("select  ");
            sb.Append("1000 + ZP.AccessProfileNo * 10 + Level AS ID ");
            sb.Append(",7 AS Weekdays ");
            sb.Append(",CONVERT(VARCHAR(5), SunFrom, 114) as TimeStart ");
            sb.Append(",CONVERT(VARCHAR(5), SunTo, 114) as TimeEnd ");
            sb.Append("from  [ZuttritProfilesTimeFrames] ");
            sb.Append("INNER JOIN ZuttritProfiles AS ZP ");
            sb.Append("ON [ZuttritProfilesTimeFrames].AccessProfID = ZP.ID ");
            sb.Append("Where ProfilAktiv = 1 ");
            sb.Append(" ");
            sb.Append("ORDER BY ID,Weekdays ");

            return dbConnection.DBDataTable(sb.ToString());
        }


        //BDE - Aufträge
        public static DataTable GetAuftraegeList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            var sb = new StringBuilder();
            sb.Append("select A_Bez as A_Beschreibung, REPLACE(STR(A_Nr,8,0),' ','0') as A_Id From Auftrag WHERE GETDATE() BETWEEN A_StartDat AND ISNULL(A_EndDat,'99991231') AND A_Bez > '' Order by 1 asc");
            //ohne 0 en davor
            //sb.Append("select A_Bez as A_Beschreibung, A_Nr as A_Id From Auftrag WHERE GETDATE() BETWEEN A_StartDat AND ISNULL(A_EndDat,'99991231') Order by 2");
            //A.Seibel 2015-01-28: The Terminal accept only 25 Chars. If we want to cut them up, we have to use the follow query 
            //sb.Append("select LEFT(A_Bez, 25 ) as A_Beschreibung, A_Nr as A_Id From Auftrag Order by 2");
            return dbConnection.DBDataTable(sb.ToString());
        }

        //BDE - Tätigkeiten
        public static DataTable GetTaetigkeitenList(ttxTools.ClsDB dbConnection, string terminalName, int Version = 0)
        {
            var sb = new StringBuilder();

            // sb.Append("select T_Bez as T_Beschreibung, REPLACE(STR(T_Nr ,8,0),' ','0') as T_Id from Taetigkeiten Order by 2");
            //sb.Append("select T_Bez as T_Beschreibung, REPLACE(STR(T_Nr ,8,0),' ','0') as T_Id,'12345678' AS A_Id from Taetigkeiten Order by 2");

            if (Version == 0)
            {
                sb.Append("select T_Bez as T_Beschreibung, REPLACE(STR(T_Nr ,8,0),' ','0') as T_Id from Taetigkeiten Order by 2");
            }

            else
            {
                sb.Append("SELECT T_Bez as T_Beschreibung, REPLACE(STR(T_Nr ,8,0),' ','0') as T_Id,REPLACE(STR(A_Nr,8,0),' ','0') AS A_ID");
                sb.Append(" FROM Auftrag AS A");
                sb.Append(" INNER JOIN AuftragPositionen AS P");
                sb.Append("ON A.A_Nr = P.PosANr");
                sb.Append(" AND P.PosTyp = 'T'");
                sb.Append(" LEFT JOIN Taetigkeiten AS T");
                sb.Append("ON T.T_Nr = P.PosNr");
                sb.Append(" WHERE 1=1 ");
                sb.Append(" AND GETDATE() BETWEEN A.A_StartDat AND ISNULL(A.A_EndDat,'99991231') AND A_Bez > ''");
                sb.Append(" AND T.T_Bez >''");
                sb.Append(" ORDER BY 2");
            }

            return dbConnection.DBDataTable(sb.ToString());
        }

        //Fingerprints
        public static DataTable GetFingerprints(ttxTools.ClsDB dbConnection)
        {
            // Uwe Jacob, 2015-03-30: Only use fingerprints from datafox-terminals
            // return dbConnection.DBDataTable("select PersNr As PID, FIndex As FID, Template from FingerPrints order by 1");
            return dbConnection.DBDataTable("select FP.PersNr As PID, FP.FIndex As FID, FP.Template from FingerPrints AS FP INNER JOIN Terminaltypen AS TT ON FP.TermTyp = TT.TERMTYP WHERE TT.HerstellerID = 5 ORDER by 1 ");
        }


        // Uwe Jacob, 2015-04-17, new functionality: cost-center
        //Kostenstellen
        // an das Gerät zu übertragende Nummern werden aus Funktionstaste 5 ermittelt
        public static DataTable GetKostenstellenList(ttxTools.ClsDB dbConnection, string terminalName)
        {
            var where_aus_funktionstasten = dbConnection.DBLookUP("SELECT KosSelection FROM FuTaGruppen AS F INNER JOIN TERMINALTAB AS T ON F.TaGrpId = T.TERMDID WHERE T.TERMBEZEICHNUNG = '" + terminalName + "' AND F.TaId= 5");

            var sb = new StringBuilder();
            sb.Append("SELECT Kos_Nr,Kos_Bezeichnung FROM [Kostenstellen] ");
            if (String.IsNullOrEmpty(where_aus_funktionstasten) == false)
            {
                sb.Append(" WHERE Kos_Nr IN (" + where_aus_funktionstasten + ")");
            }
            sb.Append(" ORDER BY Kos_Bezeichnung");
            return dbConnection.DBDataTable(sb.ToString());
        }

        //Flexkostenstellen
        // an das Gerät zu übertragende Nummern werden aus Funktionstaste 6 ermittelt
        public static DataTable GetFlexkostenstellenList(ttxTools.ClsDB dbConnection, string terminalName)
        {

            var where_aus_funktionstasten = dbConnection.DBLookUP("SELECT KosSelection FROM FuTaGruppen AS F INNER JOIN TERMINALTAB AS T ON F.TaGrpId = T.TERMDID WHERE T.TERMBEZEICHNUNG = '" + terminalName + "' AND F.TaId= 6");

            var sb = new StringBuilder();
            sb.Append("SELECT Kos_Nr,Kos_Bezeichnung FROM [Flexkostenstellen] ");
            if (String.IsNullOrEmpty(where_aus_funktionstasten) == false)
            {
                sb.Append(" WHERE Kos_Nr IN (" + where_aus_funktionstasten + ")");
            }
            sb.Append(" ORDER BY Kos_Bezeichnung");
            return dbConnection.DBDataTable(sb.ToString());
        }
    }
}
