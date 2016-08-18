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
    public class TerminalUsesRepository: KruAllBaseRepository<TerminalUse>
    {
        #region Constructor
        public TerminalUsesRepository() { }
        #endregion
        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalUse GetTerminalUseById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalUse> GetTerminalUseByTermConfId(long termConfID)
        {
            return base.GetAll().Where(x => x.TerminalConfigID == termConfID).ToList();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalUse(TerminalUse terminalUse)
        {
            if (terminalUse.ID == 0) return;
            var currentTerminalUse = GetTerminalUseById(terminalUse.ID);
            Delete(currentTerminalUse);
            Save();

        }
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalUseByTermConfId(long termConfId)
        {
            if (termConfId == 0) return;
            var currentTerminalUse = GetTerminalUseByTermConfId(termConfId);
            if (currentTerminalUse == null) return;
            foreach (TerminalUse terminalUse in currentTerminalUse)
            {
                Delete(terminalUse);
                Save();
            }
        }
        #endregion
    }
}
