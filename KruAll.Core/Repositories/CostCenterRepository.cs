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
    public class CostCenterRepository: KruallPZEBaseRepository<Kostenstellen>
    {
        public CostCenterRepository() { }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Kostenstellen> GetAllCostCenters()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Kostenstellen GetCostCenterbyId(int deptId)
        {
            return base.FindBy(loc => loc.Kos_Nr == deptId).FirstOrDefault();
        }
    }
}
