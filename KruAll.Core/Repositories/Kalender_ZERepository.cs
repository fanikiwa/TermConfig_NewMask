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
    class Kalender_ZERepository : KruAllCommBaseRepository<Kalender_ZE>
    {
        #region Constructors
        public Kalender_ZERepository() { }
        #endregion

        #region Methods
        public List<Kalender_ZE> GetAllKalender()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Kalender_ZE GetKalenderByNr(long id)
        {
            return base.FindBy(b => b.Kal_nr == id).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewKalender(Kalender_ZE Kalender)
        {
            base.Add(Kalender);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditKalender(Kalender_ZE Kalender)
        {
            if (Kalender.Kal_nr == 0) return;
            base.Edit(Kalender);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteKalender(Kalender_ZE Kalender)
        {
            if (Kalender.Kal_nr == 0) return;
            base.Delete(Kalender);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteKalenders()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void SaveKalenders()
        {
            Save();
        }
        #endregion
    }
}
