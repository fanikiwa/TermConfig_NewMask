using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.ViewModels
{
    public class CommDBVorlauftastenViewModel
    {
        #region Constructor
        public CommDBVorlauftastenViewModel() { }

        #endregion

        #region Properties
        VorlauftastenRepository commDBVorlauftastenRepo = new VorlauftastenRepository();
        #endregion

        #region Methods
        public List<Vorlauftasten> GetAllVorlauftasten()
        {
            return commDBVorlauftastenRepo.GetAllVorlauftasten();
        }
        #endregion
    }
}
