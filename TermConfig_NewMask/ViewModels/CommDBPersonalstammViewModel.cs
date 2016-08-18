using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.ViewModels
{
    public class CommDBPersonalstammViewModel
    {
        #region Constructor
        public CommDBPersonalstammViewModel() { }

        #endregion

        #region Properties
        PersonalstammRepository commDBPersonalstammRepo = new PersonalstammRepository();
        #endregion

        #region Methods
        public List<Personalstamm> GetAllPersonal()
        {
            return commDBPersonalstammRepo.GetAllPersonal();
        }
        #endregion
    }
}
