using System.Collections.Generic;
using KruAll.Core.Models;
using KruAll.Core.Models;
using System;
using System.Reflection;
using System.Linq;

namespace KruAll.Core.Models
{
    public class SollKalender_ZEGet
    {
        public void GetSollKalender_ZE()
        {
            var connectiionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();
            var commDBSollKalenderRepo = new Repositories.SollKalender_ZERepository();
            commDBSollKalenderRepo.DeleteKalenders();

            foreach (int clientKey in connectiionStrings.Keys)
            {
                var mandantSollKalenderRepo = new Repositories.PZE.SollKalender_ZERepository(connectiionStrings[clientKey]);
                List<Models.SollKalender_ZE> mandantKalZEList = mandantSollKalenderRepo.GetAllKalender();

                int thisYear = DateTime.Now.Year, lastYear = DateTime.Now.Year;
                int thisMonth = DateTime.Now.Month, lastMonth = DateTime.Now.Month;

                if (DateTime.Now.Year != DateTime.Now.AddDays(-1).Year)
                    lastYear = DateTime.Now.AddDays(-1).Year;
                if (DateTime.Now.Month != DateTime.Now.AddDays(-1).Month)
                    lastMonth = DateTime.Now.AddDays(-1).Month;


                List<Models.SollKalender_ZE> mandantCurrKalZEList = mandantKalZEList.Any(kal => kal.Jahr == thisYear && kal.Monat == thisMonth) ?
                    mandantKalZEList.Where(kal => kal.Jahr == thisYear && kal.Monat == thisMonth).ToList() : new List<Models.SollKalender_ZE>();
                List<Models.SollKalender_ZE> mandantPrevKalZEList = mandantKalZEList.Any(kal => kal.Jahr == lastYear && kal.Monat == lastMonth) ?
                    mandantKalZEList.Where(kal => kal.Jahr == thisYear && kal.Monat == thisMonth).ToList() : new List<Models.SollKalender_ZE>();

                LoadKalenders(ref commDBSollKalenderRepo, clientKey, mandantCurrKalZEList, mandantPrevKalZEList);

                commDBSollKalenderRepo.SaveKalenders();
            }
        }

        private static void LoadKalenders(ref Repositories.SollKalender_ZERepository commDBSollKalenderRepo, int clientKey, List<Models.SollKalender_ZE> mandantCurrKalZEList, List<Models.SollKalender_ZE> mandantPrevKalZEList)
        {
            foreach (SollKalender_ZE mandantCurrKalZE in mandantCurrKalZEList)
            {
                Models.SollKalender_ZE mandantPrevKalZE = mandantPrevKalZEList.Any(kal => kal.Pers_Nr == mandantCurrKalZE.Pers_Nr) ?
                    mandantPrevKalZEList.FirstOrDefault(kal => kal.Pers_Nr == mandantCurrKalZE.Pers_Nr) : new Models.SollKalender_ZE();

                Models.SollKalender_ZE commDBKalZE = new Models.SollKalender_ZE();
                Double todaySZ, yesterdaySZ, todayPZ, yesterdayPZ;
                PropertyInfo pi = mandantCurrKalZE.GetType().GetProperty(string.Format("{0}{1:0}", DateTime.Now.Day == 1 ? "sz" : "SZ", DateTime.Now.Day));
                todaySZ = Convert.ToDouble(pi.GetValue(mandantCurrKalZE));
                pi = mandantPrevKalZE.GetType().GetProperty(string.Format("{0}{1:0}", DateTime.Now.AddDays(-1).Day == 1 ? "sz" : "SZ", DateTime.Now.AddDays(-1).Day));
                yesterdaySZ = Convert.ToDouble(pi.GetValue(mandantPrevKalZE));
                pi = mandantCurrKalZE.GetType().GetProperty(string.Format("PZ{0:0}", DateTime.Now.Day));
                todayPZ = Convert.ToDouble(pi.GetValue(mandantCurrKalZE));
                pi = mandantPrevKalZE.GetType().GetProperty(string.Format("PZ{0:0}", DateTime.Now.AddDays(-1).Day));
                yesterdayPZ = Convert.ToDouble(pi.GetValue(mandantPrevKalZE));

                commDBKalZE.Jahr = mandantCurrKalZE.Jahr ?? (short)DateTime.Now.Year;
                commDBKalZE.Monat = mandantCurrKalZE.Monat ?? (short)DateTime.Now.Month;
                commDBKalZE.Pers_Nr = mandantCurrKalZE.Pers_Nr;
                commDBKalZE.SZ1 = todaySZ;
                commDBKalZE.SZ2 = yesterdaySZ;
                commDBKalZE.PZ1 = todayPZ;
                commDBKalZE.PZ2 = yesterdayPZ;
                commDBKalZE.typ = mandantCurrKalZE.typ;
                commDBKalZE.Mandant = clientKey;

                commDBSollKalenderRepo.AddNewKalender(commDBKalZE);
            }
        }
    }
}
