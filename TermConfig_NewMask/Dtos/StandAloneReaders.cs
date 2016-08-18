using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KruAll.Core.Repositories;

namespace TermConfig_NewMask.Dtos
{
    public static class StandAloneReaders
    {
        public static void AddInternalReader(long terminalID)
        {
            KruAll.Core.Models.TerminalReader terminalReader = new KruAll.Core.Models.TerminalReader()
            {
                TermID = terminalID,
                ReaderID = 1,
                ReaderInfo = "Intern",
                ReaderType = "RFID",
            };

            new TerminalReaderRepository().NewTerminalReader(terminalReader);
        }
    }
}