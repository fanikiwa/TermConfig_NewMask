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
    public class TabSystemDatenViewModel
    {
        #region Constructor
        public TabSystemDatenViewModel() { }
        #endregion

        #region
        TabSystemDatenRepository _tabSystemDatenRepository = new TabSystemDatenRepository();
        #endregion

        #region
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TabSystemDaten> GetApiSettings()
        {
            //use of sql where in() operator
            String[] apiParams = new String[] { "MobileWebApiIP", "MobileWebApiPort" };
            return _tabSystemDatenRepository.GetWebApiSettings().Where(api => apiParams.Contains(api.Schlüssel)).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void UpdateApiSettings(TabSystemDaten tabSystemDaten)
        {
            if (tabSystemDaten.Schlüssel == null) return;
            _tabSystemDatenRepository.EditWebApiSettings(tabSystemDaten);
        }
        #endregion

    }
}
