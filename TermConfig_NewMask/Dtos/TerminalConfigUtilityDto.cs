using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalConfigUtilityDto
    {
        public long ID { get; set; }
        public long TerminalConfigID { get; set; }
        public bool HasFPRead { get; set; }
        public bool HasAPPosting { get; set; }
        public bool HasTAPPosting { get; set; }
        public bool HasPersNoPin { get; set; }
        public bool RFIDCardPin { get; set; }
        public bool RFIDActive { get; set; }
        public bool AllowTransponder { get; set; }
        public bool AllowTransponderAndPin { get; set; }
        public bool HasProfFirmware { get; set; }
    }
}