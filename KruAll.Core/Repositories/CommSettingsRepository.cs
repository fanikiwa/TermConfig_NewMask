using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KruAll.Core.Repositories
{
    public class CommSettingsRepository : KruAllCommBaseRepository<CommSetting>
    {
        #region Constructors
        public CommSettingsRepository() { }
        #endregion

        #region Methods
        public List<CommSetting> GetAllCommSetting()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public CommSetting GetCommSettingByName(string name)
        {
            return base.FindBy(b => b.Name == name).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewCommSetting(CommSetting CommSetting)
        {
            base.Add(CommSetting);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditCommSetting(CommSetting CommSetting)
        {
            if (CommSetting.Name == "") return;
            CommSetting _CommSetting = GetCommSettingByName(CommSetting.Name);
            if (_CommSetting == null)
            {
                _CommSetting = new CommSetting();
                _CommSetting.Name = CommSetting.Name;
                _CommSetting.Value = CommSetting.Value ?? "0";
                _CommSetting.Memo = CommSetting.Memo ?? "";
                base.Add(_CommSetting);
            }
            else
            {
                _CommSetting.Name = CommSetting.Name;
                _CommSetting.Value = CommSetting.Value ?? "0";
                _CommSetting.Memo = CommSetting.Memo ?? "";
                base.Edit(_CommSetting);
            }
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteCommSetting(CommSetting CommSetting)
        {
            if (CommSetting.ID == 0) return;
            base.Delete(CommSetting);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteCommSettings()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void SaveCommSettings()
        {
            Save();
        }
        #endregion
    }
}
