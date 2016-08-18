using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories.Base;
using KruAll.Core.Models;
using System.ComponentModel;

namespace KruAll.Core.Repositories
{
    public class FingerPrintsRepository : KruAllBaseRepository<FingerPrint>
    {
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<FingerPrint> GetFingerPrints()
        {
            return base.GetAll().ToList();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void NewFingerPrint(FingerPrint fingerPrint)
        {
            base.Add(fingerPrint);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteAllFingerPrintsForEmployee(int  personelNumber)
        {
            List<FingerPrint> personelFinfers = null;


            personelFinfers = base.GetAll().Where(x => x.PersNr == personelNumber).ToList();

            foreach(FingerPrint finger in personelFinfers)
            {
                Delete(finger);
            }
            Save();
        }

    }
}
