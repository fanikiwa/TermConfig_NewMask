using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.ViewModels
{
    public class DatafoxTerminalInstanceViewModel
    {
        #region Constructor
        public DatafoxTerminalInstanceViewModel() { }
        #endregion

        #region Properties
        DatafoxTerminalInstanceRepository _datafoxTerminalInstanceRepository = new DatafoxTerminalInstanceRepository();
        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<DatafoxTerminalInstance> DatafoxTerminalInstances()
        {
            return _datafoxTerminalInstanceRepository.GetAllDatafoxTerminalInstances();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<DatafoxTerminalInstance> GetInstancesByTerminalNewID(long terminalNewID)
        {
            return _datafoxTerminalInstanceRepository.GetByTerminalNewId(terminalNewID);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public DatafoxTerminalInstance GetDatafoxTerminalInstanceByID(int id)
        {
            return _datafoxTerminalInstanceRepository.GetDatafoxTerminalInstanceById(id);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void CreateDatafoxTerminalInstance(DatafoxTerminalInstance DatafoxTerminalInstance)
        {
            _datafoxTerminalInstanceRepository.NewDatafoxTerminalInstance(DatafoxTerminalInstance);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void UpdateDatafoxTerminalInstance(DatafoxTerminalInstance DatafoxTerminalInstance)
        {
            _datafoxTerminalInstanceRepository.EditDatafoxTerminalInstance(DatafoxTerminalInstance);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteDatafoxTerminalInstance(DatafoxTerminalInstance _DatafoxTerminalInstance)
        {
            _datafoxTerminalInstanceRepository.DeleteDatafoxTerminalInstance(_DatafoxTerminalInstance);
        }
        public void DeleteDatafoxTerminalInstanceById(long id)
        {
            _datafoxTerminalInstanceRepository.DeleteDatafoxTerminalInstanceById(id);
        }

        #endregion
    }
}