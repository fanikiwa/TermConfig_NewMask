using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalReadersDto
    {
        #region Constructor
        public TerminalReadersDto() { }

        #endregion

        #region Properties
        public long ID { get; set; }

        [Required(ErrorMessage = "Terminal Number is required.")]
        public long? TermID { get; set; }
        public int ReaderID { get; set; }
        public int? ReaderNr { get; set; }
        public int? Direction { get; set; }
        public string DirectionDescription { get; set; }
        public int? Status { get; set; }
        public int? RelayTime { get; set; }
        public string Verify { get; set; }
        public string Category { get; set; }
        public long BeforeAlarm { get; set; }
        public long AlarmFrom { get; set; }
        public string ReaderType { get; set; }
        public string  Name { get; set; }
        public string Memo { get; set; }
        public int? Lock { get; set; }
        public int? Delay { get; set; }
        public string LockDescription { get; set; }
        public string StatusDescription { get; set; }
        public string ReaderImage { get; set; }
        public string ReaderInfo { get; set; }

        #endregion
    }
}