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
    public class TerminalConnectionRepository : KruAllBaseRepository<TerminalConnectionType>
    {
        #region Constructor
        public TerminalConnectionRepository() { }

        #endregion Constructor

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalConnectionType> GetAllTerminalConnectionType()
        {
            return base.GetAll().ToList();
        }


        #endregion
    }
}
