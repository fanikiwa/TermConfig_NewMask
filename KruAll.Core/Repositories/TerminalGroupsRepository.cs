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
    public class TerminalGroupsRepository: KruAllBaseRepository<TerminalGroup>
    {
        #region Constructor
        public TerminalGroupsRepository() { }
        #endregion
        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalGroup> GetAllTerminalGroups()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalGroup GetTerminalGroupById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        public TerminalGroup GetTerminalGroupByNumber(long group_Nr)
        {
            return base.FindBy(e => e.GroupNr == group_Nr).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalGroup(TerminalGroup terminalGroup)
        {
            base.Add(terminalGroup);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminalGroup(TerminalGroup terminalGroup)
        {
            if (terminalGroup.ID == 0) return;
            base.Edit(terminalGroup);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalGroup(TerminalGroup terminalGroup)
        {
            if (terminalGroup.ID == 0) return;
            var currentTerminalGroup = GetTerminalGroupById(terminalGroup.ID);
            Delete(currentTerminalGroup);
            Save();

        }

        #endregion
    }
}
