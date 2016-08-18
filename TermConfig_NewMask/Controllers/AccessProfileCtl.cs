using DevExpress.Web; 
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TermConfig_NewMask.Dtos;
using TermConfig_NewMask.ViewModels;

namespace TermConfig_NewMask.Controllers
{
    public class AccessProfileCtl
    {
        public void BindProfilesToGrid(ASPxGridView grdZuttritProfileTimeFrames, ASPxGridViewCustomCallbackEventArgs e)
        {
            CurrentProfilesContainer currentProfilesContainer = null;
            string[] parameters = e.Parameters.Split('*');
            if (parameters[0] == "p")
            {
                if (HttpContext.Current.Session["CurrentProfilesContainer"] != null)
                {
                    currentProfilesContainer = (CurrentProfilesContainer)HttpContext.Current.Session["CurrentProfilesContainer"];
                    int selectedProfileIndex;
                    if (int.TryParse(parameters[1], out selectedProfileIndex))
                    {
                        selectedProfileIndex--;
                        if (currentProfilesContainer.CurrentProfiles.Count > 0 && selectedProfileIndex >= 0 && currentProfilesContainer.CurrentProfiles.Count >= selectedProfileIndex)
                        {
                            currentProfilesContainer.CurrentProfiles[selectedProfileIndex].ProfilesCount = currentProfilesContainer.CurrentProfiles.Count;
                            //currentProfilesContainer.currentProfiles[selectedProfileIndex].CurrentSelectedProfile = selectedProfileIndex + 1;
                            currentProfilesContainer.CurrentSelectedProfile = selectedProfileIndex + 1;
                            //selectedProfile = currentProfiles[selectedProfileIndex];

                            grdZuttritProfileTimeFrames.DataSource = currentProfilesContainer.CurrentProfiles[selectedProfileIndex].TimeFrames;
                            grdZuttritProfileTimeFrames.DataBind();
                        }
                    }
                }
                return;
            }
            int termId = 0;
            int.TryParse(e.Parameters, out termId);

            TerminalAccessProfilesViewModel terminalProfilesViewModel = new TerminalAccessProfilesViewModel();
            TerminalConfigRepository _termRepository = new TerminalConfigRepository();
            currentProfilesContainer = new CurrentProfilesContainer();

            var terminalConfig = _termRepository.GetTerminalConfigbyTermID(termId);

            if (terminalConfig == null)
            {
                //selectedProfile = new TerminalAccessProfilesDto();
                  return;
            }

            List<TerminalAccessProfilesDto> terminalProfiles = terminalProfilesViewModel.GetTerminalAccessProfileTimeZones(terminalConfig.ID);

            if (terminalProfiles.Count > 0)
            {
                terminalProfiles[0].ProfilesCount = terminalProfiles.Count;
                terminalProfiles[0].CurrentSelectedProfile = 1;
               // selectedProfile = terminalProfiles[0];

                grdZuttritProfileTimeFrames.DataSource = terminalProfiles[0].TimeFrames;
                grdZuttritProfileTimeFrames.DataBind();

                currentProfilesContainer.CurrentProfiles = terminalProfiles;
                currentProfilesContainer.CurrentSelectedProfile = 1;
            }

            //else
            //{
            //    currentProfilesContainer.currentProfiles =new List<TerminalAccessProfilesDto> { new TerminalAccessProfilesDto() };
            //}
            HttpContext.Current.Session["CurrentProfilesContainer"] = currentProfilesContainer;
        }

        public TerminalAccessProfilesDto GetCurrentSelectedTimeAccessProfile()
        {
            TerminalAccessProfilesDto selectedProfile = null;
            if (HttpContext.Current.Session["CurrentProfilesContainer"]!=null)
            {
                try {
                    CurrentProfilesContainer currentProfilesContainer = (CurrentProfilesContainer)HttpContext.Current.Session["CurrentProfilesContainer"];
                    if (currentProfilesContainer.CurrentProfiles!=null&&currentProfilesContainer.CurrentProfiles.Count > 0)
                    {
                        selectedProfile = currentProfilesContainer.CurrentProfiles[currentProfilesContainer.CurrentSelectedProfile - 1];
                        selectedProfile.ProfilesCount = currentProfilesContainer.CurrentProfiles.Count;
                        selectedProfile.CurrentSelectedProfile = currentProfilesContainer.CurrentSelectedProfile;
                    }
                    else
                    {
                        selectedProfile = new TerminalAccessProfilesDto();
                    }
                }
                catch (Exception)
                { }
            }
            else
            {
                selectedProfile=new TerminalAccessProfilesDto();
            }
            return selectedProfile;
        }
        class CurrentProfilesContainer
        {
            public int CurrentSelectedProfile { get; set; }
            public List<TerminalAccessProfilesDto> CurrentProfiles { get; set; }
        }
    }
}