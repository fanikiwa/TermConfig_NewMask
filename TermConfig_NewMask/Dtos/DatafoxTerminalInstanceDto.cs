using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class DatafoxTerminalInstanceDto
    {
        #region Constructor
        public DatafoxTerminalInstanceDto() { }

        #endregion

        #region Properties
        public long ID { get; set; }
        public string TermType { get; set; }
        public int TermID { get; set; }
        public string Description { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string SerialNumber { get; set; }
        public string ConnectionType { get; set; }
        public string IpAddress { get; set; }
        public Nullable<int> Port { get; set; }
        public string Memo { get; set; }
        public string Image { get; set; }
        public int? TerminalOEMId { get; set; }
        public int? TerminalId { get; set; }
        public bool HasAPPosting { get; set; }
        public bool HasTAPPosting { get; set; }
        
        public bool HasPersNoPin { get; set; }
        public string InfoText1 { get; set; }
        public string InfoText2 { get; set; }
        public string InfoText3 { get; set; }
        public string InfoText4 { get; set; }
        #endregion
    }
}