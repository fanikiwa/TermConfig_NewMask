using System.Collections.Generic;
using KruAll.Core.Models;
using KruAll.Core.Models;
using System;
using System.Reflection;

namespace KruAll.Core.Models
{
    public class RealTimeSettingsGet
    {
        public void GetRealTimeSettings()
        {
            //var connectiionStrings = ClientConnectionStrings.GetClientProviderConnectionStrings();
            //var commDBRealTimeSettingRepo = new Repositories.RealTimeSettingsRepository();
            //commDBRealTimeSettingRepo.DeleteAllRealTimeSettings();

            //foreach (int clientKey in connectiionStrings.Keys)
            //{
            //    var mandantRealTimeSettingRepo = new Repositories.PZE.RealTimeSettingsRepository(connectiionStrings[clientKey]);
            //    List<Models.RealTimeSetting> mandantRealTimeSettingsList = mandantRealTimeSettingRepo.GetAllRealTimeSettings();

            //    foreach (Models.RealTimeSetting mandantRealTimeSetting in mandantRealTimeSettingsList)
            //    {
            //        Models.RealTimeSetting commDBRealTimeSetting = new Models.RealTimeSetting();

            //        commDBRealTimeSetting.Account1 = mandantRealTimeSetting.DisplayAccount1;
            //        commDBRealTimeSetting.Account3 = mandantRealTimeSetting.DisplayAccount3;
            //        commDBRealTimeSetting.Account3 = mandantRealTimeSetting.DisplayAccount3;
            //        commDBRealTimeSetting.Account4 = mandantRealTimeSetting.DisplayAccount4;
            //        commDBRealTimeSetting.Active = mandantRealTimeSetting.Active;
            //        commDBRealTimeSetting.BookingInterval = mandantRealTimeSetting.BookingInterval;
            //        commDBRealTimeSetting.CalculationMode = mandantRealTimeSetting.CalculationMode;
            //        commDBRealTimeSetting.RefreshInterval = mandantRealTimeSetting.RefreshInterval;
            //        commDBRealTimeSetting.ServerURL = mandantRealTimeSetting.ServerURL;
            //        commDBRealTimeSetting.Mandant = clientKey;

            //        commDBRealTimeSettingRepo.AddNewRealTimeSettings(commDBRealTimeSetting);
            //    }

            //    commDBRealTimeSettingRepo.SaveRealTimeSettings();
            //}
        }
    }
}
