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
    public class ViewTerminalGroupMappingRepository: KruAllBaseRepository<ViewTerminalGroupMapping>
    {
        #region Constructor
        public ViewTerminalGroupMappingRepository() { }
        #endregion
        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<ViewTerminalGroupMapping> GetAllTerminalGroupMapping()
        {
            return base.GetAll().ToList();
        }
        public List<ViewTerminalGroupMapping> GetTerminalGroupByGroupId(long groupId)
        {
            return base.FindBy(e => e.TerminalGroupId == groupId).ToList();
        }
        public ViewTerminalGroupMapping GetTerminalInstance(long groupId, long terminalInstanceId)
        {
            return base.FindBy(e => e.TerminalGroupId == groupId && e.TerminalInstanceId == terminalInstanceId).FirstOrDefault();
        }
        #endregion
    }
}
