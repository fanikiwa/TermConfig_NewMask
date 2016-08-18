using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.ViewModels
{
    public class GlobalSettingsViewModel
    {
        public GlobalSettingsViewModel() { }

        readonly GlobalSettingRepository globalSettingRepository = new GlobalSettingRepository();

        public string GetGetGlobalSettingByName(string name)
        {
            Global_Settings globalSetting = globalSettingRepository.GetGlobalSettingByName(name);
            if (globalSetting == null) return string.Empty;

            return globalSetting.Version;
        }
    }
}
