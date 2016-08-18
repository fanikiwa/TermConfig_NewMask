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
    public class HandyRepository: KruallPZEBaseRepository<Handy>
    {

        public HandyRepository() { 
        
        }

        //[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Handy> GetAllCellInfo()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Handy GetAllCellInfobyId(Int32 cellMasterId)
        {
            return base.FindBy(cell => cell.Id == cellMasterId).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewCellInfo(Handy cellDetails)
        {
            base.Add(cellDetails);
            cellDetails.Licenced = true;
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditCellInfo(Handy cellDetails)
        {
            if (cellDetails.Id == 0) return;
            cellDetails.Licenced = true;
            base.Edit(cellDetails);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteCellInfo(Handy cellDetails)
        {
            if (cellDetails.Id == 0) return;
            var currentCellInfo = GetAllCellInfobyId(cellDetails.Id);
            base.Delete(currentCellInfo);
            Save();
        }
    }
}
