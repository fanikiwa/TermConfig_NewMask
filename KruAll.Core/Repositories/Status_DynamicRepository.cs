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
    //public class Status_DynamicRepository : KruAllBaseRepository<Status_Dynamic>
    //{
    //    #region Constructor

    //    public Status_DynamicRepository() { }

    //    #endregion

    //    #region Methods
    //    [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
    //    public List<Status_Dynamic> GetAllStatus_Dynamic()
    //    {
    //        return base.GetAll().ToList();
    //    }
    //    #endregion
    //}

    public class Status_DynamicRepository : KruallPZEBaseRepository<ZITERM_V20_Vorlauftasten>
    {
        #region Constructor

        public Status_DynamicRepository() { }

        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<ZITERM_V20_Vorlauftasten> GetAllStatus_Dynamic()
        {
            return base.GetAll().ToList();
        }
        #endregion
    }
}
