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
    public class KruSysMandantRepositoy : KruSysBaseRepository<Mandanten>
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<Mandanten> GetAllMandanten()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public Mandanten GetMandantenById(long id)
        {
            return base.FindBy(b => b.ID == id).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewMandanten(Mandanten mandanten)
        {
            base.Add(mandanten);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditMandanten(Mandanten mandanten)
        {
            if (mandanten.ID == 0) return;
            base.Edit(mandanten);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteMandanten(Mandanten mandanten)
        {
            if (mandanten.ID == 0) return;
            base.Delete(mandanten);
            Save();
        }

        
    }
}
