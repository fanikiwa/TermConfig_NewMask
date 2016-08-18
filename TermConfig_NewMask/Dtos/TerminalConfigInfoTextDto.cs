using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalConfigInfoTextDto
    {
        public long ID { get; set; }
        public long TerminalConfigID { get; set; }
        public Nullable<int> InfoTextNr { get; set; }
        public string InfoText1 { get; set; }
        public string InfoText2 { get; set; }
        public string InfoText3 { get; set; }
        public string InfoText4 { get; set; }
    }
}