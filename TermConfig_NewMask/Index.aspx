<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="TermConfig_NewMask.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/Index.js"></script>
    <link href="Styles/Index.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ModuleNavBar" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentHolder" runat="server">
        <div class="indexMainHolder">
        <section class="indexTopSec">
            <section class="indexTopSecLeft">
                <section class="indxOneTitle">
                    <asp:Label ID="Label1" runat="server" Text="Installiert Krutec Software" CssClass="lblSoftwareHeader"></asp:Label>
                </section>
                <section class="indxOne">
                     <asp:Button ID="btnTermKonfig" runat="server" Text="" CssClass="btntmk btnHovers" style="display:none;" />
                    <asp:Button ID="btnAccessControl" runat="server" Text="" CssClass="btnzut btnHovers"   />
                </section>
                <section class="indxOneFooter">
                </section>
            </section>
            <section class="indexTopSecRight">

                 <asp:Button ID="btnPortal" runat="server" Text="" CssClass="btnportal btnHovers" />

            </section>
        </section>
        <section class="indexBttmSec">
            <section class="indexTopSecLeft">
                <section class="indxBttmOneTitle">
                    <asp:Label ID="Label3" runat="server" CssClass="lblTitle" Text="Service Studio von Krutec SoftCon GmbH Version: A1.0.001"></asp:Label>
                </section>
                <section class="indxBttmOne">
                     <section class="btnModulesGreen2">
                       <asp:Button ID="btndatentop" runat="server" Text="Zeiterfassung" CssClass=" topbuttonnew"  />
                          <asp:Button ID="btndaten" runat="server" Text="Datenkommunikation" CssClass="historypic btnHovers" />

                     </section>
                    
                     <section class="btnModulesGreen">
                         <asp:Button ID="Button3" runat="server" Text="Zutritt /Service Studio" CssClass="zutspic btnHovers" /></section>

                      <section class="btnModulesGreen">
                <asp:Button ID="btnHolidayCalendar" runat="server" Text="Feiertagskalender" CssClass="calenderspic btnHovers" />
            </section>
                    <section class="btnModulesGreen2">
                <asp:Button ID="btnLanguage" runat="server" Text="Sprache" CssClass="languagepic btnHovers" />
            </section>
                    <section class="btnModulesGreen">
                <asp:Button ID="btnrights" runat="server" Text="Passwörter" CssClass="rightspic btnHovers" />
            </section>
                 
                </section>
                <section class="indxBttmOne">
                    <section class="btnModulesGreenblue2">
                        <asp:Button ID="btnKrutecZK" runat="server" Text="Krutec ZK" CssClass="kruteczkpic btnHovers" />
                    </section>
                    <section class="btnModulesGreenblue">
                        <asp:Button ID="btnDatafox" runat="server" Text="Datafox" CssClass="datafoxpic btnHovers" />
                    </section>
                    <section class="btnModulesGreenblue2">
                        <asp:Button ID="btnMobileApp" runat="server" Text="Mobile Erfassung" CssClass="mobilepic btnHovers" />
                    </section>
                      <section class="btnModulesGreenblue" >
                        <asp:Button ID="btnVirtualTerminal" runat="server" Text="Virtuelle- Terminal" CssClass="VirtualTerminal btnHovers" />
                    </section>
                    <section class="btnModulesGreenblue" style="display:none;" >
                        <asp:Button ID="btnReadFingerPrints" runat="server" Text="Fingerprint einiesen" CssClass="fingerprintpic btnHovers" />
                    </section>
                   
                </section>
                <section class="indxBttmOne">
                    <section class="btnModulesRed"><asp:Button ID="btnTimeAttendance" runat="server" Text="Gruppen PZE.Net" CssClass="personalpic btnHovers" /></section>
                      <section class="btnModulesRed2"><asp:Button ID="btnASPTimeAttendance" runat="server" Text="Virtuelle Gruppen" CssClass="locationspic btnHovers" /></section>
                   
                </section>
                <section class="indxBttmOne">
                      <section class="btnModulesRedlast"> <asp:Button ID="btnAutomatic" runat="server" Text="Datenverkehr automatisch" CssClass="shiftspic btnHovers" /></section>
                      <section class="btnModulesRedlast2"><asp:Button ID="btnManual" runat="server" Text="Datenverkehr manuell" CssClass="shiftallspic btnHovers" /></section>
                  
                </section>
               
            </section>
            <section class="indexTopSecRight"></section>
        </section>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterLeft" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="FooterRight" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="Contentbottomarea" runat="server">
</asp:Content>
