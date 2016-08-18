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
    public class vwPersPasswordsProfileRepository : KruAllBaseRepository<AC_PersPasswordsProfile>
    {
        #region Constructor
        public vwPersPasswordsProfileRepository() { }
        #endregion

        #region Methods

        public List<AC_PersPasswordsProfile> GetPersPasswords()
        {
            return base.GetAll().OrderBy(x => x.Pers_Nr).ToList();
        }

        public AC_PersPasswordsProfile GetPersPasswordsByNr(int persNr)
        {
            return base.FindBy(x => x.Pers_Nr == persNr).FirstOrDefault();
        }

        public AC_PersPasswordsProfile GetPersPasswordsByName(string userName)
        {
            return base.FindBy(x => x.UserName == userName).FirstOrDefault();
        }
        #endregion
    }
}
