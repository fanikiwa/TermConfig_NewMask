<%@ Page Title="Datenkommunikation" Language="C#" MasterPageFile="~/MasterPages/Primary.Master" AutoEventWireup="true" CodeBehind="Datacommunication.aspx.cs" Inherits="TermConfig_NewMask.Content.Datacommunication" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Datacommunication.js"></script>
    <link href="Styles/Datacommunication.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ModuleNavBar" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="mainholderarea">
        <div class="holdermainleft">
            <section class="dateareatop">
                <section class="displaytimelbl">
                    <asp:Label ID="Label12" runat="server" Text="Datum" Style="padding-right: 4px;"></asp:Label>
                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                </section>
            </section>
            <section class="contentholderbtn">
                <section class="btnModulesGreen2">
                    <asp:Button ID="btnHardwareNavigation" runat="server" Text="Mandanten" CssClass="rightsepic btnHovers"   />
                </section>
                <section class="btnModulesGreen">
                    <asp:Button ID="btnIconSelection" runat="server" Text="Buchungsarchiv" CssClass="passwordspic btnHovers"   />
                </section>
            </section>
            <section class="timetbarea">
                <section class="displaytimelbl">
                    <asp:Label ID="Label10" runat="server" Text="Uhrzeit" Style="padding-right: 6px;"></asp:Label>
                    <asp:Label ID="lbltime" runat="server" Text="  "></asp:Label>
                </section>
                <asp:Label ID="Label11" runat="server" Text="Daten-Synchronisation alle..." Style="clear: both; float: left;"></asp:Label>
                <section class="tablearaesmall">
                    <asp:Table runat="server" CssClass="tableRowMain" CellPadding="0" CellSpacing="0">
                        <asp:TableHeaderRow CssClass="tableRowprofile">
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label2" runat="server" Text="1 Minuten"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync1Min" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label3" runat="server" Text="60 Minuten"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync60Min" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                        </asp:TableHeaderRow>
                        <asp:TableHeaderRow CssClass="tableRowprofile">
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label48" runat="server" Text="5 Minuten"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync5Min" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label50" runat="server" Text="4,00 Stunden"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync4Hours" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                        </asp:TableHeaderRow>
                        <asp:TableHeaderRow CssClass="tableRowprofile">
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label4" runat="server" Text="10 Minuten"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync10Min" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label5" runat="server" Text="6,00 Stunden"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync6Hours" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                        </asp:TableHeaderRow>
                        <asp:TableHeaderRow CssClass="tableRowprofile">
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label6" runat="server" Text="15 Minuten"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync15Min" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label7" runat="server" Text="12,00 Stunden"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync12Hours" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                        </asp:TableHeaderRow>
                        <asp:TableHeaderRow CssClass="tableRowprofile">
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label8" runat="server" Text="30 Minuten"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync30Min" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCelllbl">
                                <asp:Label ID="Label9" runat="server" Text="24,00 Stunden"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell CssClass="tableCellchk">
                                <asp:CheckBox ID="chkSync24Hours" CssClass="chkSync" runat="server" />
                            </asp:TableCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                    <asp:Button ID="btnSyncManual" runat="server" Text="Manuell Synchronisieren" Style="clear: both; float: right; width: 163px;" CssClass="editbtnfooterorange" />
                </section>
            </section>
        </div>
        <div class="holdermainright">
            <section class="monitorlblarea">
                <section class="lblarea">
                    <asp:Label ID="Label1" runat="server" Text="Buchungsmonitor latzten 60 Minuten"></asp:Label>
                </section>
                <section class="monitorbtn">
                    <asp:Button ID="btnSyncData" runat="server" Text="COM.DB Synchronisation" CssClass="helpbtnfooterblue" Style="width: 163px; float: left;" />
                    <asp:Button ID="btnFromTerminalData" runat="server" Text="Daten vom Terminal" CssClass="helpbtnfooterblue" Style="width: 135px; float: left;" />
                    <asp:Button ID="btnToTerminalData" runat="server" Text="Daten zum Terminal" CssClass="helpbtnfooterblue" Style="width: 135px; float: left;" />
                </section>
            </section>
            <section class="monitorgridarea">
                <dx:ASPxGridView ID="grdBookings" runat="server" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px" Width="100%"
                    SettingsPager-ShowEmptyDataRows="True" SettingsPager-PageSize="28">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Id" VisibleIndex="0" Visible="false" FieldName="ID">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Datum:" VisibleIndex="1" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff" FieldName="BookingDate">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Uhrzeit:" VisibleIndex="2" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff" FieldName="BookingTime">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Name:" VisibleIndex="3" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff" FieldName="PersName">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ausweis:" VisibleIndex="4" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff" FieldName="PersAusweisNr">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Status:" VisibleIndex="5" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff" FieldName="Status">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Terminal:" VisibleIndex="6" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff" FieldName="Terminal">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </section> 
            <section class="monitorgridarea2" style="display: none;">
                <dx:ASPxGridView ID="grdPersonalSync" runat="server" Font-Names="Segoe UI,Tahoma,Arial,Geneva,Verdana,sans-serif" Font-Size="14px" Width="100%"
                    SettingsPager-ShowEmptyDataRows="True" SettingsPager-PageSize="28">
                    <Columns>
                        <dx:GridViewDataTextColumn Caption="Id" VisibleIndex="0" Visible="false">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Datum:" VisibleIndex="1" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Gesendet um Uhrzeit:" VisibleIndex="2" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Personal Nr.:" VisibleIndex="3" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff" FieldName="Pers_Nr">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Name:" VisibleIndex="4" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff">
                            <DataItemTemplate> 
                                <dx:ASPxLabel runat="server" Text='<%# String.Format("{0}, {1}", Eval("Pers_Name1"), Eval("Pers_Name2")) %>'></dx:ASPxLabel>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Ausweis:" VisibleIndex="6" HeaderStyle-BackColor="#d9390f" HeaderStyle-ForeColor="#ffffff" FieldName="Pers_Ausweis_Nr">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
            </section>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterLeft" runat="server">
      <asp:Button ID="btnSave" CssClass="savebtnfootergreen" runat="server" Text="Daten speichern"  />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterRight" runat="server">
     <asp:Button ID="btnBack" ClientIDMode="Static" CssClass="backbtnfooterred" runat="server" Text="Zurück" OnClick="btnBack_Click" />
    <asp:Button ID="Button5" CssClass="helpbtnfooterblue" runat="server" Text="Hilfe" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Contentbottomarea" runat="server">
</asp:Content>
