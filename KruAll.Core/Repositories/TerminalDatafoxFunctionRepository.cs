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
    public class TerminalDatafoxFunctionRepository : KruAllBaseRepository<TerminalDatafoxFunction>
    {
        #region Constructor
        public TerminalDatafoxFunctionRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalDatafoxFunction> GetAllTerminalDatafoxFunctions()
        {
            return base.GetAll().ToList();
        }


        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalDatafoxFunction> GetTerminalDatafoxFunctionsByTerminalID(long TerminalID)
        {
            return base.FindBy(x => x.TerminalID == TerminalID).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalDatafoxFunction GetTerminalDatafoxFunctionById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }           

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void AddNewTerminalDatafoxFunction(TerminalDatafoxFunction terminalDatafoxFunction)
        {
            base.Add(terminalDatafoxFunction);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminalUtility(TerminalDatafoxFunction terminalDatafoxFunction)
        {
            if (terminalDatafoxFunction.ID == 0) return;
            base.Edit(terminalDatafoxFunction);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalDatafoxFunction(TerminalDatafoxFunction terminalDatafoxFunction)
        {
            if (terminalDatafoxFunction.ID == 0) return;
            var currentTerminalDatafoxFunction = GetTerminalDatafoxFunctionById(terminalDatafoxFunction.ID);
            Delete(currentTerminalDatafoxFunction);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalDatafoxFunctionById(long id)
        {
            if (id == 0) return;
            var currentTerminalDatafoxFunction = GetTerminalDatafoxFunctionById(id);
            Delete(currentTerminalDatafoxFunction);
            Save();
        }                
        #endregion
    }
}
