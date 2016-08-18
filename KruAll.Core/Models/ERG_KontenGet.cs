using System.Collections.Generic;
using System;
using System.Reflection;
using KruAll.Core.Models;
using System.Linq;
using System.Threading.Tasks;

namespace KruAll.Core.Models
{
    public class ERG_KontenGet
    {
        public ERG_KontenGet() { }
        List<int> accountSettings = new List<int>();

        public void GetERG_Konten()
        {
            List<Task> mandatenTasksList = new List<Task>();
            var connectionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();
            List<Personalstamm> persList = new Repositories.PersonalstammRepository().GetAllPersonal();
            var commDBKontenRepo = new Repositories.ERG_KontenRepository();
            commDBKontenRepo.DeleteKontens();
            commDBKontenRepo.SaveKontens();

            

            foreach (int clientNumber in connectionStrings.Keys)
            {
                bool diffZeitLoaded = false;
                //var mandantRealTimeRepo = new Repositories.PZE.RealTimeSettingsRepository(connectionStrings[clientNumber]);
                //List<Models.RealTimeSetting> realTimeSettingsList = mandantRealTimeRepo.GetAllRealTimeSettings();
                //Models.RealTimeSetting realTimeSetting = realTimeSettingsList.FirstOrDefault() ?? new Models.RealTimeSetting();


                //for (int count = 0; count < 4; count++)
                //{
                //    int? accountNr = count == 0 ? realTimeSetting.DisplayAccount1 :
                //        count == 1 ? realTimeSetting.DisplayAccount2 :
                //        count == 2 ? realTimeSetting.DisplayAccount3 :
                //        count == 3 ? realTimeSetting.DisplayAccount4 : 0;

                //    if ((accountNr ?? 0) > 0)
                //    {
                //        diffZeitLoaded = accountNr == 5;
                //        mandatenTasksList.Add(Task.Factory.StartNew((Object obj) =>
                //        {
                //            var data = (dynamic)obj;
                //            KruAll.Core.Models.ERG_KontenGet.WriteERG_Konten((int)data.accountNr, (int)data.mandantenNr,
                //                Convert.ToString(data.connString), (List<KruAll.Core.Models.Personalstamm>)data.persList);
                //        }, new { accountNr = accountNr, mandantenNr = clientNumber, connString = connectionStrings[clientNumber], persList = persList },
                //        TaskCreationOptions.LongRunning));
                //    }
                //}

                if (!diffZeitLoaded)
                {
                    mandatenTasksList.Add(Task.Factory.StartNew((Object obj) =>
                    {
                        var data = (dynamic)obj;
                        KruAll.Core.Models.ERG_KontenGet.WriteERG_Konten((int)data.accountNr, (int)data.mandantenNr,
                            Convert.ToString(data.connString), (List<KruAll.Core.Models.Personalstamm>)data.persList);
                    }, new { accountNr = 5, mandantenNr = clientNumber, connString = connectionStrings[clientNumber], persList = persList },
                    TaskCreationOptions.LongRunning));

                    mandatenTasksList.Add(Task.Factory.StartNew((Object obj) =>
                    {
                        var data = (dynamic)obj;
                        KruAll.Core.Models.ERG_KontenGet.WriteERG_Konten((int)data.accountNr, (int)data.mandantenNr,
                            Convert.ToString(data.connString), (List<KruAll.Core.Models.Personalstamm>)data.persList);
                    }, new { accountNr = 8, mandantenNr = clientNumber, connString = connectionStrings[clientNumber], persList = persList },
                    TaskCreationOptions.LongRunning));
                }
            }
        }

        public static void WriteERG_Konten(int accountNr, int mandantenNr, string connString, List<Personalstamm> persList)
        {
            var mandantKontenRepo = new Repositories.PZE.ERG_KontenRepository(connString);
            List<Personalstamm> mandantPersList = persList.Any(pers => pers.Mandant == mandantenNr) ?
                persList.Where(pers => pers.Mandant == mandantenNr).ToList() : new List<Personalstamm>();
            List<Models.ERG_Konten> mandantERGKontenList = mandantKontenRepo.GetAllKonten(accountNr, mandantPersList.Select(pers => pers.Pers_Nr).ToArray());

            var commDBKontenRepo = new Repositories.ERG_KontenRepository();

            int thisYear = DateTime.Now.Year, lastYear = DateTime.Now.Year;
            int thisMonth = DateTime.Now.Month, lastMonth = DateTime.Now.Month;

            if (DateTime.Now.Year != DateTime.Now.AddDays(-1).Year)
                lastYear = DateTime.Now.AddDays(-1).Year;
            if (DateTime.Now.Month != DateTime.Now.AddDays(-1).Month)
                lastMonth = DateTime.Now.AddDays(-1).Month;


            List<Models.ERG_Konten> mandantCurrERGKontenList = mandantERGKontenList.Any(kal => kal.Kto_Jahr == thisYear && kal.Kto_Monat == thisMonth) ?
                mandantERGKontenList.Where(kal => kal.Kto_Jahr == thisYear && kal.Kto_Monat == thisMonth).ToList() : new List<Models.ERG_Konten>();
            List<Models.ERG_Konten> mandantPrevERGKontenList = mandantERGKontenList.Any(kal => kal.Kto_Jahr == lastYear && kal.Kto_Monat == lastMonth) ?
                mandantERGKontenList.Where(kal => kal.Kto_Jahr == thisYear && kal.Kto_Monat == thisMonth).ToList() : new List<Models.ERG_Konten>();

            LoadAccounts(ref commDBKontenRepo, mandantenNr, mandantCurrERGKontenList, mandantPrevERGKontenList);

            commDBKontenRepo.SaveKontens();
        }

