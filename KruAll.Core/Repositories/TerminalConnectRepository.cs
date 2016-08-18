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
    public class TerminalConnectRepository : KruAllBaseRepository<TerminalConnect>
    {
        #region Constructor
        public TerminalConnectRepository() { }

        #endregion Constructor

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalConnect> GetAllTerminalConnectionType()
        {
            return base.GetAll().ToList();
        }

         public TerminalConnect GetTerminalbySerialNumber(string sN)
        {
            return base.FindBy(t => t.SerialNumber == sN).FirstOrDefault();
        }

    

        #endregion
    }
}
