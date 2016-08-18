using KruAll.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KruAll.Core.Models
{
    public class PersonalstammGet
    {
        public void GetPersonalDataFromClientDatabases()
        {
            //List<string> connectiionStrings = new List<string>();
            KruAll.Core.Repositories.PersonalstammRepository commDBPersonalRepository = new KruAll.Core.Repositories.PersonalstammRepository();
            List<KruAll.Core.Models.Personalstamm> commDBPersonel = commDBPersonalRepository.GetAllPersonal().ToList();

            var connectiionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();

            foreach (int clientKey in connectiionStrings.Keys)
            {
                var persRepo = new KruAll.Core.Repositories.PZE.PersonalstammRepository(connectiionStrings[clientKey]);

                List<KruAll.Core.Models.CommDB_Personalstamm> clientPersonel = persRepo.GetAllPersonal().Where(x => x.Pers_Ausweis_Nr > 0).ToList();

                int currentCommStatus = 0;

                int.TryParse(commDBPersonel.Max(x => x.CommStatus).ToString(), out currentCommStatus);

                foreach (var personal in clientPersonel)
                {
                    currentCommStatus = currentCommStatus + 1;
                    Personalstamm newPersonal = new Personalstamm();

                    newPersonal.Pers_Nr = personal.Pers_Nr;
                    newPersonal.Pers_Ausweis_Nr = personal.Pers_Ausweis_Nr;
                    newPersonal.Pers_Name1 = personal.Pers_Name1;
                    newPersonal.Pers_Name2 = personal.Pers_Name2;
                    newPersonal.Pers_Werk = personal.Pers_Werk;
                    newPersonal.Pers_Kostenstelle = personal.Pers_Kostenstelle;
                    newPersonal.Pers_Abteilung = personal.Pers_Abteilung;
                    newPersonal.Pers_Lohngruppe = personal.Pers_Lohngruppe;
                    newPersonal.Pers_Tarif = personal.Pers_Tarif;
                    newPersonal.Pers_Sollzeitkalender = personal.Pers_Sollzeitkalender;
                    newPersonal.Pers_Standardkalender = personal.Pers_Standardkalender;
                    newPersonal.Pers_Ohne_Abrechnung = Convert.ToInt32(personal.Pers_Ohne_Abrechnung);
                    newPersonal.Pers_PinCode = personal.Pers_PinCode;
                    newPersonal.Pers_Zutritt = personal.Pers_Zutritt;
                    newPersonal.Pers_QualNr = personal.Pers_QualNr;
                    newPersonal.Pers_QualGrpNr = personal.Pers_QualGrpNr;
                    newPersonal.Pers_Ausweis_nr2 = personal.Pers_Ausweis_nr2;
                    newPersonal.Pers_Zutritt2 = personal.Pers_Zutritt2;
                    newPersonal.Mandant = clientKey;
                    newPersonal.TerminalGroup = 1;
                    newPersonal.CommAction = 1;
                    newPersonal.CommStatus = currentCommStatus;
                    commDBPersonalRepository.AddOrUpdatePersonal(newPersonal);
                }
                commDBPersonalRepository.SaveAllPersonal();
            }
        }
    }
}
