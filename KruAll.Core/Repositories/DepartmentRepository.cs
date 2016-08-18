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
    public class DepartmentRepository : KruallPZEBaseRepository<Abteilungen>
    {

        public DepartmentRepository()
        {

        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Abteilungen> GetAllCellInfo()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Abteilungen GetDepartmentbyId(int deptId)
        {
            return base.FindBy(cell => cell.Abt_Nr == deptId).FirstOrDefault();
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Abteilungen> GetAllDepartments()
        {
            return base.GetAll().ToList();
        }

    }
}


