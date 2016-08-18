using KruAll.Core.Repositories.Base;
using KruAll.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace KruAll.Core.Repositories
{
    public class KruAllTerminalBookingRawDataRepository : KruAllBaseRepository<TerminalBookingRawData>
    {
        #region Constructor
        public KruAllTerminalBookingRawDataRepository() { }
        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalBookingRawData(TerminalBookingRawData terminalBookingRawData)
        {
            base.Add(terminalBookingRawData);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalBookingRawData> GetTerminalBookingRawData()
        {
            return base.GetAll().ToList();
        }
        #endregion
    }
}

