using DevExpress.Web;
using KruAll.Core.Models;
using KruAll.Core.Repositories;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TermConfig_NewMask.App_Code;
using TermConfig_NewMask.Controllers;
using TermConfig_NewMask.Dtos;
using TermConfig_NewMask.ViewModels;
using ZKSDKCommunication;
using ZKSDKCommunication.ZKBusinessObjects;

namespace TermConfig_NewMask.Content
{
    public partial class TM680tc : BasePage
    {
        #region "properties"
        TermKonfigMain mainCtl = new TermKonfigMain();
        public static accessControlPermissionModes accessControlPermissionMode = accessControlPermissionModes.Read;
        TerminalConfigViewModel _termcon = new TerminalConfigViewModel();
        private static StandAloneInterface standAloneSDK = null;
        private const int ZK_TERMINAL_DEFAULT_PORT = 4370;

        #endregion "properties"
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                //if (Session["Pers_Nr"] == null) mainCtl.RedirectToLoginPage();
                //if (!mainCtl.CheckForReadOrWritePermissions(accessControlPermissions.AllTermConfig, out accessControlPermissionMode))
                //    mainCtl.RedirectToLoginPage();

                //if (accessControlPermissionMode != accessControlPermissionModes.Edit)
                //{
                //    btnSendMasterData.Enabled = false;
                //    btnGetBookings.Enabled = false;
                //    btnSetSystemTime.Enabled = false;
                //    btnTestConnection.Enabled = false;
                //    btnSave.Enabled = false;
                //    btnDelete.Enabled = false;

