using System;
using System.Collections.Generic;
using System.Linq;
using TermConfig_NewMask.Dtos;
using System.Data.SqlClient;
using KruAll.Core.Models;
using TermConfig_NewMask.TerminalCommunication.VirtualDataObjects;
using System.Text;
using System.Data.Entity.Core.EntityClient;

namespace TermConfig_NewMask.TerminalCommunication
{
    public class VirtualTerminalData
    {
        private static SqlConnection _sqlConnection = null;
        private static SqlConnection _branchServerConnection = null;

        KruAll.Core.Models.PZE_Entities branchserverDb = new PZE_Entities();
        //Kruall.Core.Models.  branchserverDb = new Kruall.Core.Models.BranchServer();

        private const int STATUS_FUNCTION_KEY_TYPE = 1;
        private const int PROJECT_FUNCTION_KEY_TYPE = 2;
        private const int COST_CENTER_FUNCTION_KEY_TYPE = 3;

        public List<VirtualPersonal> GetListOfAllPersonal()
        {
            List<VirtualPersonal> employees = new List<VirtualPersonal>();
            SqlDataReader recordTable = null;

            string pesonalSQL = "SELECT Pers_Nr, Pers_Name1 + ' ' + Pers_Name2 AS Name, Pers_Ausweis_Nr AS CardNumber,"
                              + "ISNULL(Werke.W_Bezeichnung, '') AS LocationName, "
                              + "       ISNULL(Abteilungen.Abt_Bezeichnung, '') AS DepartmentName, "
                              + "       ISNULL(Kostenstellen.Kos_Bezeichnung, '') AS CostCenterName "
                              + "FROM Personalstamm "
                              + "LEFT JOIN Werke ON Werke.W_Nr = Pers_Werk "
                              + "LEFT JOIN Abteilungen ON Abteilungen.Abt_Nr = Pers_Abteilung "
                              + "LEFt JOIN Kostenstellen ON Kostenstellen.Kos_Nr = Pers_Kostenstelle";

            recordTable = ExecuteReaderQuery(pesonalSQL);

            while (recordTable.Read())
            {
                VirtualPersonal user = new VirtualPersonal();
                user.PersonalNumber = Convert.ToInt64(recordTable["Pers_Nr"]);
                user.PersonalName = recordTable["Name"].ToString();
                user.Location = recordTable["LocationName"].ToString();
                user.Department = recordTable["DepartmentName"].ToString();
                user.CostCenter = recordTable["CostCenterName"].ToString();
                user.CardNumber = Convert.ToInt64(recordTable["CardNumber"]);

                employees.Add(user);
            }

            recordTable.Close();
            recordTable = null;

            return employees;
        }

        public List<VirtualLocation> GetAllLocations()
        {
            List<VirtualLocation> allLocations = new List<VirtualLocation>();
            VirtualLocation VirtualLocation = null;

            SqlDataReader recordTable = null;

            string pesonalSQL = "SELECT W_Nr, W_Bezeichnung FROM Werke";

            recordTable = ExecuteReaderQuery(pesonalSQL);

            while (recordTable.Read())
            {
                VirtualLocation = new VirtualLocation();
                VirtualLocation.ID = Convert.ToInt64(recordTable["W_Nr"]);
                VirtualLocation.Description = Convert.ToString(recordTable["W_Bezeichnung"]);
                allLocations.Add(VirtualLocation);
            }
            recordTable.Close();
            recordTable = null;

            return allLocations;
        }

        public List<VirtualDepartment> GetAllDepartments()
        {
            List<VirtualDepartment> allDepartments = new List<VirtualDepartment>();
            VirtualDepartment VirtualDepartment = null;

            SqlDataReader recordTable = null;

            string pesonalSQL = "SELECT Abt_Nr, Abt_Bezeichnung FROM Abteilungen";

            recordTable = ExecuteReaderQuery(pesonalSQL);

            while (recordTable.Read())
            {
                VirtualDepartment = new VirtualDepartment();
                VirtualDepartment.ID = Convert.ToInt64(recordTable["Abt_Nr"]);
                VirtualDepartment.Description = Convert.ToString(recordTable["Abt_Bezeichnung"]);
                allDepartments.Add(VirtualDepartment);
            }

            recordTable.Close();
            recordTable = null;

            return allDepartments;
        }

        //public List<PersonalFingerTemplate> GetAllFingerPrintsFromDatabase()
        //{
        //    List<PersonalFingerTemplate> personalFingerprints = new List<PersonalFingerTemplate>();

        //    foreach (FingerPrint finger in branchserverDb.FingerPrints.ToList())
        //    {
        //        PersonalFingerTemplate fingerTemplate = new PersonalFingerTemplate();
        //        fingerTemplate.PersonalNumber = Convert.ToInt64(finger.Pers_Nr);
        //        fingerTemplate.FingerIndex = Convert.ToInt64(finger.FingerIndex);
        //        fingerTemplate.Template9 = finger.Template9;
        //        fingerTemplate.Template10 = finger.Template10;
        //        UpdateOtherDetails(fingerTemplate);
        //        personalFingerprints.Add(fingerTemplate);
        //    }

        //    return personalFingerprints;

        //}

        //private void UpdateOtherDetails(PersonalFingerTemplate VirtualPersonal)
        //{
        //    string szSQL = "SELECT Pers_Nr, Pers_Name1 +' ' + Pers_Name2 AS Name, Pers_Ausweis_Nr AS CardNumber, "
        //                 + "ISNULL(Werke.W_Bezeichnung, '') AS LocationName, "
        //                 + "ISNULL(Abteilungen.Abt_Bezeichnung, '') AS DepartmentName, "
        //                 + "ISNULL(Kostenstellen.Kos_Bezeichnung, '') AS CostCenterName "
        //                 + "FROM Personalstamm "
        //                 + "LEFT JOIN Werke ON Werke.W_Nr = Pers_Werk "
        //                 + "LEFT JOIN Abteilungen ON Abteilungen.Abt_Nr = Pers_Abteilung "
        //                 + "LEFt JOIN Kostenstellen ON Kostenstellen.Kos_Nr = Pers_Kostenstelle "
        //                 + "WHERE Pers_Nr = " + VirtualPersonal.PersonalNumber + " ";

        //    SqlDataReader recordTable = null;
        //    recordTable = ExecuteReaderQuery(szSQL);

        //    while (recordTable.Read())
        //    {
        //        VirtualPersonal.FullName = Convert.ToString(recordTable["Name"]);
        //        VirtualPersonal.VirtualCostCenter = Convert.ToString(recordTable["CostCenterName"]);
        //        VirtualPersonal.VirtualDepartment = Convert.ToString(recordTable["DepartmentName"]);
        //    }

