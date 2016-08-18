using KruAll.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories.Base;

namespace KruAll.Core.Repositories.Base
{
    public class KruAllTerminalRepository : KruAllBaseRepository<Terminal>
    {
        #region Constructor
        public KruAllTerminalRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Terminal GetTerminalByTermType(string TermType)
        {
            return base.FindBy(e => e.TermType == TermType).FirstOrDefault();
        }

         #endregion Methods
    }
}
