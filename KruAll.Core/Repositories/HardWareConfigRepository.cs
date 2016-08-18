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
    public class HardWareConfigRepository: KruallPZEBaseRepository<HardwareConfig>
    {
        public HardWareConfigRepository() { }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<HardwareConfig> GetAllHardWareConfigInfo()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewHardWareConfigInfo(HardwareConfig hardwareConfig)
        {
            base.Add(hardwareConfig);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditHardWareConfigInfo(HardwareConfig hardwareConfig)
        {
            if (hardwareConfig.Id == 0) return;
            base.Edit(hardwareConfig);
            Save();
        }
    }
}
