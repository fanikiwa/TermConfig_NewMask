using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.ViewModels
{
    public class CommSettingsViewModel
    {
        CommSettingsRepository mandantRepo = new CommSettingsRepository();

        public List<CommSetting> GetAllCommSettings()
        {
            return mandantRepo.GetAllCommSetting();
        }

        public CommSetting GetCommSettingByName(string name)
        {
            return mandantRepo.GetCommSettingByName(name) ?? new CommSetting();
        }

        public void EditCommSettingByName(CommSetting _CommSetting)
        {
            mandantRepo.EditCommSetting(_CommSetting);
        }
    }
}

