<%@ Page Title="Error" Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="TermConfig_NewMask.Error" ClientIDMode="Static" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>ERROR</title>
    <link rel="shortcut icon" href="/favicon.ico" />
    <link rel="stylesheet" href="Styles/Error.css" type="text/css" media="all" />
    <script lang="javascript" type="text/javascript" src="Scripts/Error.js"></script>
</head>

<body>
    <form id="MainForm" runat="server">
        <div id="dashboardmain">
            <div id="mainmenutop">
                <div id="MainHeader">
                    <section class="MainheaderSec">
                        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" ScriptMode="Release">
                        </asp:ScriptManager>
                        <asp:Button ID="btnDashboard" runat="server" Text="Dashboard" CssClass="btnDashboard" OnClick="btnDashboard_Click" />
                    </section>
                    <section class="MainheaderSec1">
                        <section class="secPlaceHolder"></section>
                    </section>
                    <section class="MainheaderSec2">
                        <asp:Label ID="PageTitleLbl" runat="server" Text="" CssClass="companylbl"></asp:Label>
                        <asp:Label ID="pagenamelbl" runat="server" Text="" CssClass="pagenamelbl"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="" CssClass="solutionversionlbl"></asp:Label>
                    </section>
                    <section class="MainheaderSec3">
                        <asp:Button ID="btnLogout" runat="server" Text="Login/Logout" CssClass="btnLogoutsolution" />
                    </section>
                </div>
            </div>
            <div id="companyDetails">
                <div class="dvnsLeft">
                    <div id="contentarea">
                        <div id="navcontentarea" class="NavPanel">
                        </div>
                    </div>
                </div>
                <div class="dvnsRight">
                </div>
            </div>
        </div>
    </form>
</body>
</html>

