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
    public class FingerPrintsRepositoy : KruAllCommBaseRepository<FingerPrint>
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<FingerPrint> GetAllFingerPrint()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public FingerPrint GetFingerPrintById(long id)
        {
            return base.FindBy(b => b.ID == id).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddNewFingerPrint(FingerPrint fingerprint)
        {
            base.Add(fingerprint);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddOrUpdateFingerPrint(FingerPrint fingerprint)
        {
            var finger = base.GetAll().Where(x => x.Mandant == fingerprint.Mandant && x.Pers_Nr == fingerprint.Pers_Nr && x.FingerIndex == fingerprint.FingerIndex).FirstOrDefault();

            if(finger == null)
            {
                fingerprint.CommStatus = base.GetAll().Max(x => x.CommStatus) + 1;

                if (fingerprint.CommStatus == null)
                {
                    fingerprint.CommStatus = 1;
                }

                fingerprint.CommAction = 1;
                base.Add(fingerprint);
            }
            else
            {

                if(fingerprint.Template != finger.Template)
                {
                    finger.CommStatus = base.GetAll().Max(x => x.CommStatus) + 1;
                    finger.CommAction = 1;
                    finger.Template = fingerprint.Template;
                    base.Save();
                }
                
            }
        }

        public void SaveFingerPrints()
        {
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void EditFingerPrint(FingerPrint fingerprint)
        {
            if (fingerprint.ID == 0) return;
            base.Edit(fingerprint);
            Save();
        }

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public void DeleteFingerPrint(FingerPrint fingerprint)
        {
            if (fingerprint.ID == 0) return;
            base.Delete(fingerprint);
            Save();
        }
    }
}