                //    btnAccess.Enabled = false;
                //    btnAddReader.Enabled = false;
                //    btnDeleteReader.Enabled = false;
                //    Session["TMK_NewRedirectMode"] = false;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Load Page without prompt for Save Right",
                //        "setTimeout(function() { allowTMKEdit = false }, 500)", true);
                //}
            }
            catch (Exception ex)
            {
                Utils.WriteToErrorLogFile(ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Form.DefaultButton = this.btnSaveTerminalConfig.UniqueID;

                if (!IsPostBack)
                {
                    //main_navigation.Attributes.Add("onClick", "ToggleItems(this);");
                    if (!string.IsNullOrEmpty(this.Page.Title) && this.Page.Master.FindControl("pageTitleLbl2") != null)
                    {
                        Label pageTitleLbl2 = (Label)(this.Page.Master.FindControl("pageTitleLbl2"));
                        pageTitleLbl2.Text = "TM 680tc";
                    }

                    BindddlCurrentZkTerminalInstance();
                    BindcboLinkType();
                    BindFunctionKeys();
                    LoadgrdZkTerminals();
                    loadgrdZkTerminalCon();
                    string selectedTermId = Request.QueryString["id"];
                    string childId = Request.QueryString["ChildID"];
                    int curChildId = Convert.ToInt32(childId);
                    bool isNewRedirectMode = Session["TMK_NewRedirectMode"] == null ? false : (bool)Session["TMK_NewRedirectMode"];

                    if ((selectedTermId != null) && (childId != null))
                    {
                        if (!isNewRedirectMode)
                        {
                            BindSelectedTerminalControls(selectedTermId, curChildId);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect to New Instance Creation",
                                "setTimeout(function() { checkForNewRedirectMode() }, 500)", true);
                            Session["TMK_NewRedirectMode"] = false;
                        }
                    }
                    else
                    {
                        if (!isNewRedirectMode)
                        {
                            BindInputControls(selectedTermId);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Redirect to New Instance Creation",
                                "setTimeout(function() { checkForNewRedirectMode() }, 500)", true);
                            Session["TMK_NewRedirectMode"] = false;
                        }
                    }
                    loadgrdReaderStatic();
                    loadgrdgrdREaderInfo();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        #region Methods
        public void BindSelectedTerminalControls(string terminalId, int childId)
        {
            try
            {
                int termId = Convert.ToInt32(terminalId);
                TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
                List<TerminalConfig> lst = new List<TerminalConfig>();

                lst = terminalConfigRepository.GetAllTerminalConfig().FindAll(x => x.TerminalId == termId && x.ID > 0 && x.ID == childId);
                ddlCurrentZkTerminalInstance.DataSource = lst;
                ddlCurrentZkTerminalInstance.DataBind();

                ddlCurrentZkTerminalInstance.Value = lst[0].ID.ToString();

                ddlCurrentZkTerminalInstance.Value = lst[0].TerminalId.ToString();
                txtTerminalId.Text = lst[0].TermID.ToString();
                txtDescription.Text = lst[0].Description;
                txtSerialNumber.Text = lst[0].SerialNumber;
                cboLinkType.Value = lst[0].ConnectionType;
                //if (lst[0].ConnectionType == Resources.LocalizedText.none)
                //{
                //    cboLinkType.Value = "0";
                //}
                //if (lst[0].ConnectionType == "TCP/IP")
                //{
                //    cboLinkType.Value = "1";
                //}
                //if (lst[0].ConnectionType == "USB")
                //{
                //    cboLinkType.Value = "2";
                //}
                txtIpAddress.Text = lst[0].IpAddress;
                if (lst[0].Port == -1)
                {
                    txtIpPort.Text = "";
                }
                else
                {
                    txtIpPort.Text = lst[0].Port.ToString();
                }
                if (lst[0].ZkRelayTime == -1)
                {
                    txtRelaySwitching.Text = "";
                }
                else
                {
                    txtRelaySwitching.Text = lst[0].ZkRelayTime.ToString();
                }
                if (lst[0].IsActive == true)
                {
                    btnIsActive.Style.Value = String.Format("background-image: url('/Images/FormImages/Dot_gruen.png')");
                    chkStatus.Checked = true;
                }
                else if (lst[0].IsActive == false)
                {
                    btnIsActive.Style.Value = String.Format("background-image: url('/Images/FormImages/Dot_rot.png')");
                    chkStatus.Checked = false;
                }

                if (lst[0].TerminalUtilities.Count != 0)
                {
                    chkpersonnelNumberPin.Checked = lst[0].TerminalUtilities.FirstOrDefault().HasPersNoPin;
                    chkProfFirmware.Checked = lst[0].TerminalUtilities.FirstOrDefault().HasProfFirmware;
                    chkUseAccessControl.Checked = lst[0].TerminalUtilities.FirstOrDefault().HasAPPosting;
                }

                txtInfoText1.Text = lst[0].TerminalInfoTexts.Any(x => x.InfoTextNr == 1) ? lst[0].TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 1).InfoText : "";
                txtInfoText2.Text = lst[0].TerminalInfoTexts.Any(x => x.InfoTextNr == 2) ? lst[0].TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 2).InfoText : "";
                txtInfoText3.Text = lst[0].TerminalInfoTexts.Any(x => x.InfoTextNr == 3) ? lst[0].TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 3).InfoText : "";
                txtInfoText4.Text = lst[0].TerminalInfoTexts.Any(x => x.InfoTextNr == 4) ? lst[0].TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 4).InfoText : "";

                TerminalFunctionKey f1FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 1);
                cbofunctionKey1.Value = f1FunctionKey != null ? !string.IsNullOrWhiteSpace(f1FunctionKey.FunctionKeyText)
                    ? f1FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f2FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 2);
                cbofunctionKey2.Value = f2FunctionKey != null ? !string.IsNullOrWhiteSpace(f2FunctionKey.FunctionKeyText)
                    ? f2FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f3FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 3);
                cbofunctionKey3.Value = f3FunctionKey != null ? !string.IsNullOrWhiteSpace(f3FunctionKey.FunctionKeyText)
                    ? f3FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f4FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 4);
                cbofunctionKey4.Value = f4FunctionKey != null ? !string.IsNullOrWhiteSpace(f4FunctionKey.FunctionKeyText)
                    ? f4FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f5FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 5);
                cbofunctionKey5.Value = f5FunctionKey != null ? !string.IsNullOrWhiteSpace(f5FunctionKey.FunctionKeyText)
                    ? f5FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f6FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 6);
                cbofunctionKey6.Value = f6FunctionKey != null ? !string.IsNullOrWhiteSpace(f6FunctionKey.FunctionKeyText)
                    ? f6FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f7FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 7);
                cbofunctionKey7.Value = f7FunctionKey != null ? !string.IsNullOrWhiteSpace(f7FunctionKey.FunctionKeyText)
                    ? f7FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f8FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 8);
                cbofunctionKey8.Value = f8FunctionKey != null ? !string.IsNullOrWhiteSpace(f8FunctionKey.FunctionKeyText)
                    ? f8FunctionKey.FunctionKeyText : "0" : "0";

                var readers = new TerminalReaderRepository().GetAllReadersById(childId);
                grdTerminalReader.DataSource = readers;
                grdTerminalReader.DataBind();
                BindReaderInstances(childId);

                grdZkTerminalCon.FocusedRowIndex = grdZkTerminalCon.FindVisibleIndexByKeyValue(childId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        public void BindInputControls(string terminalId)
        {
            List<TerminalConfig> lst = new List<TerminalConfig>();
            TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
            try
            {
                if (terminalId == null)
                {
                    TerminalRepository repo = new TerminalRepository();
                    Terminal terminal = new Terminal();
                    terminal = repo.GetTerminalbyType("TM 680tc");
                    int curTerminalId = terminal.ID;

                    TerminalConfigRepository termConfRepo = new TerminalConfigRepository();
                    TerminalConfig termConf = new TerminalConfig();
                    if (termConf != null)
                    {
                        termConf = termConfRepo.GetByTerminalId(curTerminalId).FirstOrDefault();
                        terminalId = termConf.ID.ToString();
                        lst = terminalConfigRepository.GetAllTerminalConfig().Where(x => x.ID == Convert.ToInt32(terminalId)).OrderBy(i => i.ID).ToList();
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    lst = terminalConfigRepository.GetAllTerminalConfig().Where(x => x.TerminalId == Convert.ToInt32(terminalId)).OrderBy(i => i.ID).ToList();
                }
                ddlCurrentZkTerminalInstance.DataSource = lst;
                ddlCurrentZkTerminalInstance.DataBind();

                ddlCurrentZkTerminalInstance.Value = lst[0].ID.ToString();
                ddlCurrentZkTerminalInstance.Value = lst[0].TerminalId.ToString();
                txtTerminalId.Text = lst[0].TermID.ToString();
                txtDescription.Text = lst[0].Description;
                txtSerialNumber.Text = lst[0].SerialNumber;
                cboLinkType.Value = lst[0].ConnectionType;
                //if (lst[0].ConnectionType == Resources.LocalizedText.none)
                //{
                //    cboLinkType.Value = "0";
                //}
                //if (lst[0].ConnectionType == "TCP/IP")
                //{
                //    cboLinkType.Value = "1";
                //}
                //if (lst[0].ConnectionType == "USB")
                //{
                //    cboLinkType.Value = "2";
                //}
                txtIpAddress.Text = lst[0].IpAddress;
                if (lst[0].Port == -1)
                {
                    txtIpPort.Text = "";
                }
                else
                {
                    txtIpPort.Text = lst[0].Port.ToString();
                }
                if (lst[0].ZkRelayTime == -1)
                {
                    txtRelaySwitching.Text = "";
                }
                else
                {
                    txtRelaySwitching.Text = lst[0].ZkRelayTime.ToString();
                }
                if (lst[0].IsActive == true)
                {
                    btnIsActive.Style.Value = String.Format("background-image: url('/Images/FormImages/Dot_gruen.png')");
                    chkStatus.Checked = true;
                }
                else if (lst[0].IsActive == false)
                {
                    btnIsActive.Style.Value = String.Format("background-image: url('/Images/FormImages/Dot_rot.png')");
                    chkStatus.Checked = false;
                }

                if (lst[0].TerminalUtilities.Count != 0)
                {
                    chkpersonnelNumberPin.Checked = lst[0].TerminalUtilities.FirstOrDefault().HasPersNoPin;
                    chkProfFirmware.Checked = lst[0].TerminalUtilities.FirstOrDefault().HasProfFirmware;
                    chkUseAccessControl.Checked = lst[0].TerminalUtilities.FirstOrDefault().HasAPPosting;
                }

                txtInfoText1.Text = lst[0].TerminalInfoTexts.Any(x => x.InfoTextNr == 1) ? lst[0].TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 1).InfoText : "";
                txtInfoText2.Text = lst[0].TerminalInfoTexts.Any(x => x.InfoTextNr == 2) ? lst[0].TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 2).InfoText : "";
                txtInfoText3.Text = lst[0].TerminalInfoTexts.Any(x => x.InfoTextNr == 3) ? lst[0].TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 3).InfoText : "";
                txtInfoText4.Text = lst[0].TerminalInfoTexts.Any(x => x.InfoTextNr == 4) ? lst[0].TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 4).InfoText : "";

                TerminalFunctionKey f1FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 1);
                cbofunctionKey1.Value = f1FunctionKey != null ? !string.IsNullOrWhiteSpace(f1FunctionKey.FunctionKeyText)
                    ? f1FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f2FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 2);
                cbofunctionKey2.Value = f2FunctionKey != null ? !string.IsNullOrWhiteSpace(f2FunctionKey.FunctionKeyText)
                    ? f2FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f3FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 3);
                cbofunctionKey3.Value = f3FunctionKey != null ? !string.IsNullOrWhiteSpace(f3FunctionKey.FunctionKeyText)
                    ? f3FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f4FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 4);
                cbofunctionKey4.Value = f4FunctionKey != null ? !string.IsNullOrWhiteSpace(f4FunctionKey.FunctionKeyText)
                    ? f4FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f5FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 5);
                cbofunctionKey5.Value = f5FunctionKey != null ? !string.IsNullOrWhiteSpace(f5FunctionKey.FunctionKeyText)
                    ? f5FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f6FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 6);
                cbofunctionKey6.Value = f6FunctionKey != null ? !string.IsNullOrWhiteSpace(f6FunctionKey.FunctionKeyText)
                    ? f6FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f7FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 7);
                cbofunctionKey7.Value = f7FunctionKey != null ? !string.IsNullOrWhiteSpace(f7FunctionKey.FunctionKeyText)
                    ? f7FunctionKey.FunctionKeyText : "0" : "0";
                TerminalFunctionKey f8FunctionKey = lst[0].TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 8);
                cbofunctionKey8.Value = f8FunctionKey != null ? !string.IsNullOrWhiteSpace(f8FunctionKey.FunctionKeyText)
                    ? f8FunctionKey.FunctionKeyText : "0" : "0";

                var readers = new TerminalReaderRepository().GetAllReadersById(lst[0].ID);
                grdTerminalReader.DataSource = readers;
                grdTerminalReader.DataBind();
                BindReaderInstances(lst[0].ID);

                grdZkTerminalCon.FocusedRowIndex = grdZkTerminalCon.FindVisibleIndexByKeyValue(lst[0].ID);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }
        protected void BindddlCurrentZkTerminalInstance()
        {
            try
            {
                TerminalRepository terminalRepository = new TerminalRepository();
                List<Terminal> lst = new List<Terminal>();

                lst = terminalRepository.GetAllTerminals().FindAll(x => x.TermOEMID == 1);
                ddlCurrentZkTerminalInstance.DataSource = lst;
                ddlCurrentZkTerminalInstance.DataBind();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }
        protected void BindcboLinkType()
        {
            try
            {
                TerminalConnectionViewModel vw = new TerminalConnectionViewModel();
                List<TerminalConnectionType> lst = new List<TerminalConnectionType>();
                List<TerminalConnectionType> lst2 = new List<TerminalConnectionType>();
                List<TerminalConnectionType> lst3 = new List<TerminalConnectionType>();
                lst2.Add(new TerminalConnectionType() { ID = 0, ConnectionType = Resources.LocalizedText.none });
                lst = vw.TerminalConnectionTypes();
                lst3 = lst2.Union(lst).ToList();
                cboLinkType.DataSource = lst3;
                cboLinkType.DataBind();
                cboLinkType.Value = "0";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }
        protected void LoadgrdZkTerminals()
        {
            try
            {
                TerminalRepository terminalRepository = new TerminalRepository();
                List<Terminal> lst = new List<Terminal>();

                lst = terminalRepository.GetAllTerminals().FindAll(x => x.TermOEMID == 1);
                grdZkTerminals.DataSource = lst;
                grdZkTerminals.DataBind();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        protected void BindFunctionKeys()
        {
            List<ZITERM_V20_Vorlauftasten> lstStatus_Dynamic = new List<ZITERM_V20_Vorlauftasten>();
            List<Status> lstStatus_Static = new List<Status>();
            Status_DynamicRepository status_DynamicRepository = new Status_DynamicRepository();
            Status_StaticRepository status_StaticRepository = new Status_StaticRepository();
            List<StatusDto> lstStatusDtoDynamic = new List<StatusDto>();
            List<StatusDto> lstStatusDto = new List<StatusDto>();
            List<StatusDto> lstStatusDtoStatic = new List<StatusDto>();

            //lstStatusDtoStatic.Add(new StatusDto() { ID = 0, StatusNr = 100, StatusName = Resources.LocalizedText.none });
            lstStatusDtoDynamic.Add(new StatusDto() { StatusNr = 0, StatusName = Resources.LocalizedText.none });

            StatusDto stDynamicDto = null;
            StatusDto stStaticDto = null;

            try
            {
                lstStatus_Dynamic = status_DynamicRepository.GetAllStatus_Dynamic();
                lstStatus_Static = status_StaticRepository.GetAllStatus_Static();

                foreach (ZITERM_V20_Vorlauftasten st in lstStatus_Dynamic)
                {
                    stDynamicDto = new StatusDto();
                    //stDynamicDto.ID = st.ID;
                    stDynamicDto.StatusNr = st.V_Nr + 10;
                    stDynamicDto.StatusName = st.V_Bezeichnung;
                    lstStatusDtoDynamic.Add(stDynamicDto);
                }

                foreach (Status st in lstStatus_Static)
                {
                    stStaticDto = new StatusDto();
                    //stStaticDto.ID = st.ID;
                    stStaticDto.StatusNr = st.Stat_Nr + 10;
                    stStaticDto.StatusName = st.Stat_Bezeichnung;
                    lstStatusDtoStatic.Add(stStaticDto);
                }

                lstStatusDto = lstStatusDtoDynamic.Union(lstStatusDtoStatic).OrderBy(x => x.StatusName).ToList();
                //lstStatusDto = lstStatusDtoDynamic.OrderBy(x => x.StatusNr).ToList();
                cbofunctionKey1.DataSource = lstStatusDto;
                cbofunctionKey1.DataBind();
                cbofunctionKey1.Value = "0";
                cbofunctionKey2.DataSource = lstStatusDto;
                cbofunctionKey2.DataBind();
                cbofunctionKey2.Value = "0";
                cbofunctionKey3.DataSource = lstStatusDto;
                cbofunctionKey3.DataBind();
                cbofunctionKey3.Value = "0";
                cbofunctionKey4.DataSource = lstStatusDto;
                cbofunctionKey4.DataBind();
                cbofunctionKey4.Value = "0";
                cbofunctionKey5.DataSource = lstStatusDto;
                cbofunctionKey5.DataBind();
                cbofunctionKey5.Value = "0";
                cbofunctionKey6.DataSource = lstStatusDto;
                cbofunctionKey6.DataBind();
                cbofunctionKey6.Value = "0";
                cbofunctionKey7.DataSource = lstStatusDto;
                cbofunctionKey7.DataBind();
                cbofunctionKey7.Value = "0";
                cbofunctionKey8.DataSource = lstStatusDto;
                cbofunctionKey8.DataBind();
                cbofunctionKey8.Value = "0";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }
        public void loadgrdZkTerminalCon()
        {
            try
            {
                int curTerminalId = (int)grdZkTerminals.GetRowValues(grdZkTerminals.FocusedRowIndex, "ID");
                TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
                List<TerminalConfig> lst = new List<TerminalConfig>();
                //lst = terminalConfigRepository.GetTermListByOEMId(1).OrderBy(x => x.TerminalId).ToList(); 
                lst = terminalConfigRepository.GetAllTerminalConfig().OrderBy(x => x.TermID).ToList();

                TerminalZkDto zkDto = null;
                List<TerminalZkDto> lstDto = new List<TerminalZkDto>();
                foreach (TerminalConfig terminalConfig in lst)
                {
                    zkDto = new TerminalZkDto();
                    zkDto.ID = terminalConfig.ID;
                    zkDto.TermID = terminalConfig.TermID;
                    zkDto.TermType = terminalConfig.TermType;
                    zkDto.Description = terminalConfig.Description;
                    zkDto.IpAddress = terminalConfig.IpAddress;
                    zkDto.ConnectionType = terminalConfig.ConnectionType;
                    zkDto.IsActive = terminalConfig.IsActive;
                    zkDto.SerialNumber = terminalConfig.SerialNumber;
                    zkDto.Port = terminalConfig.Port;
                    zkDto.TerminalOEMId = terminalConfig.TerminalOEMId;
                    zkDto.TerminalId = terminalConfig.TerminalId;
                    lstDto.Add(zkDto);
                }

                grdZkTerminalCon.DataSource = lstDto;
                grdZkTerminalCon.DataBind();

                //if (lstDto.Count <= 25)
                //{
                //    grdZkTerminalCon.SettingsPager.Mode = GridViewPagerMode.ShowPager;
                //}
                //else
                //{
                //    grdZkTerminalCon.SettingsPager.Mode = GridViewPagerMode.ShowAllRecords;
                //}

                if (lstDto.Count() > 29)
                {
                    grdZkTerminalCon.SettingsPager.PageSize = lstDto.Count();
                }

                //grdZkTerminalCon.FocusedRowIndex = grdZkTerminalCon.FindVisibleIndexByKeyValue(Request.QueryString["ChildID"]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }
        protected void grdZkTerminalCon_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var _sender = (ASPxGridView)sender;
                var ChildID = _sender.GetRowValues(_sender.FocusedRowIndex, "ID");

                var TerminalId = _sender.GetRowValues(_sender.FocusedRowIndex, "TerminalId");

                if (TerminalId.Equals(1))
                {
                    Response.RedirectLocation = Page.ResolveUrl("TM560bc.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(2))
                {
                    Response.RedirectLocation = Page.ResolveUrl("TM560tc.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(3))
                {
                    Response.RedirectLocation = Page.ResolveUrl("TM680tc.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(4))
                {
                    Response.RedirectLocation = Page.ResolveUrl("TM680bc.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(5))
                {
                    Response.RedirectLocation = Page.ResolveUrl("TM900bc.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(6))
                {
                    Response.RedirectLocation = Page.ResolveUrl("SC403.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(7))
                {
                    Response.RedirectLocation = Page.ResolveUrl("SRC100.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(8))
                {
                    Response.RedirectLocation = Page.ResolveUrl("MA300out.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(9))
                {
                    Response.RedirectLocation = Page.ResolveUrl("TF1700.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(10))
                {
                    Response.RedirectLocation = Page.ResolveUrl("ZB702in.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(11))
                {
                    Response.RedirectLocation = Page.ResolveUrl("ZB703in.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(12))
                {
                    Response.RedirectLocation = Page.ResolveUrl("ZBBI402_New.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(13))
                {
                    Response.RedirectLocation = Page.ResolveUrl("ZBBi-404New.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(14))
                {
                    Response.RedirectLocation = Page.ResolveUrl("DatafoxNew.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(18))
                {
                    Response.RedirectLocation = Page.ResolveUrl("DatafoxNewEvo43.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(1019))
                {
                    Response.RedirectLocation = Page.ResolveUrl("DatafoxPZEMasterIV.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(1020))
                {
                    Response.RedirectLocation = Page.ResolveUrl("DatafoxTMMasterIV.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(1021))
                {
                    Response.RedirectLocation = Page.ResolveUrl("DatafoxTMFlexMaster.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(1022))
                {
                    Response.RedirectLocation = Page.ResolveUrl("DatafoxTimeboxIV.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
                if (TerminalId.Equals(1023))
                {
                    Response.RedirectLocation = Page.ResolveUrl("DatafoxMasterIV.aspx?id=" + TerminalId + "&ChildID=" + ChildID);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        protected void grdZkTerminals_SelectionChanged(object sender, EventArgs e)
        {
            var _sender = (ASPxGridView)sender;
            TerminalRepository repo = new TerminalRepository();
            Terminal terminal = new Terminal();
            try
            {
                var staticTerminalId = _sender.GetRowValues(_sender.FocusedRowIndex, "ID");
                terminal = repo.GetTerminalbyID(Convert.ToInt32(staticTerminalId));
                string curTerminalPage = terminal.TerminalPage;
                Session["TMK_NewRedirectMode"] = true;
                Response.RedirectLocation = Page.ResolveUrl(curTerminalPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        protected void grdZkTerminals_DataBound(object sender, EventArgs e)
        {
            try
            {
                TerminalRepository repo = new TerminalRepository();
                Terminal terminal = new Terminal();
                terminal = repo.GetTerminalbyType("TM 680tc");
                int curTerminalId = terminal.ID;
                //grdZkTerminals.FocusedRowIndex = grdZkTerminals.FindVisibleIndexByKeyValue(curTerminalId);
                grdZkTerminals.FocusedRowIndex = -1;
                ddlCurrentZkTerminalInstance.Value = terminal.ID.ToString();
                //Session["firstRowId"] = grdZkTerminals.GetRowValues(grdZkTerminals.FocusedRowIndex, "ID").ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto CreateZkTerminalInstance(string terminalType, string terminalId, string usertermID, string terminalDescription, string serialNumber, string linkType,
                string ipAddress, int ipPort, bool backToReadFingerPrint, bool APPosting, bool ProfFirmware, string infoText1, string infoText2, string infoText3, string infoText4,
string functionKey1, string functionKey2, string functionKey3, string functionKey4, string functionKey5, string functionKey6, string functionKey7, string functionKey8, bool IsActiveTerminal, int RelaySwitching, bool PersNoPin,
string readerInternDesc, string readerExternDesc, int readerInternDirec, int readerExternDirec, string readerRelayTime, string externalReaderId, string readerType, int readerStaticId)
        {
            TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
            TerminalConfig zkInstance = null;
            TerminalZkDto zkDto = new TerminalZkDto();
            try
            {
                List<TerminalUtility> lstUtility = new List<TerminalUtility>();
                List<TerminalInfoText> lstInfoText = new List<TerminalInfoText>();
                List<TerminalFunctionKey> lstFunctionKeys = new List<TerminalFunctionKey>();
                lstUtility.Add(new TerminalUtility
                {
                    HasFPRead = backToReadFingerPrint,
                    HasProfFirmware = ProfFirmware,
                    HasAPPosting = APPosting,
                    HasPersNoPin = PersNoPin
                });

                if (infoText1 != "")
                {
                    lstInfoText.Add(new TerminalInfoText
                    {
                        InfoTextNr = 1,
                        InfoText = infoText1
                    });
                }
                if (infoText2 != "")
                {
                    lstInfoText.Add(new TerminalInfoText
                    {
                        InfoTextNr = 2,
                        InfoText = infoText2
                    });
                }
                if (infoText3 != "")
                {
                    lstInfoText.Add(new TerminalInfoText
                    {
                        InfoTextNr = 3,
                        InfoText = infoText3
                    });
                }
                if (infoText4 != "")
                {
                    lstInfoText.Add(new TerminalInfoText
                    {
                        InfoTextNr = 4,
                        InfoText = infoText4
                    });
                }
                if (functionKey1 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 1,
                        FunctionKeyText = functionKey1
                    });
                }
                if (functionKey2 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 2,
                        FunctionKeyText = functionKey2
                    });
                }
                if (functionKey3 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 3,
                        FunctionKeyText = functionKey3
                    });
                }
                if (functionKey4 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 4,
                        FunctionKeyText = functionKey4
                    });
                }
                if (functionKey5 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 5,
                        FunctionKeyText = functionKey5
                    });
                }
                if (functionKey6 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 6,
                        FunctionKeyText = functionKey6
                    });
                }
                if (functionKey7 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 7,
                        FunctionKeyText = functionKey7
                    });
                }
                if (functionKey8 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 8,
                        FunctionKeyText = functionKey8
                    });
                }

                zkInstance = new TerminalConfig()
                {
                    TermType = terminalType,
                    TerminalId = Convert.ToInt32(terminalId),
                    TermID = Convert.ToInt32(usertermID),
                    Description = terminalDescription,
                    SerialNumber = serialNumber,
                    ConnectionType = linkType,
                    IpAddress = ipAddress,
                    Port = ipPort,
                    IsActive = IsActiveTerminal,
                    ZkRelayTime = RelaySwitching,
                    TerminalOEMId = 1, //Default for all Zk terminals  
                    TerminalUtilities = lstUtility,
                    TerminalInfoTexts = lstInfoText,
                    TerminalFunctionKeys = lstFunctionKeys
                };
                terminalConfigRepository.NewTerminalConfig(zkInstance);
                //TM680tc tc = new TM680tc();
                //tc.CreateTerminalInstanceReader(zkInstance.ID);
                if (zkInstance != null)
                {

                    CreateTerminalInstanceReader(zkInstance.ID, zkInstance.Description, readerInternDesc, zkInstance.IsActive, readerInternDirec, readerRelayTime);
                    SaveExternalReader(zkInstance.ID, zkInstance.IsActive, readerExternDesc, readerExternDirec, readerRelayTime, externalReaderId, readerType, readerStaticId);
                }

                zkDto.ID = zkInstance.ID;
                zkDto.TermID = zkInstance.TermID;
                zkDto.TermType = zkInstance.TermType;
                zkDto.TerminalId = zkInstance.TerminalId;
                zkDto.Description = zkInstance.Description;
                zkDto.SerialNumber = zkInstance.SerialNumber;
                zkDto.ConnectionType = zkInstance.ConnectionType;
                zkDto.IpAddress = zkInstance.IpAddress;
                zkDto.Port = zkInstance.Port;
                zkDto.IsActive = zkInstance.IsActive;
                zkDto.ZkRelayTime = zkInstance.ZkRelayTime;
                zkDto.HasFPRead = zkInstance.TerminalUtilities.FirstOrDefault().HasFPRead;
                zkDto.HasAPPosting = zkInstance.TerminalUtilities.FirstOrDefault().HasAPPosting;
                zkDto.HasProfFirmware = zkInstance.TerminalUtilities.FirstOrDefault().HasProfFirmware;
                zkDto.HasPersNoPin = zkInstance.TerminalUtilities.FirstOrDefault().HasPersNoPin;
                zkDto.InfoText1 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 1) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 1).InfoText : "";
                zkDto.InfoText2 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 2) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 2).InfoText : "";
                zkDto.InfoText3 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 3) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 3).InfoText : "";
                zkDto.InfoText4 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 4) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 4).InfoText : "";

                zkDto.FunctionKeyText1 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 1) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 1).FunctionKeyText : "0";
                zkDto.FunctionKeyText2 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 2) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 2).FunctionKeyText : "0";
                zkDto.FunctionKeyText3 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 3) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 3).FunctionKeyText : "0";
                zkDto.FunctionKeyText4 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 4) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 4).FunctionKeyText : "0";
                zkDto.FunctionKeyText5 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 5) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 5).FunctionKeyText : "0";
                zkDto.FunctionKeyText6 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 6) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 6).FunctionKeyText : "0";
                zkDto.FunctionKeyText7 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 7) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 7).FunctionKeyText : "0";
                zkDto.FunctionKeyText8 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 8) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 8).FunctionKeyText : "0";
                // changes
                GetReadersInstances(zkInstance.ID, zkDto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return zkDto;
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto UpdateZkTerminalInstance(string Id, string terminalType, string terminalId, string usertermID, string terminalDescription, string serialNumber, string linkType,
                string ipAddress, int ipPort, bool backToReadFingerPrint, bool APPosting, bool ProfFirmware, string infoText1, string infoText2, string infoText3, string infoText4,
string functionKey1, string functionKey2, string functionKey3, string functionKey4, string functionKey5, string functionKey6, string functionKey7, string functionKey8, bool IsActiveTerminal, int RelaySwitching, bool PersNoPin,
string readerInternDesc, string readerExternDesc, int readerInternDirec, int readerExternDirec, string readerRelayTime, string externalReaderId, string internReaderId, string readerType, int readerStaticId)
        {
            TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
            TerminalConfig zkInstance = null;
            TerminalZkDto zkDto = new TerminalZkDto();
            TerminalUtilityRepository utilrepo = new TerminalUtilityRepository();
            TerminalFunctionKeyRespository functionkeyrepo = new TerminalFunctionKeyRespository();
            TerminalInfoTextRepository tifRepo = new TerminalInfoTextRepository();
            try
            {
                List<TerminalUtility> lstUtility = new List<TerminalUtility>();
                List<TerminalInfoText> lstInfoText = new List<TerminalInfoText>();
                List<TerminalFunctionKey> lstFunctionKeys = new List<TerminalFunctionKey>();
                zkInstance = terminalConfigRepository.GetTerminalConfigbyID(Convert.ToInt64(Id));
                utilrepo.DeleteTerminalUtilityByTerminalConfigId(zkInstance.ID);
                lstUtility.Add(new TerminalUtility
                {
                    HasFPRead = backToReadFingerPrint,
                    HasProfFirmware = ProfFirmware,
                    HasAPPosting = APPosting,
                    HasPersNoPin = PersNoPin
                });

                TerminalInfoText terminalInfoText1 = zkInstance.TerminalInfoTexts.FirstOrDefault(i => i.InfoTextNr == 1);
                if (terminalInfoText1 != null)
                {
                    terminalInfoText1.InfoText = infoText1;
                }
                else if (infoText1 != "")
                {
                    zkInstance.TerminalInfoTexts.Add(new TerminalInfoText
                    {
                        InfoTextNr = 1,
                        InfoText = infoText1
                    });
                }
                TerminalInfoText terminalInfoText2 = zkInstance.TerminalInfoTexts.FirstOrDefault(i => i.InfoTextNr == 2);
                if (terminalInfoText2 != null)
                {
                    terminalInfoText2.InfoText = infoText2;
                }
                else if (infoText2 != "")
                {
                    zkInstance.TerminalInfoTexts.Add(new TerminalInfoText
                    {
                        InfoTextNr = 2,
                        InfoText = infoText2
                    });
                }
                TerminalInfoText terminalInfoText3 = zkInstance.TerminalInfoTexts.FirstOrDefault(i => i.InfoTextNr == 3);
                if (terminalInfoText3 != null)
                {
                    terminalInfoText3.InfoText = infoText3;
                }
                else if (infoText3 != "")
                {
                    zkInstance.TerminalInfoTexts.Add(new TerminalInfoText
                    {
                        InfoTextNr = 3,
                        InfoText = infoText3
                    });
                }
                TerminalInfoText terminalInfoText4 = zkInstance.TerminalInfoTexts.FirstOrDefault(i => i.InfoTextNr == 4);
                if (terminalInfoText4 != null)
                {
                    terminalInfoText4.InfoText = infoText4;
                }
                else if (infoText4 != "")
                {
                    zkInstance.TerminalInfoTexts.Add(new TerminalInfoText
                    {
                        InfoTextNr = 4,
                        InfoText = infoText4
                    });
                }

                //if (infoText1 != "")
                //{
                //    tifRepo.DeleteTerminalInfoTextsByTermConfId(zkInstance.ID);
                //    lstInfoText.Add(new TerminalInfoText
                //    {
                //        InfoTextNr = 1,
                //        InfoText = infoText1
                //    });
                //}
                //if (infoText2 != "")
                //{
                //    tifRepo.DeleteTerminalInfoTextsByTermConfId(zkInstance.ID);
                //    lstInfoText.Add(new TerminalInfoText
                //    {
                //        InfoTextNr = 2,
                //        InfoText = infoText2
                //    });
                //}
                //if (infoText3 != "")
                //{
                //    tifRepo.DeleteTerminalInfoTextsByTermConfId(zkInstance.ID);
                //    lstInfoText.Add(new TerminalInfoText
                //    {
                //        InfoTextNr = 3,
                //        InfoText = infoText3
                //    });
                //}
                //if (infoText4 != "")
                //{
                //    tifRepo.DeleteTerminalInfoTextsByTermConfId(zkInstance.ID);
                //    lstInfoText.Add(new TerminalInfoText
                //    {
                //        InfoTextNr = 4,
                //        InfoText = infoText4
                //    });
                //}
                if (functionKey1 != "0")
                {
                    functionkeyrepo.DeleteTerminalFunctionKeysByTermConfId(zkInstance.ID);
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 1,
                        FunctionKeyText = functionKey1
                    });
                }
                if (functionKey2 != "0")
                {
                    functionkeyrepo.DeleteTerminalFunctionKeysByTermConfId(zkInstance.ID);
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 2,
                        FunctionKeyText = functionKey2
                    });
                }
                if (functionKey3 != "0")
                {
                    functionkeyrepo.DeleteTerminalFunctionKeysByTermConfId(zkInstance.ID);
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 3,
                        FunctionKeyText = functionKey3
                    });
                }
                if (functionKey4 != "0")
                {
                    functionkeyrepo.DeleteTerminalFunctionKeysByTermConfId(zkInstance.ID);
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 4,
                        FunctionKeyText = functionKey4
                    });
                }
                if (functionKey5 != "0")
                {
                    functionkeyrepo.DeleteTerminalFunctionKeysByTermConfId(zkInstance.ID);
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 5,
                        FunctionKeyText = functionKey5
                    });
                }
                if (functionKey6 != "0")
                {
                    functionkeyrepo.DeleteTerminalFunctionKeysByTermConfId(zkInstance.ID);
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 6,
                        FunctionKeyText = functionKey6
                    });
                }
                if (functionKey7 != "0")
                {
                    functionkeyrepo.DeleteTerminalFunctionKeysByTermConfId(zkInstance.ID);
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 7,
                        FunctionKeyText = functionKey7
                    });
                }
                if (functionKey8 != "0")
                {
                    functionkeyrepo.DeleteTerminalFunctionKeysByTermConfId(zkInstance.ID);
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 8,
                        FunctionKeyText = functionKey8
                    });
                }
                zkInstance.TermType = terminalType;
                zkInstance.TerminalId = Convert.ToInt32(terminalId);
                zkInstance.TermID = Convert.ToInt32(usertermID);
                zkInstance.Description = terminalDescription;
                zkInstance.SerialNumber = serialNumber;
                zkInstance.ConnectionType = linkType;
                zkInstance.IpAddress = ipAddress;
                zkInstance.Port = ipPort;
                zkInstance.IsActive = IsActiveTerminal;
                zkInstance.ZkRelayTime = RelaySwitching;
                zkInstance.TerminalOEMId = 1; //Default for all Zk terminals  
                zkInstance.TerminalUtilities = lstUtility;
                // zkInstance.TerminalInfoTexts = lstInfoText;
                zkInstance.TerminalFunctionKeys = lstFunctionKeys;

                terminalConfigRepository.EditTerminalConfig(zkInstance);
                if (zkInstance != null)
                {

                    UpdateTerminalInstanceReader(zkInstance.ID, zkInstance.IsActive, zkInstance.Description, readerInternDesc, readerInternDirec, readerRelayTime, internReaderId);
                    SaveExternalReader(zkInstance.ID, zkInstance.IsActive, readerExternDesc, readerExternDirec, readerRelayTime, externalReaderId, readerType, readerStaticId);
                }

                zkDto.ID = zkInstance.ID;
                zkDto.TermID = zkInstance.TermID;
                zkDto.TermType = zkInstance.TermType;
                zkDto.TerminalId = zkInstance.TerminalId;
                zkDto.Description = zkInstance.Description;
                zkDto.SerialNumber = zkInstance.SerialNumber;
                zkDto.ConnectionType = zkInstance.ConnectionType;
                zkDto.IpAddress = zkInstance.IpAddress;
                zkDto.Port = zkInstance.Port;
                zkDto.IsActive = zkInstance.IsActive;
                zkDto.ZkRelayTime = zkInstance.ZkRelayTime;
                zkDto.HasFPRead = zkInstance.TerminalUtilities.FirstOrDefault().HasFPRead;
                zkDto.HasAPPosting = zkInstance.TerminalUtilities.FirstOrDefault().HasAPPosting;
                zkDto.HasProfFirmware = zkInstance.TerminalUtilities.FirstOrDefault().HasProfFirmware;
                zkDto.HasPersNoPin = zkInstance.TerminalUtilities.FirstOrDefault().HasPersNoPin;
                zkDto.InfoText1 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 1) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 1).InfoText : "";
                zkDto.InfoText2 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 2) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 2).InfoText : "";
                zkDto.InfoText3 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 3) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 3).InfoText : "";
                zkDto.InfoText4 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 4) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 4).InfoText : "";

                zkDto.FunctionKeyText1 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 1) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 1).FunctionKeyText : "0";
                zkDto.FunctionKeyText2 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 2) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 2).FunctionKeyText : "0";
                zkDto.FunctionKeyText3 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 3) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 3).FunctionKeyText : "0";
                zkDto.FunctionKeyText4 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 4) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 4).FunctionKeyText : "0";
                zkDto.FunctionKeyText5 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 5) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 5).FunctionKeyText : "0";
                zkDto.FunctionKeyText6 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 6) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 6).FunctionKeyText : "0";
                zkDto.FunctionKeyText7 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 7) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 7).FunctionKeyText : "0";
                zkDto.FunctionKeyText8 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 8) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 8).FunctionKeyText : "0";
                // changes
                GetReadersInstances(zkInstance.ID, zkDto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return zkDto;
        }


        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalConfig GetLatestUserTermID(string dummyVar)
        {
            String Dummy = dummyVar;
            int nextUserTermID;
            TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
            List<TerminalConfig> listUserTerminals = new List<TerminalConfig>();
            TerminalConfig terminalConfig = new TerminalConfig();
            TerminalConfig terminalConfig2 = new TerminalConfig();
            try
            {
                /* listUserTerminals = terminalConfigRepository.GetTermListByOEMId(1);*/
                // 1 is the default TerminalOEM ID for Zk terminals: 2 for datafox terminals
                listUserTerminals = terminalConfigRepository.GetAllTerminalConfig();
                if (listUserTerminals.Count == 0)
                {
                    terminalConfig.TermID = 1;
                    terminalConfig2.TermID = terminalConfig.TermID;
                }
                else
                {
                    terminalConfig = listUserTerminals.OrderByDescending(i => i.TermID).FirstOrDefault();
                    nextUserTermID = terminalConfig.TermID;
                    nextUserTermID++;
                    terminalConfig2.TermID = nextUserTermID;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return terminalConfig2;
        }

        protected void grdZkTerminalCon_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            loadgrdZkTerminalCon();
        }

        protected void grdZkTerminalCon_DataBound(object sender, EventArgs e)
        {
            loadgrdZkTerminalCon();
        }

        [System.Web.Services.WebMethod]
        public static string GetLocalizedText(string key)
        {
            try
            {

                var culture = HttpContext.Current.Session["PreferredCulture"].ToString();
                var cultureInfo = new CultureInfo(culture);

                var text = (string)HttpContext.GetGlobalResourceObject("LocalizedText", key, cultureInfo);
                return text;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return null;
        }

        [System.Web.Services.WebMethod]
        [System.Web.Script.Services.ScriptMethod()]
        public static Terminal GetTerminalByTypeName(string typeName)
        {
            TerminalRepository repo = new TerminalRepository();
            Terminal terminal = new Terminal();
            Terminal terminal2 = new Terminal();
            try
            {
                terminal = repo.GetTerminalbyType(typeName);
                terminal2.ID = terminal.ID;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return terminal2;
        }

        [System.Web.Services.WebMethod]
        public static List<TerminalZkDto> DeleteCurrentZkTerminalInstance(int ID, int terminalId)
        {
            TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
            TerminalConfigRepository terminalConfigRepository2 = new TerminalConfigRepository();
            List<TerminalConfig> lstTerminalInstances = new List<TerminalConfig>();
            List<TerminalZkDto> lstTerminalInstances2 = new List<TerminalZkDto>();
            TerminalZkDto zkDto = null;
            try
            {
                terminalConfigRepository.DeleteTerminalConfigByID(ID);
                lstTerminalInstances = terminalConfigRepository2.GetByTerminalId(terminalId);
                foreach (TerminalConfig zkInstance in lstTerminalInstances)
                {
                    zkDto = new TerminalZkDto();
                    zkDto.ID = zkInstance.ID;
                    zkDto.TermID = zkInstance.TermID;
                    zkDto.TermType = zkInstance.TermType;
                    zkDto.TerminalId = zkInstance.TerminalId;
                    zkDto.Description = zkInstance.Description;
                    zkDto.SerialNumber = zkInstance.SerialNumber;
                    zkDto.ConnectionType = zkInstance.ConnectionType;
                    zkDto.IpAddress = zkInstance.IpAddress;
                    zkDto.Port = zkInstance.Port;
                    zkDto.ZkRelayTime = zkInstance.ZkRelayTime;
                    zkDto.HasFPRead = zkInstance.TerminalUtilities.FirstOrDefault().HasFPRead;
                    zkDto.HasAPPosting = zkInstance.TerminalUtilities.FirstOrDefault().HasAPPosting;
                    zkDto.HasProfFirmware = zkInstance.TerminalUtilities.FirstOrDefault().HasProfFirmware;
                    zkDto.InfoText1 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 1) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 1).InfoText : "";
                    zkDto.InfoText2 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 2) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 2).InfoText : "";
                    zkDto.InfoText3 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 3) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 3).InfoText : "";
                    zkDto.InfoText4 = zkInstance.TerminalInfoTexts.Any(x => x.InfoTextNr == 4) ? zkInstance.TerminalInfoTexts.FirstOrDefault(x => x.InfoTextNr == 4).InfoText : "";

                    zkDto.FunctionKeyText1 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 1) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 1).FunctionKeyText : "0";
                    zkDto.FunctionKeyText2 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 2) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 2).FunctionKeyText : "0";
                    zkDto.FunctionKeyText3 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 3) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 3).FunctionKeyText : "0";
                    zkDto.FunctionKeyText4 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 4) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 4).FunctionKeyText : "0";
                    zkDto.FunctionKeyText5 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 5) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 5).FunctionKeyText : "0";
                    zkDto.FunctionKeyText6 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 6) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 6).FunctionKeyText : "0";
                    zkDto.FunctionKeyText7 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 7) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 7).FunctionKeyText : "0";
                    zkDto.FunctionKeyText8 = zkInstance.TerminalFunctionKeys.Any(x => x.FunctionKeyNr == 8) ? zkInstance.TerminalFunctionKeys.FirstOrDefault(x => x.FunctionKeyNr == 8).FunctionKeyText : "0";
                    GetReadersInstances(zkInstance.ID, zkDto);
                    lstTerminalInstances2.Add(zkDto);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return lstTerminalInstances2;
        }
        #endregion

        protected void ddlCurrentZkTerminalInstance_SelectedIndexChanged(object sender, EventArgs e)
        {
            TerminalRepository repo = new TerminalRepository();
            Terminal terminal = new Terminal();
            try
            {
                var staticTerminalId = ddlCurrentZkTerminalInstance.Value;
                terminal = repo.GetTerminalbyID(Convert.ToInt32(staticTerminalId));
                string curTerminalPage = terminal.TerminalPage;
                string requestedPage = Page.ResolveUrl(curTerminalPage);
                Server.Transfer(requestedPage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        protected void btnSendMasterData_Click(object sender, EventArgs e)
        {
        }

        protected void btnGetBookings_Click(object sender, EventArgs e)
        {

        }

        protected void btnSetSystemTime_Click(object sender, EventArgs e)
        {
        }

        protected void btnTestConnection_Click(object sender, EventArgs e)
        {
        }

        private static ZKTerminal getCurrentTerminal(string Description, string IPAddress)
        {
            ZKTerminal _currentTerminal = new ZKTerminal();

            _currentTerminal.Description = Description;
            _currentTerminal.IPAddress = IPAddress;
            _currentTerminal.PortNumber = ZK_TERMINAL_DEFAULT_PORT;
            _currentTerminal.DataCollectionType = ZKTerminalEnums.DataCollectionType.TimeAttendanceAndAccessControl;
            _currentTerminal.SDKType = ZKTerminalEnums.SDKType.TFTStandAlone;

            return _currentTerminal;

        }

        private void showAlertMessage(string messageString)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showAlertMessage", "alert('" + messageString + "')", true);
        }
        //protected void CreateTerminalInstanceReader(long terminalInstanceId)
        //{
        //    try
        //    {
        //        KruAll.Core.Models.TerminalReader terminalReader = new KruAll.Core.Models.TerminalReader()
        //        {
        //            TermID = terminalInstanceId,
        //            ReaderID = 1,
        //            ReaderInfo = "Intern",
        //            ReaderType = "RFID",
        //        };
        //        new TerminalReaderRepository().NewTerminalReader(terminalReader);

        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //}

        protected void grdTerminalReader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            var id = e.Parameters;
            if (!string.IsNullOrEmpty(id))
            {
                var readers = new TerminalReaderRepository().GetAllReadersById(Convert.ToInt64(id));
                grdTerminalReader.DataSource = readers;
                grdTerminalReader.DataBind();
            }
        }
        [System.Web.Services.WebMethod]
        public static void DeleteReaderInstance(string id)
        {
            try
            {

                if (string.IsNullOrEmpty(id)) return;
                var readerInstance = new TerminalReaderRepository().GetReaderbyId(Convert.ToInt64(id));
                new TerminalReaderRepository().DeleteTerminalReader(readerInstance);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        [System.Web.Services.WebMethod]
        public static string SendMasterData(string Description, string IPAddress)
        {
            string responseMessage = string.Empty;

            ZKTerminal _currentTerminal = getCurrentTerminal(Description, IPAddress);

            standAloneSDK = new StandAloneInterface(_currentTerminal);


            if (standAloneSDK.ConnectToDevice())
            {
                standAloneSDK.SendMasterData();
                standAloneSDK.Disconnect();
                responseMessage = Resources.LocalizedText.DataSentSuccessfully;
            }
            else
            {
                responseMessage = Resources.LocalizedText.ErrorSendingData;
            }

            return responseMessage;
        }


        public void loadgrdReaderStatic()
        {
            TerminalReadersStaticRepository staticrepo = new TerminalReadersStaticRepository();
            List<TerminalReadersStatic> lst = new List<TerminalReadersStatic>();
            List<TerminalReaderStaticDto> lstDto = new List<TerminalReaderStaticDto>();
            TerminalReaderStaticDto dto = null;
            try
            {
                lst = staticrepo.GetAllReaders().Where(x => x.ReaderIdentifier == 1).ToList();

                foreach (TerminalReadersStatic rdrStatic in lst)
                {
                    dto = new TerminalReaderStaticDto();
                    dto.ID = rdrStatic.ID;
                    dto.Description = rdrStatic.Description;
                    dto.Image = rdrStatic.Image;
                    dto.Installation = rdrStatic.Installation;
                    dto.ReaderType = rdrStatic.ReaderType;
                    lstDto.Add(dto);
                }
                grdReaderStatic.DataSource = lstDto;
                grdReaderStatic.DataBind();
                grdReaderStatic.FocusedRowIndex = -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        public void loadgrdgrdREaderInfo()
        {
            TerminalReadersStaticRepository staticrepo = new TerminalReadersStaticRepository();
            List<TerminalReadersStatic> lst2 = new List<TerminalReadersStatic>();
            List<TerminalReaderStaticDto> lstDto = new List<TerminalReaderStaticDto>();
            TerminalReaderStaticDto dto = null;
            try
            {
                lst2 = staticrepo.GetAllReaders().Where(x => x.ReaderIdentifier == 3).ToList();

                foreach (TerminalReadersStatic rdrStatic in lst2)
                {
                    dto = new TerminalReaderStaticDto();
                    dto.ID = rdrStatic.ID;
                    dto.Description = rdrStatic.Description;
                    dto.Image = rdrStatic.Image;
                    dto.Installation = rdrStatic.Installation;
                    dto.ReaderType = rdrStatic.ReaderType;
                    lstDto.Add(dto);
                }

                grdReaderInfo.DataSource = lstDto;
                grdReaderInfo.DataBind();
                grdReaderInfo.FocusedRowIndex = -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetBookings(string Description, string IPAddress)
        {
            try
            {

                string responseMessage = string.Empty;

                ZKTerminal _currentTerminal = getCurrentTerminal(Description, IPAddress);

                standAloneSDK = new StandAloneInterface(_currentTerminal);


                if (standAloneSDK.ConnectToDevice())
                {
                    standAloneSDK.GetBookings();
                    standAloneSDK.Disconnect();
                    responseMessage = "Buchungen zur Datenbank erfolgreich hinzugefügt";
                }
                else
                {
                    responseMessage = "Fehler beim Abrufen der Buchungen von Terminal";
                }

                return responseMessage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return null;
        }

        [System.Web.Services.WebMethod]
        public static string SetSystemTime(string Description, string IPAddress)
        {
            try
            {

                string responseMessage = string.Empty;

                ZKTerminal _currentTerminal = getCurrentTerminal(Description, IPAddress);

                standAloneSDK = new StandAloneInterface(_currentTerminal);


                if (standAloneSDK.ConnectToDevice())
                {
                    standAloneSDK.SetDeviceDateTime();
                    standAloneSDK.Disconnect();
                    responseMessage = "Das Terminal Datum und Zeit erfolgreich aktualisiert";
                }
                else
                {
                    responseMessage = "Fehler beim Setzen Terminal Datum und Uhrzeit";
                }

                return responseMessage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return null;
        }

        [System.Web.Services.WebMethod]
        public static string ConnectionTest(string Description, string IPAddress)
        {
            string responseMessage = string.Empty;

            try
            {

                ZKTerminal _currentTerminal = getCurrentTerminal(Description, IPAddress);

                standAloneSDK = new StandAloneInterface(_currentTerminal);


                if (standAloneSDK.ConnectToDevice())
                {
                    standAloneSDK.Disconnect();
                    responseMessage = Resources.LocalizedText.ConnectionTestSuccessful;
                }
                else
                {
                    responseMessage = Resources.LocalizedText.ConnectionTestFailed;
                }

                return responseMessage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return responseMessage;
        }
        protected static void CreateTerminalInstanceReader(long terminalInstanceId, string name, string internDesc, bool status, int direction, string relayTime)
        {
            try
            {
                KruAll.Core.Models.TerminalReader terminalReader = new KruAll.Core.Models.TerminalReader()
                {
                    TermID = terminalInstanceId,
                    ReaderID = 1,
                    ReaderInfo = "Intern",
                    ReaderType = "RFID",
                    Name = (string.IsNullOrEmpty(internDesc)) ? name : internDesc,
                    Direction = direction,
                    RelayTime = (string.IsNullOrEmpty(relayTime)) ? 5 : Convert.ToInt32(relayTime),
                    Status = status == true ? 1 : 0
                };
                new TerminalReaderRepository().NewTerminalReader(terminalReader);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }
        protected static void UpdateTerminalInstanceReader(long terminalInstanceId, bool status, string name, string internDesc, int direction, string relayTime, string readerId)
        {
            try
            {
                if (string.IsNullOrEmpty(readerId)) return;
                var reader = new TerminalReaderRepository().GetReaderInstanceById(terminalInstanceId, Convert.ToInt32(readerId));
                KruAll.Core.Models.TerminalReader terminalReader = new KruAll.Core.Models.TerminalReader()
                {
                    ID = reader.ID,
                    TermID = terminalInstanceId,
                    ReaderID = 1,
                    ReaderInfo = "Intern",
                    ReaderType = "RFID",
                    Status = status == true ? 1 : 0,
                    Name = (string.IsNullOrEmpty(internDesc)) ? name : internDesc,
                    Direction = direction,
                    RelayTime = (!string.IsNullOrEmpty(relayTime)) ? Convert.ToInt32(relayTime) : (int?)null
                };
                new TerminalReaderRepository().EditReaderInfo(terminalReader);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }

        }
        protected void BindTerminalInstanceValues(int readerId, string readerInfo, string readerType, string name, int? direction, int? status, int? relayTime)
        {
            lblTIId.Text = readerId.ToString();
            lblTIInfo.Text = readerInfo;
            lblTILaserType.Text = readerType;
            txtTIDescription.Text = name;
            ddlTIDirection.Value = direction.ToString();
            lblTIStatus.Text = status == 1 ? "AKtiv" : "Inacktiv";
            txtTIRelayTime.Text = relayTime != null ? relayTime.ToString() : string.Empty;
            if (status == 1)
            {
                lblTIStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblTIStatus.ForeColor = System.Drawing.Color.Red;
            }

        }
        protected static void CreateExternalTerminalInstanceReader(long terminalInstanceId, bool status, string name, int direction, string relayTime, string readerType, int readerStaticId)
        {
            try
            {
                KruAll.Core.Models.TerminalReader terminalReader = new KruAll.Core.Models.TerminalReader()
                {
                    TermID = terminalInstanceId,
                    ReaderID = 2,
                    ReaderNr = readerStaticId,
                    ReaderInfo = "Extern",
                    ReaderType = readerType,
                    Status = status == true ? 1 : 0,
                    Name = name,
                    Direction = direction,
                    RelayTime = (!string.IsNullOrEmpty(relayTime)) ? Convert.ToInt32(relayTime) : (int?)null
                };
                new TerminalReaderRepository().NewTerminalReader(terminalReader);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }
        protected static void UpdateExternalTerminalInstanceReader(long terminalInstanceId, bool status, string name, int direction, string relayTime, long readerId, string readerType, int readerStaticId)
        {
            try
            {
                KruAll.Core.Models.TerminalReader terminalReader = new KruAll.Core.Models.TerminalReader()
                {
                    ID = readerId,
                    TermID = terminalInstanceId,
                    ReaderID = 2,
                    ReaderNr = readerStaticId,
                    ReaderInfo = "Extern",
                    ReaderType = readerType,
                    Status = status == true ? 1 : 0,
                    Name = name,
                    Direction = direction,
                    RelayTime = (!string.IsNullOrEmpty(relayTime)) ? Convert.ToInt32(relayTime) : (int?)null
                };
                new TerminalReaderRepository().EditReaderInfo(terminalReader);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }

        }
        protected static void SaveExternalReader(long terminalId, bool isActive, string name, int direction, string relayTime, string externalReaderId, string readerType, int readerStaticId)
        {
            if (!string.IsNullOrEmpty(externalReaderId))
            {
                var reader = new TerminalReaderRepository().GetReaderInstanceById(terminalId, Convert.ToInt32(externalReaderId));
                if (reader != null)
                {
                    UpdateExternalTerminalInstanceReader(terminalId, isActive, name, direction, relayTime, reader.ID, readerType, readerStaticId);
                }
                else
                {
                    CreateExternalTerminalInstanceReader(terminalId, isActive, name, direction, relayTime, readerType, readerStaticId);
                }
            }
        }
        protected void BindExternalTerminalInstanceValues(int readerId, string readerInfo, string readerType, string name, int? direction, int? status, int? relayTime)
        {
            lblExId.Text = readerId.ToString();
            lblExInfo.Text = readerInfo;
            lblExLaserType.Text = readerType;
            txtExDescription.Text = name;
            ddlExDirection.Value = direction.ToString();
            lblTIStatus.Text = status == 1 ? "AKtiv" : "Inacktiv";
            txtTIRelayTime.Text = relayTime != null ? relayTime.ToString() : string.Empty;
            if (status == 1)
            {
                lblTIStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblTIStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void BindReaderInstances(long termId)
        {
            var Readers = new TerminalReaderRepository().GetReadersByTermId(termId);
            foreach (var reader in Readers)
            {
                btnDeleteReader.Enabled = false;
                txtExDescription.ReadOnly = true;
                int reader_Id = reader.ReaderID;
                switch (reader_Id)
                {
                    case 1:
                        BindTerminalInstanceValues(reader.ReaderID, reader.ReaderInfo, reader.ReaderType, reader.Name, reader.Direction, reader.Status, reader.RelayTime);
                        break;
                    case 2:
                        BindExternalTerminalInstanceValues(reader.ReaderID, reader.ReaderInfo, reader.ReaderType, reader.Name, reader.Direction, reader.Status, reader.RelayTime);
                        btnDeleteReader.Enabled = true;
                        txtExDescription.ReadOnly = false;
                        btnAddReader.Enabled = false;
                        break;
                    default:
                        break;
                }
            }
        }
        public static void GetReadersInstances(long termId, TerminalZkDto zkDto)
        {
            var Readers = new TerminalReaderRepository().GetReadersByTermId(termId);
            foreach (var reader in Readers)
            {
                int reader_Id = reader.ReaderID;
                switch (reader_Id)
                {
                    case 1:
                        zkDto.readerInternId = reader.ReaderID;
                        zkDto.readerInfoIntern = reader.ReaderInfo;
                        zkDto.readerTypeIntern = reader.ReaderType;
                        zkDto.internDirec = reader.Direction;
                        zkDto.internReaderDesc = reader.Name;
                        zkDto.relayTime = reader.RelayTime.ToString();
                        zkDto.readerActive = zkDto.IsActive == true ? "Aktiv" : "Inaktiv";
                        break;
                    case 2:
                        zkDto.readerExternId = reader.ReaderID;
                        zkDto.readerInfoExtern = reader.ReaderInfo;
                        zkDto.readerTypeExtern = reader.ReaderType;
                        zkDto.externDirec = reader.Direction;
                        zkDto.externReaderDesc = reader.Name;
                        break;
                    default:
                        break;
                }

            }
        }
        [WebMethod]
        public static void DeleteTerminalReaderInstance(long termId)
        {
            try
            {

                var Readers = new TerminalReaderRepository().GetReadersByTermId(termId);
                if (Readers.Count > 1)
                {
                    var reader = Readers.Find(x => x.ReaderID == 2);
                    new TerminalReaderRepository().DeleteTerminalReader(reader);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }
        [System.Web.Services.WebMethod]
        public static TerminalAccessProfilesDto GetAccessprofileInformation()
        {
            try
            {

                return new AccessProfileCtl().GetCurrentSelectedTimeAccessProfile();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return null;
        }

        protected void grdZuttritProfileTimeFrames_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            try
            {
                new AccessProfileCtl().BindProfilesToGrid(grdZuttritProfileTimeFrames, e);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        protected void grdReaderStatic_OnCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            TerminalReadersStaticRepository staticrepo = new TerminalReadersStaticRepository();
            List<TerminalReadersStatic> lst = new List<TerminalReadersStatic>();
            List<TerminalReaderStaticDto> lstDto = new List<TerminalReaderStaticDto>();
            TerminalReaderStaticDto dto = null;
            try
            {
                lst = staticrepo.GetAllReaders().Where(x => x.ReaderIdentifier == 1).ToList();

                foreach (TerminalReadersStatic rdrStatic in lst)
                {
                    dto = new TerminalReaderStaticDto();
                    dto.ID = rdrStatic.ID;
                    dto.Description = rdrStatic.Description;
                    dto.Image = rdrStatic.Image;
                    dto.Installation = rdrStatic.Installation;
                    dto.ReaderType = rdrStatic.ReaderType;
                    lstDto.Add(dto);
                }

                grdReaderStatic.DataSource = lstDto;
                grdReaderStatic.DataBind();
                grdReaderStatic.FocusedRowIndex = -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        protected void grdReaderInfo_OnCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            TerminalReadersStaticRepository staticrepo = new TerminalReadersStaticRepository();
            List<TerminalReadersStatic> lst2 = new List<TerminalReadersStatic>();
            List<TerminalReaderStaticDto> lstDto = new List<TerminalReaderStaticDto>();
            TerminalReaderStaticDto dto = null;
            try
            {
                lst2 = staticrepo.GetAllReaders().Where(x => x.ReaderIdentifier == 3).ToList();

                foreach (TerminalReadersStatic rdrStatic in lst2)
                {
                    dto = new TerminalReaderStaticDto();
                    dto.ID = rdrStatic.ID;
                    dto.Description = rdrStatic.Description;
                    dto.Image = rdrStatic.Image;
                    dto.Installation = rdrStatic.Installation;
                    dto.ReaderType = rdrStatic.ReaderType;
                    lstDto.Add(dto);
                }

                grdReaderInfo.DataSource = lstDto;
                grdReaderInfo.DataBind();
                grdReaderInfo.FocusedRowIndex = -1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }


        public TerminalZkDto ConvertTerminalToDto(TerminalConfig terminalConfig)
        {
            TerminalZkDto dto = new TerminalZkDto();
            try
            {
                dto.ID = terminalConfig.ID;
                dto.TermID = terminalConfig.TermID;
                dto.TermType = terminalConfig.TermType;
                dto.Description = terminalConfig.Description;
                dto.IpAddress = terminalConfig.IpAddress;
                dto.ConnectionType = terminalConfig.ConnectionType;
                dto.IsActive = terminalConfig.IsActive;
                dto.SerialNumber = terminalConfig.SerialNumber;
                dto.Port = terminalConfig.Port;
                dto.TerminalOEMId = terminalConfig.TerminalOEMId;
                dto.TerminalId = terminalConfig.TerminalId;

                return dto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        public List<TerminalZkDto> ConvertTerminalsToDtos(List<KruAll.Core.Models.TerminalConfig> _TerminalConfigslist)
        {
            List<TerminalZkDto> _terminalConfigs = new List<TerminalZkDto>();
            try
            {
                foreach (KruAll.Core.Models.TerminalConfig _terminalConfig in _TerminalConfigslist)
                {
                    TerminalZkDto _dto = ConvertTerminalToDto(_terminalConfig);
                    _terminalConfigs.Add(_dto);
                }
                return _terminalConfigs;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return _terminalConfigs;
        }

        [System.Web.Services.WebMethod(MessageName = "GetTerminalConfigbyTermID")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto GetTerminalConfigbyTermID(int TermID)
        {
            try
            {

                TerminalRepository _TerminalRepository = new TerminalRepository();
                TerminalConfigRepository _TerminalConfigRepository = new TerminalConfigRepository();
                TerminalZkDto _TerminalDto = new TerminalZkDto();
                TM680tc _TM680tcfrm = new TM680tc();
                var TerminalConfig = _TerminalConfigRepository.GetTerminalConfigbyTermID(TermID);

                if (TerminalConfig == null)
                {
                    return _TerminalDto;
                }
                if (TerminalConfig != null)
                {
                    _TerminalDto = _TM680tcfrm.ConvertTerminalToDto(TerminalConfig);
                }
                return _TerminalDto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return null;
        }

        [System.Web.Services.WebMethod(MessageName = "InsertTerminalConfig")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto InsertTerminalConfig(TerminalZkDto dto)
        {
            TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
            TerminalZkDto zkDto = new TerminalZkDto();
            try
            {
                TerminalConfig zkInstance = new TerminalConfig()
                {
                    TermType = dto.TermType,
                    TerminalId = Convert.ToInt32(dto.TerminalId),
                    TermID = Convert.ToInt32(dto.TermID),
                    Description = dto.Description,
                    SerialNumber = dto.SerialNumber,
                    ConnectionType = dto.ConnectionType,
                    IpAddress = dto.IpAddress,
                    Port = dto.Port,
                    IsActive = dto.IsActive,
                    ZkRelayTime = dto.ZkRelayTime,
                    TerminalOEMId = 1
                };

                terminalConfigRepository.NewTerminalConfig(zkInstance);

                dto.ID = zkInstance.ID;

                InsertTerminalConfigInfoText(dto);
                InsertTerminalConfigFunctionKeys(dto);
                InsertTerminalConfigUtilities(dto);
                InsertInternalTerminalReader(dto);
                InsertExternalTerminalReader(dto);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return zkDto;
        }

        [System.Web.Services.WebMethod(MessageName = "InsertTerminalConfigFunctionKeys")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto InsertTerminalConfigFunctionKeys(TerminalZkDto dto)
        {
            TerminalFunctionKeyRespository terminalFunctionKeyRespository = new TerminalFunctionKeyRespository();
            List<TerminalFunctionKey> lstFunctionKeys = new List<TerminalFunctionKey>();
            try
            {
                if (dto.FunctionKeyText1 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 1,
                        FunctionKeyText = dto.FunctionKeyText1,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText2 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 2,
                        FunctionKeyText = dto.FunctionKeyText2,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText3 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 3,
                        FunctionKeyText = dto.FunctionKeyText3,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText4 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 4,
                        FunctionKeyText = dto.FunctionKeyText4,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText5 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 5,
                        FunctionKeyText = dto.FunctionKeyText5,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText6 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 6,
                        FunctionKeyText = dto.FunctionKeyText6,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText7 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 7,
                        FunctionKeyText = dto.FunctionKeyText7,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText8 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 8,
                        FunctionKeyText = dto.FunctionKeyText8,
                        TerminalConfigID = dto.ID
                    });
                }

                foreach (TerminalFunctionKey FunctionKey in lstFunctionKeys)
                {
                    terminalFunctionKeyRespository.NewTerminalFunctionKeys(FunctionKey);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        [System.Web.Services.WebMethod(MessageName = "InsertTerminalConfigUtilities")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto InsertTerminalConfigUtilities(TerminalZkDto dto)
        {
            TerminalUtilityRepository terminalUtilityRepository = new TerminalUtilityRepository();
            List<TerminalFunctionKey> lstFunctionKeys = new List<TerminalFunctionKey>();
            try
            {
                TerminalUtility terminalUtility = new TerminalUtility()
                {
                    AllowTransponder = false,
                    AllowTransponderAndPin = false,
                    HasAPPosting = dto.HasAPPosting,
                    HasFPRead = false,
                    HasPersNoPin = dto.HasPersNoPin,
                    HasProfFirmware = dto.HasProfFirmware,
                    HasTAPPosting = false,
                    RFIDActive = false,
                    RFIDCardPin = false,
                    TerminalConfigID = dto.ID
                };

                terminalUtilityRepository.NewTerminalUtility(terminalUtility);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        [System.Web.Services.WebMethod(MessageName = "InsertTerminalConfigInfoText")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto InsertTerminalConfigInfoText(TerminalZkDto dto)
        {
            TerminalInfoTextRepository terminalInfoTextRepository = new TerminalInfoTextRepository();
            TerminalConfigInfoTextDto zkDto = new TerminalConfigInfoTextDto();
            List<TerminalInfoText> lstInfoText = new List<TerminalInfoText>();
            try
            {
                if (!string.IsNullOrEmpty(dto.InfoText1))
                {
                    lstInfoText.Add(new TerminalInfoText()
                    {
                        InfoText = dto.InfoText1,
                        InfoTextNr = 1,
                        TerminalConfigID = dto.ID
                    });
                }
                if (!string.IsNullOrEmpty(dto.InfoText2))
                {
                    lstInfoText.Add(new TerminalInfoText()
                    {
                        InfoText = dto.InfoText2,
                        InfoTextNr = 2,
                        TerminalConfigID = dto.ID
                    });
                }
                if (!string.IsNullOrEmpty(dto.InfoText3))
                {
                    lstInfoText.Add(new TerminalInfoText()
                    {
                        InfoText = dto.InfoText3,
                        InfoTextNr = 3,
                        TerminalConfigID = dto.ID
                    });
                }
                if (!string.IsNullOrEmpty(dto.InfoText4))
                {
                    lstInfoText.Add(new TerminalInfoText()
                    {
                        InfoText = dto.InfoText4,
                        InfoTextNr = 4,
                        TerminalConfigID = dto.ID
                    });
                }

                foreach (TerminalInfoText infotext in lstInfoText)
                {
                    terminalInfoTextRepository.NewTerminalInfoTexts(infotext);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        [System.Web.Services.WebMethod(MessageName = "UpdateTerminalConfig")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto UpdateTerminalConfig(TerminalZkDto dto)
        {
            TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
            TerminalConfig zkInstance = null;
            TerminalZkDto zkDto = new TerminalZkDto();
            try
            {
                zkInstance = terminalConfigRepository.GetTerminalConfigbyTermID(dto.TermID);
                if (zkInstance != null)
                {
                    zkInstance.TermType = dto.TermType;
                    zkInstance.TerminalId = Convert.ToInt32(dto.TerminalId);
                    zkInstance.TermID = Convert.ToInt32(dto.TermID);
                    zkInstance.Description = dto.Description;
                    zkInstance.SerialNumber = dto.SerialNumber;
                    zkInstance.ConnectionType = dto.ConnectionType;
                    zkInstance.IpAddress = dto.IpAddress;
                    zkInstance.Port = dto.Port;
                    zkInstance.IsActive = dto.IsActive;
                    zkInstance.ZkRelayTime = dto.ZkRelayTime;
                    zkInstance.TerminalOEMId = dto.TerminalOEMId;

                    terminalConfigRepository.EditTerminalConfig(zkInstance);

                    dto.ID = zkInstance.ID;

                    UpdateTerminalConfigInfoText(dto);
                    UpdateTerminalConfigFunctionKeys(dto);
                    UpdateTerminalConfigUtilities(dto);
                    UpdateInternalTerminalReader(dto);
                    UpdateExternalTerminalReader(dto);

                    zkDto.ID = zkInstance.ID;
                    zkDto.TermID = zkInstance.TermID;
                    zkDto.TermType = zkInstance.TermType;
                    zkDto.TerminalId = zkInstance.TerminalId;
                    zkDto.Description = zkInstance.Description;
                    zkDto.SerialNumber = zkInstance.SerialNumber;
                    zkDto.ConnectionType = zkInstance.ConnectionType;
                    zkDto.IpAddress = zkInstance.IpAddress;
                    zkDto.Port = zkInstance.Port;
                    zkDto.IsActive = zkInstance.IsActive;
                    zkDto.ZkRelayTime = zkInstance.ZkRelayTime;
                    zkDto.HasAPPosting = zkInstance.TerminalUtilities.FirstOrDefault().HasAPPosting;
                    zkDto.HasProfFirmware = zkInstance.TerminalUtilities.FirstOrDefault().HasProfFirmware;
                    zkDto.HasPersNoPin = zkInstance.TerminalUtilities.FirstOrDefault().HasPersNoPin;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return zkDto;
        }

        [System.Web.Services.WebMethod(MessageName = "UpdateTerminalConfigFunctionKeys")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto UpdateTerminalConfigFunctionKeys(TerminalZkDto dto)
        {
            TerminalFunctionKeyRespository terminalFunctionKeyRespository = new TerminalFunctionKeyRespository();
            List<TerminalFunctionKey> lstFunctionKeys = new List<TerminalFunctionKey>();
            try
            {
                if (dto.FunctionKeyText1 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 1,
                        FunctionKeyText = dto.FunctionKeyText1,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText2 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 2,
                        FunctionKeyText = dto.FunctionKeyText2,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText3 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 3,
                        FunctionKeyText = dto.FunctionKeyText3,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText4 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 4,
                        FunctionKeyText = dto.FunctionKeyText4,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText5 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 5,
                        FunctionKeyText = dto.FunctionKeyText5,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText6 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 6,
                        FunctionKeyText = dto.FunctionKeyText6,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText7 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 7,
                        FunctionKeyText = dto.FunctionKeyText7,
                        TerminalConfigID = dto.ID
                    });
                }
                if (dto.FunctionKeyText8 != "0")
                {
                    lstFunctionKeys.Add(new TerminalFunctionKey
                    {
                        FunctionKeyNr = 8,
                        FunctionKeyText = dto.FunctionKeyText8,
                        TerminalConfigID = dto.ID
                    });
                }

                foreach (TerminalFunctionKey FunctionKey in lstFunctionKeys)
                {
                    TerminalFunctionKey TerminalFunctionKey = terminalFunctionKeyRespository.GetTerminalFunctionKeysByTermConfIdAndNr(FunctionKey.TerminalConfigID, FunctionKey.FunctionKeyNr ?? 0);
                    if (TerminalFunctionKey != null)
                    {
                        TerminalFunctionKey.FunctionKeyText = FunctionKey.FunctionKeyText;
                        terminalFunctionKeyRespository.EditTerminalFunctionKeys(TerminalFunctionKey);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        [System.Web.Services.WebMethod(MessageName = "UpdateTerminalConfigUtilities")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto UpdateTerminalConfigUtilities(TerminalZkDto dto)
        {
            TerminalUtilityRepository terminalUtilityRepository = new TerminalUtilityRepository();
            try
            {
                TerminalUtility TerminalUtility = terminalUtilityRepository.GetTerminalUtilityByTermConfId(dto.ID);
                if (TerminalUtility != null)
                {
                    TerminalUtility.AllowTransponder = false;
                    TerminalUtility.AllowTransponderAndPin = false;
                    TerminalUtility.HasAPPosting = dto.HasAPPosting;
                    TerminalUtility.HasFPRead = false;
                    TerminalUtility.HasPersNoPin = dto.HasPersNoPin;
                    TerminalUtility.HasProfFirmware = dto.HasProfFirmware;
                    TerminalUtility.HasTAPPosting = false;
                    TerminalUtility.RFIDActive = false;
                    TerminalUtility.RFIDCardPin = false;
                    TerminalUtility.TerminalConfigID = dto.ID;

                    terminalUtilityRepository.EditTerminalUtility(TerminalUtility);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        [System.Web.Services.WebMethod(MessageName = "UpdateTerminalConfigInfoText")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto UpdateTerminalConfigInfoText(TerminalZkDto dto)
        {
            TerminalInfoTextRepository terminalInfoTextRepository = new TerminalInfoTextRepository();
            List<TerminalInfoText> lstInfoText = new List<TerminalInfoText>();
            try
            {
                if (!string.IsNullOrEmpty(dto.InfoText1))
                {
                    lstInfoText.Add(new TerminalInfoText()
                    {
                        InfoText = dto.InfoText1,
                        InfoTextNr = 1,
                        TerminalConfigID = dto.ID
                    });
                }
                if (!string.IsNullOrEmpty(dto.InfoText2))
                {
                    lstInfoText.Add(new TerminalInfoText()
                    {
                        InfoText = dto.InfoText2,
                        InfoTextNr = 2,
                        TerminalConfigID = dto.ID
                    });
                }
                if (!string.IsNullOrEmpty(dto.InfoText3))
                {
                    lstInfoText.Add(new TerminalInfoText()
                    {
                        InfoText = dto.InfoText3,
                        InfoTextNr = 3,
                        TerminalConfigID = dto.ID
                    });
                }
                if (!string.IsNullOrEmpty(dto.InfoText4))
                {
                    lstInfoText.Add(new TerminalInfoText()
                    {
                        InfoText = dto.InfoText4,
                        InfoTextNr = 4,
                        TerminalConfigID = dto.ID
                    });
                }

                foreach (TerminalInfoText infotext in lstInfoText)
                {
                    TerminalInfoText TerminalInfoText = terminalInfoTextRepository.GetTerminalInfoTextByTermIdAndNr(dto.ID, infotext.InfoTextNr ?? 0);
                    if (TerminalInfoText != null)
                    {
                        TerminalInfoText.InfoText = infotext.InfoText;
                        terminalInfoTextRepository.EditTerminalInfoTexts(TerminalInfoText);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        [System.Web.Services.WebMethod(MessageName = "UpdateInternalTerminalReader")]
        [System.Web.Script.Services.ScriptMethod()]
        protected static TerminalZkDto UpdateInternalTerminalReader(TerminalZkDto dto)
        {
            TerminalReaderRepository terminalReaderRepository = new TerminalReaderRepository();
            try
            {
                KruAll.Core.Models.TerminalReader TerminalReader = terminalReaderRepository.GetReadersByTermIdAndNr(dto.ID, dto.readerInternId);
                if (TerminalReader != null)
                {
                    TerminalReader.Name = (string.IsNullOrEmpty(dto.internReaderDesc)) ? dto.internReaderDesc : dto.internReaderDesc;
                    TerminalReader.Direction = dto.internDirec;
                    TerminalReader.RelayTime = (string.IsNullOrEmpty(dto.relayTime)) ? 5 : Convert.ToInt32(dto.relayTime);
                    TerminalReader.Status = dto.IsActive == true ? 1 : 0;

                    terminalReaderRepository.EditReaderInfo(TerminalReader);
                }
                return dto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        [System.Web.Services.WebMethod(MessageName = "UpdateExternalTerminalReader")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto UpdateExternalTerminalReader(TerminalZkDto dto)
        {
            TerminalReaderRepository terminalReaderRepository = new TerminalReaderRepository();
            try
            {
                KruAll.Core.Models.TerminalReader TerminalReader = terminalReaderRepository.GetReadersByTermIdAndNr(dto.ID, dto.readerInternId);
                if (TerminalReader != null)
                {
                    TerminalReader.Name = (string.IsNullOrEmpty(dto.externReaderDesc)) ? dto.externReaderDesc : dto.externReaderDesc;
                    TerminalReader.Direction = dto.externDirec;
                    TerminalReader.RelayTime = (string.IsNullOrEmpty(dto.relayTime)) ? 5 : Convert.ToInt32(dto.relayTime);
                    TerminalReader.Status = dto.IsActive == true ? 1 : 0;

                    terminalReaderRepository.EditReaderInfo(TerminalReader);
                }
                return dto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        protected void grdZkTerminalCon_OnCustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Parameters))
            {
                _bindTerminalConfigsToDataGridFocus(e.Parameters.ToString());
            }
            else
            {
                _bindAreasToDataGrid();
            }
        }

        private void _bindTerminalConfigsToDataGridFocus(string ID)
        {
            List<TerminalZkDto> lstDto = RetrieveTerminalConfigsDtos();
            grdZkTerminalCon.DataSource = lstDto;
            grdZkTerminalCon.DataBind();
            hiddenFieldShiftCount.Value = lstDto.Count.ToString();
            var _termconfig = lstDto.Find(x => x.ID == Convert.ToInt64(ID));
            if (_termconfig != null)
            {
                var index = grdZkTerminalCon.FindVisibleIndexByKeyValue(_termconfig.ID);
                if (index >= 0)
                {
                    //grdZkTerminalCon.FocusedRowIndex = index;
                }
            }

            if (lstDto.Count() > 29)
            {
                grdZkTerminalCon.SettingsPager.PageSize = lstDto.Count();
            }
        }

        protected void _bindAreasToDataGrid()
        {
            List<TerminalZkDto> lstDto = RetrieveTerminalConfigsDtos();
            grdZkTerminalCon.DataSource = lstDto;
            grdZkTerminalCon.DataBind();
            hiddenFieldShiftCount.Value = lstDto.Count.ToString();

            if (lstDto.Count() > 29)
            {
                grdZkTerminalCon.SettingsPager.PageSize = lstDto.Count();
            }
        }

        public List<TerminalZkDto> RetrieveTerminalConfigsDtos()
        {
            try
            {
                TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
                List<TerminalConfig> lst = terminalConfigRepository.GetAllTerminalConfig().OrderBy(x => x.TermID).ToList();
                List<TerminalZkDto> lstDto = new List<TerminalZkDto>();

                foreach (TerminalConfig terminalConfig in lst)
                {
                    TerminalZkDto zkDto = new TerminalZkDto();
                    zkDto.ID = terminalConfig.ID;
                    zkDto.TermID = terminalConfig.TermID;
                    zkDto.TermType = terminalConfig.TermType;
                    zkDto.Description = terminalConfig.Description;
                    zkDto.IpAddress = terminalConfig.IpAddress;
                    zkDto.ConnectionType = terminalConfig.ConnectionType;
                    zkDto.IsActive = terminalConfig.IsActive;
                    zkDto.SerialNumber = terminalConfig.SerialNumber;
                    zkDto.Port = terminalConfig.Port;
                    zkDto.TerminalOEMId = terminalConfig.TerminalOEMId;
                    zkDto.TerminalId = terminalConfig.TerminalId;

                    lstDto.Add(zkDto);
                }

                return lstDto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        public static string GetNextTerminalConfigNr()
        {
            try
            {

                int termNr = 0; TerminalConfigRepository terminalConfigRepository = new TerminalConfigRepository();
                var termDetails = new TerminalConfigRepository().GetAllTerminalConfig();
                if (termDetails.Count() > 0)
                {
                    termNr = Convert.ToInt32(termDetails.Max(i => i.TermID));
                }
                else
                {
                    termNr = 0;
                }
                int nextNr = termNr + 1;
                return nextNr.ToString();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return null;
        }

        [System.Web.Services.WebMethod]
        public static void DeleteSelectedTerminalConfig(int cid)
        {
            try
            {
                var TerminalConfigID = new TerminalConfigRepository().GetTerminalConfigbyTermID(cid);

                if (TerminalConfigID != null)
                {
                    new TerminalConfigRepository().DeleteTerminalConfig(TerminalConfigID);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        [System.Web.Services.WebMethod(MessageName = "GetTerminalbyType")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto GetTerminalbyType()
        {
            TerminalRepository repo = new TerminalRepository();
            Terminal terminal = new Terminal();
            TerminalZkDto dto = new TerminalZkDto();
            try
            {
                terminal = repo.GetTerminalbyType("TM 680tc");

                if (terminal == null)
                {
                    return null;
                }
                dto = new TerminalZkDto()
                {
                    TerminalTypeId = terminal.ID.ToString(),
                    TerminalTypeDescription = terminal.TermType,
                    ID = terminal.ID
                };
                return dto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        [System.Web.Services.WebMethod(MessageName = "InsertInternalTerminalReader")]
        [System.Web.Script.Services.ScriptMethod()]
        protected static void InsertInternalTerminalReader(TerminalZkDto dto)
        {
            try
            {
                KruAll.Core.Models.TerminalReader terminalReader = new KruAll.Core.Models.TerminalReader()
                {
                    TermID = dto.ID,
                    ReaderID = 1,
                    ReaderInfo = "Intern",
                    ReaderType = "RFID",
                    Name = (string.IsNullOrEmpty(dto.internReaderDesc)) ? dto.internReaderDesc : dto.internReaderDesc,
                    Direction = dto.internDirec,
                    RelayTime = (string.IsNullOrEmpty(dto.relayTime)) ? 5 : Convert.ToInt32(dto.relayTime),
                    Status = dto.IsActive == true ? 1 : 0
                };
                new TerminalReaderRepository().NewTerminalReader(terminalReader);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }

        [System.Web.Services.WebMethod(MessageName = "InsertExternalTerminalReader")]
        [System.Web.Script.Services.ScriptMethod()]
        public static TerminalZkDto InsertExternalTerminalReader(TerminalZkDto dto)
        {
            try
            {
                var reader = new TerminalReaderRepository().GetReaderInstanceById(dto.TermID, Convert.ToInt32(dto.readerExternId));
                if (reader != null)
                {
                    UpdateExternalTerminalInstanceReader(dto);
                }
                else
                {
                    CreateExternalTerminalInstanceReader(dto);
                }
                return dto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }
        public static TerminalZkDto CreateExternalTerminalInstanceReader(TerminalZkDto dto)
        {
            try
            {
                KruAll.Core.Models.TerminalReader terminalReader = new KruAll.Core.Models.TerminalReader()
                {
                    TermID = dto.ID,
                    ReaderID = 2,
                    ReaderNr = dto.readerStaticId,
                    ReaderInfo = "Extern",
                    ReaderType = dto.readerTypeExtern,
                    Status = dto.IsActive == true ? 1 : 0,
                    Name = dto.externReaderDesc,
                    Direction = dto.externDirec,
                    RelayTime = (!string.IsNullOrEmpty(dto.relayTime)) ? Convert.ToInt32(dto.relayTime) : (int?)null
                };
                new TerminalReaderRepository().NewTerminalReader(terminalReader);
                return dto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }
        public static TerminalZkDto UpdateExternalTerminalInstanceReader(TerminalZkDto dto)
        {
            TerminalReaderRepository rep = new TerminalReaderRepository();

            try
            {
                if (dto.readerExternId != 0)
                {
                    var reader = rep.GetReaderInstanceById(long.Parse(dto.TermID.ToString()), Convert.ToInt32(dto.readerExternId));
                    if (reader != null)
                    {
                        reader.TermID = dto.ID;
                        reader.ReaderID = 2;
                        reader.ReaderNr = dto.readerStaticId;
                        reader.ReaderInfo = "Extern";
                        reader.ReaderType = dto.readerTypeExtern;
                        reader.Status = dto.IsActive == true ? 1 : 0;
                        reader.Name = dto.externReaderDesc;
                        reader.Direction = dto.externDirec;
                        reader.RelayTime = (!string.IsNullOrEmpty(dto.relayTime)) ? Convert.ToInt32(dto.relayTime) : (int?)null;

                        rep.EditReaderInfo(reader);
                    }
                }
                return dto;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
            return dto;
        }

        [System.Web.Services.WebMethod(MessageName = "DeleteSelectedTerminalReaderInstance")]
        [System.Web.Script.Services.ScriptMethod()]
        public static void DeleteSelectedTerminalReaderInstance(long termId)
        {
            try
            {

                var Readers = new TerminalReaderRepository().GetReadersByTermId(termId);
                if (Readers.Count > 1)
                {
                    var reader = Readers.Find(x => x.ReaderID == 2);
                    new TerminalReaderRepository().DeleteTerminalReader(reader);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Utils.WriteToErrorLogFile(ex);
            }
        }





    }
}