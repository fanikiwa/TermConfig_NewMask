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
    public class PersPasswordsRepository : KruAllBaseRepository<AC_PersPasswords>
    {
        #region Constructor
        public PersPasswordsRepository() { }
        #endregion

        #region Methods

        public List<AC_PersPasswords> GetPersPasswords()
        {
            return base.GetAll().OrderBy(x => x.ID).ToList();
        }

        public AC_PersPasswords GetPersPasswordsByNr(int persNr)
        {
            return base.FindBy(x => x.Pers_Nr == persNr).FirstOrDefault();
        }

        public AC_PersPasswords GetPersCurrentPasswordByName(string userName)
        {
            AC_PersPasswords persPassword = new AC_PersPasswords();
            persPassword = base.FindBy(x => x.Username == userName).OrderByDescending(x => x.ID).FirstOrDefault();
            if (persPassword == null)
                persPassword = new AC_PersPasswords();
            return persPassword;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void AddPassword(AC_PersPasswords persPassword)
        {
            base.Add(persPassword);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditPermissionsProfile(AC_PersPasswords persPassword)
        {
            if (persPassword.ID == 0) return;
            base.Edit(persPassword);
            Save();
        }
        #endregion
    }
}
