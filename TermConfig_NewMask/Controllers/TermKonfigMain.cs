using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using KruAll.Core.Repositories;
using TermConfig_NewMask.Dtos;
using KruAll.Core.Models;
using System.Diagnostics;

namespace TermConfig_NewMask.Controllers
{
    public enum accessControlPermissions //From Table: AC_Persmissions
    {
        AllTermConfig = 1, AllAccessControl = 2, Settings = 3, PersonnelSettings = 4, Location = 5, Department = 6, CostCenter = 7, Vehicle = 8, AccessControlSettings = 9,
        GeneralSettings = 10, Language = 11, ProfileGroup = 12, PlanGroup = 13, Icons = 14, RightsAssignment = 15, Password = 16, MasterData = 17, PersonnelDataset = 18,
        BuildingPlan = 19, AccessPlan = 20, AccessPlanSettings = 21, AccessPlanInfo = 22, SwitchPlan = 23, SwitchPlanSettings = 24, SwitchPlanInfo = 25, VisitorSettings = 26,
        VisitorLogin = 27, VisitorDataManagement = 28, VisitorPlan = 29, SafetySettings = 30, Grader = 31, SecurityManagement = 32, AlarmFunction = 33, AccessControlLists = 34,
        Communication = 35
    }
    public enum accessControlPermissionModes
    {
        Edit = 1, Read = 2
    }

    public class TermKonfigMain
    {
        List<AC_PermissionMapping> permissionKeysMappingList = new List<AC_PermissionMapping>();
        PersPasswordsRepository persPasswordsRepository = new PersPasswordsRepository();
        vwPersPasswordsProfileRepository _vwPersPasswordsProfileRepository = new vwPersPasswordsProfileRepository();
        PermissionMappingRepository _permissionMappingRepository = new PermissionMappingRepository();
        EncryptionCtl _encryptionCtl = new EncryptionCtl();
        PermissionDto permissionsDto = new PermissionDto();
        AC_PersPasswordsProfile passwordProfile = new AC_PersPasswordsProfile();
        AC_PersPasswords persPasswords = new AC_PersPasswords();

        #region Login Helpers
        public void LoginUser(string password, string userName, string redirectLocation)
        {
            if (userName == "admin") LoginAdmin(password, redirectLocation);

            GetUserLoginDetails(userName);
            GetUserLoginPermissions();

            if (passwordProfile.Pers_Nr == 0 || passwordProfile.UserName == "") return;

            if (passwordProfile.CurrentPassword.Equals(password) && (permissionsDto.AllTermConfigEdit || permissionsDto.AllTermConfigRead))
            {
                AddPersPermissionToSession();
                AddPermissionKeysMappingToSession();
                AddPersDetailsToSession(password, userName);
                if (redirectLocation != "") HttpContext.Current.Response.Redirect(redirectLocation);
                HttpContext.Current.Response.Redirect("/Index_New.aspx");
            }
            else
            {
                //Wrong password Action
            }
        }

        private void LoginAdmin(string password, string redirectLocation)
        {
            GetPseudoUserLoginPassword("admin");
            if (persPasswords.Password != null) { if (persPasswords.Password.Trim() != "" && password != persPasswords.Password.Trim()) return; }
            if (persPasswords.Password == null && password != "admin") return;

            HttpContext.Current.Session.Add("Pers_Nr", "-10000");
            HttpContext.Current.Session.Add("Pers_FirstName", "Admin");
            HttpContext.Current.Session.Add("Pers_LastName", "Admin");
            HttpContext.Current.Session.Add("Pers_Name", "Admin");
            HttpContext.Current.Session.Add("Pers_LoginName", "admin");
            HttpContext.Current.Session.Add("Pers_LoginPassword", password);

            permissionsDto = new PermissionDto();
            permissionsDto.AllTermConfigEdit = true;
            permissionsDto.SettingsEdit = true;
            HttpContext.Current.Session.Add("Pers_Permissions", permissionsDto);
            List<AC_PermissionMapping> pl = new List<AC_PermissionMapping>();
            pl.Add(new AC_PermissionMapping()
            {
                PermissionType = (int)accessControlPermissionModes.Edit,
                PermissionKey = (int)accessControlPermissions.AllTermConfig
            });
            HttpContext.Current.Session.Add("Pers_PermissionKeysMapping", pl);
            if (redirectLocation != "") HttpContext.Current.Response.Redirect(redirectLocation);
            HttpContext.Current.Response.Redirect("/index_New.aspx");
        }

