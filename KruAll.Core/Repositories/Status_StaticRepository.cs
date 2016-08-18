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
    //public class Status_StaticRepository : KruAllBaseRepository<Status_Static>
    //{
    //    #region Constructor
    //    public Status_StaticRepository() { }

    //    #endregion

    //    #region Methods
    //    [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
    //    public List<Status_Static> GetAllStatus_Static()
    //    {
    //        return base.GetAll().ToList();
    //    }
    //    #endregion
    //}

    public class Status_StaticRepository : KruallPZEBaseRepository<Status>
    {
        #region Constructor
        public Status_StaticRepository() { }

        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Status> GetAllStatus_Static()
        {
            return base.GetAll().ToList();
        }
        #endregion
    }
}
