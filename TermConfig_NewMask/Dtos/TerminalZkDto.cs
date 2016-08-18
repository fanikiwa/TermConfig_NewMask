using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalZkDto
    {
        #region Constructor
        public TerminalZkDto() { }

        #endregion

        #region Properties
        public long ID { get; set; }
        public int TermID { get; set; }
        public int? TerminalId { get; set; }
        public int? TerminalOEMId { get; set; }
        public string TermType { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string ConnectionType { get; set; }
        public string IpAddress { get; set; }
        public int? Port { get; set; }
        public bool HasFPRead { get; set; }
        public bool HasAPPosting { get; set; }
        public bool HasTAPPosting { get; set; }
        public bool HasPersNoPin { get; set; }
        public bool IsActive { get; set; }
        public bool RFIDCardPin { get; set; }
        public bool RFIDActive { get; set; }
        public bool AllowTransponder { get; set; }
        public bool AllowTransponderAndPin { get; set; }
        public bool HasProfFirmware { get; set; }
        public string IsActiveText
        {
            get
            {
                return GetStatusText();
            }
        }
        public string GetStatusText()
        {
            if (IsActive)
            {
                return Resources.LocalizedText.active;
            }
            else
            {
                return Resources.LocalizedText.statusInaktiv;
            }
        }
        public int? ZkRelayTime { get; set; }
        public int? ZkExternalReaders { get; set; }
        public string InfoText1 { get; set; }
        public string InfoText2 { get; set; }
        public string InfoText3 { get; set; }
        public string InfoText4 { get; set; }
        public string FunctionKeyText1 { get; set; }
        public string FunctionKeyText2 { get; set; }
        public string FunctionKeyText3 { get; set; }
        public string FunctionKeyText4 { get; set; }
        public string FunctionKeyText5 { get; set; }
        public string FunctionKeyText6 { get; set; }
        public string FunctionKeyText7 { get; set; }
        public string FunctionKeyText8 { get; set; }
        public string internReaderDesc { get; set; }
        public string externReaderDesc { get; set; }
        public int? internDirec { get; set; }
        public int? externDirec { get; set; }
        public string relayTime { get; set; }
        public string readerActive { get; set; }
        public int readerExternId { get; set; }
        public int readerInternId { get; set; }
        public string readerInfoExtern { get; set; }
        public string readerInfoIntern { get; set; }
        public string readerInfoIntern_2 { get; set; }
        public string readerTypeExtern { get; set; }
        public string readerTypeIntern { get; set; }
        public string readerTypeIntern_2 { get; set; }
        public int readerStaticId { get; set; }
      
        public string TerminalTypeId { get; set; }
        public string TerminalTypeDescription { get; set; }
        #endregion
    }
}