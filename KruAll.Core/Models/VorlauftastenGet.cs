using System.Collections.Generic;
using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Reflection;

namespace KruAll.Core.Models
{
    public class VorlauftastenGet
    {
        public void GetVorlauftasten()
        {
            var connectiionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();
            var commDBTPRepo = new Repositories.VorlauftastenRepository();
            commDBTPRepo.DeleteAllVorlauftasten();

            foreach (int clientKey in connectiionStrings.Keys)
            {
                var mandantTPRepo = new VorlauftastenRepository(connectiionStrings[clientKey]);
                List<Models.ZITERM_V20_Vorlauftasten> vorlauftastenList = mandantTPRepo.GetAllVorlauftasten();


                foreach (Models.ZITERM_V20_Vorlauftasten vorlauftasten in vorlauftastenList)
                {
                    Models.Vorlauftasten commDBVorlauftasten = new Models.Vorlauftasten();

                    commDBVorlauftasten.ID = vorlauftasten.ID;
                    commDBVorlauftasten.V_AbSofort = vorlauftasten.V_AbSofort;
                    commDBVorlauftasten.V_Abw_nr = vorlauftasten.V_Abw_nr;
                    commDBVorlauftasten.V_BeginnSollzeit = vorlauftasten.V_BeginnSollzeit;
                    commDBVorlauftasten.V_Bezeichnung = vorlauftasten.V_Bezeichnung;
                    commDBVorlauftasten.V_Fortschreiben = vorlauftasten.V_Fortschreiben;
                    commDBVorlauftasten.V_Funk = vorlauftasten.V_Funk;
                    commDBVorlauftasten.V_Grenzbuch = vorlauftasten.V_Grenzbuch;
                    commDBVorlauftasten.V_KeineEinbuchung = vorlauftasten.V_KeineEinbuchung;
                    commDBVorlauftasten.V_K_nr = vorlauftasten.V_K_nr;
                    commDBVorlauftasten.V_NichtAnTerminalSenden = vorlauftasten.V_NichtAnTerminalSenden;
                    commDBVorlauftasten.V_NOISTVERR = vorlauftasten.V_NOISTVERR;
                    commDBVorlauftasten.V_Nr = vorlauftasten.V_Nr;
                    commDBVorlauftasten.V_Pausen = vorlauftasten.V_Pausen;
                    commDBVorlauftasten.V_SollAZ = vorlauftasten.V_SollAZ;
                    commDBVorlauftasten.V_StartRegelarbeitszeit = vorlauftasten.V_StartRegelarbeitszeit;
                    commDBVorlauftasten.V_StartWertungsrahmen = vorlauftasten.V_StartWertungsrahmen;
                    commDBVorlauftasten.V_Tarif = vorlauftasten.V_Tarif;
                    commDBVorlauftasten.V_WertungEnd = vorlauftasten.V_WertungEnd;
                    commDBVorlauftasten.Mandant = clientKey;

                    commDBTPRepo.AddNewVorlauftasten(commDBVorlauftasten);
                }

                commDBTPRepo.SaveVorlauftasten();
            }
        }
    }
}
