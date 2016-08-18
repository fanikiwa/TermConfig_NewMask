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
    public class TerminalsNewRepository : KruAllBaseRepository<TerminalsNew>
    {
                 #region Constructor
        public TerminalsNewRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalsNew> GetAllDatafoxTerminals()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalsNew GetTerminalsNewById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }     
       
     
        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalsNew(TerminalsNew TerminalsNew)
        {
            base.Add(TerminalsNew);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminalsNew(TerminalsNew TerminalsNew)
        {
            if (TerminalsNew.ID == 0) return;
            base.Edit(TerminalsNew);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalsNew(TerminalsNew TerminalsNew)
        {
            if (TerminalsNew.ID == 0) return;
            var currentTerminalsNew = GetTerminalsNewById(TerminalsNew.ID);
            Delete(currentTerminalsNew);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalsNewById(long id)
        {
            if (id == 0) return;
            var currentTerminalsNew = GetTerminalsNewById(id);
            Delete(currentTerminalsNew);
            Save();
        }

        #endregion
    }
}
