using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System.ComponentModel;

namespace KruAll.Core.Repositories
{ 
    public class RealTimeSettingsRepository :  KruAllCommBaseRepository<RealTimeSetting>
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<RealTimeSetting> GetAllRealTimeSettings()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public RealTimeSetting GetRealTimeSettingsById(long id)
        {
            return base.FindBy(b => b.ID == id).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewRealTimeSettings(RealTimeSetting RealTimeSettings)
        {
            base.Add(RealTimeSettings);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditRealTimeSettings(RealTimeSetting RealTimeSettings)
        {
            if (RealTimeSettings.ID == 0) return;
            base.Edit(RealTimeSettings);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteRealTimeSettings(RealTimeSetting RealTimeSettings)
        {
            if (RealTimeSettings.ID == 0) return;
            base.Delete(RealTimeSettings);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteAllRealTimeSettings()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        public void SaveRealTimeSettings()
        {
            Save();
        }
    }
}
