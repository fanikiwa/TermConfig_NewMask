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
    public class DatafoxTerminalInstanceRepository : KruAllBaseRepository<DatafoxTerminalInstance>
    {
         #region Constructor
        public DatafoxTerminalInstanceRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<DatafoxTerminalInstance> GetAllDatafoxTerminalInstances()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DatafoxTerminalInstance GetDatafoxTerminalInstanceById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
       
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<DatafoxTerminalInstance> GetByTerminalOEMId(long terminalOEMId)
        {
            return base.FindBy(e => e.TerminalOEMId == terminalOEMId).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<DatafoxTerminalInstance> GetByTerminalNewId(long terminalNewId)
        {
            return base.FindBy(e => e.TerminalNewId == terminalNewId).ToList();
        }
     
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewDatafoxTerminalInstance(DatafoxTerminalInstance DatafoxTerminalInstance)
        {
            base.Add(DatafoxTerminalInstance);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditDatafoxTerminalInstance(DatafoxTerminalInstance DatafoxTerminalInstance)
        {
            if (DatafoxTerminalInstance.ID == 0) return;
            base.Edit(DatafoxTerminalInstance);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteDatafoxTerminalInstance(DatafoxTerminalInstance DatafoxTerminalInstance)
        {
            if (DatafoxTerminalInstance.ID == 0) return;
            var currentDatafoxTerminalInstance = GetDatafoxTerminalInstanceById(DatafoxTerminalInstance.ID);
            Delete(currentDatafoxTerminalInstance);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteDatafoxTerminalInstanceById(long id)
        {
            if (id == 0) return;
            var currentDatafoxTerminalInstance = GetDatafoxTerminalInstanceById(id);
            Delete(currentDatafoxTerminalInstance);
            Save();
        }

        #endregion
    }
}
