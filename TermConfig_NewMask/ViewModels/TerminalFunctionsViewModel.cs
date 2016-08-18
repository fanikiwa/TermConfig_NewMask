using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.ViewModels
{
    public class TerminalFunctionsViewModel
    {
        #region Constructor
        public TerminalFunctionsViewModel() { }

        #endregion

        #region Properties

        TerminalFunctionsRepository _terminalFunctionRepository = new TerminalFunctionsRepository();

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<View_TerminalFunction> TerminalFunctions()
        {
            return _terminalFunctionRepository.GetAllTerminalFunctions();
        }

        #endregion
    }
}