using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace KruAll.Core.Repositories
{
    public class VirtualTerminalRepository : KruAllBaseRepository<VirtualTerminalGroup>
    {
        #region Constructor
        public VirtualTerminalRepository() { }
        #endregion
        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<VirtualTerminalGroup> GetAllVirtualTerminalGroups()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public VirtualTerminalGroup GetVTerminalGroupById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        public VirtualTerminalGroup GetVTerminalGroupByNumber(long group_Nr)
        {
            return base.FindBy(e => e.GroupNr == group_Nr).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewVTerminalGroup(VirtualTerminalGroup vTerminalGroup)
        {
            base.Add(vTerminalGroup);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditVTerminalGroup(VirtualTerminalGroup vTerminalGroup)
        {
            if (vTerminalGroup.ID == 0) return;
            base.Edit(vTerminalGroup);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteVTerminalGroup(VirtualTerminalGroup vTerminalGroup)
        {
            if (vTerminalGroup.ID == 0) return;
            var currentTerminalGroup = GetVTerminalGroupById(vTerminalGroup.ID);
            Delete(currentTerminalGroup);
            Save();
        }
        #endregion
    }
}
