using System.Collections.Generic;
using KruAll.Core.Models;
using KruAll.Core.Models;
using System;
using System.Reflection;

namespace KruAll.Core.Models
{
    public class Tagesprogramme_SchichtenGet
    {
        public void GetTagesprogrammeSchichten()
        {
            var connectiionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();
            var commDBTPSchichtenRepo = new Repositories.Tagesprogramme_Schichten_Repository();
            commDBTPSchichtenRepo.DeleteTagesprogrammeSchichtens();

            foreach (int clientKey in connectiionStrings.Keys)
            {
                var mandantTPSchichtenRepo = new Repositories.PZE.Tagesprogramme_Schichten_Repository(connectiionStrings[clientKey]);
                List<Models.Tagesprogramme_Schichten> mandantTPSchichtenList = mandantTPSchichtenRepo.GetAllTagesprogrammeSchichten();


                foreach (Models.Tagesprogramme_Schichten mandantTPSchichten in mandantTPSchichtenList)
                {
                    Models.Tprogramme_Schichten commDBTPSchichten = new Models.Tprogramme_Schichten();

                    commDBTPSchichten.S_Index = mandantTPSchichten.S_Index;
                    commDBTPSchichten.S_TP_Nr = mandantTPSchichten.S_TP_Nr;
                    commDBTPSchichten.S_Nr = mandantTPSchichten.S_Nr;
                    commDBTPSchichten.S_Bezeichnung = mandantTPSchichten.S_Bezeichnung;
                    commDBTPSchichten.S_AutoSWechsel = mandantTPSchichten.S_AutoSWechsel;
                    commDBTPSchichten.S_Fenster_Von = mandantTPSchichten.S_Fenster_Von;
                    commDBTPSchichten.S_Fenster_Bis = mandantTPSchichten.S_Fenster_Bis;
                    commDBTPSchichten.S_Sollzeit = mandantTPSchichten.S_Sollzeit;
                    commDBTPSchichten.S_Wertung_Von = mandantTPSchichten.S_Wertung_Von;
                    commDBTPSchichten.S_Wertung_Bis = mandantTPSchichten.S_Wertung_Bis;
                    commDBTPSchichten.S_Rahmen_Von = mandantTPSchichten.S_Rahmen_Von;
                    commDBTPSchichten.S_Rahmen_Bis = mandantTPSchichten.S_Rahmen_Bis;
                    commDBTPSchichten.S_Kern_Von = mandantTPSchichten.S_Kern_Von;
                    commDBTPSchichten.S_Kern_Bis = mandantTPSchichten.S_Kern_Bis;
                    commDBTPSchichten.S_Arbeitstag = mandantTPSchichten.S_Arbeitstag;
                    commDBTPSchichten.S_Fehlgrund = mandantTPSchichten.S_Fehlgrund;
                    commDBTPSchichten.S_Pausen = mandantTPSchichten.S_Pausen;
                    commDBTPSchichten.S_Raster = mandantTPSchichten.S_Raster;
                    commDBTPSchichten.S_Abrechnung = mandantTPSchichten.S_Abrechnung;
                    commDBTPSchichten.S_Schichtende = mandantTPSchichten.S_Schichtende;
                    commDBTPSchichten.S_AushilfeMaxAnw = mandantTPSchichten.S_AushilfeMaxAnw;
                    commDBTPSchichten.s_plan_buchen = mandantTPSchichten.s_plan_buchen;
                    commDBTPSchichten.s_plan_von = mandantTPSchichten.s_plan_von;
                    commDBTPSchichten.s_plan_bis = mandantTPSchichten.s_plan_bis;
                    commDBTPSchichten.S_PLANZEIT = mandantTPSchichten.S_PLANZEIT;
                    commDBTPSchichten.S_TARIF = mandantTPSchichten.S_TARIF;
                    commDBTPSchichten.S_SollFromLastSBuch = mandantTPSchichten.S_SollFromLastSBuch;
                    commDBTPSchichten.S_WriteAtNextDay = mandantTPSchichten.S_WriteAtNextDay;
                    commDBTPSchichten.S_SollIst = mandantTPSchichten.S_SollIst;
                    commDBTPSchichten.S_PauseAbMin = mandantTPSchichten.S_PauseAbMin;
                    commDBTPSchichten.S_NurKontingent = mandantTPSchichten.S_NurKontingent;
                    commDBTPSchichten.Vordergrund = mandantTPSchichten.Vordergrund;
                    commDBTPSchichten.Hintergrund = mandantTPSchichten.Hintergrund;
                    commDBTPSchichten.S_Zuschl1Bis = mandantTPSchichten.S_Zuschl1Bis;
                    commDBTPSchichten.S_Zuschl1Von = mandantTPSchichten.S_Zuschl1Von;
                    commDBTPSchichten.S_Zuschl2Bis = mandantTPSchichten.S_Zuschl2Bis;
                    commDBTPSchichten.S_Zuschl2Von = mandantTPSchichten.S_Zuschl2Von;
                    commDBTPSchichten.S_Zuschl3Bis = mandantTPSchichten.S_Zuschl3Bis;
                    commDBTPSchichten.S_Zuschl3Von = mandantTPSchichten.S_Zuschl3Von;
                    commDBTPSchichten.S_Zuschl1Lohn = mandantTPSchichten.S_Zuschl1Lohn;
                    commDBTPSchichten.S_Zuschl2Lohn = mandantTPSchichten.S_Zuschl2Lohn;
                    commDBTPSchichten.S_Zuschl3Lohn = mandantTPSchichten.S_Zuschl3Lohn;
                    commDBTPSchichten.S_memo = mandantTPSchichten.S_memo;
                    commDBTPSchichten.Mandant = clientKey;

                    commDBTPSchichtenRepo.AddNewTagesprogrammeSchichten(commDBTPSchichten);
                }

                commDBTPSchichtenRepo.SaveTagesprogrammeSchichtens();
            }
        }
    }
}
