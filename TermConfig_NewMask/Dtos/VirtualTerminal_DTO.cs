using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TermConfig_NewMask.TerminalCommunication.VirtualDataObjects;

namespace TermConfig_NewMask.Dtos
{
    public class VirtualTerminal_DTO
    {
        public VirtualTerminal_DTO()
        {
            //Terminal
            this.ID = 0;
            this.TerminalID = 0;
            this.TerminalDescription = string.Empty;
            this.LocationNumber = 0;
            this.LocationDescription = string.Empty;
            this.CostCenterNumber = 0;
            this.TerminalActive = false;
            this.TerminalType = 0;
            this.LogoutCode = 0;
            this.RealTimeActive = false;
            this.TerminalCostenter = 0;
            this.DisplayDepartment = false;
            this.DisplayPersonalNumber = false;
            this.DisplayCostCenter = false;

            //TerminalInputSettings
            this.BiomentricEnabled = false;
            this.RFIDEnabled = false;
            this.BarCodeEnabled = false;
            this.PinCodeEnabled = false;
            this.PersonalNumberEnabled = false;
            this.IndividualNumberEnabled = false;

            //TerminalCommunicationSettings
            this.StaticIPAddress = "0.0.0.0";
            this.DynamicIPAddress = string.Empty;
            this.StaticIPEnabled = false;
            this.DynamicIPEnabled = false;
            this.StaticPortNumber = 0;
            this.DynamicPortNumber = 0;

            //TerminalFunctionKeys
            this.FunctionKey1 = 0;
            this.FunctionKey2 = 0;
            this.FunctionKey3 = 0;
            this.FunctionKey4 = 0;
            this.FunctionKey5 = 0;
            this.FunctionKey6 = 0;
            this.FunctionKey7 = 0;
            this.FunctionKey8 = 0;

            this.FunctionType1 = 1;
            this.FunctionType2 = 1;
            this.FunctionType3 = 1;
            this.FunctionType4 = 1;
            this.FunctionType5 = 1;
            this.FunctionType6 = 1;
            this.FunctionType7 = 1;
            this.FunctionType8 = 1;


            this.TerminalOffline = false;
            this.DatabaseServer = string.Empty;
            this.DataIntervalHours = 0;
            this.DataintervalMinutes = 0;
            this.DatabaseUser = "sa";
            this.DatabasePassword = "krutecKR02";

            this.Type1FunctionKeys = new List<VirtualFunctionKeyItem>();
            this.Type2FunctionKeys = new List<VirtualFunctionKeyItem>();
            this.Type3FunctionKeys = new List<VirtualFunctionKeyItem>();
            this.FunctionKeysDisplayMode = 4;

        }

        //===================
        //TERMINAL-INSTANCE: Table : Terminal
        //===================
        public long ID { get; set; }
        public long TerminalID { get; set; }
        public string TerminalDescription { get; set; }
        public int LocationNumber { get; set; }
        public string LocationDescription { get; set; }
        public int CostCenterNumber { get; set; }

        public bool TerminalActive { get; set; }
        public int TerminalType { get; set; }
        public int LogoutCode { get; set; }
        public bool RealTimeActive { get; set; }
        public int TerminalCostenter { get; set; }
        //DisplaySettings Table : Terminal
        //=================
        public bool DisplayDepartment { get; set; }
        public bool DisplayPersonalNumber { get; set; }
        public bool DisplayCostCenter { get; set; }

        // TerminalInputSettings Table : Terminal
        //====================
        public bool BiomentricEnabled { get; set; }
        public bool RFIDEnabled { get; set; }
        public bool BarCodeEnabled { get; set; }
        public bool PinCodeEnabled { get; set; }
        public bool PersonalNumberEnabled { get; set; }
        public bool IndividualNumberEnabled { get; set; }

        //CommunicationSettings Table : TerminalCommunicationSettings
        //====================

        public string StaticIPAddress { get; set; }
        public string DynamicIPAddress { get; set; }
        public bool StaticIPEnabled { get; set; }
        public bool DynamicIPEnabled { get; set; }
        public int StaticPortNumber { get; set; }
        public int DynamicPortNumber { get; set; }

        //FunctionKeys Table : TerminalFunctionKeys
        //==============
        public int FunctionKey1 { get; set; }
        public int FunctionKey2 { get; set; }
        public int FunctionKey3 { get; set; }
        public int FunctionKey4 { get; set; }
        public int FunctionKey5 { get; set; }
        public int FunctionKey6 { get; set; }
        public int FunctionKey7 { get; set; }
        public int FunctionKey8 { get; set; }
        public int FunctionType1 { get; set; }
        public int FunctionType2 { get; set; }
        public int FunctionType3 { get; set; }
        public int FunctionType4 { get; set; }
        public int FunctionType5 { get; set; }
        public int FunctionType6 { get; set; }
        public int FunctionType7 { get; set; }
        public int FunctionType8 { get; set; }
        public int FunctionKeysDisplayMode { get; set; }

        public bool TerminalOffline { get; set; }
        public string DatabaseServer { get; set; }
        public int DataIntervalHours { get; set; }
        public int DataintervalMinutes { get; set; }

        public List<VirtualFunctionKeyItem> Type1FunctionKeys { get; set; }
        public List<VirtualFunctionKeyItem> Type2FunctionKeys { get; set; }
        public List<VirtualFunctionKeyItem> Type3FunctionKeys { get; set; }
        public string DatabaseUser { get; set; }
        public string DatabasePassword { get; set; }
    }
}