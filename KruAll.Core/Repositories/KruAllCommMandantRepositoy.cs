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
    public class KruAllCommMandantRepositoy :  KruAllCommBaseRepository<CommMandanten>
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<CommMandanten> GetAllMandanten()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public CommMandanten GetMandantenById(long id)
        {
            return base.FindBy(b => b.ID == id).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewMandanten(CommMandanten mandanten)
        {
            base.Add(mandanten);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditMandanten(CommMandanten mandanten)
        {
            if (mandanten.ID == 0) return;
            base.Edit(mandanten);
            //Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteMandanten(CommMandanten mandanten)
        {
            if (mandanten.ID == 0) return;
            base.Delete(mandanten);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteAllMandanten()
        {
            foreach (var _entity in base.GetAll())
            {
                base.Delete(_entity);
            }
        }

        public void SaveMandanten()
        {
            Save();
        }
    }
}
