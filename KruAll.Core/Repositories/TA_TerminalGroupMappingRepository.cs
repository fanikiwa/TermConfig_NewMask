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
    public class TA_TerminalGroupMappingRepository: KruAllBaseRepository<TA_TerminalGroupMapping>
    {
        #region Constructor
        public TA_TerminalGroupMappingRepository() { }
        #endregion
        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TA_TerminalGroupMapping> GetAllTerminalGroupMapping()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TA_TerminalGroupMapping GetTerminalGroupById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        public List<TA_TerminalGroupMapping> GetTerminalGroupByGroupId(long groupId)
        {
            return base.FindBy(e => e.TerminalGroupId == groupId).ToList();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TA_TerminalGroupMapping GetTerminalInstance(long groupId, long terminalInstanceId)
        {
            return base.FindBy(e => e.TerminalGroupId == groupId && e.TerminalInstanceId == terminalInstanceId).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalGroupMapping(TA_TerminalGroupMapping terminalGroupMapping)
        {
            base.Add(terminalGroupMapping);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminalGroupMapping(TA_TerminalGroupMapping terminalGroupMapping)
        {
            if (terminalGroupMapping.ID == 0) return;
            base.Edit(terminalGroupMapping);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalGroupMapping(TA_TerminalGroupMapping terminalGroupMapping)
        {
            if (terminalGroupMapping.ID == 0) return;
            var currentTerminalGroupMapping = GetTerminalGroupById(terminalGroupMapping.ID);
            Delete(currentTerminalGroupMapping);
            Save();

        }

        #endregion
    }
}
