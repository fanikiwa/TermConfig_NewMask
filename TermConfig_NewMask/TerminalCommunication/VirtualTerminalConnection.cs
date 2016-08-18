using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using TermConfig_NewMask.Dtos;
using VirtualTerminalCommunication;

namespace TermConfig_NewMask.TerminalCommunication
{
    public class VirtualTerminalConnection
    {
        TerminalConnection branchServerConnection = null;
        BranchTerminal _branchTerminal = null;

        public bool ConnectToterminal(VirtualTerminal_DTO taskTerminal)
        {
            branchServerConnection = new TerminalConnection();
            _moveSettingsToBranchTerminal(taskTerminal);
            return branchServerConnection.connectToTerminal(_branchTerminal);
            
        }

        public void SendMasterData(long groupID)
        {
            branchServerConnection.SendMasterData(groupID, _branchTerminal);
        }

        public void Disconnect()
        {
            branchServerConnection.Disconnect();
        }

        public bool GetBookings()
        {
            bool taskSucceded = false;

            branchServerConnection.GetBookings();
            return taskSucceded;
        }

        public bool SendSystemTime()
        {
            bool taskSucceded = false;
            return taskSucceded;
        }

        private void _moveSettingsToBranchTerminal(VirtualTerminal_DTO connectedTerminal)
        {
            _branchTerminal = new BranchTerminal();

            _branchTerminal.ID = connectedTerminal.ID;
            _branchTerminal.TerminalID = connectedTerminal.TerminalID;
            _branchTerminal.TerminalDescription = connectedTerminal.TerminalDescription;
            _branchTerminal.LocationNumber = connectedTerminal.LocationNumber;
            _branchTerminal.LocationDescription = connectedTerminal.LocationDescription;
            _branchTerminal.CostCenterNumber = connectedTerminal.CostCenterNumber;
            _branchTerminal.TerminalActive = connectedTerminal.TerminalActive;
            _branchTerminal.TerminalType = connectedTerminal.TerminalType;
            _branchTerminal.LogoutCode = connectedTerminal.LogoutCode;
            _branchTerminal.RealTimeActive = connectedTerminal.RealTimeActive;
            _branchTerminal.TerminalCostenter = connectedTerminal.TerminalCostenter;
            _branchTerminal.DisplayDepartment = connectedTerminal.DisplayDepartment;
            _branchTerminal.DisplayPersonalNumber = connectedTerminal.DisplayPersonalNumber;
            _branchTerminal.DisplayCostCenter = connectedTerminal.DisplayCostCenter;
            _branchTerminal.BiomentricEnabled = connectedTerminal.BiomentricEnabled;
            _branchTerminal.RFIDEnabled = connectedTerminal.RFIDEnabled;
            _branchTerminal.BarCodeEnabled = connectedTerminal.BarCodeEnabled;
            _branchTerminal.PinCodeEnabled = connectedTerminal.PinCodeEnabled;
            _branchTerminal.PersonalNumberEnabled = connectedTerminal.PersonalNumberEnabled;
            _branchTerminal.IndividualNumberEnabled = connectedTerminal.IndividualNumberEnabled;
            _branchTerminal.StaticIPAddress = connectedTerminal.StaticIPAddress;
            _branchTerminal.DynamicIPAddress = connectedTerminal.DynamicIPAddress;
            _branchTerminal.StaticIPEnabled = connectedTerminal.StaticIPEnabled;
            _branchTerminal.DynamicIPEnabled = connectedTerminal.DynamicIPEnabled;
            _branchTerminal.StaticPortNumber = connectedTerminal.StaticPortNumber;
            _branchTerminal.DynamicPortNumber = connectedTerminal.DynamicPortNumber;
            _branchTerminal.FunctionKey1 = connectedTerminal.FunctionKey1;
            _branchTerminal.FunctionKey2 = connectedTerminal.FunctionKey2;
            _branchTerminal.FunctionKey3 = connectedTerminal.FunctionKey3;
            _branchTerminal.FunctionKey4 = connectedTerminal.FunctionKey4;
            _branchTerminal.FunctionKey5 = connectedTerminal.FunctionKey5;
            _branchTerminal.FunctionKey6 = connectedTerminal.FunctionKey6;
            _branchTerminal.FunctionKey7 = connectedTerminal.FunctionKey7;
            _branchTerminal.FunctionKey8 = connectedTerminal.FunctionKey8;
            _branchTerminal.FunctionType1 = connectedTerminal.FunctionType1;
            _branchTerminal.FunctionType2 = connectedTerminal.FunctionType2;
            _branchTerminal.FunctionType3 = connectedTerminal.FunctionType3;
            _branchTerminal.FunctionType4 = connectedTerminal.FunctionType4;
            _branchTerminal.FunctionType5 = connectedTerminal.FunctionType5;
            _branchTerminal.FunctionType6 = connectedTerminal.FunctionType6;
            _branchTerminal.FunctionType7 = connectedTerminal.FunctionType7;
            _branchTerminal.FunctionType8 = connectedTerminal.FunctionType8;
            _branchTerminal.TerminalOffline = Convert.ToBoolean(connectedTerminal.TerminalOffline);
            _branchTerminal.DatabaseServer = connectedTerminal.DatabaseServer;
            _branchTerminal.DataIntervalHours = Convert.ToInt32(connectedTerminal.DataIntervalHours);
            _branchTerminal.DataintervalMinutes = Convert.ToInt32(connectedTerminal.DataintervalMinutes);
            _branchTerminal.FunctionKeysDisplayMode = connectedTerminal.FunctionKeysDisplayMode;
            _branchTerminal.DatabaseUser = Convert.ToString(connectedTerminal.DatabaseUser);
            _branchTerminal.DatabasePassword = connectedTerminal.DatabasePassword;
    }
    }
}