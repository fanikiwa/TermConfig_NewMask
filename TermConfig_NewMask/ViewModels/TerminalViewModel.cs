using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TermConfig_NewMask.Dtos;

namespace TermConfig_NewMask.ViewModels
{
    public class TerminalViewModel
    {
        #region Constructor
        public TerminalViewModel() { }

        #endregion

        #region Properties

        TerminalRepository _termRepository = new TerminalRepository();

        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Terminal> GetTerminals()
        {
            return _termRepository.GetAllTerminals();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Terminal> GetTermbyOEM(string oem)
        {
            return _termRepository.GetTerminalbyOEM(oem);
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Terminal GetTermbyId(int Id)
        {
            return _termRepository.GetTerminalbyID(Id);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Terminal GetTermbyDescription(string d)
        {
            return _termRepository.GetTerminalbyDesc(d);
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Terminal GetTermbyType(string t)
        {
            return _termRepository.GetTerminalbyType(t);
        }
        
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void CreateTerminal(Terminal terminal)
        {
            _termRepository.NewTerminal(terminal);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void UpdateTerminal(Terminal terminal)
        {
            _termRepository.EditTerminal(terminal);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DelTerminal(Terminal terminal)
        {
            _termRepository.DeleteTerminal(terminal);
        }

        #endregion
    }
}