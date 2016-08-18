using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.TerminalCommunication.VirtualDataObjects
{
    public class VirtualPersonal
    {
        public long PersonalNumber { get; set; }
        public string PersonalName { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string CostCenter { get; set; }
        public long CardNumber { get; set; }
    }
}