        //    recordTable.Close();
        //}

        public List<VirtualCostCenter> GetAllCostCenters()
        {
            List<VirtualCostCenter> allCostCenters = new List<VirtualCostCenter>();
            VirtualCostCenter VirtualCostCenter = null;

            SqlDataReader recordTable = null;

            string pesonalSQL = "SELECT Kos_Nr, Kos_Bezeichnung FROM Kostenstellen";

            recordTable = ExecuteReaderQuery(pesonalSQL);

            while (recordTable.Read())
            {
                VirtualCostCenter = new VirtualCostCenter();
                VirtualCostCenter.ID = Convert.ToInt64(recordTable["Kos_Nr"]);
                VirtualCostCenter.Description = Convert.ToString(recordTable["Kos_Bezeichnung"]);
                allCostCenters.Add(VirtualCostCenter);
            }

            recordTable.Close();
            recordTable = null;

            return allCostCenters;
        }

        public List<VirtualPersonal> GetListOfAllPersonalOverLocationDepartmentAndCostCenter(long locationID, long departmentID, long costcenterID)
        {
            List<VirtualPersonal> employees = new List<VirtualPersonal>();
            SqlDataReader recordTable = null;
            List<string> whereStreams = new List<string>();
            string whereANDString = "";
            string WhereFilterString = string.Empty;

            if (locationID != 0)
            {

                whereStreams.Add(" WHERE Werke.W_Nr = " + locationID);
            }
            if (departmentID != 0)
            {
                if (whereStreams.Count == 0)
                {
                    whereStreams.Add(" WHERE Abteilungen.Abt_Nr = " + departmentID);
                }
                else
                {
                    whereStreams.Add(" Abteilungen.Abt_Nr = " + departmentID);
                }

            }

            if (costcenterID != 0)
            {
                if (whereStreams.Count == 0)
                {
                    whereStreams.Add(" WHERE Kostenstellen.Kos_Nr = " + costcenterID);
                }
                else
                {
                    whereStreams.Add(" Kostenstellen.Kos_Nr = " + costcenterID);
                }


            }

            foreach (string whereStream in whereStreams)
            {
                WhereFilterString = WhereFilterString + whereANDString + whereStream;
                whereANDString = " AND ";
            }


            string pesonalSQL = "SELECT Pers_Nr, Pers_Name1 + ' ' + Pers_Name2 AS Name, Pers_Ausweis_Nr AS CardNumber,"
                              + "ISNULL(Werke.W_Bezeichnung, '') AS LocationName, "
                              + "       ISNULL(Abteilungen.Abt_Bezeichnung, '') AS DepartmentName, "
                              + "       ISNULL(Kostenstellen.Kos_Bezeichnung, '') AS CostCenterName "
                              + "FROM Personalstamm "
                              + "LEFT JOIN Werke ON Werke.W_Nr = Pers_Werk "
                              + "LEFT JOIN Abteilungen ON Abteilungen.Abt_Nr = Pers_Abteilung "
                              + "LEFt JOIN Kostenstellen ON Kostenstellen.Kos_Nr = Pers_Kostenstelle"
                              + WhereFilterString;

            recordTable = ExecuteReaderQuery(pesonalSQL);

            while (recordTable.Read())
            {
                VirtualPersonal user = new VirtualPersonal();
                user.PersonalNumber = Convert.ToInt64(recordTable["Pers_Nr"]);
                user.PersonalName = recordTable["Name"].ToString();
                user.Location = recordTable["LocationName"].ToString();
                user.Department = recordTable["DepartmentName"].ToString();
                user.CostCenter = recordTable["CostCenterName"].ToString();
                user.CardNumber = Convert.ToInt64(recordTable["CardNumber"]);

                employees.Add(user);
            }

            recordTable.Close();
            recordTable = null;

            return employees;
        }
        public static SqlDataReader ExecuteReaderQuery(string sqlStatement)
        {
            SqlCommand command = _SQLCommand(sqlStatement);
            return command.ExecuteReader();
        }

        public static void ExecuteNonReaderQuery(string sqlStatement)
        {
            SqlCommand command = _SQLCommand(sqlStatement);
            command.ExecuteNonQuery();
        }

        private static SqlCommand _SQLCommand(string SQLString)
        {
            string entityConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["KrutecPZEEntities"].ConnectionString;
            string _connectionString = new EntityConnectionStringBuilder(entityConnectionString).ProviderConnectionString;

            SqlCommand sqlCommand = null;

            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection(_connectionString);
                _sqlConnection.Open();
            }

            sqlCommand = new SqlCommand(SQLString, _sqlConnection);
            return sqlCommand;
        }

        private static SqlCommand _BranchServerSQLCommandSecond(string SQLString)
        {
            string entityConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PZE_Entities"].ConnectionString;
            string _connectionString = new EntityConnectionStringBuilder(entityConnectionString).ProviderConnectionString;

            SqlCommand sqlCommand = null;

            if (_branchServerConnection == null)
            {
                _branchServerConnection = new SqlConnection(_connectionString);
                _branchServerConnection.Open();
            }

            sqlCommand = new SqlCommand(SQLString, _branchServerConnection);
            return sqlCommand;
        }

        private static SqlCommand _BranchServerSQLCommand(string SQLString)
        {
            string entityConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["BranchServer"].ConnectionString;
            string _connectionString = new EntityConnectionStringBuilder(entityConnectionString).ProviderConnectionString;

            SqlCommand sqlCommand = null;

            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection(_connectionString);
                _sqlConnection.Open();
            }

