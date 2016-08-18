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
    public class Tagesprogramme_Schichten_Repository : KruAllCommBaseRepository<Tprogramme_Schichten>
    {
        #region Constructors
        public Tagesprogramme_Schichten_Repository() { }
        #endregion

        #region Methods
        public List<Tprogramme_Schichten> GetAllTagesprogrammeSchichten()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Tprogramme_Schichten GetTagesprogrammeSchichtenByNr(string Nr)
        {
            return base.FindBy(b => b.S_Nr == Nr).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewTagesprogrammeSchichten(Tprogramme_Schichten TagesprogrammeSchichten)
        {
            base.Add(TagesprogrammeSchichten);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditTagesprogrammeSchichten(Tprogramme_Schichten TagesprogrammeSchichten)
        {
            if (TagesprogrammeSchichten.S_Nr.Trim() != "") return;
            base.Edit(TagesprogrammeSchichten);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteTagesprogrammeSchichten(Tprogramme_Schichten TagesprogrammeSchichten)
        {
            if (TagesprogrammeSchichten.S_Nr.Trim() != "") return;
            base.Delete(TagesprogrammeSchichten);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteTagesprogrammeSchichtens()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void SaveTagesprogrammeSchichtens()
        {
            Save();
        }
        #endregion
    }
}
