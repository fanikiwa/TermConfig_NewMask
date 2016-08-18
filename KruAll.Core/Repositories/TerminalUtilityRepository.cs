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
    public class TerminalUtilityRepository : KruAllBaseRepository<TerminalUtility>
    {
        #region Constructor
        public TerminalUtilityRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalUtility> GetAllTerminalUtility()
        {
            return base.GetAll().ToList();
        }

        
            [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalUtility> GetTerminalFunctionKeysByTermConfId(long TermConfId)
        {
            return base.GetAll().Where(x=>x.TerminalConfigID==TermConfId).ToList();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalUtility GetTerminalUtilityById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalUtility GetTerminalUtilityByTermConfId(long id)
        {
            return base.FindBy(e => e.TerminalConfigID == id).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalUtility(TerminalUtility TerminalUtility)
        {
            base.Add(TerminalUtility);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminalUtility(TerminalUtility TerminalUtility)
        {
            if (TerminalUtility.ID == 0) return;
            base.Edit(TerminalUtility);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalUtility(TerminalUtility TerminalUtility)
        {
            if (TerminalUtility.ID == 0) return;
            var currentTerminalUtility = GetTerminalUtilityById(TerminalUtility.ID);
            Delete(currentTerminalUtility);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalUtilityById(long id)
        {
            if (id == 0) return;
            var currentTerminalUtility = GetTerminalUtilityById(id);
            Delete(currentTerminalUtility);
            Save();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalUtilityByTerminalConfigId(long TermConfId)
        {
            if (TermConfId == 0) return;
            var currentTerminalUtility = GetTerminalUtilityByTermConfId(TermConfId);
            if (currentTerminalUtility == null) return; 
            Delete(currentTerminalUtility);
            Save();
        }
        #endregion
    }
}
