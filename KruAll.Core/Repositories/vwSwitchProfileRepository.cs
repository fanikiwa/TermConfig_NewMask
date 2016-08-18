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
    public class vwSwitchProfileRepository : KruAllBaseRepository<RV_SwitchProfileGroupedPerTerminal>
    {
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<RV_SwitchProfileGroupedPerTerminal> GetvwSwitchProfile()
        {
            return base.GetAll().ToList();
        }
    }
}
