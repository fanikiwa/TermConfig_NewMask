using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System.ComponentModel;

namespace KruAll.Core.Repositories
{
    public class ViewTerminalReaderRepository : KruAllBaseRepository<View_TerminalReader>
    {
        #region Constructor
        public ViewTerminalReaderRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<View_TerminalReader> GetAllTerminals()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public View_TerminalReader GetTerminalsById(long id)
        {
            return base.FindBy(e => e.TerminalID == id).FirstOrDefault();
        }


        #endregion
    }
}
