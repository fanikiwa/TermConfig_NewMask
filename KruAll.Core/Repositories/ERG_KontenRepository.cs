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
    class ERG_KontenRepository : KruAllCommBaseRepository<ERG_Konten>
    {
        #region Constructors
        public ERG_KontenRepository() { }
        #endregion

        #region Methods
        public List<ERG_Konten> GetAllKonten()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public ERG_Konten GetKontenById(string id)
        {
            return base.FindBy(b => b.Kto_Index == id).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewKonten(ERG_Konten Konten)
        {
            base.Add(Konten);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditKonten(ERG_Konten Konten)
        {
            if ((Konten.Kto_Index ?? "").Trim() == "") return;
            base.Edit(Konten);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteKonten(ERG_Konten Konten)
        {
            if ((Konten.Kto_Index ?? "").Trim() == "") return;
            base.Delete(Konten);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteKontens()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void SaveKontens()
        {
            Save();
        }
        #endregion
    }
}
