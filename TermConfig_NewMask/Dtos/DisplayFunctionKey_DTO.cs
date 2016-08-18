using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TermConfig_NewMask.TerminalCommunication.VirtualDataObjects;

namespace TermConfig_NewMask.Dtos
{
    public class DisplayFunctionKey_DTO
    {
        public DisplayFunctionKey_DTO()
        {
            this.FunctionKeyNumber = 0;
            this.FunctionKeyType = 0;
            this.functionKeyItems = new List<VirtualFunctionKeyItem>();
        }
        public int FunctionKeyNumber { get; set; }
        public int FunctionKeyType { get; set; }
        public List<VirtualFunctionKeyItem> functionKeyItems { get; set; }
    }
}