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
    public class LocationsRepository : KruallPZEBaseRepository<Werke>
    {
        public LocationsRepository(){}

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Werke> GetAllLocations()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Werke GetLocationbyId(int deptId)
        {
            return base.FindBy(loc => loc.W_Nr == deptId).FirstOrDefault();
        }
    }
}
