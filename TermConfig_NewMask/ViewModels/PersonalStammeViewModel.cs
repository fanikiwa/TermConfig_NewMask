using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TermConfig_NewMask.Dtos;

namespace TermConfig_NewMask.ViewModels
{
    public class PersonalStammeViewModel
    {
        #region Constructor
        public PersonalStammeViewModel() { }

        #endregion

        #region Properties
        PersonalStammeRepository _personalStammeRepository = new PersonalStammeRepository();
        PersonalStammeListRepository _personalStammeListRepository = new PersonalStammeListRepository();
        #endregion

        #region Methods
        private PersonalStammeDto PopulateEmployees(Personalstamm personalStamm)
        {
            return new PersonalStammeDto
            {
                //Abt_Nr= personalStamm.Abteilungen.Abt_Nr,
                //Abt_Bezeichnung = personalStamm.Abteilungen.Abt_Bezeichnung,
                Pers_Nr = personalStamm.Pers_Nr,
                Pers_Name1 = personalStamm.Pers_Name1,
                Pers_Name2 = personalStamm.Pers_Name2

            };
        }
        private List<PersonalStammeDto> SearchResults(IList<Personalstamm> personalStamm)
        {
            var employeesListing = new List<PersonalStammeDto>();
            foreach (var personal in personalStamm)
            {
                employeesListing.Add(PopulateEmployees(personal));
            }
            return employeesListing;
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<PersonalStammeDto> GetPersonals()
        {
            var personals = _personalStammeRepository.GetAllPersonal();
            if (personals.Count == 0) return null;
            return SearchResults(personals);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<PersonalStammeList> GetPersonalCellInfo()
        {
            var personals = _personalStammeListRepository.GetAllCellInfo();
            //if (personals.Count == 0) return null;
            return personals ?? new List<PersonalStammeList>();
        }

        public List<Personalstamm> GetPersonnels(List<long?> personnelIds)
        {
            return _personalStammeRepository.GetAllPersonal().Where(p => personnelIds.Contains(p.Pers_Nr)).ToList();
        }
        public List<Personalstamm> GetPersonnels(int personalIdFrom, int personalIdTo, int locationFrom, int locationTo,
                                                int departmentFrom, int departmentTo, int costCentreFrom, int costCentreTo)
        {
            var personals = _personalStammeRepository.GetPersonalsQuery();
            var query = from Personalstamm emp in personals select emp;
            if ((personalIdFrom > 0) || (personalIdTo > 0))
            {
                query = query.Where(p => p.Pers_Nr >= personalIdFrom && p.Pers_Nr <= personalIdTo);
            }

            if ((locationFrom > 0) || (locationTo > 0))
            {
                query = query.Where(p => p.Pers_Werk >= locationFrom && p.Pers_Werk <= locationTo);
            }

            if ((departmentFrom > 0) || (departmentTo > 0))
            {
                query = query.Where(p => p.Pers_Abteilung >= departmentFrom && p.Pers_Abteilung <= departmentTo);
            }

            if ((costCentreFrom > 0) || (costCentreTo > 0))
            {
                query = query.Where(p => p.Pers_Kostenstelle >= costCentreFrom && p.Pers_Kostenstelle <= costCentreTo);
            }

            var personalList = query.ToList();

            return personalList;
        }
        #endregion
    }
}
