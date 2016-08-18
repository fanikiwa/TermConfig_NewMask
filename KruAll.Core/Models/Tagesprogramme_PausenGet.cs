using System.Collections.Generic;
using KruAll.Core.Models;
using KruAll.Core.Models;
using System;
using System.Reflection;

namespace KruAll.Core.Models
{
    public class Tagesprogramme_PausenGet
    {
        public void GetTagesprogrammePausen()
        {
            var connectiionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();
            var commDBTPPausenRepo = new Repositories.Tagesprogramme_Pausen_Repository();
            commDBTPPausenRepo.DeleteTagesprogrammePausens();

            foreach (int clientKey in connectiionStrings.Keys)
            {
                var mandantTPPausenRepo = new Tagesprogramme_Pausen_Repository(connectiionStrings[clientKey]);
                List<Models.Tagesprogramme_Pausen> mandantTPPausenList = mandantTPPausenRepo.GetAllTagesprogrammePausen();

                foreach (Models.Tagesprogramme_Pausen mandantTPPausen in mandantTPPausenList)
                {
                    Models.Tprogramme_Pausen commDBTPPausen = new Models.Tprogramme_Pausen();

                    commDBTPPausen.Paus_Nr = mandantTPPausen.Paus_Nr;
                    commDBTPPausen.Paus_TP_Nr = mandantTPPausen.Paus_TP_Nr;
                    commDBTPPausen.Paus_S_Nr = mandantTPPausen.Paus_S_Nr;
                    commDBTPPausen.Paus_Typ = mandantTPPausen.Paus_Typ;
                    commDBTPPausen.Paus_Von = mandantTPPausen.Paus_Von;
                    commDBTPPausen.Paus_Bis = mandantTPPausen.Paus_Bis;
                    commDBTPPausen.Paus_Zeit = mandantTPPausen.Paus_Zeit;
                    commDBTPPausen.Paus_Min = mandantTPPausen.Paus_Min;
                    commDBTPPausen.Paus_Max = mandantTPPausen.Paus_Max;
                    commDBTPPausen.Mandant = clientKey;

                    commDBTPPausenRepo.AddNewTagesprogrammePausen(commDBTPPausen);
                }

                commDBTPPausenRepo.SaveTagesprogrammePausens();
            }
        }
    }
}
