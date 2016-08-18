using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.Dtos
{
    public class TerminalAccessProfilesDto
    {
        public TerminalAccessProfilesDto()
        {
            this.TimeFrames = new List<TerminalProfileTimeFrameDto>();
        }
        public int GroupNumber { get; set; }
        public string GroupDescription { get; set; }
        public int ProfileNumber { get; set; }
        public string ProfileID { get; set; }
        public string ProfileDescription { get; set; }
        public string Memo { get; set; }
        public List<TerminalProfileTimeFrameDto> TimeFrames { get; set; }
        public int ProfilesCount { get; set; }
        public int CurrentSelectedProfile { get; set; }
    }
}