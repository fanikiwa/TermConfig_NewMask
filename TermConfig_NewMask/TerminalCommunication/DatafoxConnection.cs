using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KruAll.Core.Models;
using System.Configuration;
using TermConfig_NewMask.ViewModels;
using KruAll.Core.Models;
using System.Data.Entity.Core.EntityClient;

namespace TermConfig_NewMask.TerminalCommunication
{
    public class DatafoxConnection : ITerminalConnection
    {
        private string terminalIPAddress = string.Empty;
        private string terminalPortNumber = string.Empty;
        private string terminalDescription = string.Empty;
        DataCommunication.SDKType _sdkType = new DataCommunication.SDKType();

        public string IPAddress
        {
            get
            {
                return terminalIPAddress;
            }

            set
            {
                terminalIPAddress = value;
            }
        }

        public string PortNumber
        {
            get
            {
                return terminalPortNumber;
            }

            set
            {
                terminalPortNumber = value;
            }
        }

        public string TerminalDescription
        {
            get
            {
                return terminalDescription;
            }

            set
            {
                terminalDescription = value;
            }
        }
        public DataCommunication.SDKType SDKType
        {
            get
            {
                return _sdkType;
            }

            set
            {
                _sdkType = value;
            }
        }

        public void SendMasterData()
        {
            string responseMessage = string.Empty;
            string IPAddress = this.IPAddress;
            string DeviceName = this.terminalDescription;

            var df = DatafoxFunctionSettings(DeviceName);

            var dbConnection = new ttxTools.ClsDB(connectionString);
            var termRecord = DataFoxEx.newTermRecord(DeviceName, IPAddress, dbConnection);
            var terminal = new KrutecDfTrans.Classes.DfTerminal(dbConnection, termRecord, 254);

            if (terminal.Info == "Offline")
            {
                this.LastActionResult = TerminalInterface.ActionResultType.Error;
                this.LastActionResultMessage = Resources.LocalizedText.ErrorSendingData;
            }
            else
            {
                terminal.SendCommonData(df);
                this.LastActionResult = TerminalInterface.ActionResultType.Success;
                this.LastActionResultMessage = Resources.LocalizedText.DataSentSuccessfully;
            }
        }

        public void GetBookings()
        {
            string IPAddress = this.IPAddress;
            string DeviceName = this.terminalDescription;


            var df = DatafoxFunctionSettings(DeviceName);


            while (true)
            {
                var dbConnection = new ttxTools.ClsDB(this.KrutecPZEconnectionString);

                var termRecord = DataFoxEx.newTermRecord(DeviceName, IPAddress, dbConnection);
                var terminal = new KrutecDfTrans.Classes.DfTerminal(dbConnection, termRecord, 254);

                if (terminal.Info == "Offline")
                {
                    this.LastActionResult = TerminalInterface.ActionResultType.Error;
                    this.LastActionResultMessage = "Fehler beim Abrufen der Buchungen von Terminal";
                    break;
                }

                terminal.ReadTransactions(df.BookingSpan);
                this.LastActionResult = TerminalInterface.ActionResultType.Success;
                this.LastActionResultMessage = "Buchungen zur Datenbank erfolgreich hinzugefügt";
                break;
            }
        }

        public void SendSystemTime()
        {
            string responseMessage = string.Empty;
            string IPAddress = this.IPAddress;
            string DeviceName = this.terminalDescription;

            var dbConnection = new ttxTools.ClsDB(connectionString);

            var termRecord = DataFoxEx.newTermRecord(DeviceName, IPAddress, dbConnection);
            var terminal = new KrutecDfTrans.Classes.DfTerminal(dbConnection, termRecord, 254);

            if (terminal.Info == "Offline")
            {
                this.LastActionResult = TerminalInterface.ActionResultType.Error;
                this.LastActionResultMessage = "Fehler beim Setzen Terminal Datum und Uhrzeit";
            }
            else
            {
                terminal.SyncTime();
                this.LastActionResult = TerminalInterface.ActionResultType.Success;
                this.LastActionResultMessage = "Das Terminal Datum und Zeit erfolgreich aktualisiert";
            }
        }

        private TerminalDatafoxFunction DatafoxFunctionSettings(string terminalDescription)
        {
            TerminalDatafoxFunction datafoxFunctionSetting = null;
            TerminalConfigViewModel terminalViewModel = new TerminalConfigViewModel();

            datafoxFunctionSetting = terminalViewModel.GetTerminalDatafoxFunctions(terminalDescription);

            if(datafoxFunctionSetting == null)
            {
                datafoxFunctionSetting = new TerminalDatafoxFunction()
                {
                    AccessControl = true,
                    ActionList = true,
                    BookingSpan = 0,
                    EventList = true,
                    HolidayList = true,
                    IdentificationList = true,
                    LocationList = true,
                    Project = false,
                    ReaderList = true,
                    TimeList = true
                };
            }

            return datafoxFunctionSetting;
        }

        public void TestConnection()
        {
            string IPAddress = this.IPAddress;
            string DeviceName = this.terminalDescription;

            var dbConnection = new ttxTools.ClsDB(connectionString);

            var termRecord = DataFoxEx.newTermRecord(DeviceName, IPAddress, dbConnection);
            var terminal = new KrutecDfTrans.Classes.DfTerminal(dbConnection, termRecord, 254);

            if (terminal.Info == "Offline")
            {
                this.LastActionResult = TerminalInterface.ActionResultType.Error;
                this.LastActionResultMessage = Resources.LocalizedText.ConnectionTestFailed;
            }
            else
            {
                this.LastActionResult = TerminalInterface.ActionResultType.Success;
                this.LastActionResultMessage = Resources.LocalizedText.ConnectionTestSuccessful;
                terminal.Stream.Close();
            }
        }

        private string connectionString
        {
            get
            {
                string entityConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PZE_Entities"].ConnectionString;
                return new EntityConnectionStringBuilder(entityConnectionString).ProviderConnectionString;
            }
        }

        private string KrutecPZEconnectionString
        {
            get
            {
                string entityConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["KrutecPZEEntities"].ConnectionString;
                return new EntityConnectionStringBuilder(entityConnectionString).ProviderConnectionString;
            }
        }

        public TerminalInterface.ActionResultType LastActionResult
        {
            get; set;
        }

        public string LastActionResultMessage
        {
            get; set;
        }

        public bool HasBiometricdata
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}