<%@ Page Title="Datafox Terminals" Language="C#" MasterPageFile="~/MasterPages/DatafoxTerminals.Master" AutoEventWireup="true" CodeBehind="DatafoxTerminalsDashBoard.aspx.cs" Inherits="TermConfig_NewMask.Content.DatafoxTerminalsDashBoard" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/DatafoxTerminalsDashBoard.js"></script>
    <link href="Styles/DatafoxTerminalsDashBoard.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ModuleNavBar" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="holdermain">
        <section class="contentholder">
            <asp:Label ID="Label1" runat="server" Text="Datafox Terminals" CssClass="toptitle"></asp:Label>
            <section class="indxBttmOne">
                <section class="btnModulesGreen">
                    <asp:Button ID="Button2" runat="server" Text="TM EVO 2.8" CssClass="TMEVO28 btnHovers" />
                </section>
                <section class="btnModulesGreen2">
                    <asp:Button ID="Button3" runat="server" Text="TM EVO 4.3" CssClass="TMEVO43 btnHovers" />
                </section>
                <section class="btnModulesGreen">
                    <asp:Button ID="Button4" runat="server" Text="PZE MasterIV" CssClass="PZEMasterIV btnHovers" />
                </section>
                <section class="btnModulesGreen2">
                    <asp:Button ID="Button5" runat="server" Text="TM Master IV" CssClass="TMMasterIV btnHovers" />
                </section>
                <section class="btnModulesGreen">
                    <asp:Button ID="Button6" runat="server" Text="TM Flex Master" CssClass="TMFlexMaster btnHovers" />
                </section>
                <section class="btnModulesGreen2">
                    <asp:Button ID="Button7" runat="server" Text="Timeboy IV" CssClass="TimeboyIV btnHovers" />
                </section>
            </section>
            <section class="indxBttmOne">
                <section class="btnModulesgoldarea">
                    <asp:Button ID="Button8" runat="server" Text="ZK Master IV" CssClass="ZKMasterIV btnHovers" />
                </section>

            </section>

        </section>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterLeft" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterRight" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Contentbottomarea" runat="server">
</asp:Content>
