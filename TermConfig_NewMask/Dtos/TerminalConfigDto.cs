using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalConfigDto
    {
        public long ID { get; set; }
        public int TermID { get; set; }
        public string TermType { get; set; }
        public string Description { get; set; }
        public string ConnectionType { get; set; }
        public string Status { get; set; }
        public string SerialNumber { get; set; }
        public string IpAddress { get; set; }
        public int? Port { get; set; }
        public bool hasSelection { get; set; }
        public bool IsActive { get; set; }
    }
}