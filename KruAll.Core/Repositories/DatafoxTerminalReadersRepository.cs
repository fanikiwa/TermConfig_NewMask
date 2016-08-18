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
    public class DatafoxTerminalReadersRepository: KruAllBaseRepository<DatafoxTerminalReader>
    {
                #region Constructor
        public DatafoxTerminalReadersRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<DatafoxTerminalReader> GetAllDatafoxTerminalReaders()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DatafoxTerminalReader GetDatafoxTerminalReaderById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
       
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<DatafoxTerminalReader> GetByTerminalInstanceId(long terminalInstanceId)
        {
            return base.FindBy(e => e.DatafoxTerminalID == terminalInstanceId).ToList();
        }
     
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewDatafoxTerminalReader(DatafoxTerminalReader DatafoxTerminalReader)
        {
            base.Add(DatafoxTerminalReader);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditDatafoxTerminalReader(DatafoxTerminalReader DatafoxTerminalReader)
        {
            if (DatafoxTerminalReader.ID == 0) return;
            base.Edit(DatafoxTerminalReader);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteDatafoxTerminalReader(DatafoxTerminalReader DatafoxTerminalReader)
        {
            if (DatafoxTerminalReader.ID == 0) return;
            var currentDatafoxTerminalReader = GetDatafoxTerminalReaderById(DatafoxTerminalReader.ID);
            Delete(currentDatafoxTerminalReader);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteDatafoxTerminalReaderById(long id)
        {
            if (id == 0) return;
            var currentDatafoxTerminalReader = GetDatafoxTerminalReaderById(id);
            Delete(currentDatafoxTerminalReader);
            Save();
        }

        #endregion
    }
}
