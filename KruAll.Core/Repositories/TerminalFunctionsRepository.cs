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
    public class TerminalFunctionsRepository : KruAllBaseRepository<View_TerminalFunction>
    {
        #region Constructors
        public TerminalFunctionsRepository() { }

        #endregion Constructors

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<View_TerminalFunction> GetAllTerminalFunctions()
        {
            return base.GetAll().ToList();
        }

        #endregion

    }
}
