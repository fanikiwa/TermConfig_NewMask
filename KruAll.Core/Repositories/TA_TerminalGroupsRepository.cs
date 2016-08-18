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
    public class TA_TerminalGroupsRepository: KruAllBaseRepository<TA_TerminalGroups>
    {
        #region Constructor
        public TA_TerminalGroupsRepository() { }
        #endregion
        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TA_TerminalGroups> GetAllTerminalGroups()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TA_TerminalGroups GetTerminalGroupById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        public TA_TerminalGroups GetTerminalGroupByNumber(long group_Nr)
        {
            return base.FindBy(e => e.GroupNr == group_Nr).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalGroup(TA_TerminalGroups terminalGroup)
        {
            base.Add(terminalGroup);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminalGroup(TA_TerminalGroups terminalGroup)
        {
            if (terminalGroup.ID == 0) return;
            base.Edit(terminalGroup);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalGroup(TA_TerminalGroups terminalGroup)
        {
            if (terminalGroup.ID == 0) return;
            var currentTerminalGroup = GetTerminalGroupById(terminalGroup.ID);
            Delete(currentTerminalGroup);
            Save();

        }

        #endregion
    }
}
