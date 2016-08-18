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
    public class PermissionMappingRepository : KruAllBaseRepository<AC_PermissionMapping>
    {

        #region Constructor
        public PermissionMappingRepository() { }
        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<AC_PermissionMapping> GetPermissionMappings()
        {
            return base.GetAll().OrderBy(x => x.PermissionKey).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<AC_PermissionMapping> GetPermissionMappingsByProfileId(int id)
        {
            return base.FindBy(x => x.ProfileId == id).ToList();
        }
        
        public void AddPermissionProfile(AC_PermissionMapping permissionProfile)
        {
            base.Add(permissionProfile);
            Save();
        }

        public void EditPermissionsProfile(AC_PermissionMapping permissionProfile)
        {
            //if (permissionProfile.ID == 0) return;
            //base.Edit(permissionProfile);
            //Save();
        }
        
        public void DeletePermissionsProfile(AC_PermissionMapping permissionProfile)
        {
            //if (permissionProfile.ID == 0) return;
            //var permissionProfileEntity = GetPermissionMappingsByProfileId(permissionProfile.ID);
            //base.Delete(permissionProfileEntity);
            //Save();
        }
        #endregion
    }
}