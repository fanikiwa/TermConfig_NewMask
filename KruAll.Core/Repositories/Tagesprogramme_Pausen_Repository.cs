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
    public class Tagesprogramme_Pausen_Repository : KruAllCommBaseRepository<Tprogramme_Pausen>
    {
        #region Constructors
        public Tagesprogramme_Pausen_Repository() { }
        #endregion

        #region Methods
        public List<Tprogramme_Pausen> GetAllTagesprogrammePausen()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Tprogramme_Pausen GetTagesprogrammePausenByNr(string Nr)
        {
            return base.FindBy(b => b.Paus_Nr == Nr).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewTagesprogrammePausen(Tprogramme_Pausen TagesprogrammePausen)
        {
            base.Add(TagesprogrammePausen);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditTagesprogrammePausen(Tprogramme_Pausen TagesprogrammePausen)
        {
            if (TagesprogrammePausen.Paus_Nr.Trim() != "") return;
            base.Edit(TagesprogrammePausen);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteTagesprogrammePausen(Tprogramme_Pausen TagesprogrammePausen)
        {
            if (TagesprogrammePausen.Paus_Nr.Trim() != "") return;
            base.Delete(TagesprogrammePausen);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteTagesprogrammePausens()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void SaveTagesprogrammePausens()
        {
            Save();
        }
        #endregion
    }
}