            sqlCommand = new SqlCommand(SQLString, _sqlConnection);
            return sqlCommand;
        }

        public List<VirtualFunctionKeyItem> GetFunctionKeyItemsOverType(int functionType)
        {

            List<VirtualFunctionKeyItem> allFunctionKeyItem = new List<VirtualFunctionKeyItem>();
            SqlDataReader recordTable = null;
            string sqlFunctionKeys = string.Empty;

            switch (functionType)
            {
                case STATUS_FUNCTION_KEY_TYPE:
                    //sqlFunctionKeys = "SELECT Stat_Nr AS Number, Stat_Bezeichnung AS Name FROM Status";
                    sqlFunctionKeys = "SELECT Stat_Nr AS Number, Stat_Bezeichnung AS Name FROM Status WHERE Stat_Nr >= -2 "
                                    + "UNION ALL "
                                    + "SELECT V_Nr AS Number, V_Bezeichnung AS Name FROM ZITERM_V20_Vorlauftasten ";
                    break;
                case PROJECT_FUNCTION_KEY_TYPE:
                    sqlFunctionKeys = "SELECT Stat_Nr AS Number, Stat_Bezeichnung AS Name FROM Status WHERE Stat_Nr <= -3";
                    break;
                case COST_CENTER_FUNCTION_KEY_TYPE:
                    sqlFunctionKeys = "SELECT Kos_Nr AS Number, Kos_Bezeichnung AS Name FROM Kostenstellen";
                    break;
            }

            recordTable = ExecuteReaderQuery(sqlFunctionKeys);

            while (recordTable.Read())
            {
                allFunctionKeyItem.Add(new VirtualFunctionKeyItem
                {
                    FunctionId = Convert.ToInt32(recordTable["Number"]) + 10,
                    FunctionName = Convert.ToString(recordTable["Name"])
                });
            }

            allFunctionKeyItem.Insert(0, new VirtualFunctionKeyItem { FunctionId = 0, FunctionName = "Keine" });

            recordTable.Close();
            recordTable = null;



            return allFunctionKeyItem;
        }
        public List<VirtualFunctionKeyItem> GetFunctionKeyItems()
        {

            List<VirtualFunctionKeyItem> allFunctionKeyItem = new List<VirtualFunctionKeyItem>();

            SqlDataReader recordTable = null;

            string functionKeyItemsSQL = "SELECT V_Nr, V_Bezeichnung FROM ZITERM_V20_Vorlauftasten";

            recordTable = ExecuteReaderQuery(functionKeyItemsSQL);

            while (recordTable.Read())
            {
                allFunctionKeyItem.Add(new VirtualFunctionKeyItem
                {
                    FunctionId = Convert.ToInt32(recordTable["V_Nr"]) + 10,
                    FunctionName = Convert.ToString(recordTable["V_Bezeichnung"])
                });
            }

            recordTable.Close();
            recordTable = null;

            string statusSQL = "SELECT Stat_Nr, Stat_Bezeichnung FROM Status";

            recordTable = ExecuteReaderQuery(statusSQL);

            while (recordTable.Read())
            {
                //VirtualFunctionKeyItem = new VirtualFunctionKeyItem();
                //VirtualFunctionKeyItem.FunctionId = Convert.ToInt32(recordTable["V_Nr"]);
                //VirtualFunctionKeyItem.FunctionName = Convert.ToString(recordTable["V_Bezeichnung"]);
                allFunctionKeyItem.Add(new VirtualFunctionKeyItem
                {
                    FunctionId = Convert.ToInt32(recordTable["Stat_Nr"]) + 10,
                    FunctionName = Convert.ToString(recordTable["Stat_Bezeichnung"])
                });
            }

            recordTable.Close();
            recordTable = null;

            return allFunctionKeyItem;
        }

        public Terminal InsertTerminal(Terminal terminalToInsert)
        {
            //int savedSuccessFully = 0;
            //try
            //{
            //    branchserverDb.Terminals.Add(terminalToInsert);
            //   savedSuccessFully= branchserverDb.SaveChanges();
            //}
            //catch (Exception ex) { }
            //if (savedSuccessFully == 0)
            //    return null;
            //return terminalToInsert;
            branchserverDb.Terminals.Add(terminalToInsert);
            branchserverDb.SaveChanges();
            return terminalToInsert;
        }
        //public TerminalSetting InsertTerminalSetting(bool biometrics, bool VirtualCostCenter, bool VirtualDepartment, bool personalNumber, bool realTime, long terminalId,
        //    bool pinCode, string logOutCode)
        //{
        //    TerminalSetting terminalSettingToInsert = null;
        //    try
        //    {
        //        terminalSettingToInsert = new TerminalSetting
        //        {
        //            Biometrics = biometrics,
        //            VirtualCostCenter = VirtualCostCenter,
        //            VirtualDepartment = VirtualDepartment,
        //            PersonalNumber = personalNumber,
        //            RealTime = realTime,
        //            TerminalId = terminalId,
        //            PinCode = pinCode,
        //            LogOutCode = logOutCode
        //        };
        //        branchserverDb.TerminalSettings.Add(terminalSettingToInsert);
        //        branchserverDb.SaveChanges();
        //    }
        //    catch (Exception ex) { }
        //    return terminalSettingToInsert;
        //}
        public void InsertTerminalFunctionKeys(VirtualTerminal_DTO terminalDTO)
        {
            VirtualTerminalFunctionKey terminalFunctionKeyToInsert = null;
            try
            {
                terminalFunctionKeyToInsert = new VirtualTerminalFunctionKey
                {
                    FunctionKey1 = terminalDTO.FunctionKey1,
                    FunctionKey2 = terminalDTO.FunctionKey2,
                    FunctionKey3 = terminalDTO.FunctionKey3,
                    FunctionKey4 = terminalDTO.FunctionKey4,
                    FunctionKey5 = terminalDTO.FunctionKey5,
                    FunctionKey6 = terminalDTO.FunctionKey6,
                    FunctionKey7 = terminalDTO.FunctionKey7,
                    FunctionKey8 = terminalDTO.FunctionKey8,
                    FunctionType1 = terminalDTO.FunctionType1,
                    FunctionType2 = terminalDTO.FunctionType2,
                    FunctionType3 = terminalDTO.FunctionType3,
                    FunctionType4 = terminalDTO.FunctionType4,
                    FunctionType5 = terminalDTO.FunctionType5,
                    FunctionType6 = terminalDTO.FunctionType6,
                    FunctionType7 = terminalDTO.FunctionType7,
                    FunctionType8 = terminalDTO.FunctionType8,
                    TerminalID = terminalDTO.ID
                };
                branchserverDb.VirtualTerminalFunctionKeys.Add(terminalFunctionKeyToInsert);
                branchserverDb.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public void updateTerminalFunctionKeys(VirtualTerminal_DTO terminalDTO)
        {
            VirtualTerminalFunctionKey terminalFunctionKeyToInsert = null;
            try
            {
                terminalFunctionKeyToInsert = branchserverDb.VirtualTerminalFunctionKeys.Where(x => x.TerminalID == terminalDTO.ID).FirstOrDefault();

                if (terminalFunctionKeyToInsert != null)
                {
                    terminalFunctionKeyToInsert.FunctionKey1 = terminalDTO.FunctionKey1;
                    terminalFunctionKeyToInsert.FunctionKey2 = terminalDTO.FunctionKey2;
                    terminalFunctionKeyToInsert.FunctionKey3 = terminalDTO.FunctionKey3;
                    terminalFunctionKeyToInsert.FunctionKey4 = terminalDTO.FunctionKey4;
                    terminalFunctionKeyToInsert.FunctionKey5 = terminalDTO.FunctionKey5;
                    terminalFunctionKeyToInsert.FunctionKey6 = terminalDTO.FunctionKey6;
                    terminalFunctionKeyToInsert.FunctionKey7 = terminalDTO.FunctionKey7;
                    terminalFunctionKeyToInsert.FunctionKey8 = terminalDTO.FunctionKey8;
                    terminalFunctionKeyToInsert.FunctionType1 = terminalDTO.FunctionType1;
                    terminalFunctionKeyToInsert.FunctionType2 = terminalDTO.FunctionType2;
                    terminalFunctionKeyToInsert.FunctionType3 = terminalDTO.FunctionType3;
                    terminalFunctionKeyToInsert.FunctionType4 = terminalDTO.FunctionType4;
                    terminalFunctionKeyToInsert.FunctionType5 = terminalDTO.FunctionType5;
                    terminalFunctionKeyToInsert.FunctionType6 = terminalDTO.FunctionType6;
                    terminalFunctionKeyToInsert.FunctionType7 = terminalDTO.FunctionType7;
                    terminalFunctionKeyToInsert.FunctionType8 = terminalDTO.FunctionType8;
                    branchserverDb.SaveChanges();
                }

            }
            catch (Exception ex) { }
        }

        public void InsertTerminalCommunicationSetting(VirtualTerminal_DTO newTerminal)
        {
            VirtualTerminalCommunicationSetting terminalCommunicationSettingToInsert = null;
            try
            {
                terminalCommunicationSettingToInsert = new VirtualTerminalCommunicationSetting
                {
                    DynamicIPAddress = newTerminal.DynamicIPAddress,
                    DynamicIPAddressEnabled = newTerminal.DynamicIPEnabled,
                    DynamicIPAddressPort = newTerminal.DynamicPortNumber,
                    FixedIPAddress = newTerminal.StaticIPAddress,
                    FixedIPAddressEnabled = newTerminal.StaticIPEnabled,
                    FixedIPAddressPort = newTerminal.StaticPortNumber,
                    Active = false,
                    TerminalId = newTerminal.ID
                };
                branchserverDb.VirtualTerminalCommunicationSettings.Add(terminalCommunicationSettingToInsert);
                branchserverDb.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public void UpdateTerminalCommunicationSetting(VirtualTerminal_DTO newTerminal)
        {
            VirtualTerminalCommunicationSetting terminalCommunicationSettingToUpdate = null;
            try
            {
                terminalCommunicationSettingToUpdate = branchserverDb.VirtualTerminalCommunicationSettings.Where(x => x.TerminalId == newTerminal.ID).FirstOrDefault();

                if (terminalCommunicationSettingToUpdate != null)
                {
                    terminalCommunicationSettingToUpdate.DynamicIPAddress = newTerminal.DynamicIPAddress;
                    terminalCommunicationSettingToUpdate.DynamicIPAddressEnabled = newTerminal.DynamicIPEnabled;
                    terminalCommunicationSettingToUpdate.DynamicIPAddressPort = newTerminal.DynamicPortNumber;
                    terminalCommunicationSettingToUpdate.FixedIPAddress = newTerminal.StaticIPAddress;
                    terminalCommunicationSettingToUpdate.FixedIPAddressEnabled = newTerminal.StaticIPEnabled;
                    terminalCommunicationSettingToUpdate.FixedIPAddressPort = newTerminal.StaticPortNumber;
                    terminalCommunicationSettingToUpdate.Active = false;
                    branchserverDb.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }

        public void InsertTerminalInputSettings(VirtualTerminal_DTO newTerminal)
        {
            VirtualTerminalInputSetting terminalInPutSetting = new VirtualTerminalInputSetting();
            try
            {
                if (terminalInPutSetting != null)
                {
                    terminalInPutSetting.Biometric = newTerminal.BiomentricEnabled;
                    terminalInPutSetting.RFID = newTerminal.RFIDEnabled;
                    terminalInPutSetting.BarCode = newTerminal.BarCodeEnabled;
                    terminalInPutSetting.PinCode = newTerminal.PinCodeEnabled;
                    terminalInPutSetting.PersonalNumber = newTerminal.PersonalNumberEnabled;
                    terminalInPutSetting.InividualNumber = newTerminal.IndividualNumberEnabled;
                    terminalInPutSetting.TerminalID = newTerminal.ID;
                    branchserverDb.VirtualTerminalInputSettings.Add(terminalInPutSetting);
                    branchserverDb.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }

        public void UpdateTerminalInputSettings(VirtualTerminal_DTO newTerminal)
        {
            VirtualTerminalInputSetting terminalInPutSetting = null;
            try
            {
                terminalInPutSetting = branchserverDb.VirtualTerminalInputSettings.Where(x => x.TerminalID == newTerminal.ID).FirstOrDefault();

                if (terminalInPutSetting != null)
                {
                    terminalInPutSetting.Biometric = newTerminal.BiomentricEnabled;
                    terminalInPutSetting.RFID = newTerminal.RFIDEnabled;
                    terminalInPutSetting.BarCode = newTerminal.BarCodeEnabled;
                    terminalInPutSetting.PinCode = newTerminal.PinCodeEnabled;
                    terminalInPutSetting.PersonalNumber = newTerminal.PersonalNumberEnabled;
                    terminalInPutSetting.InividualNumber = newTerminal.IndividualNumberEnabled;
                    branchserverDb.SaveChanges();
                }
            }
            catch (Exception ex) { }
        }

        public List<VirtualGroupTerminal_DTO> GetAllVirtualGroupTerminals()
        {
            List<VirtualGroupTerminal_DTO> lcGroupTerminals = new List<VirtualGroupTerminal_DTO>();
            VirtualGroupTerminal_DTO groupTerminal = null;
            string sqlTerminalDetails = string.Empty;


            var terminals = branchserverDb.VirtualTerminals.ToList();

            if(terminals != null)
            {
                foreach(VirtualTerminal terminal in terminals)
                {
                    
                    groupTerminal = new VirtualGroupTerminal_DTO();
                    groupTerminal.ID = terminal.ID;
                    groupTerminal.TerminalID = terminal.TerminalId;
                    groupTerminal.TerminalDescription = terminal.Description;
                    groupTerminal.LocationNumber = terminal.LocationId;
                    groupTerminal.CostCenterNumber = terminal.CostCenterId;
                    groupTerminal.Status = Convert.ToBoolean(terminal.TerminalActive);
                    lcGroupTerminals.Add(groupTerminal);
                }

                foreach(VirtualGroupTerminal_DTO groupTerminalDTO in lcGroupTerminals)
                {
                    sqlTerminalDetails = "SELECT ISNULL((SELECT W_Bezeichnung FROM Werke WHERE W_Nr = " + groupTerminalDTO.LocationNumber + "), '')  AS Location, "
                                       + "       ISNULL((SELECT Kos_Bezeichnung FROM Kostenstellen WHERE Kos_Nr = " + groupTerminalDTO.CostCenterNumber + "), '') AS CostCenter";

                    SqlCommand terminalDetails = _SQLCommand(sqlTerminalDetails);
                    SqlDataReader recordTable = terminalDetails.ExecuteReader();

                    if(recordTable.Read())
                    {
                        groupTerminalDTO.LocationDescription = recordTable["Location"].ToString();
                        groupTerminalDTO.CostCenterDescription = recordTable["CostCenter"].ToString();
                    }

                    recordTable.Close();
                    recordTable = null;
                }
            }

            return lcGroupTerminals;

        }

        
        public void ClearGrouMappedTerminals(long groupID)
        {
            string sqlClearGroupMapping = "DELETE FROM VirtualTerminalGroupsMapping WHERE GroupID = " + groupID + ";";

            SqlCommand cmdClearMapping = _BranchServerSQLCommandSecond(sqlClearGroupMapping);
            cmdClearMapping.ExecuteNonQuery();
        }

        public void MapGrouTerminal(long groupID, long terminalID)
        {
            string sqlSetGroupMapping = "INSERT INTO VirtualTerminalGroupsMapping (GroupID , TerminalID) VALUES ( " + groupID  + " , " + terminalID  + " );";

            SqlCommand cmdClearMapping = _BranchServerSQLCommandSecond(sqlSetGroupMapping);
            cmdClearMapping.ExecuteNonQuery();
        }

        public List<VirtualGroupTerminal_DTO> GetGroupTerminals(long groupID)
        {
            string sqlGetGroupMapping = "SELECT * FROM VirtualTerminalGroupsMapping WHERE GroupID = " + groupID;
            string sqlTerminalDetails = string.Empty;
            List<VirtualGroupTerminal_DTO> groupTerminals = new List<VirtualGroupTerminal_DTO>();
            VirtualGroupTerminal_DTO groupTerminal = null;

            SqlCommand cmdClearMapping = _BranchServerSQLCommandSecond(sqlGetGroupMapping);
            SqlDataReader recordTable =  cmdClearMapping.ExecuteReader();

            while(recordTable.Read())
            {
                long terminalID = Convert.ToInt64(recordTable["TerminalID"]);
                var terminal = branchserverDb.VirtualTerminals.Where(x => x.ID == terminalID).FirstOrDefault();

                if(terminal!= null)
                {
                    groupTerminal = new VirtualGroupTerminal_DTO();
                    groupTerminal.ID = terminal.ID;
                    groupTerminal.TerminalID = terminal.TerminalId;
                    groupTerminal.TerminalDescription = terminal.Description;
                    groupTerminal.LocationNumber = terminal.LocationId;
                    groupTerminal.CostCenterNumber = terminal.CostCenterId;
                    groupTerminal.Status = Convert.ToBoolean(terminal.TerminalActive);
                    groupTerminals.Add(groupTerminal);
                }
            }
            recordTable.Close();
            recordTable = null;



            foreach (VirtualGroupTerminal_DTO groupTerminalDTO in groupTerminals)
            {
                sqlTerminalDetails = "SELECT ISNULL((SELECT W_Bezeichnung FROM Werke WHERE W_Nr = " + groupTerminalDTO.LocationNumber + "), '')  AS Location, "
                                    + "       ISNULL((SELECT Kos_Bezeichnung FROM Kostenstellen WHERE Kos_Nr = " + groupTerminalDTO.CostCenterNumber + "), '') AS CostCenter";

                SqlCommand terminalDetails = _SQLCommand(sqlTerminalDetails);
                SqlDataReader recordTable2 = terminalDetails.ExecuteReader();

                if (recordTable2.Read())
                {
                    groupTerminalDTO.LocationDescription = recordTable2["Location"].ToString();
                    groupTerminalDTO.CostCenterDescription = recordTable2["CostCenter"].ToString();
                }

                recordTable2.Close();
                recordTable2 = null;
            }

            return groupTerminals;
        }

        public void InsertTerminalCommunicationSettings(VirtualTerminal_DTO newTerminal)
        {
            VirtualTerminalCommunicationSetting terminalCommunicationSetting = null;
            try
            {
                terminalCommunicationSetting = new VirtualTerminalCommunicationSetting
                {
                    DynamicIPAddress = newTerminal.DynamicIPAddress,
                    DynamicIPAddressEnabled = newTerminal.DynamicIPEnabled,
                    DynamicIPAddressPort = newTerminal.DynamicPortNumber,
                    FixedIPAddress = newTerminal.StaticIPAddress,
                    FixedIPAddressEnabled = newTerminal.StaticIPEnabled,
                    FixedIPAddressPort = newTerminal.StaticPortNumber,
                    TerminalId = newTerminal.TerminalID,
                    Active = false
                };

                branchserverDb.VirtualTerminalCommunicationSettings.Add(terminalCommunicationSetting);
                branchserverDb.SaveChanges();
            }
            catch (Exception ex) { }
        }

        public VirtualTerminal GetTerminal(long terminalId)
        {
            VirtualTerminal terminalToGet = null;
            try
            {
                terminalToGet = branchserverDb.VirtualTerminals.Where(t => t.TerminalId == terminalId).SingleOrDefault();
            }
            catch (Exception ex) { }
            return terminalToGet;
        }

        public List<VirtualTerminal> GetAllTerminals()
        {
            List<VirtualTerminal> allTerminals = null;

            allTerminals = branchserverDb.VirtualTerminals.ToList();

            if (allTerminals == null)
            {
                allTerminals = new List<VirtualTerminal>();
            }

            return allTerminals;
        }

        public long GetNextAvailableTerminalID()
        {
            List<VirtualTerminal> allTerminals = null;
            long nextAvailableTerminalID = 0;

            allTerminals = branchserverDb.VirtualTerminals.ToList();

            if (allTerminals != null && allTerminals.Count > 0)
            {
                nextAvailableTerminalID = allTerminals.OrderByDescending(x => x.TerminalId).FirstOrDefault().TerminalId + 1;
            }
            else
            {
                nextAvailableTerminalID = nextAvailableTerminalID + 1;
            }

            return nextAvailableTerminalID;
        }
        //public List<TerminalSetting> GetTerminalSettings(long terminalId)
        //{
        //    List<TerminalSetting> terminalSettingsToGet = null;
        //    try
        //    {
        //        terminalSettingsToGet = branchserverDb.TerminalSettings.Where(t => t.TerminalId == terminalId).ToList();
        //    }
        //    catch (Exception ex) { }
        //    return terminalSettingsToGet;
        //}

        public List<VirtualTerminalFunctionKey> GetTerminalFunctionKeys(long terminalId)
        {
            List<VirtualTerminalFunctionKey> terminalFunctionKeysToGet = null;
            try
            {
                terminalFunctionKeysToGet = branchserverDb.VirtualTerminalFunctionKeys.Where(t => t.TerminalID == terminalId).ToList();
            }
            catch (Exception ex) { }
            return terminalFunctionKeysToGet;
        }
        public List<VirtualTerminalCommunicationSetting> GetTerminalCommunicationSettings(long terminalId)
        {
            List<VirtualTerminalCommunicationSetting> terminalCommunicationSettingsToGet = null;
            try
            {
                terminalCommunicationSettingsToGet = branchserverDb.VirtualTerminalCommunicationSettings.Where(t => t.TerminalId == terminalId).ToList();
            }
            catch (Exception ex) { }
            return terminalCommunicationSettingsToGet;
        }

        public void CreateNewterminalInDatabase(VirtualTerminal_DTO newTerminalDTO)
        {
            VirtualTerminal newTerminal = new VirtualTerminal();

            //newTerminal.ID = 0;
            newTerminal.TerminalId = newTerminalDTO.TerminalID;
            newTerminal.Description = newTerminalDTO.TerminalDescription;
            newTerminal.LocationId = newTerminalDTO.LocationNumber;
            //newTerminal. = string.Empty;
            newTerminal.CostCenterId = newTerminalDTO.CostCenterNumber;
            newTerminal.TerminalActive = newTerminalDTO.TerminalActive;
            newTerminal.TerminalType = newTerminalDTO.TerminalType;
            newTerminal.LogoutCode = newTerminalDTO.LogoutCode;
            newTerminal.RealTimeActive = newTerminalDTO.RealTimeActive;
            newTerminal.TerminalCostCenter = newTerminalDTO.TerminalCostenter.ToString();
            newTerminal.DisplayDepartment = newTerminalDTO.DisplayDepartment;
            newTerminal.DisplayPersonalNumber = newTerminalDTO.DisplayPersonalNumber;
            newTerminal.DisplayCostCenter = newTerminalDTO.DisplayCostCenter;
            newTerminal.FunctionKeysDisplayMode = newTerminalDTO.FunctionKeysDisplayMode;
            newTerminal.TerminalOffline = newTerminalDTO.TerminalOffline;
            newTerminal.DatabaseServer = newTerminalDTO.DatabaseServer;
            newTerminal.DataIntervalHours = newTerminalDTO.DataIntervalHours;
            newTerminal.DataintervalMinutes = newTerminalDTO.DataintervalMinutes;

            branchserverDb.VirtualTerminals.Add(newTerminal);
            branchserverDb.SaveChanges();
            newTerminalDTO.ID = newTerminal.ID;
        }

        //public void SaveFingerPrintsInDatabase(List<PersonalFingerTemplate> personlFingerTemplates)
        //{
        //    string deleteSQL = string.Empty;
        //    string insertSQL = string.Empty;

        //    foreach (PersonalFingerTemplate finger in personlFingerTemplates)
        //    {
        //        deleteSQL = string.Format("DELETE FROM FingerPrints WHERE Pers_Nr = {0} AND FingerIndex = {1}", finger.PersonalNumber, finger.FingerIndex);
        //        _executeNonQuery(deleteSQL);
        //        insertSQL = string.Format("INSERT INTO FingerPrints(Pers_Nr, FingerIndex, Template9, Template10) VALUES({0}, {1}, '{2}', '{3}')", finger.PersonalNumber, finger.FingerIndex, finger.Template9, finger.Template10);
        //        _executeNonQuery(insertSQL);
        //    }
        //}

        private void _executeNonQuery(string SQL)
        {
            SqlCommand command = _BranchServerSQLCommandSecond(SQL);
            command.ExecuteNonQuery();
        }

        public void UpdateTerminalInDatabase(VirtualTerminal_DTO newTerminalDTO)
        {
            VirtualTerminal updateTerminal = null;

            updateTerminal = branchserverDb.VirtualTerminals.Where(x => x.ID == newTerminalDTO.ID).FirstOrDefault();

            if (updateTerminal != null)
            {
                updateTerminal.TerminalId = newTerminalDTO.TerminalID;
                updateTerminal.Description = newTerminalDTO.TerminalDescription;
                updateTerminal.LocationId = newTerminalDTO.LocationNumber;
                //newTerminal. = string.Empty;
                updateTerminal.CostCenterId = newTerminalDTO.CostCenterNumber;
                updateTerminal.TerminalActive = newTerminalDTO.TerminalActive;
                updateTerminal.TerminalType = newTerminalDTO.TerminalType;
                updateTerminal.LogoutCode = newTerminalDTO.LogoutCode;
                updateTerminal.RealTimeActive = newTerminalDTO.RealTimeActive;
                updateTerminal.TerminalCostCenter = newTerminalDTO.TerminalCostenter.ToString();
                updateTerminal.DisplayDepartment = newTerminalDTO.DisplayDepartment;
                updateTerminal.DisplayPersonalNumber = newTerminalDTO.DisplayPersonalNumber;
                updateTerminal.DisplayCostCenter = newTerminalDTO.DisplayCostCenter;
                updateTerminal.FunctionKeysDisplayMode = newTerminalDTO.FunctionKeysDisplayMode;
                updateTerminal.TerminalOffline = newTerminalDTO.TerminalOffline;
                updateTerminal.DatabaseServer = newTerminalDTO.DatabaseServer;
                updateTerminal.DataIntervalHours = newTerminalDTO.DataIntervalHours;
                updateTerminal.DataintervalMinutes = newTerminalDTO.DataintervalMinutes;
                branchserverDb.SaveChanges();
            }
        }

        public void saveBookingInDatabase(long persNumber, int status, DateTime bookingDateTime, string terminalDescription)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("DECLARE @newBooking datetime = CONVERT(DATETIME, '{0:yyyy-MM-dd HH:mm}', 20); ", bookingDateTime));
            sb.Append("EXEC sp_InsertBooking ");
            sb.Append("@datetime =  ");
            sb.Append(" @newBooking ");
            sb.Append(", @persNr = ");
            sb.Append(persNumber);
            sb.Append(",@status = ");
            sb.Append(status);
            sb.Append(",@termDesc = ");
            sb.Append("'" + terminalDescription.Replace("'", "''") + "'");

            SqlCommand sqlCommand = _SQLCommand(sb.ToString());
            sqlCommand.ExecuteNonQuery();
        }

        //public List<BookingInfo_DTO> GetAllBookingInfosFromDatabase()
        //{
        //    List<BookingInfo_DTO> dailyBookingInfos = new List<BookingInfo_DTO>();
        //    BookingInfo_DTO dailyBookingInfo = null;
        //    StringBuilder sb = new StringBuilder();
        //    long currentPersonalNumber = 0;
        //    int currentBookingInfoIndex = 0;
        //    bool personalChanged = false;
        //    bool IndexMaxReached = false;

        //    sb.Append("SELECT Pers_Nr, Pers_Name1 +' ' + Pers_Name2 AS Name, Z_Status, RIGHT(CONVERT(VARCHAR, Z_Zeit, 108),7) AS Time FROM Zeitbuchungen ");
        //    sb.Append("INNER JOIN Personalstamm ON Personalstamm.Pers_Nr = Zeitbuchungen.Z_Pers_Nr ");
        //    //sb.Append("--WHERE CAST(Z_ZDatum AS DATE) = CAST(GETDATE() AS DATE) ");
        //    sb.Append("ORDER BY Pers_Nr, Z_Zeit ");

        //    SqlCommand sqlCommand = _SQLCommand(sb.ToString());
        //    SqlDataReader recordTable = sqlCommand.ExecuteReader();


        //    while (recordTable.Read())
        //    {
        //        currentBookingInfoIndex = currentBookingInfoIndex + 1;
        //        IndexMaxReached = currentBookingInfoIndex > 12;
        //        personalChanged = currentPersonalNumber != Convert.ToInt64(recordTable["Pers_Nr"]);

        //        if (IndexMaxReached || personalChanged)
        //        {
        //            if (dailyBookingInfo != null)
        //            {
        //                dailyBookingInfos.Add(dailyBookingInfo);
        //            }

        //            currentPersonalNumber = Convert.ToInt64(recordTable["Pers_Nr"]);
        //            currentBookingInfoIndex = 1;
        //            dailyBookingInfo = new BookingInfo_DTO();
        //            dailyBookingInfo.ID = dailyBookingInfos.Count + 1;
        //            dailyBookingInfo.PersonalNumber = Convert.ToString(recordTable["Pers_Nr"]);
        //            dailyBookingInfo.PersonalName = Convert.ToString(recordTable["Name"]);
        //        }

        //        switch (currentBookingInfoIndex)
        //        {
        //            case 1:
        //                dailyBookingInfo.Status1 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime1 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 2:
        //                dailyBookingInfo.Status2 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime2 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 3:
        //                dailyBookingInfo.Status3 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime3 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 4:
        //                dailyBookingInfo.Status4 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime4 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 5:
        //                dailyBookingInfo.Status5 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime5 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 6:
        //                dailyBookingInfo.Status6 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime6 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 7:
        //                dailyBookingInfo.Status7 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime7 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 8:
        //                dailyBookingInfo.Status8 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime8 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 9:
        //                dailyBookingInfo.Status9 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime9 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 10:
        //                dailyBookingInfo.Status10 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime10 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 11:
        //                dailyBookingInfo.Status11 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime11 = Convert.ToString(recordTable["Time"]);
        //                break;
        //            case 12:
        //                dailyBookingInfo.Status12 = Convert.ToString(recordTable["Z_Status"]);
        //                dailyBookingInfo.BookingTime12 = Convert.ToString(recordTable["Time"]);
        //                break;
        //        }
        //    }

        //    recordTable.Close();
        //    recordTable = null;

        //    return dailyBookingInfos;
        //}

        public void DeleteTerminalInstanceFromDatabase(long terminalID)
        {
            VirtualTerminal terminal = null;

            terminal = branchserverDb.VirtualTerminals.Where(x => x.ID == terminalID).FirstOrDefault();

            if (terminal != null)
            {
                branchserverDb.VirtualTerminals.Remove(terminal);
                branchserverDb.SaveChanges();
            }

        }

        public VirtualTerminal_DTO GetTerminalInstanceFromDatabase(long terminalID)
        {
            VirtualTerminal_DTO terminalInstance = new VirtualTerminal_DTO();
            VirtualTerminal terminal = null;

            terminal = branchserverDb.VirtualTerminals.Where(x => x.ID == terminalID).FirstOrDefault();

            terminalInstance.ID = terminal.ID;
            terminalInstance.TerminalID = terminal.TerminalId;
            terminalInstance.TerminalDescription = terminal.Description;
            terminalInstance.LocationNumber = terminal.LocationId;
            //terminalInstance. = string.Empty;
            terminalInstance.CostCenterNumber = terminal.CostCenterId;
            terminalInstance.TerminalActive = Convert.ToBoolean(terminal.TerminalActive);
            terminalInstance.LogoutCode = Convert.ToInt32(terminal.LogoutCode);
            terminalInstance.RealTimeActive = Convert.ToBoolean(terminal.RealTimeActive);
            terminalInstance.TerminalCostenter = Convert.ToInt32(terminal.TerminalCostCenter);
            terminalInstance.DisplayDepartment = terminal.DisplayDepartment;
            terminalInstance.DisplayPersonalNumber = terminal.DisplayPersonalNumber;
            terminalInstance.DisplayCostCenter = terminal.DisplayCostCenter;
            terminalInstance.FunctionKeysDisplayMode = terminal.FunctionKeysDisplayMode;
            terminalInstance.TerminalOffline = Convert.ToBoolean(terminal.TerminalOffline);
            terminalInstance.DatabaseServer = terminal.DatabaseServer;
            terminalInstance.DataIntervalHours = Convert.ToInt32(terminal.DataIntervalHours);
            terminalInstance.DataintervalMinutes = Convert.ToInt32(terminal.DataintervalMinutes);
            return terminalInstance;
        }

        public VirtualTerminal_DTO GetFirstTerminalInstanceFromDatabase()
        {
            VirtualTerminal_DTO terminalInstance = null;
            VirtualTerminal terminal = null;

            terminal = branchserverDb.VirtualTerminals.FirstOrDefault();

            if (terminal != null)
            {
                terminalInstance = new VirtualTerminal_DTO();
                terminalInstance.ID = terminal.ID;
                terminalInstance.TerminalID = terminal.TerminalId;
                terminalInstance.TerminalDescription = terminal.Description;
                terminalInstance.LocationNumber = terminal.LocationId;
                //terminalInstance. = string.Empty;
                terminalInstance.CostCenterNumber = terminal.CostCenterId;
                terminalInstance.TerminalActive = Convert.ToBoolean(terminal.TerminalActive);
                terminalInstance.LogoutCode = Convert.ToInt32(terminal.LogoutCode);
                terminalInstance.RealTimeActive = Convert.ToBoolean(terminal.RealTimeActive);
                terminalInstance.TerminalCostenter = Convert.ToInt32(terminal.TerminalCostCenter);
                terminalInstance.DisplayDepartment = terminal.DisplayDepartment;
                terminalInstance.DisplayPersonalNumber = terminal.DisplayPersonalNumber;
                terminalInstance.DisplayCostCenter = terminal.DisplayCostCenter;
                terminalInstance.FunctionKeysDisplayMode = terminal.FunctionKeysDisplayMode;
            }

            return terminalInstance;
        }

        public List<DisplayBooking_DTO> GetAllDailyBookingsForpersonal(long personalNumber)
        {
            List<DisplayBooking_DTO> dailyBookings = new List<DisplayBooking_DTO>();
            DisplayBooking_DTO dailyBooking = null;

            string dailyBookingsSQL = "SELECT Z_Status, Z_Zeit FROM Zeitbuchungen "
                                    + "WHERE CAST(Z_ZDatum AS DATE) = CAST(GETDATE() AS DATE) AND Z_Pers_Nr = " + personalNumber + " "
                                    + "ORDER BY Z_Zeit DESC ";

            SqlCommand sqlCommand = _SQLCommand(dailyBookingsSQL);
            SqlDataReader recordTable = sqlCommand.ExecuteReader();

            int currentRecordNumber = 0;

            while (recordTable.Read())
            {
                currentRecordNumber = currentRecordNumber + 1;

                if (currentRecordNumber % 2 != 0)
                {
                    dailyBooking = new DisplayBooking_DTO();
                    dailyBooking.ID = dailyBookings.Count + 1;
                    dailyBooking.Status1 = recordTable["Z_Status"].ToString();
                    dailyBooking.BookingTime1 = Convert.ToDateTime(recordTable["Z_Zeit"]).ToString("HH:mm");
                }
                else
                {
                    dailyBooking.Status2 = recordTable["Z_Status"].ToString();
                    dailyBooking.BookingTime2 = Convert.ToDateTime(recordTable["Z_Zeit"]).ToString("HH:mm");
                    dailyBookings.Add(dailyBooking);
                }
            }

            recordTable.Close();
            recordTable = null;

            return dailyBookings;

        }

        public void GetTerminalInputSettingsFromDatabase(VirtualTerminal_DTO terminalDTO)
        {
            VirtualTerminal_DTO terminalInstance = new VirtualTerminal_DTO();
            VirtualTerminalInputSetting terminalInputSetting = null;

            terminalInputSetting = branchserverDb.VirtualTerminalInputSettings.Where(x => x.TerminalID == terminalDTO.ID).FirstOrDefault();

            if (terminalInputSetting != null)
            {
                terminalDTO.BiomentricEnabled = terminalInputSetting.Biometric;
                terminalDTO.RFIDEnabled = terminalInputSetting.RFID;
                terminalDTO.BarCodeEnabled = terminalInputSetting.BarCode;
                terminalDTO.PersonalNumberEnabled = terminalInputSetting.PersonalNumber;
                terminalDTO.PinCodeEnabled = terminalInputSetting.PinCode;
                terminalDTO.IndividualNumberEnabled = terminalInputSetting.InividualNumber;
            }
        }

        public void GetTerminalCommunicationSettingsFromDatabase(VirtualTerminal_DTO terminalDTO)
        {
            VirtualTerminal_DTO terminalInstance = new VirtualTerminal_DTO();
            VirtualTerminalCommunicationSetting terminalCommunicationSetting = null;

            branchserverDb = new PZE_Entities();
            terminalCommunicationSetting = branchserverDb.VirtualTerminalCommunicationSettings.Where(x => x.TerminalId == terminalDTO.ID).FirstOrDefault();

            if (terminalCommunicationSetting != null)
            {
                terminalDTO.DynamicIPAddress = terminalCommunicationSetting.DynamicIPAddress;
                terminalDTO.StaticIPAddress = terminalCommunicationSetting.FixedIPAddress;
                terminalDTO.DynamicPortNumber = Convert.ToInt32(terminalCommunicationSetting.DynamicIPAddressPort);
                terminalDTO.StaticPortNumber = Convert.ToInt32(terminalCommunicationSetting.FixedIPAddressPort);
                terminalDTO.DynamicIPEnabled = Convert.ToBoolean(terminalCommunicationSetting.DynamicIPAddressEnabled);
                terminalDTO.StaticIPEnabled = Convert.ToBoolean(terminalCommunicationSetting.FixedIPAddressEnabled);
            }
        }

        public void GetTerminalFunctionKeysFromDatabase(VirtualTerminal_DTO terminalDTO)
        {
            VirtualTerminal_DTO terminalInstance = new VirtualTerminal_DTO();
            VirtualTerminalFunctionKey terminalFunctionKey = null;

            terminalFunctionKey = branchserverDb.VirtualTerminalFunctionKeys.Where(x => x.TerminalID == terminalDTO.ID).FirstOrDefault();

            if (terminalFunctionKey != null)
            {
                terminalDTO.FunctionKey1 = terminalFunctionKey.FunctionKey1;
                terminalDTO.FunctionKey2 = terminalFunctionKey.FunctionKey2;
                terminalDTO.FunctionKey3 = terminalFunctionKey.FunctionKey3;
                terminalDTO.FunctionKey4 = terminalFunctionKey.FunctionKey4;
                terminalDTO.FunctionKey5 = terminalFunctionKey.FunctionKey5;
                terminalDTO.FunctionKey6 = terminalFunctionKey.FunctionKey6;
                terminalDTO.FunctionKey7 = terminalFunctionKey.FunctionKey7;
                terminalDTO.FunctionKey8 = terminalFunctionKey.FunctionKey8;

                terminalDTO.FunctionType1 = terminalFunctionKey.FunctionType1;
                terminalDTO.FunctionType2 = terminalFunctionKey.FunctionType2;
                terminalDTO.FunctionType3 = terminalFunctionKey.FunctionType3;
                terminalDTO.FunctionType4 = terminalFunctionKey.FunctionType4;
                terminalDTO.FunctionType5 = terminalFunctionKey.FunctionType5;
                terminalDTO.FunctionType6 = terminalFunctionKey.FunctionType6;
                terminalDTO.FunctionType7 = terminalFunctionKey.FunctionType7;
                terminalDTO.FunctionType8 = terminalFunctionKey.FunctionType8;
            }
        }
    }
}