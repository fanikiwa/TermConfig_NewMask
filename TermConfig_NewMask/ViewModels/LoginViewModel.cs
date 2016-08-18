using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories;
using System.ComponentModel;
using KruAll.Core.Models;

namespace TermConfig_NewMask.ViewModels
{
    class LoginViewModel
    {
        readonly vwPersPasswordsProfileRepository _vwPersPasswordsProfileRepository = new vwPersPasswordsProfileRepository();

        public AC_PersPasswordsProfile GetPersPasswordsProfile(string userName)
        {
            return _vwPersPasswordsProfileRepository.GetPersPasswordsByName(userName);
        }
    }
}
