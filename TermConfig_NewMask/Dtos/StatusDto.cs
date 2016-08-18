using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class StatusDto
    {
        #region Constructor
        public StatusDto() { }

        #endregion

        #region Properties
        public long ID { get; set; }
        public int? StatusNr { get; set; }
        public string StatusName { get; set; }
        public string StatusNrName
        {
            get
            {
                return String.Format("{0} - {1}", StatusNr, StatusName);
            }
        }

        #endregion
    }
}