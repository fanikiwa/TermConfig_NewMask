using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalConfigFunctionKeyDto
    {
        public long ID { get; set; }
        public long TerminalConfigID { get; set; }
        public Nullable<int> FunctionKeyNr { get; set; }
        public string FunctionKeyText1 { get; set; }
        public string FunctionKeyText2 { get; set; }
        public string FunctionKeyText3 { get; set; }
        public string FunctionKeyText4 { get; set; }
        public string FunctionKeyText5 { get; set; }
        public string FunctionKeyText6 { get; set; }
        public string FunctionKeyText7 { get; set; }
        public string FunctionKeyText8 { get; set; }
    }
}