        private void GetUserLoginDetails(string userName)
        {
            passwordProfile = _vwPersPasswordsProfileRepository.GetPersPasswordsByName(userName);
            if (passwordProfile == null)
                passwordProfile = new AC_PersPasswordsProfile();
        }

        private void GetPseudoUserLoginPassword(string userName)
        {
            persPasswords = persPasswordsRepository.GetPersCurrentPasswordByName(userName);
            if (persPasswords == null)
                persPasswords = new AC_PersPasswords();
        }

        public void GetUserLoginPermissions()
        {
            permissionsDto = passwordProfile.ProfileID == null ? new PermissionDto() : GetPermissionsById((int)passwordProfile.ProfileID);
        }

        private void AddPersPermissionToSession()
        {
            //permissionsDto = passwordProfile.ProfileID == null ? new PermissionDto() : GetPermissionsById((int)passwordProfile.ProfileID);
            HttpContext.Current.Session.Add("Pers_Permissions", permissionsDto);
        }

        private void AddPermissionKeysMappingToSession()
        {
            permissionKeysMappingList = passwordProfile.ProfileID == null ? new List<AC_PermissionMapping>() :
                _permissionMappingRepository.GetPermissionMappingsByProfileId((int)passwordProfile.ProfileID);
            HttpContext.Current.Session.Add("Pers_PermissionKeysMapping", permissionKeysMappingList);
        }

        private void AddPersDetailsToSession(string password, string userName)
        {
            HttpContext.Current.Session.Add("Pers_Nr", passwordProfile.Pers_Nr);
            HttpContext.Current.Session.Add("Pers_FirstName", passwordProfile.FirstName);
            HttpContext.Current.Session.Add("Pers_LastName", passwordProfile.LastName);
            HttpContext.Current.Session.Add("Pers_Name", String.Format("{0}, {1}", passwordProfile.FirstName, passwordProfile.LastName));
            HttpContext.Current.Session.Add("Pers_LoginName", userName);
            HttpContext.Current.Session.Add("Pers_LoginPassword", password);
        }

        public void CheckQStringForLoginAttempt()
        {
            if (HttpContext.Current.Request.QueryString["user"] == null || HttpContext.Current.Request.QueryString["pass"] == null) return;

            string userName = HttpContext.Current.Request.QueryString["user"].Trim();
            string encryptedPassword = HttpContext.Current.Request.QueryString["pass"].Trim();
            string encryptedRedirectLocation = HttpContext.Current.Request.QueryString["moveTo"] != null ? HttpContext.Current.Request.QueryString["moveTo"] : "";

            LoginUser(_encryptionCtl.Decrypt(encryptedPassword), userName,
                (encryptedRedirectLocation == "" ? "" : _encryptionCtl.Decrypt(encryptedRedirectLocation)));
        }

        public void CheckAuthCookie()
        {
            try
            {
                HttpCookie zutAUTH = HttpContext.Current.Request.Cookies["ZUT_AUTH"];

                if (!string.Equals(zutAUTH, "") && !string.Equals(zutAUTH, null))
                {
                    string strAUTH = HttpUtility.UrlDecode(zutAUTH.Value);
                    if (strAUTH.Trim() != "" && strAUTH.Split('#').Length == 2)
                    {
                        string[] userName = strAUTH.Split('#')[0].Split(':');
                        string[] password = strAUTH.Split('#')[1].Split(':');

                        LoginUser(_encryptionCtl.Decrypt(HttpUtility.UrlDecode(password[1])), userName[1],
                            HttpContext.Current.Request.Path.Trim() != "" && !(HttpContext.Current.Request.Path.ToLower().Contains("login_new.aspx")) ?
                            HttpContext.Current.Request.Path : "/Index_New.aspx");
                    }
                }
            }
            catch (Exception e)
            { }
        }
        #endregion

