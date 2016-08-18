using System.Collections.Generic;
using KruAll.Core.Models;
using KruAll.Core.Models;
using System;
using System.Reflection;

namespace KruAll.Core.Models
{
    public class TagesprogrammeGet
    {
        public void GetTagesprogramme()
        {
            var connectiionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();
            var commDBTPRepo = new Repositories.Tagesprogramme_Repository();
            commDBTPRepo.DeleteTagesprogrammes();

            foreach (int clientKey in connectiionStrings.Keys)
            {
                var mandantTPRepo = new Repositories.PZE.Tagesprogramme_Repository(connectiionStrings[clientKey]);
                List<Models.Tagesprogramme> mandantTPList = mandantTPRepo.GetAllTagesprogramme();


                foreach (Models.Tagesprogramme mandantTP in mandantTPList)
                {
                    Models.Tprogramme commDBTP = new Models.Tprogramme();

                    commDBTP.TP_Nr = mandantTP.TP_Nr;
                    commDBTP.TP_Bezeichnung = mandantTP.TP_Bezeichnung;
                    commDBTP.TP_Abrechnung = mandantTP.TP_Abrechnung;
                    commDBTP.LastAccess = mandantTP.LastAccess;
                    commDBTP.TP_Grp_Nr = mandantTP.TP_Grp_Nr;
                    commDBTP.tp_script = mandantTP.tp_script;
                    commDBTP.RAS_ERSTEBUCHUNG = mandantTP.RAS_ERSTEBUCHUNG;
                    commDBTP.PEPSchicht = mandantTP.PEPSchicht;
                    commDBTP.TP_Ber_Nr = mandantTP.TP_Ber_Nr;
                    commDBTP.TP_Std_Nr = mandantTP.TP_Std_Nr;
                    commDBTP.TP_Abt_Nr = mandantTP.TP_Abt_Nr;
                    commDBTP.TP_Man_Nr = mandantTP.TP_Man_Nr;
                    commDBTP.Mandant = clientKey;

                    commDBTPRepo.AddNewTagesprogramme(commDBTP);
                }

                commDBTPRepo.SaveTagesprogrammes();
            }
        }
    }
}
