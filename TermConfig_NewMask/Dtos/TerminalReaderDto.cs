using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalReaderDto
    {
        public long? ID { get; set; }
        public int? ReaderID { get; set; }
        public string ReaderType { get; set; }
        public string Name { get; set; }
        public int? Direction { get; set; }
        public int? Status { get; set; }
        public int? RelayTime { get; set; }
        public string Memo { get; set; }
        public string ReaderImage { get; set; }
        public int? ReaderNr { get; set; }
        public int? Lock { get; set; }
        public int? Delay { get; set; }
    }

        
}