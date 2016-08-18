using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories.Base;
using KruAll.Core.Models;
using System.ComponentModel;

namespace KruAll.Core.Repositories
{
    public class GlobalSettingRepository : KruAllBaseRepository<Global_Settings>
    {
        #region Constructor
        public GlobalSettingRepository() { }

        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Global_Settings> GetglobalSettings()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewVersion(Global_Settings versionDetails)
        {
            base.Add(versionDetails);           
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void AddVersion(Global_Settings _globalSettings)
        {
            base.Add(_globalSettings);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditVersion(Global_Settings _globalSettings)
        {
            if (_globalSettings.ID == 0) return;
            base.Edit(_globalSettings);
            Save();
        }

        public Global_Settings GetGlobalSettingByName(string name)
        {
            return base.FindBy(x => x.AppName == name).FirstOrDefault();
        }
        #endregion
    }
}
