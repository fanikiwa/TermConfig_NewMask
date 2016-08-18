using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KruAll.Core.Repositories
{
    public class Tagesprogramme_Repository : KruAllCommBaseRepository<Tprogramme>
    {
        #region Constructors
        public Tagesprogramme_Repository() { }
        #endregion

        #region Methods
        public List<Tprogramme> GetAllTagesprogramme()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Tprogramme GetTagesprogrammeByNr(string Nr)
        {
            return base.FindBy(b => b.TP_Nr == Nr).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewTagesprogramme(Tprogramme Tagesprogramme)
        {
            base.Add(Tagesprogramme);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditTagesprogramme(Tprogramme Tagesprogramme)
        {
            if (Tagesprogramme.TP_Nr.Trim() != "") return;
            base.Edit(Tagesprogramme);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteTagesprogramme(Tprogramme Tagesprogramme)
        {
            if (Tagesprogramme.TP_Nr.Trim() != "") return;
            base.Delete(Tagesprogramme);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteTagesprogrammes()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void SaveTagesprogrammes()
        {
            Save();
        }
        #endregion
    }
}
