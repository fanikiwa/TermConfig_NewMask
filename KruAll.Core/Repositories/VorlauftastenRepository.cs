using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KruAll.Core.Repositories
{
    public class VorlauftastenRepository : KruAllCommBaseRepository<Vorlauftasten>
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Vorlauftasten> GetAllVorlauftasten()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Vorlauftasten GetVorlauftastenById(long id)
        {
            return base.FindBy(b => b.ID == id).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewVorlauftasten(Vorlauftasten Vorlauftasten)
        {
            base.Add(Vorlauftasten);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditVorlauftasten(Vorlauftasten _Vorlauftasten)
        {
            if (_Vorlauftasten.ID == 0) return;
            base.Edit(_Vorlauftasten);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteVorlauftasten(Vorlauftasten _Vorlauftasten)
        {
            if (_Vorlauftasten.ID == 0) return;
            base.Delete(_Vorlauftasten);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteAllVorlauftasten()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        public void SaveVorlauftasten()
        {
            Save();
        }
    }
}
