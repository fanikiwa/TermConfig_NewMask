using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories;
using System.ComponentModel;
using KruAll.Core.Models;
using TermConfig_NewMask.Controllers;
using TermConfig_NewMask.Dtos;

namespace TermConfig_NewMask.ViewModels
{

    class PermissionMappingViewModel
    {
        readonly PermissionMappingRepository _permissionMappingRepository = new PermissionMappingRepository();

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<AC_PermissionMapping> GetPermissionMappingsByProfileId(int id)
        {
            return _permissionMappingRepository.GetPermissionMappingsByProfileId(id);
        }

        public void AddPermissionsProfile(AC_PermissionMapping permissionsProfile)
        {
            //_permissionMappingRepository.AddPermissionProfile(permissionsProfile);
        }

        public void EditPermissionsProfile(AC_PermissionMapping permissionsProfile)
        {
            //_permissionMappingRepository.EditPermissionsProfile(permissionsProfile);
        }

        public void DeletePermissionsProfile(AC_PermissionMapping permissionsProfile)
        {
            //_permissionMappingRepository.DeletePermissionsProfile(permissionsProfile);
        }

        public PermissionDto GetPermissions(int id)
        {
            PermissionDto permissionsDto = new PermissionDto();
            List<AC_PermissionMapping> permissionMappings = new List<AC_PermissionMapping>();
            permissionMappings = GetPermissionMappingsByProfileId(id);

            CheckSuperUser(ref permissionMappings, ref permissionsDto);
            CheckGroupLevelSuperUser(ref permissionMappings, ref permissionsDto);

            CheckPersonnelSettings(ref permissionMappings, ref permissionsDto);
            CheckAccessControlSettings(ref permissionMappings, ref permissionsDto);
            CheckGeneralSettings(ref permissionMappings, ref permissionsDto);

            CheckPersonnelDataSet(ref permissionMappings, ref permissionsDto);
            CheckBuildingPlan(ref permissionMappings, ref permissionsDto);
            CheckAccessPlanSettings(ref permissionMappings, ref permissionsDto);
            CheckSwitchPlanSettings(ref permissionMappings, ref permissionsDto);

            CheckVisitorLogin(ref permissionMappings, ref permissionsDto);
            CheckVisitorDataSet(ref permissionMappings, ref permissionsDto);
            CheckVisitorPlan(ref permissionMappings, ref permissionsDto);

            CheckGrader(ref permissionMappings, ref permissionsDto);
            CheckSafetyManagement(ref permissionMappings, ref permissionsDto);
            CheckAlarmFunction(ref permissionMappings, ref permissionsDto);

            return permissionsDto;
        }

