using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.ViewModels
{
    public class TerminalConnectionViewModel
    {
        #region Constructor
        public TerminalConnectionViewModel() { }

        #endregion

        #region Properties

        TerminalConnectionRepository _terminalConnectionRepository = new TerminalConnectionRepository();

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalConnectionType> TerminalConnectionTypes()
        {
            return _terminalConnectionRepository.GetAllTerminalConnectionType();
        }

        #endregion
    }
}