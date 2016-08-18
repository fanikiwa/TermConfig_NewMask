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
    public class TerminalFunctionKeyRespository : KruAllBaseRepository<TerminalFunctionKey>
    {
        #region Constructor
        public TerminalFunctionKeyRespository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalFunctionKey> GetAllTerminalFunctionKeys()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public  TerminalFunctionKey GetTerminalFunctionKeysByTermConfIdAndNr(long termConfID,int nr)
        {
            return base.GetAll().Where(x => x.TerminalConfigID == termConfID && x.FunctionKeyNr == nr).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalFunctionKey> GetTerminalFunctionKeysByTermConfId(long termConfID)
        {
            return base.GetAll().Where(x => x.TerminalConfigID == termConfID).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalFunctionKey GetTerminalFunctionKeysById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalFunctionKeys(TerminalFunctionKey TerminalFunctionKeys)
        {
            base.Add(TerminalFunctionKeys);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminalFunctionKeys(TerminalFunctionKey TerminalFunctionKeys)
        {
            if (TerminalFunctionKeys.ID == 0) return;
            base.Edit(TerminalFunctionKeys);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalFunctionKeys(TerminalFunctionKey TerminalFunctionKeys)
        {
            if (TerminalFunctionKeys.ID == 0) return;
            var currentTerminalFunctionKeys = GetTerminalFunctionKeysById(TerminalFunctionKeys.ID);
            Delete(currentTerminalFunctionKeys);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalFunctionKeysById(long id)
        {
            if (id == 0) return;
            var currentTerminalFunctionKeys = GetTerminalFunctionKeysById(id);
            Delete(currentTerminalFunctionKeys);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalFunctionKeysByTermConfId(long termConfId)
        {
            if (termConfId == 0) return;
            var currentTerminalFunctionKeys = GetTerminalFunctionKeysByTermConfId(termConfId);
            if (currentTerminalFunctionKeys == null) return;
            foreach (TerminalFunctionKey tfk in currentTerminalFunctionKeys)
            {
                Delete(tfk);
                Save();
            }
        }
        #endregion
    }
}
