using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZKSDKCommunication;
using ZKSDKCommunication.ZKBusinessObjects;

namespace TermConfig_NewMask.TerminalCommunication
{
    public class ZKPullConnection : ITerminalConnection
    {
        private string terminalIPAddress = string.Empty;
        private string terminalPortNumber = string.Empty;
        private string terminalDescription = string.Empty;
        DataCommunication.SDKType _sdkType = new DataCommunication.SDKType();
        private const int ZK_TERMINAL_DEFAULT_PORT = 4370;

        private PullInterface pullInterfaceSDK = null;

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

        public void GetBookings()
        {
            ZKTerminal terminal = this.getCurrentTerminal();

            pullInterfaceSDK = new PullInterface(terminal);

            if (pullInterfaceSDK.ConnectToTerminal())
            {
                pullInterfaceSDK.GetBookings();
                pullInterfaceSDK.DisconnectDevice();
                this.LastActionResult = TerminalInterface.ActionResultType.Success;
                this.LastActionResultMessage = "Booking Sent To Database";
            }
            else
            {
                this.LastActionResult = TerminalInterface.ActionResultType.Error;
                this.LastActionResultMessage = "Fehler beim Abrufen der Buchungen von Terminal";
            }
        }

        public void SendMasterData()
        {
            ZKTerminal terminal = this.getCurrentTerminal();

            pullInterfaceSDK = new PullInterface(terminal);

            if (pullInterfaceSDK.ConnectToTerminal())
            {
                pullInterfaceSDK.SendMasterData();
                pullInterfaceSDK.DisconnectDevice();
                this.LastActionResult = TerminalInterface.ActionResultType.Success;
                this.LastActionResultMessage = Resources.LocalizedText.DataSentSuccessfully;
            }
            else
            {
                this.LastActionResult = TerminalInterface.ActionResultType.Error;
                this.LastActionResultMessage = Resources.LocalizedText.ErrorSendingData;
            }
        }

        public void SendSystemTime()
        {
            ZKTerminal terminal = this.getCurrentTerminal();

            pullInterfaceSDK = new PullInterface(terminal);

            if (pullInterfaceSDK.ConnectToTerminal())
            {
                pullInterfaceSDK.SetDeviceDateTime();
                pullInterfaceSDK.DisconnectDevice();
                this.LastActionResult = TerminalInterface.ActionResultType.Success;
                this.LastActionResultMessage = "Terminal Time Has Been set";
            }
            else
            {
                this.LastActionResult = TerminalInterface.ActionResultType.Error;
                this.LastActionResultMessage = "Error Synchronize Time";
            }
        }

        public void TestConnection()
        {
            ZKTerminal terminal = this.getCurrentTerminal();

            pullInterfaceSDK = new PullInterface(terminal);

            if (pullInterfaceSDK.ConnectToTerminal())
            {
                pullInterfaceSDK.DisconnectDevice();
                this.LastActionResult = TerminalInterface.ActionResultType.Success;
                this.LastActionResultMessage = Resources.LocalizedText.ConnectionTestSuccessful;
            }
            else
            {
                this.LastActionResult = TerminalInterface.ActionResultType.Error;
                this.LastActionResultMessage = Resources.LocalizedText.ConnectionTestFailed;
            }
        }

        private ZKTerminal getCurrentTerminal()
        {
            ZKTerminal _currentTerminal = new ZKTerminal();

            _currentTerminal.Description = this.TerminalDescription;
            _currentTerminal.IPAddress = this.IPAddress;
            _currentTerminal.PortNumber = ZK_TERMINAL_DEFAULT_PORT;
            _currentTerminal.DataCollectionType = ZKTerminalEnums.DataCollectionType.AccessControl;
            _currentTerminal.SDKType = ZKTerminalEnums.SDKType.StandardPULL;

            return _currentTerminal;

        }
    }
}