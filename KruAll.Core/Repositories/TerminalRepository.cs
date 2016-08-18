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
    public class TerminalRepository : KruAllBaseRepository<Terminal>
    {
        #region Constructor

        public TerminalRepository() { }

        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Terminal> GetAllTerminals()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Terminal GetTerminalbyDesc(string description)
        {
            return base.FindBy(term => term.Description == description).FirstOrDefault();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Terminal GetTerminalbyID(int ID)
        {
            return base.FindBy(term => term.ID == ID).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Terminal> GetTerminalbyOEM(string OEM)
        {
            var filter = GetAllTerminals().Where(term => term.TermOEM == OEM);

            return filter.ToList();
        }


        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Terminal GetTerminalbyType(string type)
        {
            return base.FindBy(term => term.TermType == type).FirstOrDefault();
        }
        
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminal(Terminal term)
        {
            base.Add(term);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminal(Terminal term)
        {
            //terminal edit using terminaltype
            if (term.TermType == null) return;
            base.Edit(term);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminal(Terminal term)
        {
            //terminal deletion using terminaltype
            if (term.TermType == null) return;
            var currentTerminal = GetTerminalbyType(term.TermType);
            base.Delete(currentTerminal);
            Save();
        }
        #endregion
    }
}
