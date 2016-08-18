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
    public class CellInfoViewModel
    {
        #region Constructor

        public CellInfoViewModel() { }

        #endregion

        #region Properties

        HandyRepository _handyRepository = new HandyRepository();
        PersonalStammeRepository _personalStammeRepository = new PersonalStammeRepository();
        DepartmentRepository _DepartmentRepository = new DepartmentRepository();

        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Handy> GetHandys()
        {
            return _handyRepository.GetAllCellInfo();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Personalstamm GetPersonByNr(int d)
        {
            return _personalStammeRepository.GetPersonalbyNr(d);
        }

        public Abteilungen GetDepartmentById(int d)
        {
            return _DepartmentRepository.GetDepartmentbyId(d);
        }

        //[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        //public Handy GetTermbyType(string t)
        //{
        //    return _handyRepository.GetHandybyType(t);
        //}
        //[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        //public Handy GetTermbySerial(string s)
        //{
        //    return _handyRepository.GetHandybySerialNo(s);
        //}

        //[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        //public void CreateHandy(Handy Handy)
        //{
        //    _handyRepository.NewHandy(Handy);
        //}

        //[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        //public void UpdateHandy(Handy Handy)
        //{
        //    _handyRepository.EditHandy(Handy);
        //}

        //[DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        //public void DelHandy(Handy Handy)
        //{
        //    _handyRepository.DeleteHandy(Handy);
        //}

        #endregion
    }
}