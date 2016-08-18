$(document).ready(function () {

    //$("#btnTermKonfig").click(function (evt) {
    //    evt.preventDefault();
    //    document.location.href = "/Content/Customer.aspx";
    //});

    $("#btnAccessControl").click(function (evt) {
        evt.preventDefault();
        window.location.href = "http://www.allkrucloud.com:209";
    });
    $("#btnPortal").click(function (evt) {
        evt.preventDefault();
        window.location.href = "http://www.allkrucloud.com/MemberSolutions.aspx";
    });
    $("#btndaten").click(function (evt) {
        evt.preventDefault();
        document.location.href = "/Content/Datacommunication.aspx";
    });
    $("#btndatentop").click(function (evt) {
        evt.preventDefault();
        document.location.href = "/Content/Datacommunication.aspx";
    });
    $("#btnHolidayCalendar").click(function (evt) {
        evt.preventDefault();
        //document.location.href = "/Content/HolidayCalendar.aspx";
    });
    $("#btnLanguage").click(function (evt) {
        evt.preventDefault();
        //document.location.href = "/Content/Language.aspx";
    });

    $("#btnKrutecZK").click(function (evt) {
        evt.preventDefault();
        window.location = "/Content/ZkTerminalsDashBoard.aspx";
    });

    $("#btnDatafox").click(function (evt) {
        evt.preventDefault();
        document.location.href = "/Content/DatafoxTerminalsDashBoard.aspx";
    });

    $("#btnMobileApp").click(function (evt) {
        evt.preventDefault();
        //document.location.href = "/Content/MobileAppSettings.aspx";
    });

    $("#btnReadFingerPrints").click(function (evt) {
        evt.preventDefault();
        //document.location.href = "/Content/FingerprintreadNew.aspx";
    });

    $("#btnTimeAttendance").click(function (evt) {
        evt.preventDefault();
        //document.location.href = "/Content/TimeAttendanceGroupe_Net.aspx";
    });

    $("#btnASPTimeAttendance").click(function (evt) {
        evt.preventDefault();
        //document.location.href = "/Content/VirtualTerminalGoups.aspx";
    });

    $("#btnAutomatic").click(function (evt) {
        evt.preventDefault();
        //document.location.href = "/Content/DashboardSettings.aspx";
    });

    $("#btnManual").click(function (evt) {
        evt.preventDefault();
        //document.location.href = "/Content/DataCommunicationManual.aspx";
    });

})
