<%@ Page Title="Krutec ZK Terminals" Language="C#" MasterPageFile="~/MasterPages/ZkTerminals.Master" AutoEventWireup="true" CodeBehind="ZkTerminalsDashBoard.aspx.cs" Inherits="TermConfig_NewMask.Content.ZkTerminalsDashBoard" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/ZkTerminalsDashBoard.js"></script>
    <link href="Styles/ZkTerminalsDashBoard.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ModuleNavBar" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="server">
    <div class="holdermain">
        <section class="contentholder">
            <asp:Label ID="Label2" runat="server" Text="Krutec ZK Terminals" CssClass="toptitle"></asp:Label>
            <section class="indxBttmOne">
                <section class="btnModulesGreen">
                    <asp:Button ID="btn560bc" runat="server" Text="TM 560bc" CssClass="ZK560bc btnHovers" />
                </section>
                <section class="btnModulesGreen2">
                    <asp:Button ID="btn560tc" runat="server" Text="TM 560tc" CssClass="zk560tc btnHovers" />
                </section>
                <section class="btnModulesGreen">
                    <asp:Button ID="btn680tc" runat="server" Text="TM 680tc" CssClass="zk680tc btnHovers" OnClick="btn680tc_Click"/>
                </section>
                <section class="btnModulesGreen2">
                    <asp:Button ID="btn680bc" runat="server" Text="TM 680bc" CssClass="zk680bc btnHovers" OnClick="btn680bc_Click" />
                </section>
                <section class="btnModulesGreen">
                    <asp:Button ID="btn900bc" runat="server" Text="TM 900bc" CssClass="zk900bc btnHovers"  OnClick="btn900bc_Click"/>
                </section>
            </section>
            <section class="indxBttmOne">
                <section class="btnModulesgoldarea">
                    <asp:Button ID="btnSC403" runat="server" Text="SC403" CssClass="zkSC403 btnHovers" />
                </section>
                <section class="btnModulesgoldarea2">
                    <asp:Button ID="btnScr100" runat="server" Text="SCR 100" CssClass="zkSCR100 btnHovers" />
                </section>
                <section class="btnModulesgoldarea">
                    <asp:Button ID="btnMA300out" runat="server" Text="MA300out" CssClass="zkMA300out btnHovers" />
                </section>
                <section class="btnModulesgoldarea2">
                    <asp:Button ID="btnTF1700" runat="server" Text="TF 1700" CssClass="zkTF1700 btnHovers" />
                </section>
                <section class="btnModulesgoldarea">
                    <asp:Button ID="btnZB702in" runat="server" Text="ZB702in" CssClass="zkZB702in btnHovers" />
                </section>
                <section class="btnModulesgoldarea2">
                    <asp:Button ID="btnZB703in" runat="server" Text="ZB703in" CssClass="zkZB703in btnHovers" />
                </section>
                <section class="btnModulesgoldarea">
                    <asp:Button ID="btnZBBi402" runat="server" Text="ZBBi-402" CssClass="zkZBBi402 btnHovers" />
                </section>
                <section class="btnModulesgoldarea2">
                    <asp:Button ID="btnZBBi404" runat="server" Text="ZBBi-404" CssClass="zkZBBi404 btnHovers" />
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
