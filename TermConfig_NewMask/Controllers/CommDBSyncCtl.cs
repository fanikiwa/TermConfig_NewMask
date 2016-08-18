using KruAll.Core.Models;
using KruAll.Core.Models;
using System;
using System.Diagnostics;
using TermConfig_NewMask.ViewModels;

namespace TermConfig_NewMask.Controllers
{
    public class CommDBSyncCtl
    {
        #region Constructor
        public CommDBSyncCtl() { }
        #endregion

        #region Methods
        public static double GetCommRefreshMinutes()
        {
            CommSettingsViewModel commSettingViewModel = new CommSettingsViewModel();
            CommSetting _CommSetting = commSettingViewModel.GetCommSettingByName("Comm_Refresh");
            string refreshValueString = _CommSetting.Value ?? "";

            if (refreshValueString.Trim() != "")
            {
                double refreshValue = 0;
                double.TryParse(refreshValueString, out refreshValue);

                return refreshValue;
            }

            return 0;
        }

        public static void DoCommDBRefresh()
        {
            try
            {
                MandantenGet getKal = new MandantenGet();
                getKal.GetMandantenFromKruSysDatabase();
            }
            catch (Exception) { }
            try
            {
                PersonalstammGet getPers = new PersonalstammGet();
                getPers.GetPersonalDataFromClientDatabases();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            try
            {
                Kalender_ZEGet getKal = new Kalender_ZEGet();
                getKal.GetKalender_ZE();
            }
            catch (Exception) { }
            try
            {
                SollKalender_ZEGet getSollKal = new SollKalender_ZEGet();
                getSollKal.GetSollKalender_ZE();
            }
            catch (Exception) { }
            try
            {
                TagesprogrammeGet getTP = new TagesprogrammeGet();
                getTP.GetTagesprogramme();
            }
            catch (Exception) { }
            try
            {
                Tagesprogramme_PausenGet getTPPausen = new Tagesprogramme_PausenGet();
                getTPPausen.GetTagesprogrammePausen();
            }
            catch (Exception) { }
            try
            {
                Tagesprogramme_SchichtenGet getTPSchichten = new Tagesprogramme_SchichtenGet();
                getTPSchichten.GetTagesprogrammeSchichten();
            }
            catch (Exception) { }
            try
            {
                ERG_KontenGet getERGKonten = new ERG_KontenGet();
                getERGKonten.GetERG_Konten();
            }
            catch (Exception e) { }
            try
            {
                VorlauftastenGet getRSettings = new VorlauftastenGet();
                getRSettings.GetVorlauftasten();
            }
            catch (Exception e) { }
        }
        #endregion 
    }
}