        private static void LoadAccounts(ref Repositories.ERG_KontenRepository commDBKontenRepo, int mandantenNr, List<Models.ERG_Konten> mandantCurrERGKontenList, List<Models.ERG_Konten> mandantPrevERGKontenList)
        {
            foreach (Models.ERG_Konten mandantCurrERGKonten in mandantCurrERGKontenList)
            {
                Models.ERG_Konten mandantPrevERGKonten = mandantPrevERGKontenList.Any(ergKto => ergKto.Kto_Pers_Nr == mandantCurrERGKonten.Kto_Pers_Nr) ?
                    mandantPrevERGKontenList.FirstOrDefault(ergKto => ergKto.Kto_Pers_Nr == mandantCurrERGKonten.Kto_Pers_Nr) : new Models.ERG_Konten();

                Models.ERG_Konten commDBERGKonten = new Models.ERG_Konten();
                Double dayAccount1, dayAccount2, weekAccount1, weekAccount2, monthAccount1, monthAccount2, yearAccount1, yearAccount2;
                PropertyInfo pi = mandantCurrERGKonten.GetType().GetProperty(string.Format("T{0:0}", DateTime.Now.Day));
                dayAccount1 = Convert.ToDouble(pi.GetValue(mandantCurrERGKonten));
                pi = mandantPrevERGKonten.GetType().GetProperty(string.Format("T{0:0}", DateTime.Now.AddDays(-1).Day));
                dayAccount2 = Convert.ToDouble(pi.GetValue(mandantPrevERGKonten));
                pi = mandantCurrERGKonten.GetType().GetProperty(string.Format("W{0:0}", DateTime.Now.Day));
                weekAccount1 = Convert.ToDouble(pi.GetValue(mandantCurrERGKonten));
                pi = mandantPrevERGKonten.GetType().GetProperty(string.Format("W{0:0}", DateTime.Now.AddDays(-1).Day));
                weekAccount2 = Convert.ToDouble(pi.GetValue(mandantPrevERGKonten));
                pi = mandantCurrERGKonten.GetType().GetProperty(string.Format("M{0:0}", DateTime.Now.Day));
                monthAccount1 = Convert.ToDouble(pi.GetValue(mandantCurrERGKonten));
                pi = mandantPrevERGKonten.GetType().GetProperty(string.Format("M{0:0}", DateTime.Now.AddDays(-1).Day));
                monthAccount2 = Convert.ToDouble(pi.GetValue(mandantPrevERGKonten));
                pi = mandantCurrERGKonten.GetType().GetProperty(string.Format("J{0:0}", DateTime.Now.Day));
                yearAccount1 = Convert.ToDouble(pi.GetValue(mandantCurrERGKonten));
                pi = mandantPrevERGKonten.GetType().GetProperty(string.Format("J{0:0}", DateTime.Now.AddDays(-1).Day));
                yearAccount2 = Convert.ToDouble(pi.GetValue(mandantPrevERGKonten));

                commDBERGKonten.Kto_Index = mandantCurrERGKonten.Kto_Index;
                commDBERGKonten.Kto_K_Nr = mandantCurrERGKonten.Kto_K_Nr ?? 0;
                commDBERGKonten.Kto_Pers_Nr = mandantCurrERGKonten.Kto_Pers_Nr ?? 0;
                commDBERGKonten.Mandant = mandantenNr;
                commDBERGKonten.T1 = dayAccount1;
                commDBERGKonten.T2 = dayAccount2;
                commDBERGKonten.W1 = weekAccount1;
                commDBERGKonten.W2 = weekAccount2;
                commDBERGKonten.M1 = monthAccount1;
                commDBERGKonten.M2 = monthAccount2;
                commDBERGKonten.J1 = yearAccount1;
                commDBERGKonten.J2 = yearAccount2;

                commDBKontenRepo.AddNewKonten(commDBERGKonten);
            }
        }
    }
}
