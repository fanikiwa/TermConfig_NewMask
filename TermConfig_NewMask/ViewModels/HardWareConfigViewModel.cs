using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.ViewModels
{
    class HardWareConfigViewModel
    {
        #region Constructor
        public HardWareConfigViewModel() { }
        #endregion

        #region Properties
        HardWareConfigRepository _hardWareConfigRepository = new HardWareConfigRepository();
        HardWareConfigRepository _HardWareConfigRepository = new HardWareConfigRepository();
        #endregion

        #region
        public List<HardwareConfig> GetHardWareConfigInfo()
        {
            //var result = _hardWareConfigRepository.GetAllHardWareConfigInfo().Select(hw => new { hw.TimeTrackingMobile }).FirstOrDefault();
            var result = _hardWareConfigRepository.GetAllHardWareConfigInfo().ToList();
            return result;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<HardwareConfig> GetAllHardWareConfigInfo()
        {
            //return base.GetAll().ToList();
            return _hardWareConfigRepository.GetAllHardWareConfigInfo();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewHardWareConfigInfo(HardwareConfig hardwareConfig)
        {
            //base.Add(hardwareConfig);
            //Save();
            _hardWareConfigRepository.NewHardWareConfigInfo(hardwareConfig);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditHardWareConfigInfo(HardwareConfig hardwareConfig)
        {
            //if (hardwareConfig.Id == 0) return;
            //base.Edit(hardwareConfig);
            //Save();

            _hardWareConfigRepository.EditHardWareConfigInfo(hardwareConfig);
        }

        #endregion
    }
}
