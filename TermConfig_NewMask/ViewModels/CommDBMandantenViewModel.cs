using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.ViewModels
{
    public class CommDBMandantenViewModel
    {
        KruAllCommMandantRepositoy mandantRepo = new KruAllCommMandantRepositoy();

        public List<CommMandanten> GetAllMandanten()
        {
            return mandantRepo.GetAllMandanten();
        }
        public void AddNewMandanten(CommMandanten mandanten)
        {
            mandantRepo.AddNewMandanten(mandanten);
        }

        public void EditMandanten(CommMandanten mandanten)
        {
            if (mandanten.ID == 0) return;
            mandantRepo.EditMandanten(mandanten);
        }

        public void SaveMandanten()
        {
            mandantRepo.SaveMandanten();
        }
    }
}
