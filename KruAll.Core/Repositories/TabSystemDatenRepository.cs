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
    public class TabSystemDatenRepository: KruallPZEBaseRepository<TabSystemDaten>
    {
        #region Constructor
        public TabSystemDatenRepository() { }
        #endregion

        #region
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TabSystemDaten> GetWebApiSettings()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditWebApiSettings(TabSystemDaten tabSystemDaten)
        {
            if (tabSystemDaten.Schlüssel == null) return;
            base.Edit(tabSystemDaten);
            Save();
        }

        #endregion
    }
}
