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
    public class TerminalAccessProfilesRepository : KruAllBaseRepository<View_TerminalAccessProfiles>
    {
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<View_TerminalAccessProfiles> GetAllTerminalAccessProfiles(long terminalSerialNumber)
        {
            return base.FindBy(x => x.ID == terminalSerialNumber).Distinct().ToList();
        }
    }
}
