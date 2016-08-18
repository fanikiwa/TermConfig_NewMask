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
    public class TerminalConfigRepository : KruAllBaseRepository<TerminalConfig>
    {
        #region Constructor

        public TerminalConfigRepository() { }

        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalConfig> GetAllTerminalConfig()
        {
            return base.GetAll().ToList();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalConfig> GetAllTerminalsInstances()
        {
            return base.GetAll().ToList();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalConfig GetTerminalInstance(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalConfig GetTerminalConfigbyTermID(int termID)
        {
            return base.FindBy(term => term.TermID == termID).FirstOrDefault();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalConfig GetTerminalConfigbyID(long Id)
        {
            return base.FindBy(term => term.ID == Id).FirstOrDefault();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalConfig GetTerminalConfigbyIP(string IpAdress)
        {
            return base.FindBy(term => term.IpAddress == IpAdress).FirstOrDefault();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalConfig GetTerminalConfigbyType(string type)
        {
            return base.FindBy(term => term.TermType == type).FirstOrDefault();
        }        
        
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalConfig GetTerminalConfigbyDesc(string description)
        {
            return base.FindBy(term => term.Description == description).LastOrDefault();
        }

        public TerminalConfig GetTerminalbySerialNumber(string sN)
        {
            return base.FindBy(t => t.TerminalConnect.SerialNumber == sN).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalConfig> GetTerminalByOEM(int termOEM)
        {
            return base.FindBy(t => t.TerminalOEM.TermOEMId == termOEM).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalConfig> GetTermListByOEMId(int termOEM)
        {
            return base.FindBy(t => t.TerminalOEMId == termOEM).ToList();
        }


        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalConfig> GetByTerminalId(int terminalId)
        {
            return base.FindBy(t => t.TerminalId == terminalId).ToList();
        }
        
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalConfig(TerminalConfig termcon)
        {
            base.Add(termcon);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalConfig> GetTerminalByType(string termType)
        {
            return base.FindBy(t => t.TermType == termType).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminalConfig(TerminalConfig termcon)
        {
            if (termcon.TermID == 0) return;
            base.Edit(termcon);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalConfig(TerminalConfig termcon)
        {
            if (termcon.ID == 0) return;
            var currentTermCon = GetTerminalConfigbyID(termcon.ID);
            base.Delete(currentTermCon);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalConfigByID(long curID)
        {
            if (curID == 0) return;
            var currentTermCon = GetTerminalConfigbyID(curID);
            base.Delete(currentTermCon);
            Save();
        }
        #endregion
    }
}
