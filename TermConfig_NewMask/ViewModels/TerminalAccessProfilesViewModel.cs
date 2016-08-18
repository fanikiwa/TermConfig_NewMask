using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KruAll.Core.Repositories;
using KruAll.Core.Models;
using TermConfig_NewMask.Dtos;

namespace TermConfig_NewMask.ViewModels
{
    public class TerminalAccessProfilesViewModel
    {
        private TerminalAccessProfilesRepository profilesRepository = null;

        public TerminalAccessProfilesViewModel()
        {
            profilesRepository = new TerminalAccessProfilesRepository();
        }

        public List<TerminalAccessProfilesDto> GetTerminalAccessProfileTimeZones(long terminalSerialNumber)
        {
            List<TerminalAccessProfilesDto> terminalAccessProfilesDTOs = new List<TerminalAccessProfilesDto>();
            TerminalAccessProfilesDto terminalAccessProfilesDTO = null;
            TerminalProfileTimeFrameDto terminalProfileTimeFrameDTO = null;
            int currentProfileNumber = 0;
            bool currentProfileChanged = false;

            var terminalProfiles = profilesRepository.GetAllTerminalAccessProfiles(terminalSerialNumber);

            foreach(View_TerminalAccessProfiles accessProfile in terminalProfiles)
            {
                currentProfileChanged = accessProfile.AccessProfileNo != currentProfileNumber;

                if (currentProfileChanged)
                {
                    terminalAccessProfilesDTO = new TerminalAccessProfilesDto();
                    terminalAccessProfilesDTO.GroupNumber = accessProfile.AccessGroupNumber;
                    terminalAccessProfilesDTO.GroupDescription = accessProfile.AccessGroupName;
                    terminalAccessProfilesDTO.ProfileNumber = accessProfile.AccessProfileNo;
                    terminalAccessProfilesDTO.ProfileID = accessProfile.AccessProfileID;
                    terminalAccessProfilesDTO.ProfileDescription = accessProfile.AccessDescription;
                    terminalAccessProfilesDTO.Memo = accessProfile.Memo;
                    terminalAccessProfilesDTOs.Add(terminalAccessProfilesDTO);
                    currentProfileNumber = accessProfile.AccessProfileNo;
                }

                terminalProfileTimeFrameDTO = new TerminalProfileTimeFrameDto
                {   ProfilAktiv = true,
                    ID = accessProfile.TimeframeID,
                    MonFrom = accessProfile.MonFrom,
                    MonTo = accessProfile.MonTo,
                    TueFrom = accessProfile.TueFrom,
                    TueTo = accessProfile.TueTo,
                    WedFrom = accessProfile.WedFrom,
                    WedTo = accessProfile.WedTo,
                    ThurFrom = accessProfile.ThurFrom,
                    ThurTo = accessProfile.ThurTo,
                    FriFrom = accessProfile.FriFrom,
                    FriTo = accessProfile.FriTo,
                    SatFrom = accessProfile.SatFrom,
                    SatTo = accessProfile.SatTo,
                    SunFrom = accessProfile.SunFrom,
                    SunTo = accessProfile.SunTo
                };

                terminalAccessProfilesDTO.TimeFrames.Add(terminalProfileTimeFrameDTO);

            }

            return terminalAccessProfilesDTOs;
        }
    }
}
