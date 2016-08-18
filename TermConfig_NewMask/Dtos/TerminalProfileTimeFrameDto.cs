using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalProfileTimeFrameDto
    {
        public long ID { get; set; }
        public bool ProfilAktiv { get; set; }
        public DateTime MonFrom { get; set; }
        public DateTime MonTo { get; set; }
        public DateTime TueFrom { get; set; }
        public DateTime TueTo { get; set; }
        public DateTime WedFrom { get; set; }
        public DateTime WedTo { get; set; }
        public DateTime ThurFrom { get; set; }
        public DateTime ThurTo { get; set; }
        public DateTime FriFrom { get; set; }
        public DateTime FriTo { get; set; }
        public DateTime SatFrom { get; set; }
        public DateTime SatTo { get; set; }
        public DateTime SunFrom { get; set; }
        public DateTime SunTo { get; set; }        
    }
}