        #region Get Permissions
        public PermissionDto GetPermissionsById(int id)
        {
            permissionsDto = new PermissionDto();
            List<AC_PermissionMapping> permissionMappings;
            permissionMappings = _permissionMappingRepository.GetPermissionMappingsByProfileId(id);
            if (permissionMappings == null) { permissionMappings = new List<AC_PermissionMapping>(); return permissionsDto; }

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

        private void CheckSuperUser(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
        {
            permissionsDto.AllTermConfigEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AllTermConfig) && x.PermissionType.Equals(1));
            permissionsDto.AllTermConfigRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AllTermConfig) && x.PermissionType.Equals(2));

            permissionsDto.AllAccessControlEdit = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AllAccessControl) && x.PermissionType.Equals(1));
            permissionsDto.AllAccessControlRead = permissionMappings.Any(x => x.PermissionKey.Equals((int)accessControlPermissions.AllAccessControl) && x.PermissionType.Equals(2));
        }

        private void CheckGroupLevelSuperUser(ref List<AC_PermissionMapping> permissionMappings, ref PermissionDto permissionsDto)
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

        public void GetSessionPermissionKeysMapping()
        {
            permissionKeysMappingList = (List<AC_PermissionMapping>)HttpContext.Current.Session["Pers_PermissionKeysMapping"];
            if (permissionKeysMappingList == null) permissionKeysMappingList = new List<AC_PermissionMapping>();
        }

        public bool CheckForReadOrWritePermissions(accessControlPermissions accessControlPermission)
        {
            GetSessionPermissionKeysMapping();

            if (permissionKeysMappingList.Count == 0) return false;


            return permissionKeysMappingList.Any(x => x.PermissionKey == (int)accessControlPermission &&
            (x.PermissionType == (int)accessControlPermissionModes.Edit || x.PermissionType == (int)accessControlPermissionModes.Read)) ||
            permissionKeysMappingList.Any(x => x.PermissionKey == (int)accessControlPermissions.AllAccessControl &&
            (x.PermissionType == (int)accessControlPermissionModes.Edit || x.PermissionType == (int)accessControlPermissionModes.Read));
            //return false;
        }

        public bool CheckForReadOrWritePermissions(accessControlPermissions accessControlPermission, out accessControlPermissionModes accessControlPermissionMode)
        {
            GetSessionPermissionKeysMapping();
            accessControlPermissionMode = accessControlPermissionModes.Read;

            if (permissionKeysMappingList.Count == 0) return false;

            if (permissionKeysMappingList.Any(x => x.PermissionKey == (int)accessControlPermission &&
            (x.PermissionType == (int)accessControlPermissionModes.Edit)) ||
            permissionKeysMappingList.Any(x => x.PermissionKey == (int)accessControlPermissions.AllAccessControl &&
            (x.PermissionType == (int)accessControlPermissionModes.Edit)))
                accessControlPermissionMode = accessControlPermissionModes.Edit;


            return permissionKeysMappingList.Any(x => x.PermissionKey == (int)accessControlPermission &&
            (x.PermissionType == (int)accessControlPermissionModes.Edit || x.PermissionType == (int)accessControlPermissionModes.Read)) ||
            permissionKeysMappingList.Any(x => x.PermissionKey == (int)accessControlPermissions.AllAccessControl &&
            (x.PermissionType == (int)accessControlPermissionModes.Edit || x.PermissionType == (int)accessControlPermissionModes.Read));
            //return false;
        }
        #endregion

        public void RedirectToLoginPage()
        {
            CheckAuthCookie();
            HttpContext.Current.Response.Redirect("/Content/Login_New.aspx");
        }
        public void SetPromptRedirectPage(string redirectPage)
        {
            HttpContext.Current.Session.Add("Prompt_RedirectPage", redirectPage);
        }

        public void RedirectToPromptPage()
        {
            var redirectPage = HttpContext.Current.Session["Prompt_RedirectPage"];
            if (redirectPage == null) return;
            HttpContext.Current.Session.Remove("Prompt_RedirectPage");
            HttpContext.Current.Response.Redirect(redirectPage.ToString());
        }

        public static T LoadSessionItem<T>(string sessionKey)
        {
            T sessionObject = default(T);
            if (HttpContext.Current.Session[sessionKey] != null)
            {
                try
                {
                    sessionObject = (T)HttpContext.Current.Session[sessionKey];
                }
                catch (Exception ex)
                { Debug.WriteLine(ex.Message); }
            }

            return sessionObject;
        }
    }
}