using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace KruAll.Core.Repositories
{
    public class TerminalInfoTextRepository : KruAllBaseRepository<TerminalInfoText>
    {
        #region Constructor
        public TerminalInfoTextRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalInfoText> GetAllTerminalInfoTexts()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalInfoText GetTerminalInfoTextByTermIdAndNr(long termId, int nr)
        {
            return base.GetAll().Where(x => x.TerminalConfigID == termId && x.InfoTextNr == nr).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalInfoText> GetTerminalInfoTextsByTermConfId(long termConfId)
        {
            return base.GetAll().Where(x => x.TerminalConfigID == termConfId).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalInfoText GetTerminalInfoTextsById(long id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalInfoTexts(TerminalInfoText TerminalInfoTexts)
        {
            base.Add(TerminalInfoTexts);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditTerminalInfoTexts(TerminalInfoText TerminalInfoTexts)
        {
            if (TerminalInfoTexts.ID == 0) return;
            base.Edit(TerminalInfoTexts);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalInfoTexts(TerminalInfoText TerminalInfoTexts)
        {
            if (TerminalInfoTexts.ID == 0) return;
            var currentTerminalInfoTexts = GetTerminalInfoTextsById(TerminalInfoTexts.ID);
            Delete(currentTerminalInfoTexts);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalInfoTextsById(long id)
        {
            if (id == 0) return;
            var currentTerminalInfoTexts = GetTerminalInfoTextsById(id);
            Delete(currentTerminalInfoTexts);
            Save();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalInfoTextsByTermConfId(long termConfId)
        {
            if (termConfId == 0) return;
            var currentTerminalInfoTexts = GetTerminalInfoTextsByTermConfId(termConfId);
            foreach (TerminalInfoText tif in currentTerminalInfoTexts)
            {
                Delete(tif);
                Save();
            }
        }
        #endregion




    }

}

