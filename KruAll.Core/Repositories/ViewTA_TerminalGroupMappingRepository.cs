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
    public class ViewTA_TerminalGroupMappingRepository: KruAllBaseRepository<ViewTA_TerminalGroupMapping>
    {
        #region Constructor
        public ViewTA_TerminalGroupMappingRepository() { }
        #endregion
        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<ViewTA_TerminalGroupMapping> GetAllTerminalGroupMapping()
        {
            return base.GetAll().ToList();
        }
        public List<ViewTA_TerminalGroupMapping> GetTerminalGroupByGroupId(long groupId)
        {
            return base.FindBy(e => e.TerminalGroupId == groupId).ToList();
        }
        public ViewTA_TerminalGroupMapping GetTerminalInstance(long groupId, long terminalInstanceId)
        {
            return base.FindBy(e => e.TerminalGroupId == groupId && e.TerminalInstanceId == terminalInstanceId).FirstOrDefault();
        }
        

        #endregion
    }
}
