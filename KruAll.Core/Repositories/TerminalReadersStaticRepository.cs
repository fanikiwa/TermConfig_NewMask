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
    public class TerminalReadersStaticRepository : KruAllBaseRepository<TerminalReadersStatic>
    {
        TerminalConfigRepository _TerminalRepository = new TerminalConfigRepository();
        public TerminalReadersStaticRepository()
        {

        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalReadersStatic> GetAllReaders()
        {
            return base.GetAll().ToList();
        }

        
    }
}