        public void CheckSuperUser(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.AllTermConfigEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AllTermConfig) && x.PermissionType.Equals(1));
            permissionsDto.AllTermConfigRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AllTermConfig) && x.PermissionType.Equals(2));

            permissionsDto.AllAccessControlEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AllAccessControl) && x.PermissionType.Equals(1));
            permissionsDto.AllAccessControlRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AllAccessControl) && x.PermissionType.Equals(2));
        }

        public void CheckGroupLevelSuperUser(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.SettingsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Settings) && x.PermissionType.Equals(1));
            permissionsDto.SettingsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Settings) && x.PermissionType.Equals(2));

            permissionsDto.MasterDataEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.MasterData) && x.PermissionType.Equals(1));
            permissionsDto.MasterDataRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.MasterData) && x.PermissionType.Equals(2));

            permissionsDto.VisitorSettingsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.VisitorSettings) && x.PermissionType.Equals(1));
            permissionsDto.VisitorSettingsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.VisitorSettings) && x.PermissionType.Equals(2));

            permissionsDto.SafetySettingsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SafetySettings) && x.PermissionType.Equals(1));
            permissionsDto.SafetySettingsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SafetySettings) && x.PermissionType.Equals(2));

            permissionsDto.AccessControlListsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessControlLists) && x.PermissionType.Equals(1));
            permissionsDto.AccessControlListsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessControlLists) && x.PermissionType.Equals(2));

            permissionsDto.CommunicationEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Communication) && x.PermissionType.Equals(1));
        }


        private void CheckPersonnelSettings(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.PersonnelSettingsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.PersonnelSettings) && x.PermissionType.Equals(1));
            permissionsDto.PersonnelSettingsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.PersonnelSettings) && x.PermissionType.Equals(2));

            permissionsDto.DepartmentEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Department) && x.PermissionType.Equals(1));
            permissionsDto.DepartmentRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Department) && x.PermissionType.Equals(2));

            permissionsDto.LocationEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Location) && x.PermissionType.Equals(1));
            permissionsDto.LocationRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Location) && x.PermissionType.Equals(2));

            permissionsDto.CostCenterEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.CostCenter) && x.PermissionType.Equals(1));
            permissionsDto.CostCenterRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.CostCenter) && x.PermissionType.Equals(2));

            permissionsDto.VehicleEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Vehicle) && x.PermissionType.Equals(1));
            permissionsDto.VehicleRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Vehicle) && x.PermissionType.Equals(2));
        }

        private void CheckAccessControlSettings(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.AccessControlSettingsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessControlSettings) && x.PermissionType.Equals(2));
            permissionsDto.AccessControlSettingsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessControlSettings) && x.PermissionType.Equals(1));
        }

        private void CheckGeneralSettings(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.GeneralSettingsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.GeneralSettings) && x.PermissionType.Equals(1));
            permissionsDto.GeneralSettingsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.GeneralSettings) && x.PermissionType.Equals(2));

            permissionsDto.LanguageEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Language) && x.PermissionType.Equals(1));
            permissionsDto.LanguageRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Language) && x.PermissionType.Equals(2));

            permissionsDto.ProfileGroupEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.ProfileGroup) && x.PermissionType.Equals(1));
            permissionsDto.ProfileGroupRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.ProfileGroup) && x.PermissionType.Equals(2));

            permissionsDto.PlanGroupEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.PlanGroup) && x.PermissionType.Equals(1));
            permissionsDto.PlanGroupRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.PlanGroup) && x.PermissionType.Equals(2));

            permissionsDto.IconsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Icons) && x.PermissionType.Equals(1));
            permissionsDto.IconsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Icons) && x.PermissionType.Equals(2));

            permissionsDto.RightsAssignmentEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.RightsAssignment) && x.PermissionType.Equals(1));
            permissionsDto.RightsAssignmentRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.RightsAssignment) && x.PermissionType.Equals(2));

            permissionsDto.PasswordEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Password) && x.PermissionType.Equals(1));
            permissionsDto.PasswordRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Password) && x.PermissionType.Equals(2));
        }

        private void CheckPersonnelDataSet(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.PersonnelDataSetEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.PersonnelDataset) && x.PermissionType.Equals(1));
            permissionsDto.PersonnelDataSetRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.PersonnelDataset) && x.PermissionType.Equals(2));
        }

        private void CheckBuildingPlan(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.BuildingPlanEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.BuildingPlan) && x.PermissionType.Equals(1));
            permissionsDto.BuildingPlanRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.BuildingPlan) && x.PermissionType.Equals(2));
        }

        private void CheckAccessPlanSettings(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.AccessPlanEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessPlan) && x.PermissionType.Equals(1));
            permissionsDto.AccessPlanRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessPlan) && x.PermissionType.Equals(2));

            permissionsDto.AccessPlanSettingsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessPlanSettings) && x.PermissionType.Equals(1));
            permissionsDto.AccessPlanSettingsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessPlanSettings) && x.PermissionType.Equals(2));

            permissionsDto.AccessPlanInfoEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessPlanInfo) && x.PermissionType.Equals(1));
            permissionsDto.AccessPlanInfoRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AccessPlanInfo) && x.PermissionType.Equals(2));
        }

        private void CheckSwitchPlanSettings(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.SwitchPlanEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SwitchPlan) && x.PermissionType.Equals(1));
            permissionsDto.SwitchPlanRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SwitchPlan) && x.PermissionType.Equals(2));

            permissionsDto.SwitchPlanSettingsEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SwitchPlanSettings) && x.PermissionType.Equals(1));
            permissionsDto.SwitchPlanSettingsRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SwitchPlanSettings) && x.PermissionType.Equals(2));

            permissionsDto.SwitchPlanInfoEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SwitchPlanInfo) && x.PermissionType.Equals(1));
            permissionsDto.SwitchPlanInfoRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SwitchPlanInfo) && x.PermissionType.Equals(2));
        }

        private void CheckVisitorLogin(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.VisitorLoginEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.VisitorLogin) && x.PermissionType.Equals(1));
            permissionsDto.VisitorLoginRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.VisitorLogin) && x.PermissionType.Equals(2));
        }

        private void CheckVisitorDataSet(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.VisitorDataManagementEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.VisitorDataManagement) && x.PermissionType.Equals(1));
            permissionsDto.VisitorDataManagementRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.VisitorDataManagement) && x.PermissionType.Equals(2));
        }

        private void CheckVisitorPlan(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.VisitorPlanEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.VisitorPlan) && x.PermissionType.Equals(1));
            permissionsDto.VisitorPlanRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.VisitorPlan) && x.PermissionType.Equals(2));
        }

        private void CheckGrader(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.GraderEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Grader) && x.PermissionType.Equals(1));
            permissionsDto.GraderRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.Grader) && x.PermissionType.Equals(2));
        }

        private void CheckSafetyManagement(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.SecurityManagementEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SecurityManagement) && x.PermissionType.Equals(1));
            permissionsDto.SecurityManagementRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.SecurityManagement) && x.PermissionType.Equals(2));
        }

        private void CheckAlarmFunction(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.AlarmFunctionEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AlarmFunction) && x.PermissionType.Equals(1));
            permissionsDto.AlarmFuctionRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AlarmFunction) && x.PermissionType.Equals(2));
        }
    }
}
