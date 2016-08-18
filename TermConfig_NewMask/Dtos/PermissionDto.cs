using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TermConfig_NewMask.Dtos
{
    public class PermissionDto
    {
        public PermissionDto() { }
        //Terminal Configuration Complete Access
        public bool AllTermConfigEdit { get; set; }
        public bool AllTermConfigRead { get; set; }

        //Access Control Complete Access
        public bool AllAccessControlEdit { get; set; }
        public bool AllAccessControlRead { get; set; }

        //1. All Settings
        public bool SettingsEdit { get; set; }
        public bool SettingsRead { get; set; }

        //Personal Settings
        public bool PersonnelSettingsRead { get; set; }
        public bool PersonnelSettingsEdit { get; set; }
        public bool LocationRead { get; set; }
        public bool LocationEdit { get; set; }
        public bool DepartmentRead { get; set; }
        public bool DepartmentEdit { get; set; }
        public bool CostCenterRead { get; set; }
        public bool CostCenterEdit { get; set; }
        public bool VehicleRead { get; set; }
        public bool VehicleEdit { get; set; }

        //Access Control Settings
        public bool AccessControlSettingsRead { get; set; }
        public bool AccessControlSettingsEdit { get; set; }

        //General Settings
        public bool GeneralSettingsRead { get; set; }
        public bool GeneralSettingsEdit { get; set; }
        public bool LanguageRead { get; set; }
        public bool LanguageEdit { get; set; }
        public bool ProfileGroupRead { get; set; }
        public bool ProfileGroupEdit { get; set; }
        public bool PlanGroupRead { get; set; }
        public bool PlanGroupEdit { get; set; }
        public bool IconsRead { get; set; }
        public bool IconsEdit { get; set; }
        public bool RightsAssignmentRead { get; set; }
        public bool RightsAssignmentEdit { get; set; }
        public bool PasswordRead { get; set; }
        public bool PasswordEdit { get; set; }

        //2. Master Data
        public bool MasterDataEdit { get; set; }
        public bool MasterDataRead { get; set; }

        //Personnel Data
        public bool PersonnelDataSetRead { get; set; }
        public bool PersonnelDataSetEdit { get; set; }

        //Building Plan
        public bool BuildingPlanRead { get; set; }
        public bool BuildingPlanEdit { get; set; }

        //Access Plan
        public bool AccessPlanRead { get; set; }
        public bool AccessPlanEdit { get; set; }
        public bool AccessPlanSettingsRead { get; set; }
        public bool AccessPlanSettingsEdit { get; set; }
        public bool AccessPlanInfoRead { get; set; }
        public bool AccessPlanInfoEdit { get; set; }

        //Switch Plan
        public bool SwitchPlanRead { get; set; }
        public bool SwitchPlanEdit { get; set; }
        public bool SwitchPlanSettingsRead { get; set; }
        public bool SwitchPlanSettingsEdit { get; set; }
        public bool SwitchPlanInfoRead { get; set; }
        public bool SwitchPlanInfoEdit { get; set; }

        //3. Visitor Managment
        public bool VisitorSettingsEdit { get; set; }
        public bool VisitorSettingsRead { get; set; }

        //Visitor Login Management
        public bool VisitorLoginRead { get; set; }
        public bool VisitorLoginEdit { get; set; }

        //Visitor DataSet Management
        public bool VisitorDataManagementEdit { get; set; }
        public bool VisitorDataManagementRead { get; set; }

        //Visitor Plan Management
        public bool VisitorPlanRead { get; set; }
        public bool VisitorPlanEdit { get; set; }

        //4. Safety Managment
        public bool SafetySettingsEdit { get; set; }
        public bool SafetySettingsRead { get; set; }

        //Access Control Corrections
        public bool GraderRead { get; set; }
        public bool GraderEdit { get; set; }

        //Security Management
        public bool SecurityManagementEdit { get; set; }
        public bool SecurityManagementRead { get; set; }

        //Alarm Function 
        public bool AlarmFuctionRead { get; set; }
        public bool AlarmFunctionEdit { get; set; }

        //5. Access Control Lists 
        public bool AccessControlListsRead { get; set; }
        public bool AccessControlListsEdit { get; set; }

        //6. Communication Permit
        public bool CommunicationEdit { get; set; }

    }
}
