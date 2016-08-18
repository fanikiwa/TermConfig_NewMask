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
    public class PersonalStammeRepository : KruallPZEBaseRepository<Personalstamm>
    {

        public PersonalStammeRepository()
        {

        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Personalstamm> GetAllActivePersonal()
        {
            return base.GetAll().Where(x => x.Pers_Ausweis_Nr > 0 && x.Pers_Ausw_Gesperrt == false).ToList();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Personalstamm> GetAllPersonal()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Personalstamm GetPersonalbyNr(int personalNr)
        {
            return base.FindBy(cell => cell.Pers_Nr == personalNr).FirstOrDefault();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public IQueryable<Personalstamm> GetPersonalsQuery()
        {
            return base.GetAll();
        }


    }
}

