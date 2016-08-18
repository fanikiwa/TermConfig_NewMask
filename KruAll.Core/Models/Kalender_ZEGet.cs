using System.Collections.Generic;
using KruAll.Core.Models;
using KruAll.Core.Models;
using System;
using System.Reflection;
using System.Linq;

namespace KruAll.Core.Models
{
    public class Kalender_ZEGet
    {

        public void GetKalender_ZE()
        {
            var connectiionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();
            var commDBKalenderRepo = new Repositories.Kalender_ZERepository();
            commDBKalenderRepo.DeleteKalenders();

            foreach (int clientKey in connectiionStrings.Keys)
            {
                var mandantKalenderRepo = new Repositories.PZE.Kalender_ZERepository(connectiionStrings[clientKey]);
                List<Models.Kalender_ZE> mandantKalZEList = mandantKalenderRepo.GetAllKalender();

                int thisYear = DateTime.Now.Year, lastYear = DateTime.Now.Year;
                int thisMonth = DateTime.Now.Month, lastMonth = DateTime.Now.Month;

                if (DateTime.Now.Year != DateTime.Now.AddDays(-1).Year)
                    lastYear = DateTime.Now.AddDays(-1).Year;
                if (DateTime.Now.Month != DateTime.Now.AddDays(-1).Month)
                    lastMonth = DateTime.Now.AddDays(-1).Month;


                List<Models.Kalender_ZE> mandantCurrKalZEList = mandantKalZEList.Any(kal => kal.Jahr == thisYear && kal.Monat == thisMonth) ?
                    mandantKalZEList.Where(kal => kal.Jahr == thisYear && kal.Monat == thisMonth).ToList() : new List<Models.Kalender_ZE>();
                List<Models.Kalender_ZE> mandantPrevKalZEList = mandantKalZEList.Any(kal => kal.Jahr == lastYear && kal.Monat == lastMonth) ?
                    mandantKalZEList.Where(kal => kal.Jahr == thisYear && kal.Monat == thisMonth).ToList() : new List<Models.Kalender_ZE>();

                LoadKalenders(ref commDBKalenderRepo, clientKey, mandantCurrKalZEList, mandantPrevKalZEList);

                commDBKalenderRepo.SaveKalenders();
            }
        }

        private static void LoadKalenders(ref Repositories.Kalender_ZERepository commDBKalenderRepo, int clientKey, List<Models.Kalender_ZE> mandantCurrKalZEList, List<Models.Kalender_ZE> mandantPrevKalZEList)
        {
            foreach (Models.Kalender_ZE mandantCurrKalZE in mandantCurrKalZEList)
            {
                Models.Kalender_ZE mandantPrevKalZE = mandantPrevKalZEList.Any(kal => kal.Pers_nr == mandantCurrKalZE.Pers_nr) ?
                    mandantPrevKalZEList.FirstOrDefault(kal => kal.Pers_nr == mandantCurrKalZE.Pers_nr) : new Models.Kalender_ZE();

                Models.Kalender_ZE commDBKalZE = new Models.Kalender_ZE();
                string todayTP, yesterdayTP, todaySchicht, yesterdaySchicht, todayABW, yesterdayABW;
                PropertyInfo pi = mandantCurrKalZE.GetType().GetProperty(string.Format("TP{0:0}", DateTime.Now.Day));
                todayTP = Convert.ToString(pi.GetValue(mandantCurrKalZE));
                pi = mandantPrevKalZE.GetType().GetProperty(string.Format("TP{0:0}", DateTime.Now.AddDays(-1).Day));
                yesterdayTP = Convert.ToString(pi.GetValue(mandantPrevKalZE));
                pi = mandantCurrKalZE.GetType().GetProperty(string.Format("Schicht{0:0}", DateTime.Now.Day));
                todaySchicht = Convert.ToString(pi.GetValue(mandantCurrKalZE));
                pi = mandantPrevKalZE.GetType().GetProperty(string.Format("Schicht{0:0}", DateTime.Now.AddDays(-1).Day));
                yesterdaySchicht = Convert.ToString(pi.GetValue(mandantPrevKalZE));
                pi = mandantCurrKalZE.GetType().GetProperty(string.Format("abw{0:0}", DateTime.Now.Day));
                todayABW = Convert.ToString(pi.GetValue(mandantCurrKalZE));
                pi = mandantPrevKalZE.GetType().GetProperty(string.Format("abw{0:0}", DateTime.Now.AddDays(-1).Day));
                yesterdayABW = Convert.ToString(pi.GetValue(mandantPrevKalZE));

                commDBKalZE.Jahr = mandantCurrKalZE.Jahr ?? (short)DateTime.Now.Year;
                commDBKalZE.Kal_nr = mandantCurrKalZE.Kal_nr;
                commDBKalZE.Monat = mandantCurrKalZE.Monat ?? (short)DateTime.Now.Month;
                commDBKalZE.Pers_nr = mandantCurrKalZE.Pers_nr ?? 0;
                commDBKalZE.TP1 = todayTP;
                commDBKalZE.TP2 = yesterdayTP;
                commDBKalZE.Schicht1 = todaySchicht;
                commDBKalZE.Schicht2 = yesterdaySchicht;
                commDBKalZE.ABW1 = todayABW;
                commDBKalZE.ABW2 = yesterdayABW;
                commDBKalZE.Mandant = clientKey;

                commDBKalenderRepo.AddNewKalender(commDBKalZE);
            }
        }
    }
}
