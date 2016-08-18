using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories.Base;
using KruAll.Core.Models;
using System.ComponentModel;
using System.Web;
using System.Windows;
using System.Globalization;
using System.Threading;

namespace KruAll.Core.Repositories
{
    public class TerminalReaderRepository : KruAllBaseRepository<TerminalReader>
    {
        TerminalConfigRepository _TerminalRepository = new TerminalConfigRepository();
        public TerminalReaderRepository()
        {

        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalReader> GetAllReaders()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalReader> GetAllReadersById(long Id)
        {
            List<TerminalReader> newlist = new List<TerminalReader>();
            newlist = base.GetAll().Where(r => r.TermID == Id).ToList();
            return base.GetAll().Where(r => r.TermID == Id).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalReader GetReadersByTermIdAndNr(long Id, int nr)
        {
            return base.GetAll().Where(r => r.TermID == Id && r.ReaderNr == nr).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalReader> GetReadersByTermId(long Id)
        {
            List<TerminalReader> newlist = new List<TerminalReader>();
            newlist = base.GetAll().Where(r => r.TermID == Id).ToList();
            return newlist;
        }


        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalReader GetReaderbyId(long id)
        {
            return base.FindBy(reader => reader.ID == id).FirstOrDefault();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalReader GetReaderInstanceById(long terminalId, int readerId)
        {
            return base.FindBy(x => x.TermID == terminalId && x.ReaderID == readerId).FirstOrDefault();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewReaderInfo(TerminalReader ReaderDetails)
        {
            base.Add(ReaderDetails);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewTerminalReader(TerminalReader reader)
        {
            base.Add(reader);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditReaderInfo(TerminalReader ReaderDetails)
        {
            if (ReaderDetails.ID == 0) return;
            base.Edit(ReaderDetails);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteReaderInfo(long ReaderDetails)
        {
            if (ReaderDetails == 0) return;
            var currentReaderInfo = GetReaderbyId(ReaderDetails);
            base.Delete(currentReaderInfo);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteTerminalReader(TerminalReader terminalReader)
        {
            if (terminalReader.ID == 0) return;
            var currentTerminalReader = GetReaderbyId(terminalReader.ID);
            Delete(currentTerminalReader);
            Save();
        }


    }
}


