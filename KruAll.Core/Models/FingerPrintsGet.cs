using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Models;

namespace KruAll.Core.Models
{
    public class FingerPrintsGet
    {
        public void GetPersonalFingerPrintsFromClientDatabase()
        {
            KruAll.Core.Repositories.FingerPrintsRepositoy commDBfingerPrintRepository = new Repositories.FingerPrintsRepositoy();

            var connectiionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();

            int currentCommStatus = 0;

            int.TryParse(commDBfingerPrintRepository.GetAllFingerPrint().Max(x => x.CommStatus).ToString(), out currentCommStatus);

            foreach (int clientKey in connectiionStrings.Keys)
            {
                var fingerPrintsRepo = new KruAll.Core.Repositories.PZE.FingerPrintsRepositoy(connectiionStrings[clientKey]);

                List<KruAll.Core.Models.FingerPrint> clientFingerprints = fingerPrintsRepo.GetAllFingerPrint();

                foreach (var fingerPrint in clientFingerprints)
                {
                    currentCommStatus = currentCommStatus + 1;
                    FingerPrint newfingerPrint = new FingerPrint();

                    newfingerPrint.Pers_Nr = fingerPrint.PersNr;
                    newfingerPrint.FingerIndex = fingerPrint.FIndex;
                    newfingerPrint.Template = fingerPrint.Template;
                    newfingerPrint.Mandant = clientKey;
                    newfingerPrint.CommAction = 1;
                    newfingerPrint.CommStatus = currentCommStatus;

                    commDBfingerPrintRepository.AddOrUpdateFingerPrint(newfingerPrint);
                }

                commDBfingerPrintRepository.SaveFingerPrints();
            }
        }
    }
}
