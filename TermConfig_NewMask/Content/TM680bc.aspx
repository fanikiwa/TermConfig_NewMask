<%@ Page Title="TM 680bc" Language="C#" MasterPageFile="~/MasterPages/ZkTerminals.Master" AutoEventWireup="true" CodeBehind="TM680bc.aspx.cs" Inherits="TermConfig_NewMask.Content.TM680bc" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Styles/jquery.notifyBar.css"
        type="text/css" media="all" />
    <link rel="stylesheet" href="Styles/ImportantInfoDialog.css"
        type="text/css" media="all" />
    <link rel="stylesheet" href="Styles/TM680bc.css"
        type="text/css" media="all" />
    <script lang="javascript" type="text/javascript" src="Scripts/underscore.js"></script>
    <script lang="javascript" type="text/javascript" src="Scripts/jquery.notifyBar.js"></script>
    <script lang="javascript" type="text/javascript" src="Scripts/Utils.js"></script>
    <script lang="javascript" type="text/javascript" src="Scripts/TM680bc.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ModuleNavBar" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="server">

    <div id="confirmDelete" class="dialogBox"></div>
    <div id="importantInfoDialog" class="dialogBox"></div>

    <asp:HiddenField ID="hiddenFieldConfirmDialog" runat="server" />
    <asp:HiddenField ID="hiddenFieldIsInsert" runat="server" />
    <asp:HiddenField ID="hiddenFieldType" runat="server" />
    <asp:HiddenField ID="hiddenselectedcost" runat="server" />
    <asp:HiddenField ID="hiddenselectedgrid" runat="server" />
    <asp:HiddenField ID="Showinggrid" runat="server" />
    <asp:HiddenField ID="Hiddenonnew" runat="server" />
    <asp:HiddenField ID="hiddenFieldprogramSelectionType" runat="server" />
    <asp:HiddenField ID="ShiftTARHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hiddenFieldShiftCount" runat="server" />
    <asp:HiddenField ID="hiddenFieldPlantafelShift" runat="server" />
    <asp:HiddenField ID="hfdtxtTerminalTypeId" runat="server" />

    <section class="divcontentBottom">
        <section class="divcontentBottomLeft">
            <asp:Label ID="Label30" runat="server" Text="Terminal Bearbeitung und Information" CssClass="lblbttmsecbttm" Style="margin-left: 10px;"></asp:Label>
            <section class="bttmsec1">
                <section class="bttmsec1left">
                    <section class="bttmsec1lbs">
                        <asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, terminalType %>" CssClass="lblbttmsec"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="<%$ Resources:LocalizedText, terminalID %>" CssClass="lblbttmsec"></asp:Label>
                        <asp:Label ID="Label3" runat="server" Text="<%$ Resources:LocalizedText, description %>" CssClass="lblbttmsec"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text="<%$ Resources:LocalizedText, serialNumber %>" CssClass="lblbttmsec"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text="<%$ Resources:LocalizedText, linkType %>" CssClass="lblbttmsec"></asp:Label>
                        <asp:Label ID="Label6" runat="server" Text="<%$ Resources:LocalizedText, ipAddress %>" CssClass="lblbttmsec"></asp:Label>
                        <asp:Label ID="Label7" runat="server" Text="<%$ Resources:LocalizedText, ipPort %>" CssClass="lblbttmsec"></asp:Label>
                    </section>

                    <section class="bttmsec1txts">
                        <dx:ASPxComboBox ID="ddlCurrentZkTerminalInstance" runat="server" ValueType="System.String" CssClass="txtbttmsec2 IDHolder" Theme="Office2003Blue"></dx:ASPxComboBox>
                        <dx:ASPxComboBox ID="ddlTerminalType" runat="server" ValueType="System.String" CssClass="dllTermType" Theme="Office2003Blue" ClientInstanceName="ddlTerminalType" ClientIDMode="Static" ValueField="ID" TextField="TermType" SelectedIndex="0" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            TextFormatString="{1}" DropDownRows="20" DropDownWidth="400px" AutoPostBack="false" DataTextField="TermType" Style="display: none !important;" DataValueField="ID" OnSelectedIndexChanged="ddlTerminalType_SelectedIndexChanged">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="ID Nr.:" Name="ID Nr.:" Width="20%" />
                                <dx:ListBoxColumn FieldName="TermType" Caption="<%$ Resources:localizedText ,connectiontypedescription%>" Name="TermType" ToolTip="Bezeichnung:" Width="80%" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:TextBox ID="txtTermTyp" runat="server" Text="" ReadOnly="true" Enabled="false" BackColor="White" CssClass="txtTermTyp"></asp:TextBox>
                        <asp:TextBox ID="txtTerminalId" runat="server" CssClass="txtbttmsec numbersOnly"></asp:TextBox>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="txtbttmsec"></asp:TextBox>
                        <asp:TextBox ID="txtSerialNumber" runat="server" CssClass="txtbttmsec"></asp:TextBox>
                        <dx:ASPxComboBox ID="cboLinkType" runat="server" ValueType="System.String" CssClass="dllTermType" Theme="Office2003Blue" ClientInstanceName="cboLinkType" ClientIDMode="Static" ValueField="ID" TextField="ConnectionType" SelectedIndex="0" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            TextFormatString="{1}" DropDownRows="20" DropDownWidth="400px" AutoPostBack="false">
                            <Columns>
                                <dx:ListBoxColumn FieldName="ID" Caption="ID Nr.:" Name="ID Nr.:" Width="20%" />
                                <dx:ListBoxColumn FieldName="ConnectionType" Caption="<%$ Resources:localizedText ,connectiontypedescription%>" Name="ID" ToolTip="Bezeichnung:" Width="80%" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:TextBox ID="txtIpAddress" runat="server" CssClass="txtbttmsec"></asp:TextBox>
                        <asp:TextBox ID="txtIpPort" runat="server" CssClass="txtbttmsec numbersOnly"></asp:TextBox>
                    </section>

                    <section class="leftBttmsec">
                        <section class="bttmsecLonglbls">
                            <asp:Label ID="Label8" runat="server" Text="<%$ Resources:LocalizedText,personnelNumberPin %>" CssClass="lblbttmsecbttm" Style="display: none;"></asp:Label>
                            <asp:Label ID="Label9" runat="server" Text="<%$ Resources:LocalizedText,backToReadFingerPrint %>" CssClass="lblbttmsecbttm" Style="display: none;"></asp:Label>
                            <asp:Label ID="lbldtnk4" runat="server" Text="<%$ Resources:LocalizedText,RFIDreadersactive %>" CssClass="lblbttmsecbttm" Style="display: none;"></asp:Label>
                            <asp:Label ID="lbldtnk2" runat="server" Text="<%$ Resources:LocalizedText, Accessbookings %>" CssClass="lblbttmsecbttm"></asp:Label>
                            <asp:Label ID="Label26" runat="server" Text="<%$ Resources:LocalizedText, Allowstaffno %>" CssClass="lblbttmsecbttm"></asp:Label>
                            <asp:Label ID="Label10" runat="server" Text="<%$ Resources:LocalizedText,Resetfingerprint %>" CssClass="lblbttmsecbttm"></asp:Label>
                            <asp:Label ID="lbldtnk1" runat="server" Text="<%$ Resources:LocalizedText, hasProfessionalFirmware %>" CssClass="lblbttmsecbttm"></asp:Label>
                        </section>

                        <section class="bttmsecLongTxt">
                            <asp:CheckBox ID="chkTAPPosting" runat="server" Style="display: none" CssClass="chkBttm" />
                            <asp:CheckBox ID="chkAPPosting" runat="server" CssClass="chkBttm" />
                            <asp:CheckBox ID="chkpersonnelNumberPin" runat="server" CssClass="chkBttm" />
                            <asp:CheckBox ID="chkbackToReadFingerPrint" runat="server" CssClass="chkBttm" />
                            <asp:CheckBox ID="chkProfFirmware" runat="server" CssClass="chkBttm" />
                        </section>
                    </section>
                </section>

                <section class="bttmsec1right">
                    <section class="tab1leftsection5">
                        <section class="photo">
                            <div id="Photoholder" class="sec1pics tm680bc">
                            </div>
                            <section class="secbtnActivation">
                                <asp:Label ID="lblterminalActivation" runat="server" Text="<%$ Resources:LocalizedText,terminalActivation %>" CssClass="lblterActivation"></asp:Label>
                                <asp:Button ID="btnIsActive" ClientIDMode="Static" runat="server" Text="" CssClass="btnred" Width="30px" />
                                <asp:CheckBox ID="chkStatus" runat="server" ClientIDMode="Static" CssClass="greenredchk" />
                            </section>
                        </section>
                    </section>
                </section>
            </section>

            <section class="bttmsec5">
                <section class="bttmsec5Left">
                    <section class="top">
                        <section class="lblLeft2">
                            <asp:Label ID="lblInfoText1" runat="server" Text="<%$ Resources:LocalizedText, infoText1 %>" CssClass="lblbttmsec"></asp:Label>
                            <asp:Label ID="lblInfoText2" runat="server" Text="<%$ Resources:LocalizedText, infoText2 %>" CssClass="lblbttmsec"></asp:Label>
                            <asp:Label ID="lblInfoText3" runat="server" Text="<%$ Resources:LocalizedText, infoText3 %>" CssClass="lblbttmsec"></asp:Label>
                            <asp:Label ID="lblInfoText4" runat="server" Text="<%$ Resources:LocalizedText, infoText4 %>" CssClass="lblbttmsec"></asp:Label>
                        </section>

                        <section class="txtRight2">
                            <asp:TextBox ID="txtInfoText1" runat="server" CssClass="txtsec5"></asp:TextBox>
                            <asp:TextBox ID="txtInfoText2" runat="server" CssClass="txtsec5"></asp:TextBox>
                            <asp:TextBox ID="txtInfoText3" runat="server" CssClass="txtsec5"></asp:TextBox>
                            <asp:TextBox ID="txtInfoText4" runat="server" CssClass="txtsec5"></asp:TextBox>
                        </section>
                    </section>

                    <section class="bottom">
                        <section class="bttmsecsek" style="display: none;">
                            <asp:Label ID="lblRelaySwitching" runat="server" Text="<%$ Resources:LocalizedText, relaySwitchingTimeForInternalReader %>" CssClass="txtsec5sek"></asp:Label>
                            <asp:TextBox ID="txtRelaySwitching" runat="server" CssClass="lblbttmsec2 "></asp:TextBox>
                        </section>

                        <section class="secBtmAccessProfiles">
                            <section class="secHeader">
                                <asp:Label ID="Label11" runat="server" Text="<%$ Resources:LocalizedText, Amaximumof5timeprofiles %>" CssClass="txtHeaderMaxProfiles colorRed"></asp:Label>
                            </section>

                            <section class="secCont">
                                <section class="btnAccsPrfl">
                                    <asp:Button ID="btnAccess" runat="server" Text="<%$ Resources:LocalizedText, Accessprofile %>" CssClass="newstandardbutton topareabtn" />
                                </section>
                            </section>
                        </section>
                    </section>
                </section>

                <section class="bttmsec5Right">
                    <section class="lblLeft">
                        <asp:Label ID="lblfunctionKey1" runat="server" Text="<%$ Resources:LocalizedText, functionKey1 %>" CssClass="lblbttmsec23"></asp:Label>
                        <asp:Label ID="lblfunctionKey2" runat="server" Text="<%$ Resources:LocalizedText, functionKey2 %>" CssClass="lblbttmsec23"></asp:Label>
                        <asp:Label ID="lblfunctionKey3" runat="server" Text="<%$ Resources:LocalizedText, functionKey3 %>" CssClass="lblbttmsec23"></asp:Label>
                        <asp:Label ID="lblfunctionKey4" runat="server" Text="<%$ Resources:LocalizedText, functionKey4 %>" CssClass="lblbttmsec23"></asp:Label>
                        <asp:Label ID="lblfunctionKey5" runat="server" Text="<%$ Resources:LocalizedText, functionKey5 %>" CssClass="lblbttmsec23"></asp:Label>
                        <asp:Label ID="lblfunctionKey6" runat="server" Text="<%$ Resources:LocalizedText, functionKey6 %>" CssClass="lblbttmsec23"></asp:Label>
                        <asp:Label ID="lblfunctionKey7" runat="server" Text="<%$ Resources:LocalizedText, functionKey7 %>" CssClass="lblbttmsec23"></asp:Label>
                        <asp:Label ID="lblfunctionKey8" runat="server" Text="<%$ Resources:LocalizedText, functionKey8 %>" CssClass="lblbttmsec23"></asp:Label>
                    </section>

                    <section class="txtRight">
                        <dx:ASPxComboBox ID="cbofunctionKey1" runat="server" ValueType="System.String" CssClass="ddlFunctions" TextField="StatusName" ValueField="StatusNr" Theme="Office2003Blue" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            DropDownRows="20" DropDownWidth="200px">
                        </dx:ASPxComboBox>
                        <dx:ASPxComboBox ID="cbofunctionKey2" runat="server" ValueType="System.String" CssClass="ddlFunctions" TextField="StatusName" ValueField="StatusNr" Theme="Office2003Blue" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            DropDownRows="20" DropDownWidth="200px">
                        </dx:ASPxComboBox>
                        <dx:ASPxComboBox ID="cbofunctionKey3" runat="server" ValueType="System.String" CssClass="ddlFunctions" TextField="StatusName" ValueField="StatusNr" Theme="Office2003Blue" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            DropDownRows="20" DropDownWidth="200px">
                        </dx:ASPxComboBox>
                        <dx:ASPxComboBox ID="cbofunctionKey4" runat="server" ValueType="System.String" CssClass="ddlFunctions" TextField="StatusName" ValueField="StatusNr" Theme="Office2003Blue" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            DropDownRows="20" DropDownWidth="200px">
                        </dx:ASPxComboBox>
                        <dx:ASPxComboBox ID="cbofunctionKey5" runat="server" ValueType="System.String" CssClass="ddlFunctions" TextField="StatusName" ValueField="StatusNr" Theme="Office2003Blue" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            DropDownRows="20" DropDownWidth="200px">
                        </dx:ASPxComboBox>
                        <dx:ASPxComboBox ID="cbofunctionKey6" runat="server" ValueType="System.String" CssClass="ddlFunctions" TextField="StatusName" ValueField="StatusNr" Theme="Office2003Blue" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            DropDownRows="20" DropDownWidth="200px">
                        </dx:ASPxComboBox>
                        <dx:ASPxComboBox ID="cbofunctionKey7" runat="server" ValueType="System.String" CssClass="ddlFunctions" TextField="StatusName" ValueField="StatusNr" Theme="Office2003Blue" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            DropDownRows="20" DropDownWidth="200px">
                        </dx:ASPxComboBox>
                        <dx:ASPxComboBox ID="cbofunctionKey8" runat="server" ValueType="System.String" CssClass="ddlFunctions" TextField="StatusName" ValueField="StatusNr" Theme="Office2003Blue" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px"
                            DropDownRows="20" DropDownWidth="200px">
                        </dx:ASPxComboBox>
                    </section>
                </section>
            </section>

            <div class="sectGridAndButtons">
                <section class="secTopGrid">
                    <section class="secHeaderNew">
                        <asp:Label ID="Label12" runat="server" Text="Leser Info" CssClass="lblHeader"></asp:Label>
                    </section>

                    <asp:Table runat="server" CssClass="tbtAddReader" CellPadding="0" CellSpacing="0">
                        <asp:TableRow CssClass="tblRowReader2">
                            <asp:TableCell CssClass="tblCellSmallWith">
                                <asp:Label ID="Label13" runat="server" Text="<%$ Resources:LocalizedText, Idreaders %>" CssClass="lblTblHeader"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellsHeader">
                                <asp:Label ID="Label22" runat="server" Text="<%$ Resources:LocalizedText, Readersinfo%>" CssClass="lblTblHeader"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellsHeader">
                                <asp:Label ID="Label23" runat="server" Text="<%$ Resources:LocalizedText, Readerstype %>" CssClass="lblTblHeader"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellSmallWith2">
                                <asp:Label ID="Label24" runat="server" Text="<%$ Resources:LocalizedText, description%>" CssClass="lblTblHeaderLeft"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellsHeader">
                                <asp:Label ID="Label25" runat="server" Text="<%$ Resources:LocalizedText, direction %>" CssClass="lblTblHeaderLeft"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellSmallWith">
                                <asp:Label ID="Label27" runat="server" Text="Status" CssClass="lblTblHeader"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellSmallWith">
                                <asp:Label ID="Label28" runat="server" Text="Rel. In Sek." CssClass="lblTblHeader2"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow CssClass="tblRowReader">
                            <asp:TableCell CssClass="tblCellSmallWithNew">
                                <asp:Label ID="lblTIId" runat="server" Text="" CssClass="lblTblCells"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCells" RowSpan="2">
                                <asp:Label ID="lblTIInfo" runat="server" Text="" CssClass="lblTblCells"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCells">
                                <asp:Label ID="lblTILaserType" runat="server" Text="" CssClass="lblTblCells"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellSmallWith2new" RowSpan="2">
                                <asp:TextBox ID="txtTIDescription" runat="server" CssClass="txtTblCells3"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCells">
                                <dx:ASPxComboBox ID="ddlTIDirection" runat="server" ValueType="System.String" Theme="Office2003Blue" CssClass="cmbnewarea">
                                    <Items>
                                        <dx:ListEditItem Text="<%$ Resources:LocalizedText, doorEntry %>" Value="0" Selected="True" />
                                        <dx:ListEditItem Text="<%$ Resources:localizedText, doorExit %>" Value="1" Selected="false" />
                                    </Items>
                                </dx:ASPxComboBox>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellSmallWithNew" RowSpan="2">
                                <asp:Label ID="lblTIStatus" runat="server" Text="" CssClass="lblTblCells"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellSmallWith" RowSpan="2">
                                <asp:TextBox ID="txtTIRelayTime" runat="server" CssClass="txtTblCells2"></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow CssClass="tblRowReader">
                            <asp:TableCell CssClass="tblCellSmallWithNew">
                                <asp:Label ID="lblExId" runat="server" Text="" CssClass="lblTblCells"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCells">
                                <asp:Label ID="lblExLaserType" runat="server" Text="" CssClass="lblTblCells"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tblCellSmallWith2" RowSpan="2">
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </section>

                <section class="secTopBtns" style="display: none">
                    <asp:Button ID="btnAddReader" runat="server" Text="<%$ Resources:localizedText, Externalreaders %>" CssClass="btnnewexterner btnNew" />
                    <asp:Button ID="btnDeleteReader" runat="server" Text="<%$ Resources:localizedText, delete %>" CssClass="btnnewexterner btnDelete" />
                </section>
            </div>

            <section class="bttmsec6">
                <asp:Button ID="btnSendMasterData" CssClass="editbtnfooterorange" runat="server" Text="<%$ Resources:localizedText, sendReferenceData %>" Style="padding-left: 0px; width: 130px; margin-left: 0px;" />
                <asp:Button ID="btnGetBookings" CssClass="editbtnfooterorange" runat="server" Text="<%$ Resources:localizedText, getBookings %>" Style="padding-left: 0px; width: 120px; margin-left: 7px;" />
                <asp:Button ID="btnSetSystemTime" CssClass="editbtnfooterorange" runat="server" Text="<%$ Resources:localizedText, setSystemTime %>" Style="padding-left: 0px; width: 120px; margin-left: 0px;" />
                <asp:Button ID="btnTestConnection" CssClass="editbtnfooterorange" runat="server" Text="<%$ Resources:localizedText, testconnection %>" Style="padding-left: 0px; width: 113px; margin-left: 0px;" />
            </section>
        </section>

        <asp:Label ID="Label29" runat="server" Text="Zugeordnete Terminal" CssClass="lblbttmsecbttmnew"></asp:Label>
        <section class="divcontentBottomRight">
            <section class="bttmsecRightbtns" style="display: none">
                <section class="accessprofile" style="display: none">
                    <asp:Label ID="lblAcceeProfile" runat="server" Text="Zutrittsprofile" CssClass="accessProfilelbl"></asp:Label>
                    <asp:TextBox ID="txtFvTotalEntries" runat="server" CssClass="txtSecTopAccess" ReadOnly="true"></asp:TextBox>
                </section>

                <section class="classprevbtn" style="display: none">
                    <section class="fvNavPrevious">
                        <span></span>
                        <asp:Button ID="fvNavPrev" runat="server" Text="" CssClass="btnFvNavPrev" />
                    </section>
                    <section class="fvNavTotalEmpNum">
                        <asp:TextBox ID="txtAccessProfile" runat="server" Enabled="true" CssClass="txtcenter" ReadOnly="true" />
                    </section>
                    <section class="fvNavNext">
                        <span></span>
                        <asp:Button ID="fvNavNext" runat="server" Text="" CssClass="btnFvNavNext" />
                    </section>
                </section>
                <asp:Button ID="btnAssgn" CssClass="btnbttmsecRight btn" runat="server" Text="<%$ Resources:localizedText, assignment %>" Style="margin-right: 21px;" />
                <asp:Button ID="btnTerminals" CssClass="btnbttmsecRight btn coloRed" runat="server" Text="<%$ Resources:localizedText, terminals %>" />
            </section>

            <section class="grdSection secpart1">
                <dx:ASPxGridView ID="grdZkTerminalCon" runat="server" ClientInstanceName="grdZkTerminalCon" AutoGenerateColumns="False" KeyFieldName="ID" EnableTheming="True" Theme="Office2003Blue" Width="100%" SettingsBehavior-ProcessSelectionChangedOnServer="True" ClientIDMode="Static" EnableCallBacks="True" OnSelectionChanged="grdZkTerminalCon_SelectionChanged"
                    font-family="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif !important" Font-Size="14px" OnCustomCallback="grdZkTerminalCon_OnCustomCallback">
                    <ClientSideEvents RowClick="function(s, e) { grdZkTerminalConRowClick(s, e); }"></ClientSideEvents>
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" Visible="False" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, termID %>" VisibleIndex="2" FieldName="TermID">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, type %>" VisibleIndex="3" FieldName="TermType">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, description %>" VisibleIndex="4" FieldName="Description">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, ipAddress1 %>" VisibleIndex="5" FieldName="IpAddress">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, connections %>" VisibleIndex="6" FieldName="ConnectionType">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, status %>" VisibleIndex="7" FieldName="IsActiveText">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, status %>" VisibleIndex="8" FieldName="TerminalId" Visible="false">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" AllowSort="False" AllowDragDrop="False" AllowGroup="False" ProcessSelectionChangedOnServer="true" />
                    <SettingsPager PageSize="33" ShowEmptyDataRows="True" Visible="False" />
                    <SettingsDataSecurity AllowEdit="False" AllowInsert="False" AllowDelete="False" />
                </dx:ASPxGridView>
            </section>

            <section class="secpart2" style="display: none">
                <dx:ASPxGridView ID="grdZkTerminals" runat="server" AutoGenerateColumns="False" EnableTheming="True" Theme="Office2003Blue" Width="100%" KeyFieldName="ID" SettingsBehavior-ProcessSelectionChangedOnServer="True">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" Visible="False" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, type %>" VisibleIndex="2" FieldName="TermType">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, description %>" VisibleIndex="3" FieldName="Description">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, reader %>" VisibleIndex="4" FieldName="Reader">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, connection %>" VisibleIndex="5" FieldName="Connection">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="<%$ Resources:LocalizedText, access %>" VisibleIndex="6" FieldName="Access">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataImageColumn Caption="<%$ Resources:LocalizedText, image %>" VisibleIndex="7" FieldName="Image">
                            <PropertiesImage DescriptionUrlField="Image">
                            </PropertiesImage>
                            <DataItemTemplate>
                                <dx:ASPxImage ID="ASPxImage3" runat="server" ShowLoadingImage="true" ImageUrl='<%# Eval("Image") %>'></dx:ASPxImage>
                            </DataItemTemplate>
                        </dx:GridViewDataImageColumn>
                    </Columns>
                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" AllowSort="False" AllowDragDrop="False" AllowGroup="False" ProcessSelectionChangedOnServer="True" />
                    <SettingsPager PageSize="13" ShowEmptyDataRows="True" Visible="False">
                    </SettingsPager>
                    <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
                </dx:ASPxGridView>
            </section>

            <section class="secpart3 secGrid1">
                <section class="secBottomLeft">
                    <section class="top1Left">
                        <section class="secTopleftlabels">
                            <section class="secLablesDivisions">
                                <asp:Label ID="Label33" runat="server" Text="Gruppen Nr." CssClass="lblgrpPrflNo"></asp:Label>
                            </section>
                            <section class="secLablesDivisions2">
                                <asp:Label ID="Label35" runat="server" Text="Gruppen Bezeichnung" CssClass="lblgrpPrfldesc"></asp:Label>
                            </section>
                            <section class="secLablesDivisions3">
                                <asp:Label ID="Label36" runat="server" Text="Profil Nr." CssClass="lblaccsPrflNo"></asp:Label>
                            </section>
                            <section class="secLablesDivisions">
                                <asp:Label ID="Label37" runat="server" Text="Profil ID" CssClass="lblZtrt"></asp:Label>
                            </section>
                            <section class="secLablesDivisions4">
                                <asp:Label ID="Label38" runat="server" Text="Profil Bezeichnung" CssClass="lblaccsdsc"></asp:Label>
                            </section>
                        </section>
                        <section class="secToplefttextboxes">
                            <section class="secLablesDivisions">
                                <asp:TextBox ID="txtGroupProfileNo1" runat="server" CssClass="drpDwnSecTop" ReadOnly="true"></asp:TextBox>
                            </section>
                            <section class="secLablesDivisions2">
                                <asp:TextBox ID="txtGroupProfileDescription1" runat="server" CssClass="txtSecTop1" ReadOnly="true"></asp:TextBox>
                            </section>
                            <section class="secLablesDivisions3">
                                <asp:TextBox ID="txtAccessProfileNo" runat="server" CssClass="drpDwnSecTopssd" ReadOnly="true"></asp:TextBox>
                            </section>
                            <section class="secLablesDivisions">
                                <asp:TextBox ID="txtAccessProfileID" runat="server" CssClass="txtZutrttID maxlength4" ReadOnly="true"></asp:TextBox>
                            </section>
                            <section class="secLablesDivisions4">
                                <asp:TextBox ID="txtAccessDescription" runat="server" CssClass="txtSecTop1dsds" ReadOnly="true"></asp:TextBox>
                            </section>
                        </section>
                    </section>

                    <section class="sectionGrid">
                        <asp:Table runat="server" ID="tableHeader" CssClass="tableRowMain" CellPadding="0" CellSpacing="0">
                            <asp:TableHeaderRow CssClass="tableRowprofile">
                                <asp:TableCell CssClass="tableCell1">
                                    <asp:Label ID="Label39" runat="server" Text="Profile aktiv"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label40" runat="server" Text="Mo."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label41" runat="server" Text="Mo."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label42" runat="server" Text="Di."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label43" runat="server" Text="Di."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label44" runat="server" Text="Mit."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label45" runat="server" Text="Mit."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label46" runat="server" Text="Do."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label47" runat="server" Text="Do."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label48" runat="server" Text="Fr."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label49" runat="server" Text="Fr."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label50" runat="server" Text="Sa."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label51" runat="server" Text="Sa."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label52" runat="server" Text="So."></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell CssClass="tableCell">
                                    <asp:Label ID="Label53" runat="server" Text="So."></asp:Label>
                                </asp:TableCell>
                            </asp:TableHeaderRow>
                        </asp:Table>


                        <dx:ASPxGridView ID="grdZuttritProfileTimeFrames" runat="server" AutoGenerateColumns="False" UseDisabledStatePainter="false" SettingsBehavior-AllowSort="false" Theme="Office2003Blue" CssClass="grid" Width="100%" ClientInstanceName="grdZuttritProfileTimeFrames" KeyFieldName="ID" ForeColor="Black" OnCustomCallback="grdZuttritProfileTimeFrames_CustomCallback" ClientSideEvents-EndCallback="displayProfileInformation">
                            <Columns>
                                <dx:GridViewDataCheckColumn VisibleIndex="4" Caption="Alle" Width="9.5%" FieldName="ProfilAktiv">
                                    <PropertiesCheckEdit EnableClientSideAPI="True">
                                        <ClientSideEvents CheckedChanged="function(s, e) {
	activateProfile(s, e);
}" />
                                    </PropertiesCheckEdit>
                                </dx:GridViewDataCheckColumn>

                                <dx:GridViewDataTimeEditColumn Caption="von" FieldName="MonFrom" VisibleIndex="6" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm" EnableFocusedStyle="False">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="bis" FieldName="MonTo" VisibleIndex="8" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="von" FieldName="TueFrom" VisibleIndex="10" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="bis" FieldName="TueTo" VisibleIndex="12" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="von" FieldName="WedFrom" VisibleIndex="14" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="bis" FieldName="WedTo" VisibleIndex="16" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="von" FieldName="ThurFrom" VisibleIndex="18" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="bis" FieldName="ThurTo" VisibleIndex="20" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="von" FieldName="FriFrom" VisibleIndex="22" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="bis" FieldName="FriTo" VisibleIndex="24" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="von" FieldName="SatFrom" VisibleIndex="26" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="bis" FieldName="SatTo" VisibleIndex="28" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="von" FieldName="SunFrom" VisibleIndex="30" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTimeEditColumn Caption="bis" FieldName="SunTo" VisibleIndex="31" Width="6%">
                                    <PropertiesTimeEdit DisplayFormatString="HH:mm">
                                        <SpinButtons Enabled="False" ShowIncrementButtons="False">
                                        </SpinButtons>
                                    </PropertiesTimeEdit>
                                </dx:GridViewDataTimeEditColumn>
                                <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" Visible="False" VisibleIndex="3">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" AllowSelectSingleRowOnly="True" />
                            <SettingsPager PageSize="10" ShowEmptyDataRows="True" Mode="ShowAllRecords" Visible="False">
                            </SettingsPager>
                            <SettingsEditing EditFormColumnCount="20" Mode="Batch">
                                <BatchEditSettings ShowConfirmOnLosingChanges="False" />
                            </SettingsEditing>
                            <Settings ShowStatusBar="Hidden" />
                            <SettingsDetail ShowDetailButtons="False" />
                            <SettingsDataSecurity AllowDelete="False" AllowEdit="false" AllowInsert="false" />
                        </dx:ASPxGridView>
                    </section>

                    <section class="sectionMemo">
                        <section class="secMemoHeader">
                            <asp:Label ID="lblMemo" runat="server" Text="Memo" CssClass="lblMemo"></asp:Label>
                        </section>
                        <section class="secMemo">
                            <asp:TextBox ID="txtMemoNotes" runat="server" CssClass="txtMemo" TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                        </section>
                    </section>
                </section>
            </section>
        </section>
    </section>

    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True" ShowImage="true" ClientIDMode="Static" Text="Tun aufgabe auf ausgewählten terminnals">
        <Image Url="../Images/FormImages/Loading.gif" />
    </dx:ASPxLoadingPanel>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="FooterLeft" runat="server">
    <asp:Button ID="btnNew" CssClass="newbtnfooterblue" runat="server" Text="Terminal neu" Style="width: 89px;" />
    <asp:Button ID="btnSaveTerminalConfig" CssClass="savebtnfootergreen" runat="server" Text="Terminal speichern" Style="width: 122px;" />
    <asp:Button ID="btnDelete" CssClass="deletebtnfooterred" runat="server" Text="Terminal löschen" Style="width: 122px;" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="FooterRight" runat="server">
    <asp:Button ID="btnBackToDashBoard" CssClass="backbtnfooterred" runat="server" Text="<%$ Resources:localizedText, back %>" />
    <asp:Button ID="Button11" CssClass="helpbtnfooterblue" runat="server" Text="<%$ Resources:localizedText, help %>" />
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="Contentbottomarea" runat="server">
</asp:Content>
