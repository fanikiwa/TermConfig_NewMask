using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalsConDto
    {
        #region Constructor
        public TerminalsConDto() { }

        #endregion

        #region Properties
        public long ID { get; set; }

        [Required(ErrorMessage = "Terminal Number is required.")]
        public int TermID { get; set; }
        public string TermType { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int TerminalConnectId { get; set; }
        public int TerminalInfoId { get; set; }
        public int TerminalOEMId { get; set; }

        //Maps to fields in the Terminal Connect table
        public string IPPort { get; set; }
        public string IPAddress { get; set; }
        public string Connection { get; set; }
        public bool ActiveTerminal { get; set; }
        public bool PersnoPIN { get; set; }
        public bool FPRead { get; set; }
        public bool APPosting { get; set; }
        public bool TAPPosting { get; set; }
        public string SerialNumber { get; set; }

        //Maps to fields in the Terminal Info table
        public string InfoText1 { get; set; }
        public string InfoText2 { get; set; }
        public string InfoText3 { get; set; }
        public string InfoText4 { get; set; }
        public string Functionkey1 { get; set; }
        public string Functionkey2 { get; set; }
        public string Functionkey3 { get; set; }
        public string Functionkey4 { get; set; }
        public string Functionkey5 { get; set; }
        public string Functionkey6 { get; set; }
        public string Functionkey7 { get; set; }
        public string Functionkey8 { get; set; }
        public string DoorAssign { get; set; }
        public string Memo { get; set; }

        //Maps to fields in the Terminal OEM table
        public int? TermOEMId { get; set; }
        public string TermOEMDesc { get; set; }

        #endregion
    }
}