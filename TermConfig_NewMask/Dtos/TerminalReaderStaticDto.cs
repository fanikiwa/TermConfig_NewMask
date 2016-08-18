using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalReaderStaticDto
    {
        public TerminalReaderStaticDto() { }

        public string Description { get; set; }
        public int ID { get; set; }
        public string Image { get; set; }
        public string Installation { get; set; }
        public string ReaderType { get; set; }

        public string SpecialID
        {
            get
            {
                return String.Format("{0}#{1}", ID, GetUiSec(ReaderType));
            }
        }

        public String GetUiSec(string readerType)
        {
            if(readerType.Equals("IO Box"))
            {
                return "2";
            }
            return "1";
        }
    }
}