/// <reference path="ASPxScriptIntelliSense.js" />
var edit_mode = false;
var gobackhide = 0;
var levelCaption = "";

$(function () {
    try {

        //if (_aspxGetCookie("TermKonfigZk") === "0") {
        //    $('#btnAssgn').css('color', 'red');
        //    $('#btnTerminals').css('color', 'black');
        //    $('#btnMoreFunctions').css('color', 'black');
        //    _secAuftrage = $(".secpart1")
        //    if (_secAuftrage.css("display") === "none") {
        //        $(".secpart, .secpart2").hide();
        //        _secAuftrage.show();
        //    }
        //}

        //if (_aspxGetCookie("TermKonfigZk") === "1") {
        //    $('#btnTerminals').css('color', 'red');
        //    $('#btnAssgn').css('color', 'black');
        //    $('#btnMoreFunctions').css('color', 'black');
        //    _secAuftrage = $(".secpart2")
        //    if (_secAuftrage.css("display") === "none") {
        //        $(".secpart1, .secpart").hide();
        //        _secAuftrage.show();
        //    }
        //}

        $('.numbersOnly').keyup(function () {
            this.value = this.value.replace(/[^0-9\.]/g, '');
        });

        $("#btnNew").click(function (e) {
            e.preventDefault();
            ClearControls();
            ClearOtherControls();
            sessionStorage.setItem("terminalconfigid", "0");
            PageMethods.GetNextTerminalConfigNr(SetNextTerminalConfigNr);
            PageMethods.GetTerminalbyType(SetTerminalTypeId);
            $('#txtTerminalId').removeAttr('disabled');
            $("#txtDescription").focus();
            document.getElementById('txtIpPort').value = "4370";

            sessionStorage.setItem("isnew", "1");
            window.location.href = "/Content/ZkTerminalsDashBoard.aspx";
        });

        //$('#btnNew').click(function (evt) {
        //    evt.preventDefault();
        //    SetControlsOnNew();
        //    $("#ddlCurrentZkTerminalInstance").val("");
        //    var index = grdZkTerminals.GetFocusedRowIndex();
        //    var terminalType = grdZkTerminals.GetRow(index).cells[0].childNodes[0].textContent;
        //    $("#txtTerminalType").val(terminalType);
        //    SetNextUserTerminalId();

        //    if (_aspxGetCookie("TermKonfigZk") === "0") {
        //        $('#btnAssgn').css('color', 'red');
        //        $('#btnTerminals').css('color', 'black');
        //        $('#btnMoreFunctions').css('color', 'black');
        //        _secAuftrage = $(".secpart1")
        //        if (_secAuftrage.css("display") === "none") {
        //            $(".secpart, .secpart2").hide();
        //            _secAuftrage.show();
        //        }
        //    }

        //    if (_aspxGetCookie("TermKonfigZk") === "1") {
        //        $('#btnTerminals').css('color', 'red');
        //        $('#btnAssgn').css('color', 'black');
        //        $('#btnMoreFunctions').css('color', 'black');
        //        _secAuftrage = $(".secpart2")
        //        if (_secAuftrage.css("display") === "none") {
        //            $(".secpart1, .secpart").hide();
        //            _secAuftrage.show();
        //        }
        //    }
        //});

        $('#btnAccess').css("background-color", "");

        $("#btnAccess").prop("disabled", false);

        $("#btnAccess").click(function (e) {
            e.preventDefault();
            $('.secpart3').show();
            $('.bttmsecRightbtns').show();
            $('.bttmsecRightbtns').css({
                'background-color': '#d0e4f7',
                'border': '1px solid gray',
                'margin-left': 'auto',
                'margin-right': 'auto',
                'border-radius': '5px',
                'width': '99%'
            });
            $('#btnAssgn').hide();
            $('#btnTerminals').hide();
            $('.accessprofile').show();
            $('.classprevbtn').show();
            $('.secpart1').hide();
            $('.secpart').hide();
            $('.secpart2 ').hide();
            $('.secgridhJDNew ').hide();

            $(".secpart1").hide();
            $(".secpart2").hide();
            $(".secpart3, .top, .secGrid1, .secBottomLeft, .bttmsecRightbtns").show();
            $(".secGridview").hide();
            $(".secGridviewsplit2").hide();
            $(".secAcceptChanges").hide();
            $(".secgridhJDNew").hide();

            gobackhide = 2;
            grdZuttritProfileTimeFrames.PerformCallback($("#txtTerminalId").val());
        });

        $("#btnAddReader").click(function (e) {
            e.preventDefault();
            AddExternalReader();
        });

        $("#btnAssgn").click(function (e) {
            e.preventDefault();
            ShowUserZkTerminals();
        });

        $("#btnTerminals").click(function (e) {
            e.preventDefault();
            HideUserZkTerminals();
        });

        $("#btnSendMasterData").click(function (e) {
            e.preventDefault();
            var Description = $("#txtDescription").val();
            var IpAddress = $("#txtIpAddress").val();
            showLoadingPanel();
            PageMethods.SendMasterData(Description, IpAddress, doTaskAfterCommunicationAction);
        });

        $("#btnGetBookings").click(function (e) {
            e.preventDefault();
            var Description = $("#txtDescription").val();
            var IpAddress = $("#txtIpAddress").val();
            showLoadingPanel();
            PageMethods.GetBookings(Description, IpAddress, doTaskAfterCommunicationAction);
        });

        $("#btnSetSystemTime").click(function (e) {
            e.preventDefault();
            var Description = $("#txtDescription").val();
            var IpAddress = $("#txtIpAddress").val();
            showLoadingPanel();
            PageMethods.SetSystemTime(Description, IpAddress, doTaskAfterCommunicationAction);
        });

        $("#btnTestConnection").click(function (e) {
            e.preventDefault();
            var Description = $("#txtDescription").val();
            var IpAddress = $("#txtIpAddress").val();
            showLoadingPanel();
            PageMethods.ConnectionTest(Description, IpAddress, doTaskAfterCommunicationAction);
        });

        $("#btnIsActive").click(function (e) {
            e.preventDefault();
            //var ActiveImgUrl = "/Images/FormImages/Dot_gruen.png";
            //var InactiveImgUrl = "/Images/FormImages/Dot_rot.png";
            //var browserUrl = $('.btnblue').css('background-image').substring($('.btnblue').css('background-image').indexOf('/Images'), $('.btnblue').css('background-image').indexOf('g"')).concat('g');

            //if (browserUrl === ActiveImgUrl) {
            //    $('.btnblue').css('background-image', 'url(' + InactiveImgUrl + ')');
            //    IsActiveTerminal = false;
            //}
            //if (browserUrl === InactiveImgUrl) {
            //    $('.btnblue').css('background-image', 'url(' + ActiveImgUrl + ')');
            //    IsActiveTerminal = true;
            //}
        });

        $("#chkStatus").change(function (e) {
            e.preventDefault();
            var ActiveImgUrl = "/Images/FormImages/Dot_gruen.png";
            var InactiveImgUrl = "/Images/FormImages/Dot_rot.png";
            //var browserUrl = $('.btnblue').css('background-image').substring($('.btnblue').css('background-image').indexOf('/Images'), $('.btnblue').css('background-image').indexOf('g"')).concat('g');
            if ($("#chkStatus")[0].checked === true) {
                // IsActiveTerminal = true;
                $('.btnred').css('background-image', 'url(' + ActiveImgUrl + ')');
                $("#lblTIStatus").text("Aktiv");
                $("#lblTIStatus").css("color", "green");
                edit_mode = true;
            }
            else if ($("#chkStatus")[0].checked === false) {
                // IsActiveTerminal = false;
                $('.btnred').css('background-image', 'url(' + InactiveImgUrl + ')');
                $("#lblTIStatus").text("Inaktiv");
                $("#lblTIStatus").css("color", "red");
                edit_mode = true;
            }
        });

        $("#ddlTIDirection").change(function () {
            var selectvalFrom = $("#ddlTIDirection option:selected").val();
            $("#ddlExDirection").val(selectvalFrom);
        });

        $("#ddlExDirection").change(function () {
            var selectvalFrom = $("#ddlExDirection option:selected").val();
            $("#ddlTIDirection").val(selectvalFrom);
        });

        $("#btnMoreFunctions").click(function (e) {
            e.preventDefault();
            $('#btnMoreFunctions').css('color', 'red');
            $('#btnAssgn').css('color', 'black');
            $('#btnTerminals').css('color', 'black');

            _secAuftrage = $(".secpart")

            if (_secAuftrage.css("display") === "none") {
                $(".secpart1, .secpart2").hide();
                _secAuftrage.show();
            }
        });

        $("#btnBackToDashBoard").click(function (e) {
            e.preventDefault();

            //if (edit_mode) {
            //    BackButtonConfirm();
            //} else {
            //    redirectPageBackToDashBoard();
            //}
            switch (gobackhide) {
                case 0:
                    PromptSaveChanges();
                    if (gobackhide === 1) {
                        HideUserMoresetting();
                        gobackhide = 0;
                    }
                    else if (gobackhide === 2) {
                        ShowUserMoresetting();
                        gobackhide = 0;
                    }
                    break;
                case 1:
                    PromptSaveChanges();
                    gobackhide = 0;
                    break;
                case 2:

                    $('.divcontentBottomLeft').show();
                    $('.secgridhJDNew').show();
                    $('.secpart1').show();
                    $('.secpart2').hide();
                    $('.secpart3').hide();
                    $(".secGridview").hide();
                    $(".secGridviewsplit2").hide();
                    $(".secAcceptChanges").hide();
                    $('.bttmsecRightbtns').hide();


                    //$('.divcontentBottomLeft').show();
                    //$('.secpart2').show();
                    //$('.secpart3').hide();
                    //$('.secgridhJDNew').hide();
                    //$('.secpart1').hide();
                    //$('.accessprofile').hide();
                    //$('#btnAssgn').show();
                    //$('#btnTerminals').show();
                    //$('.classprevbtn').hide();
                    //$('.bttmsecRightbtns').css({
                    //    'background-color': '',
                    //    'border': 'none',
                    //    'width': '100%'
                    //});

                    gobackhide = 0;
                    break;
                case 3:
                    PromptSaveChanges();
                    gobackhide = 0;
                    break;
            }
        });

        //$("#btnBack").click(function (e) {
        //    e.preventDefault();

        //    //if (edit_mode) {
        //    //    BackButtonConfirm();
        //    //} else {
        //    //    redirectPageBackToDashBoard();
        //    //}
        //    switch (gobackhide) {
        //        case 0:
        //            if (edit_mode && allowTMKEdit) {
        //                BackButtonConfirm();
        //            } else {
        //                redirectPageBackToDashBoard();
        //            }

        //            //if (gobackhide === 0) {
        //            //    redirectPageBackToDashBoard();

        //            //}else
        //            if (gobackhide === 1) {
        //                HideUserMoresetting();
        //                gobackhide = 0;
        //            }
        //            else if (gobackhide === 2) {
        //                ShowUserMoresetting();
        //                gobackhide = 0;
        //            }
        //            break;
        //        case 1:

        //            gobackhide = 0;
        //            break;
        //        case 2:
        //            $('.divcontentBottomLeft').show();
        //            $('.secpart2').show();
        //            $('.secpart3').hide();
        //            $('.secgridhJDNew').hide();
        //            $('.secpart1').hide();
        //            $('.accessprofile').hide();
        //            $('#btnAssgn').show();
        //            $('#btnTerminals').show();
        //            $('.classprevbtn').hide();
        //            $('.bttmsecRightbtns').css({
        //                'background-color': '',
        //                'border': 'none',
        //                'width': '100%'
        //            });

        //            gobackhide = 0;
        //            break;
        //        case 3:

        //            gobackhide = 0;
        //            break;
        //    }
        //});

        //$('#btnSave').click(function (evt) {
        //    evt.preventDefault();
        //    $('.secgridhJDNew').hide();
        //    $('.secpart3').hide();
        //    $('.accessprofile').hide();
        //    $('#btnAssgn').show();
        //    $('#btnTerminals').show();
        //    $(".classprevbtn").hide();
        //    $('.bttmsecRightbtns').css({
        //        'background-color': '',
        //        'border': 'none',
        //        'width': '100%'
        //    });
        //    save();
        //});

        $('#btnSaveTerminalConfig').on("click", function (e) {
            e.preventDefault();
            if ($("#txtTerminalId").val().trim() === "" || $("#txtTerminalId").val().trim() === "0") {
                getLocalizedText("noSelection");
                alert(levelCaption);
            } else {
                resetConfirmationDiv();
                saveTerminalConfig();
                setTimeout(function () { ClearControls(); }, 1000);
                setTimeout(function () { ClearOtherControls(); }, 1000);
            }
            $(".secpart1").show();
            $(".secpart2").hide();
            $(".secGridview").hide();
            $(".secGridviewsplit2").hide();
            $(".secAcceptChanges").hide();
            $("#btnAddReader").prop("disabled", false);
            var InactiveImgUrl = "/Images/FormImages/Dot_rot.png";
            $('.btnred').css('background-image', 'url(' + InactiveImgUrl + ')');
            setTimeout(function () { SetNewInternReader(); }, 2000);
            grdZkTerminalCon.SetFocusedRowIndex(-1);
            grdZkTerminalCon.UnselectRows();
        });

        //$('#btnDelete').click(function (evt) {
        //    evt.preventDefault();
        //    if ($("#ddlCurrentZkTerminalInstance").val().trim() === "" || $("#ddlCurrentZkTerminalInstance").val().trim() === "0") {
        //        getLocalizedText("noSelection");
        //        alert(levelCaption);
        //    }
        //    else {
        //        getLocalizedText("TerminalInstanceDeleteWarning");
        //        ConfirmDelete(levelCaption);
        //    }
        //});

        $("#txtTerminalId").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#txtDescription").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#txtSerialNumber").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#ddlLinkType").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#txtIpAddress").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#txtIpPort").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#txtInfoText1").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#txtInfoText2").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#txtInfoText3").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#txtInfoText4").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#ddlFunctionKey1").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#ddlFunctionKey2").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#ddlFunctionKey3").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#ddlFunctionKey4").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#ddlFunctionKey5").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#ddlFunctionKey6").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#ddlFunctionKey7").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#ddlFunctionKey8").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#chkbackToReadFingerPrint").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#chkAPPosting").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#chkpersonnelNumberPin").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $("#chkProfFirmware").change(function (e) {
            e.preventDefault();
            edit_mode = true;
        });

        $('#btnDeleteReader').click(function (evt) {
            evt.preventDefault();
            if ($("#ddlCurrentZkTerminalInstance").val().trim() === "" || $("#ddlCurrentZkTerminalInstance").val().trim() === "0") {
                getLocalizedText("noSelection");
                alert(levelCaption);
            }
            else {
                getLocalizedText("deleteReaderInstance");
                ConfirmDeleteReader(levelCaption);
            }
        });

        //$('#btnBackToDashBoard').click(function (evt) {
        //    evt.preventDefault();
        //    var initdto = JSON.parse(sessionStorage.getItem("terminaldto"));
        //    var currdto = RetrieveDataFromClient();

        //    console.log(initdto);
        //    console.log(currdto);

        //    var areObjectsEqual = _.isEqual(initdto, currdto);

        //    console.log(areObjectsEqual);

        //    if (!areObjectsEqual) {
        //        CornfirmSaveDialog();
        //    } else {
        //        redirectPageBackToDashBoard();
        //    }
        //});

        setTimeout(function () {
            var isnew = JSON.parse(sessionStorage.getItem("isnew"));
            if (isnew === 1) {
                ClearControls();
                ClearOtherControls();
                PageMethods.GetNextTerminalConfigNr(SetNextTerminalConfigNr);
                PageMethods.GetTerminalbyType(SetTerminalTypeDescription);
                PageMethods.GetTerminalbyType(SetTerminalTypeId);
                $('#txtTerminalId').removeAttr('disabled');
                $("#txtDescription").focus();
                SetNewInternReader();
                var InactiveImgUrl = "/Images/FormImages/Dot_rot.png";
                $('.btnred').css('background-image', 'url(' + InactiveImgUrl + ')');
                document.getElementById('txtIpPort').value = "4370";
            }
        }, 1000);

        setTimeout(function () { sessionStorage.setItem("isnew", "0"); }, 10000);

        setTimeout(function () {
            PageMethods.GetTerminalbyType(SetTerminalTypeDescription);
            PageMethods.GetTerminalbyType(SetTerminalTypeId);
        }, 1000);

        $("#txtDescription").focus();

        setTimeout(function () {
            PageMethods.GetTerminalbyType(SetTerminalTypeId);
            $(".secgridhJDNew").show();
            $(".secpart1").show();
            $(".secpart2").hide();
            $(".secGridview").hide();
            $(".secGridviewsplit2").hide();
            $(".secAcceptChanges").hide();
        }, 1000);

        suppressNonNumericInput();


    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
});

function SetNextTerminalConfigNr(response) {
    try {
        console.log(response);
        document.getElementById('txtTerminalId').value = response;
        $("#txtDescription").focus();
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function SetTerminalTypeDescription(response) {
    try {
        if (response !== null) {
            if (response.ID !== null && response.ID !== 0) {
                console.log(response);
                document.getElementById('txtTermTyp').value = response.TerminalTypeDescription;
                document.getElementById('hfdtxtTerminalTypeId').value = response.TerminalTypeId;
                $("#txtDescription").focus();
            }
        }
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function SetTerminalTypeId(response) {
    try {
        if (response !== null) {
            if (response.ID !== null && response.ID !== 0) {
                console.log(response);
                document.getElementById('txtTermTyp').value = response.TerminalTypeDescription;
                document.getElementById('hfdtxtTerminalTypeId').value = response.TerminalTypeId;
                $("#txtDescription").focus();
            }
        }
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function PromptSaveChanges() {
    try {
        var initdto = JSON.parse(sessionStorage.getItem("terminaldto"));
        var currdto = RetrieveDataFromClient();

        console.log(initdto);
        console.log(currdto);

        var areObjectsEqual = _.isEqual(initdto, currdto);

        console.log(areObjectsEqual);

        if (!areObjectsEqual) {
            CornfirmSaveDialog();
        } else {
            redirectPageBackToDashBoard();
        }
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function saveTerminalConfig() {
    try {
        if (document.getElementById("txtTerminalId").value.length < 1) { return; }

        var dto = new Object();
        dto.TermType = document.getElementById("txtTermTyp").value;
        dto.TerminalId = document.getElementById("hfdtxtTerminalTypeId").value;
        dto.TermID = document.getElementById("txtTerminalId").value;
        dto.Description = document.getElementById("txtDescription").value;
        dto.SerialNumber = document.getElementById("txtSerialNumber").value;
        dto.IpAddress = document.getElementById("txtIpAddress").value;
        dto.Port = document.getElementById("txtIpPort").value;
        dto.ConnectionType = cboLinkType.GetValue();
        dto.IsActive = document.getElementById("chkStatus").checked;

        dto.HasAPPosting = document.getElementById("chkUseAccessControl").checked;
        dto.HasPersNoPin = document.getElementById("chkpersonnelNumberPin").checked;
        dto.HasProfFirmware = document.getElementById("chkProfFirmware").checked;

        dto.InfoText1 = document.getElementById("txtInfoText1").value;
        dto.InfoText2 = document.getElementById("txtInfoText2").value;
        dto.InfoText3 = document.getElementById("txtInfoText3").value;
        dto.InfoText4 = document.getElementById("txtInfoText4").value;

        dto.FunctionKeyText1 = cbofunctionKey1.GetValue();
        dto.FunctionKeyText2 = cbofunctionKey2.GetValue();
        dto.FunctionKeyText3 = cbofunctionKey3.GetValue();
        dto.FunctionKeyText4 = cbofunctionKey4.GetValue();
        dto.FunctionKeyText5 = cbofunctionKey5.GetValue();
        dto.FunctionKeyText6 = cbofunctionKey6.GetValue();
        dto.FunctionKeyText7 = cbofunctionKey7.GetValue();
        dto.FunctionKeyText8 = cbofunctionKey8.GetValue();

        dto.internReaderDesc = $("#txtTIDescription").val();
        dto.externReaderDesc = $("#txtExDescription").val();

        if ($("#ddlTIDirection option:selected").val() === undefined) {
            dto.internDirec = 0;
        }
        else {
            dto.internDirec = $("#ddlTIDirection option:selected").val();
        }

        if ($("#ddlExDirection option:selected").val() === undefined) {
            dto.externDirec = 0;
        }
        else {
            dto.externDirec = $("#ddlExDirection option:selected").val();
        }

        dto.relayTime = $("#txtTIRelayTime").val();
        dto.readerExternId = $("#lblExId").text();
        dto.readerInternId = $("#lblTIId").text();
        dto.readerTypeExtern = $("#lblExLaserType").text();
        dto.readerStaticId = readerStaticId;

        StoreDataFromClientInSSession();

        var exists = false;
        if (dto.TermID !== "" || dto.TermID !== null || dto.TermID !== undefined) {
            for (var i = 0; i < grdZkTerminalCon.pageRowCount; i++) {
                var existingTermID = grdZkTerminalCon.GetRow(i).cells[0].childNodes[0].textContent;
                if (existingTermID === dto.TermID) {
                    exists = true;
                }
            }
        }

        if (exists) {
            PageMethods.UpdateTerminalConfig(dto, SettingComboCallBacks, function (err) { console.log(err) });
            PageMethods.GetTerminalConfigbyTermID(dto.TermID, ClearControls);
        } else {
            PageMethods.InsertTerminalConfig(dto, SettingComboCallBacks, function (err) { console.log(err) });
            PageMethods.GetTerminalConfigbyTermID(dto.TermID, ClearControls);
        }
    } catch (e) {
        ShowErrorNotificationBar(e);
    }
}

function saveTerminalConfigInfoText(response) {
    try {
        if (response !== null) {
            if (response.ID !== null && response.ID !== 0) {

                var dto = new Object();
                dto.InfoText1 = document.getElementById("txtInfoText1").value;
                dto.InfoText2 = document.getElementById("txtInfoText2").value;
                dto.InfoText3 = document.getElementById("txtInfoText3").value;
                dto.InfoText4 = document.getElementById("txtInfoText4").value;
                dto.TerminalConfigID = response.ID;

                PageMethods.InsertTerminalConfigInfoText(dto, function (err) { console.log(err) });
            }
        }
        saveTerminalConfigFunctionKeys(response);
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function saveTerminalConfigFunctionKeys(response) {
    try {
        if (response !== null) {
            if (response.ID !== null && response.ID !== 0) {

                var dto = new Object();
                dto.FunctionKeyNr1 = cbofunctionKey1.GetValue();
                dto.FunctionKeyNr2 = cbofunctionKey2.GetValue();
                dto.FunctionKeyNr3 = cbofunctionKey3.GetValue();
                dto.FunctionKeyNr4 = cbofunctionKey4.GetValue();
                dto.FunctionKeyNr5 = cbofunctionKey5.GetValue();
                dto.FunctionKeyNr6 = cbofunctionKey6.GetValue();
                dto.FunctionKeyNr7 = cbofunctionKey7.GetValue();
                dto.FunctionKeyNr8 = cbofunctionKey8.GetValue();
                dto.TerminalConfigID = response.ID;

                PageMethods.InsertTerminalConfigFunctionKeys(dto, function (err) { console.log(err) });
            }
        }
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function saveTerminalConfigUtilities(response) {
    try {
        if (response !== null) {
            if (response.ID !== null && response.ID !== 0) {

                var dto = new Object();
                dto.HasAPPosting = document.getElementById("chkUseAccessControl").checked;
                dto.HasPersNoPin = document.getElementById("chkpersonnelNumberPin").checked;
                dto.HasProfFirmware = document.getElementById("chkProfFirmware").checked;
                dto.TerminalConfigID = response.ID;

                PageMethods.InsertTerminalConfigUtilities(dto, function (err) { console.log(err) });
            }
        }
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function redirectPageBackToDashBoard() {
    try {
        window.location.href = "/Content/ZkTerminalsDashBoard.aspx";
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function RetrieveDataFromClient() {
    try {
        var dto = new Object();
        dto.TerminalId = document.getElementById("txtTerminalId").value;
        dto.TermID = document.getElementById("txtTermTyp").value;
        dto.Description = document.getElementById("txtDescription").value;
        dto.SerialNumber = document.getElementById("txtSerialNumber").value;
        dto.IpAddress = document.getElementById("txtIpAddress").value;
        dto.Port = document.getElementById("txtIpPort").value;
        dto.ConnectionType = cboLinkType.GetValue();
        dto.IsActive = document.getElementById("chkStatus").checked;

        return dto;
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function StoreDataFromClientInSSession() {
    try {
        var dto = new Object();
        dto.TerminalId = document.getElementById("txtTerminalId").value;
        dto.TermID = document.getElementById("txtTermTyp").value;
        dto.Description = document.getElementById("txtDescription").value;
        dto.SerialNumber = document.getElementById("txtSerialNumber").value;
        dto.IpAddress = document.getElementById("txtIpAddress").value;
        dto.Port = document.getElementById("txtIpPort").value;
        dto.ConnectionType = cboLinkType.GetValue();
        dto.IsActive = document.getElementById("chkStatus").checked;

        sessionStorage.setItem("terminaldto", JSON.stringify(dto));

        return dto;
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function resetConfirmationDiv() {
    try {
        document.getElementById('confirmDelete').innerHTML = "";
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function CornfirmSaveDialog() {
    try {
        getLocalizedText("saveChangesConfirmation");
        var message = levelCaption;
        var box_content = '<div id="overlay"><div id="box_flame"><div id="dialogBox"><div id="prompttopclose"><label id="lbltitletop">Zeiterfassung</label><button id="promptbtnclose" onclick="resetConfirmationDiv()" /></div><div id="areasavepop"><img src="../../Images/FormImages/stop-save2-02.png" alt="Stop" class="stopPic" height="150" width="150" align="middle"><div id="lbltitlemsg">' + message + '</div><button id="promptButtonok" style="margin-left: 32%; color: forestgreen !important;" onclick="SaveOnBackButton()"></button><button id="btnNo" onclick="CancelOnBackButton()" style="margin-top: 1px; width: 196px;" ></button><button id="btnCancel"  onclick="resetConfirmationDiv()"></button></div></div></div></div>';
        document.getElementById('confirmDelete').innerHTML = box_content;
        getLocalizedText("save_dialong");
        $('#promptButtonok').text(levelCaption);
        getLocalizedText("no_new");
        $('#btnNo').text(levelCaption);
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function ConfirmDeleteDialog(message) {
    try {
        getLocalizedText("deleteprompttm680bc");
        var message = levelCaption;
        var box_content = '<div id="overlay"><div id="box_flame"><div id="dialogBox"><div id="prompttopclose"><label id="lbltitletop">Zeiterfassung</label><button id="promptbtnclose" onclick="CancelOnDeleteButton(); return false;" /></div><div id="areasavepop"><img src="../../Images/FormImages/stop-save1-01.png" alt="Stop" class="stopPic" height="150" width="150" align="middle"><div id="lbltitlemsg">' + message + '</div><button id="btnOk" style="margin-left: 33%; margin-right: 0px; color: red !important;" onclick="Delete()"></button><button id="btnCancel" onclick="CancelOnDeleteButton(); return false;" style="position: relative; color: forestgreen !important;"></button></div></div></div></div>';
        document.getElementById("importantInfoDialog").innerHTML = box_content;
        getLocalizedText("acceptdeletetm680bc");
        $("#btnOk").text(levelCaption);
        getLocalizedText("no");
        $("#btnNoDelete").text(levelCaption);
        getLocalizedText("canceldeletetm680bc");
        $("#btnCancel").text(levelCaption);
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function ConfirmDeleteReaderDialog(message) {
    try {
        getLocalizedText("deleteprompttm680bc");
        var message = levelCaption;
        var box_content = '<div id="overlay"><div id="box_flame"><div id="dialogBox"><div id="prompttopclose"><label id="lbltitletop">Zeiterfassung</label><button id="promptbtnclose" onclick="CancelOnDeleteButton(); return false;" /></div><div id="areasavepop"><img src="../../Images/FormImages/stop-save1-01.png" alt="Stop" class="stopPic" height="150" width="150" align="middle"><div id="lbltitlemsg">' + message + '</div><button id="btnOk" style="margin-left: 33%; margin-right: 0px; color: red !important;" onclick="DeleteReader()"></button><button id="btnCancel" onclick="CancelOnDeleteButton(); return false;" style="position: relative; color: forestgreen !important;"></button></div></div></div></div>';
        document.getElementById("importantInfoDialog").innerHTML = box_content;
        getLocalizedText("acceptdeletetm680bc");
        $("#btnOk").text(levelCaption);
        getLocalizedText("no");
        $("#btnNoDelete").text(levelCaption);
        getLocalizedText("canceldeletetm680bc");
        $("#btnCancel").text(levelCaption);
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function ImportantInfoPromptDialog(message) {
    try {
        getLocalizedText("deleteprompttm680bc");
        var message = levelCaption;
        var box_content = '<div id="overlay"><div id="box_flame"><div id="dialogBox"><div id="prompttopclose"><label id="lbltitletop">Zeiterfassung</label><button id="promptbtnclose" onclick="CancelOnDeleteButton(); return false;" /></div><div id="areasavepop"><img src="../../Images/FormImages/FormImages/greeninfo-01.png" alt="Stop" class="stopPic" height="150" width="150" align="middle"><div id="lbltitlemsg">' + message + '</div><button id="btnOk" style="margin-left: 33%; margin-right: 0px; color: red !important;" onclick="DeleteReader()"></button><button id="btnCancel" onclick="CancelOnDeleteButton(); return false;" style="position: relative; color: forestgreen !important;"></button></div></div></div></div>';
        document.getElementById("importantInfoDialog").innerHTML = box_content;
        getLocalizedText("acceptdeletetm680bc");
        $("#btnOk").text(levelCaption);
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function Delete() {
    try {
        var selectedTerminalId = document.getElementById("txtTerminalId").value;
        PageMethods.DeleteSelectedTerminalConfig(selectedTerminalId, ReloadPage);
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function DeleteReader() {
    try {
        document.getElementById('confirmDelete').innerHTML = "";
        $("#btnAddReader").prop("disabled", false);
        var terminalType = document.getElementById('hfdtxtTerminalTypeId').value;
        if (terminalType === null) {
            $("#lblExId").text("");
            $("#lblExInfo").text("");
            $("#lblExLaserType").text("");
            $("#txtExDescription").val("");
            $("#ddlExDirection").val("0").attr("selected", "selected");
            $("#btnDeleteReader").prop("disabled", true);
            $("#txtExDescription").prop("readonly", true);
        }
        else {
            PageMethods.DeleteSelectedTerminalReaderInstance(terminalType, OnDeleteReader_CallBack);
        }
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function OnDeleteReader_CallBack() {
    try {
        $("#lblExId").text("");
        $("#lblExInfo").text("");
        $("#lblExLaserType").text("");
        $("#txtExDescription").val("");
        $("#ddlExDirection").val("0").attr("selected", "selected");
        $("#btnDeleteReader").prop("disabled", true);
        $("#txtExDescription").prop("readonly", true);

    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function ReloadPage() {
    try {
        window.location = window.location;
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function SaveOnBackButton() {
    try {
        resetConfirmationDiv();
        saveTerminalConfig();
        redirectPageBackToDashBoard();
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function CancelOnBackButton() {
    try {
        resetConfirmationDiv();
        redirectPageBackToDashBoard();
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function CancelOnDeleteButton() {
    try {
        resetImportantInfoDialogDiv();
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function getLocalizedText(key) {
    try {
        var data = { key: key };
        $.ajax({
            type: "POST",
            async: false,
            url: "TM680bc.aspx/GetLocalizedText",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success: function (result) {
                levelCaption = result.d;
            }
        });
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function SetControls(response) {
    try {
        ClearControlsNoRecord();

        if (response !== null) {

            if (response.ID !== null && response.ID !== 0) {

                cboLinkType.SetValue(response.ConnectionType);

                document.getElementById('txtTermTyp').value = response.TerminalId;
                document.getElementById('txtTerminalId').value = response.TermID;
                document.getElementById('txtDescription').value = response.Description;
                document.getElementById('txtSerialNumber').value = response.SerialNumber;
                document.getElementById('txtIpAddress').value = response.IpAddress;
                document.getElementById('txtIpPort').value = response.Port;
                document.getElementById("chkStatus").checked = response.IsActive;

                if (response.readerInternId === 0) {
                    $("#lblTIId").text("");
                }
                else {
                    $("#lblTIId").text(response.readerInternId);
                }
                $("#lblTIInfo").text(response.readerInfoIntern);
                $("#lblTILaserType").text(response.readerTypeIntern);
                $("#txtTIDescription").val(response.internReaderDesc);
                $("#ddlTIDirection").val(response.internDirec).attr("selected", "selected");
                $("#lblTIStatus").text(response.readerActive);
                $("#txtTIRelayTime").val(response.relayTime);

                if (response.readerExternId === 0) {
                    $("#lblExId").text("");
                }
                else {
                    $("#lblExId").text(response.readerExternId);
                }
                $("#lblExInfo").text(response.readerInfoExtern);
                $("#lblExLaserType").text(response.readerTypeExtern);
                $("#txtExDescription").val(response.externReaderDesc);
                $("#ddlExDirection").val(response.externDirec).attr("selected", "selected");

                document.getElementById("chkUseAccessControl").checked = response.HasAPPosting;
                document.getElementById("chkpersonnelNumberPin").checked = response.HasPersNoPin;
                document.getElementById("chkProfFirmware").checked = response.HasProfFirmware;

                document.getElementById("txtInfoText1").value = response.InfoText1;
                document.getElementById("txtInfoText2").value = response.InfoText2;
                document.getElementById("txtInfoText3").value = response.InfoText3;
                document.getElementById("txtInfoText4").value = response.InfoText4;

                if (response.FunctionKeyText1 !== null) cbofunctionKey1.SetValue(response.FunctionKeyText1.toString());
                if (response.FunctionKeyText2 !== null) cbofunctionKey2.SetValue(response.FunctionKeyText2.toString());
                if (response.FunctionKeyText3 !== null) cbofunctionKey3.SetValue(response.FunctionKeyText3.toString());
                if (response.FunctionKeyText4 !== null) cbofunctionKey4.SetValue(response.FunctionKeyText4.toString());
                if (response.FunctionKeyText5 !== null) cbofunctionKey5.SetValue(response.FunctionKeyText5.toString());
                if (response.FunctionKeyText6 !== null) cbofunctionKey6.SetValue(response.FunctionKeyText6.toString());
                if (response.FunctionKeyText7 !== null) cbofunctionKey7.SetValue(response.FunctionKeyText7.toString());
                if (response.FunctionKeyText8 !== null) cbofunctionKey8.SetValue(response.FunctionKeyText8.toString());

                var ActiveImgUrl = "/Images/FormImages/Dot_gruen.png";
                var InactiveImgUrl = "/Images/FormImages/Dot_rot.png";

                if (response.IsActive === true) {
                    $('.btnblue').css('background-image', 'url(' + ActiveImgUrl + ')');
                    $("#lblTIStatus").css("color", "green");
                }
                if (response.IsActive === false) {
                    $('.btnblue').css('background-image', 'url(' + InactiveImgUrl + ')');
                    $("#lblTIStatus").css("color", "red");
                }

                $("#lblTIId").text(response.readerInternId);
                $("#lblTIInfo").text(response.readerInfoIntern);
                $("#lblTILaserType").text(response.readerTypeIntern);
                $("#txtTIDescription").val(response.internReaderDesc);
                $("#ddlTIDirection").val(response.internDirec).attr("selected", "selected");
                $("#lblTIStatus").text(response.readerActive);
                $("#txtTIRelayTime").val(response.relayTime);
                if (response.readerExternId === 0) {
                    $("#lblExId").text("");
                }
                else {
                    $("#lblExId").text(response.readerExternId);
                }

                $("#lblExInfo").text(response.readerInfoExtern);
                $("#lblExLaserType").text(response.readerTypeExtern);
                $("#txtExDescription").val(response.externReaderDesc);
                $("#ddlExDirection").val(response.externDirec).attr("selected", "selected");

                grdZkTerminalCon.PerformCallback();

            }
        }
        StoreDataFromClientInSSession();
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function ClearControls() {
    try {
        cboLinkType.SetValue("0");

        document.getElementById('txtTermTyp').value = "";
        document.getElementById('txtTerminalId').value = "";
        document.getElementById('txtDescription').value = "";
        document.getElementById('txtSerialNumber').value = "";
        document.getElementById('txtIpAddress').value = "";
        document.getElementById('txtIpPort').value = "";
        document.getElementById("chkStatus").checked = false;

        StoreDataFromClientInSSession();
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function ClearOtherControls() {
    try {
        $("#hiddenFieldSaveChanges").attr("value", "0");
        cboLinkType.SetValue("0");

        document.getElementById('txtTermTyp').value = "";
        document.getElementById('txtTerminalId').value = "";
        document.getElementById('txtDescription').value = "";
        document.getElementById('txtSerialNumber').value = "";
        document.getElementById('txtIpAddress').value = "";
        document.getElementById('txtIpPort').value = "";
        document.getElementById("chkStatus").checked = false;

        document.getElementById("chkUseAccessControl").checked = false;
        document.getElementById("chkpersonnelNumberPin").checked = false;
        document.getElementById("chkProfFirmware").checked = false;

        cbofunctionKey1.SetValue("0");
        cbofunctionKey2.SetValue("0");
        cbofunctionKey3.SetValue("0");
        cbofunctionKey4.SetValue("0");
        cbofunctionKey5.SetValue("0");
        cbofunctionKey6.SetValue("0");
        cbofunctionKey7.SetValue("0");
        cbofunctionKey8.SetValue("0");

        document.getElementById("txtInfoText1").value = "";
        document.getElementById("txtInfoText2").value = "";
        document.getElementById("txtInfoText3").value = "";
        document.getElementById("txtInfoText4").value = "";

        $("#lblExId").text("");
        $("#lblExInfo").text("");
        $("#lblExLaserType").text("");
        $("#txtExDescription").val("");
        $("#ddlExDirection").val("0").attr("selected", "selected");
        $("#btnDeleteReader").prop("disabled", true);
        $("#txtExDescription").prop("readonly", false);

        $("#lblTIId").text("");
        $("#lblTIInfo").text("");
        $("#lblTILaserType").text("");
        $("#txtTIDescription").val("");
        $("#ddlTIDirection").val("1").attr("selected", "selected");
        $("#lblTIStatus").text("Inacktiv");
        $("#txtTIRelayTime").val("5");
        $("#txtTIDescription").prop("readonly", false);

        StoreDataFromClientInSSession();
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function ClearControlsNoRecord() {
    try {
        cboLinkType.SetValue("0");

        document.getElementById('txtTermTyp').value = "";
        document.getElementById('txtTerminalId').value = "";
        document.getElementById('txtDescription').value = "";
        document.getElementById('txtSerialNumber').value = "";
        document.getElementById('txtIpAddress').value = "";
        document.getElementById('txtIpPort').value = "";
        document.getElementById("chkStatus").checked = false;
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function grdZkTerminalConRowClick(s, e) {
    try {
        var index = e.visibleIndex;
        if (index > -1) {
            var TermID = grdZkTerminalCon.GetRow(index).cells[0].textContent.trim();
            SetValuesGivenTerminalId(TermID);
        }
        $("#btnSave").attr("disabled", false);
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function SetValuesGivenTerminalId(TermID) {
    try {
        PageMethods.GetTerminalConfigbyTermID(TermID, SetControls);
        PageMethods.GetTerminalConfigbyTermID(TermID, SettingComboCallBacks);
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function SettingComboCallBacks(response) {
    try {
        grdZkTerminalCon.PerformCallback("0");
        if (response !== null) {
            if (response.ID !== null && response.ID !== 0) {
                grdZkTerminalCon.PerformCallback(response.ID.toString());
            }
        }
    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

function suppressNonNumericInput() {
    try {
        $("#txtIpPort").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A
                            (e.keyCode === 65 && e.ctrlKey === true) ||
                // Allow: Ctrl+C
                                    (e.keyCode === 67 && e.ctrlKey === true) ||
                // Allow: Ctrl+X
                                            (e.keyCode === 88 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                                                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#txtTerminalId").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A
                            (e.keyCode === 65 && e.ctrlKey === true) ||
                // Allow: Ctrl+C
                                    (e.keyCode === 67 && e.ctrlKey === true) ||
                // Allow: Ctrl+X
                                            (e.keyCode === 88 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                                                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#txtIpPort").on("keydown keyup click input submit mouseenter change paste", function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A
                            (e.keyCode === 65 && e.ctrlKey === true) ||
                // Allow: Ctrl+C
                                    (e.keyCode === 67 && e.ctrlKey === true) ||
                // Allow: Ctrl+X
                                            (e.keyCode === 88 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                                                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        $("#txtTerminalId").on("keydown keyup click input submit mouseenter change paste", function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A
                            (e.keyCode === 65 && e.ctrlKey === true) ||
                // Allow: Ctrl+C
                                    (e.keyCode === 67 && e.ctrlKey === true) ||
                // Allow: Ctrl+X
                                            (e.keyCode === 88 && e.ctrlKey === true) ||
                // Allow: home, end, left, right
                                                    (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

    } catch (e) {
        console.log(e);
        ShowErrorNotificationBar(e);
    }
}

















function showLoadingPanel() {
    LoadingPanel.Show();
}

function hideLoadingPanel() {
    LoadingPanel.Hide();
}

function doTaskAfterCommunicationAction(messageText) {
    hideLoadingPanel();
    alert(messageText);
}

function checkForNewRedirectMode() {
    edit_mode = true;
    SetControlsOnNew();
    $("#ddlCurrentZkTerminalInstance").val("");
    var index = grdZkTerminals.GetFocusedRowIndex();
    var terminalType = grdZkTerminals.GetRow(index).cells[0].childNodes[0].textContent;
    $("#txtTerminalType").val(terminalType);
    SetNewInternReader();
    SetNextUserTerminalId();

    //if (_aspxGetCookie("TermKonfigZk") === "0") {
    //    $('#btnAssgn').css('color', 'red');
    //    $('#btnTerminals').css('color', 'black');
    //    $('#btnMoreFunctions').css('color', 'black');
    //    _secAuftrage = $(".secpart1")
    //    if (_secAuftrage.css("display") === "none") {
    //        $(".secpart, .secpart2").hide();
    //        _secAuftrage.show();
    //    }
    //}
    //if (_aspxGetCookie("TermKonfigZk") === "1") {
    //    $('#btnTerminals').css('color', 'red');
    //    $('#btnAssgn').css('color', 'black');
    //    $('#btnMoreFunctions').css('color', 'black');
    //    _secAuftrage = $(".secpart2")
    //    if (_secAuftrage.css("display") === "none") {
    //        $(".secpart1, .secpart").hide();
    //        _secAuftrage.show();
    //    }
    //}
}

function HideUserZkTerminals() {
    $('#btnTerminals').css('color', 'red');
    $('#btnAssgn').css('color', 'black');
    $('#btnMoreFunctions').css('color', 'black');

    _secAuftrage = $(".secpart2")

    if (_secAuftrage.css("display") === "none") {
        $(".secpart1, .secpart").hide();
        _secAuftrage.show();
    }
    //_aspxDelCookie("TermKonfigZk");
    //_aspxSetCookie("TermKonfigZk", "1", (moment(Date(Date.now)).add('hour', 1))["_d"]);
}

function ShowUserZkTerminals() {
    $('#btnAssgn').css('color', 'red');
    $('#btnTerminals').css('color', 'black');
    $('#btnMoreFunctions').css('color', 'black');

    _secAuftrage = $(".secpart1")

    if (_secAuftrage.css("display") === "none") {
        $(".secpart, .secpart2").hide();
        _secAuftrage.show();
    }
    //_aspxDelCookie("TermKonfigZk");
    //_aspxSetCookie("TermKonfigZk", "0", (moment(Date(Date.now)).add('hour', 1))["_d"]);
}

//function getLocalizedText(key) {
//    var data = { key: key };
//    $.ajax({
//        type: "POST",
//        async: false,
//        url: "TM680bc.aspx/GetLocalizedText",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        data: JSON.stringify(data),
//        success: function (result) {
//            levelCaption = result.d;
//        }
//    });
//}

function ConfirmDelete(message) {
    var box_content = '<div id="overlay"><div id="box_flame"><div id="dialogBox">  <img src="../../Images/FormImages/stop-save1-01.png" alt="Stop" class="stopPic" height="150" width="150" align="middle"> <br/>' + message + '<br/> <button id="btnOk"  onclick="DeleteTerminal()"></button><button id="btnNo"  onclick="CancelDelete()"></button><button id="btnCancel"  onclick="CancelDelete()"></button></div></div></div>';
    document.getElementById('confirmDelete').innerHTML = box_content;
    getLocalizedText("yes");
    $('#btnOk').text(levelCaption);
    getLocalizedText("no");
    $('#btnNo').text(levelCaption);
    getLocalizedText("cancel");
    $('#btnCancel').text(levelCaption);
}

var selectval;
function DeleteTerminal() {
    resetConfirmationDiv();
    selectval = $("#ddlCurrentZkTerminalInstance").val();
    var index = grdZkTerminals.GetFocusedRowIndex();
    var terminalType = grdZkTerminals.GetRow(index).cells[0].childNodes[0].textContent;
    PageMethods.GetTerminalByTypeName(terminalType, GetTerminalNewIdByType_DeleteCallback, OnFailGetTerminalNewIdByType_Callback);
}

function ConfirmDeleteReader(message) {
    var box_content = '<div id="overlay"><div id="box_flame"><div id="dialogBox">  <img src="../../Images/FormImages/stop-save1-01.png" alt="Stop" class="stopPic" height="150" width="150" align="middle"> <br/>' + message + '<br/> <button id="btnOk"  onclick="DeleteTerminalReader()"></button><button id="btnNo"  onclick="CancelDelete()"></button><button id="btnCancel"  onclick="CancelDelete()"></button></div></div></div>';
    document.getElementById('confirmDelete').innerHTML = box_content;
    getLocalizedText("yes");
    $('#btnOk').text(levelCaption);
    getLocalizedText("no");
    $('#btnNo').text(levelCaption);
    getLocalizedText("cancel");
    $('#btnCancel').text(levelCaption);
}

function DeleteTerminalReader() {
    document.getElementById('confirmDelete').innerHTML = "";
    PageMethods.DeleteTerminalReaderInstance($("#ddlCurrentZkTerminalInstance").val(), OnDeleteReader_CallBack);
}

function OnDeleteReader_CallBack() {
    $("#lblExId").text("");
    $("#lblExInfo").text("");
    $("#lblExLaserType").text("");
    $("#txtExDescription").val("");
    $("#ddlExDirection").val("1").attr("selected", "selected");

}

function CancelDelete() {
    resetConfirmationDiv();
}

function SaveOnBackButton() {
    resetConfirmationDiv();
    save();
    //redirectPageBackToDashBoard();
    setTimeout(function () { redirectPageBackToDashBoard() }, 2000);
}

function RedirectBackWithoutSaving() {
    resetConfirmationDiv();
    redirectPageBackToDashBoard();
}

function redirectPageBackToDashBoard() {
    var url = "/index.aspx";
    window.location.replace(url);
}

function CancelOnBackButton() {
    resetConfirmationDiv();
}

function resetConfirmationDiv() {
    document.getElementById('confirmDelete').innerHTML = "";
}

function GetTerminalNewIdByType_DeleteCallback(response) {
    if (selectval != 0) {
        terminalNewId = response.ID;
        PageMethods.DeleteCurrentZkTerminalInstance(selectval, terminalNewId, DeleteCurrentZkTerminalInstance_Callback, OnDeleteTerminalInstanceFail_Callback);
    }
}

function OnFailGetTerminalNewIdByType_Callback() {

}

function DeleteCurrentZkTerminalInstance_Callback(response) {
    if (response === undefined || response === null || response.length === 0) {
        SetControlsOnNew();
        ClearReaderControls();
        grdZkTerminalCon.PerformCallback();
    }
    else {
        $("#ddlCurrentZkTerminalInstance option").remove();
        $("#ddlCurrentZkTerminalInstance").append($('<option></option>').val(response[0].ID).html(response[0].ID));
        $("#ddlCurrentZkTerminalInstance").val(response[0].ID).attr("selected", "selected");

        $("#ddlTerminalType").append($('<option></option>').val(response[0].TerminalId).html(response[0].TermType));
        $("#ddlTerminalType").val(response[0].TerminalId).attr("selected", "selected");
        $("#txtTerminalId").val(response[0].TermID);
        $("#txtDescription").val(response[0].Description);
        $("#txtSerialNumber").val(response[0].SerialNumber);
        if (response[0].ZkRelayTime === -1) {
            $("#txtRelaySwitching").val("");
        } else {
            $("#txtRelaySwitching").val(response[0].ZkRelayTime);
        }
        if (response[0].ConnectionType === "keine") {
            $("#ddlLinkType option").remove();
            $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
            $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
            $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
            $("#ddlLinkType").val("0").attr("selected", "selected");
        }
        if (response[0].ConnectionType === "None") {
            $("#ddlLinkType option").remove();
            $("#ddlLinkType").append($('<option></option>').val(0).html("None"));
            $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
            $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
            $("#ddlLinkType").val("0").attr("selected", "selected");
        }
        if (response[0].ConnectionType === "TCP/IP") {
            $("#ddlLinkType option").remove();
            $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
            $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
            $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
            $("#ddlLinkType").val("1").attr("selected", "selected");
        }
        if (response[0].ConnectionType === "USB") {
            $("#ddlLinkType option").remove();
            $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
            $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
            $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
            $("#ddlLinkType").val("2").attr("selected", "selected");
        }
        $("#txtIpAddress").val(response[0].IpAddress);
        if (response[0].Port === -1) {
            $("#txtIpPort").val("");
        } else {
            $("#txtIpPort").val(response[0].Port);
        }
        if (response[0].HasFPRead === true) {
            $('#chkbackToReadFingerPrint').prop('checked', true);
        } else {
            $('#chkbackToReadFingerPrint').prop('checked', false);
        }
        if (response[0].HasAPPosting === true) {
            $('#chkAPPosting').prop('checked', true);
        } else {
            $('#chkAPPosting').prop('checked', false);
        }
        if (response.HasPersNoPin === true) {
            $('#chkpersonnelNumberPin').prop('checked', true);
        } else {
            $('#chkpersonnelNumberPin').prop('checked', false);
        }
        if (response.HasProfFirmware === true) {
            $('#chkProfFirmware').prop('checked', true);
        } else {
            $('#chkProfFirmware').prop('checked', false);
        }
        $("#txtInfoText1").val(response[0].InfoText1);
        $("#txtInfoText2").val(response[0].InfoText2);
        $("#txtInfoText3").val(response[0].InfoText3);
        $("#txtInfoText4").val(response[0].InfoText4);

        $("#ddlFunctionKey1").val(response[0].FunctionKeyText1);
        $("#ddlFunctionKey2").val(response[0].FunctionKeyText2);
        $("#ddlFunctionKey3").val(response[0].FunctionKeyText3);
        $("#ddlFunctionKey4").val(response[0].FunctionKeyText4);
        $("#ddlFunctionKey5").val(response[0].FunctionKeyText5);
        $("#ddlFunctionKey6").val(response[0].FunctionKeyText6);
        $("#ddlFunctionKey7").val(response[0].FunctionKeyText7);
        $("#ddlFunctionKey8").val(response[0].FunctionKeyText8);

        if (response[0].readerInternId === 0) {
            $("#lblTIId").text("");
        }
        else {
            $("#lblTIId").text(response[0].readerInternId);
        }
        $("#lblTIInfo").text(response[0].readerInfoIntern);
        $("#lblTILaserType").text(response[0].readerTypeIntern);
        $("#txtTIDescription").val(response[0].internReaderDesc);
        $("#ddlTIDirection").val(response[0].internDirec).attr("selected", "selected");
        $("#lblTIStatus").text(response[0].readerActive);
        $("#txtTIRelayTime").val(response[0].relayTime);

        if (response[0].readerInternId === 0) {
            $("#lblExId").text("");
        }
        else {
            $("#lblExId").text("1a");
        }
        //$("#lblExInfo").text(response[0].readerInfoExtern);
        $("#lblExLaserType").text(response[0].readerTypeIntern_2);
        //$("#txtExDescription").val(response[0].externReaderDesc);
        //$("#ddlExDirection").val(response[0].externDirec).attr("selected", "selected");

        grdZkTerminalCon.PerformCallback();
    }
}

function OnDeleteTerminalInstanceFail_Callback(response) {

}

function save() {
    edit_mode = false;

    if ($("#chkStatus")[0].checked === true) {
        IsActiveTerminal = true;;
    }
    else if ($("#chkStatus")[0].checked === false) {
        IsActiveTerminal = false;
    }

    var selectedTerminalID = $("#ddlTerminalType option:selected")[0].value;
    var ddlTerminalType = $("#ddlTerminalType option:selected")[0].innerHTML;
    var txtUserTerminalId = $("#txtTerminalId").val();
    var txtDescription = $("#txtDescription").val();
    var txtSerialNumber = $("#txtSerialNumber").val();
    var ddlLinkType = $("#ddlLinkType option:selected")[0] === null ? "TCP/IP" : $("#ddlLinkType option:selected")[0].innerHTML;
    var txtIpAddress = $("#txtIpAddress").val();
    var txtIpPort = isNaN(parseInt($("#txtIpPort").val())) ? -1 : parseInt($("#txtIpPort").val());
    var txtRelaySwitching = isNaN(parseInt($("#txtRelaySwitching").val())) ? -1 : parseInt($("#txtRelaySwitching").val());
    var chkbackToReadFingerPrint = $('#chkbackToReadFingerPrint')[0].checked;
    var chkAPPosting = $('#chkAPPosting')[0].checked;
    var chkProfFirmware = $('#chkProfFirmware')[0].checked;
    var chkpersonnelNumberPin = $('#chkpersonnelNumberPin')[0].checked;

    var ddlCurrentZkTerminalInstance = $("#ddlCurrentZkTerminalInstance").val();

    var txtInfoText1 = $("#txtInfoText1").val();
    var txtInfoText2 = $("#txtInfoText2").val();
    var txtInfoText3 = $("#txtInfoText3").val();
    var txtInfoText4 = $("#txtInfoText4").val();

    var ddlFunctionKey1 = $("#ddlFunctionKey1").val();
    var ddlFunctionKey2 = $("#ddlFunctionKey2").val();
    var ddlFunctionKey3 = $("#ddlFunctionKey3").val();
    var ddlFunctionKey4 = $("#ddlFunctionKey4").val();
    var ddlFunctionKey5 = $("#ddlFunctionKey5").val();
    var ddlFunctionKey6 = $("#ddlFunctionKey6").val();
    var ddlFunctionKey7 = $("#ddlFunctionKey7").val();
    var ddlFunctionKey8 = $("#ddlFunctionKey8").val();
    //changes
    var readerInternDesc = $("#txtTIDescription").val();
    var readerExternDesc = $("#txtExDescription").val();
    var readerInternDirec = $("#ddlTIDirection option:selected").val();
    var readerExternDirec = $("#ddlExDirection option:selected").val();
    var readerRelayTime = $("#txtTIRelayTime").val();
    var externalReaderId = $("#lblExId").text();
    var internReaderId = $("#lblTIId").text();

    if (ddlCurrentZkTerminalInstance === "" || ddlCurrentZkTerminalInstance === null || ddlCurrentZkTerminalInstance === undefined) {
        for (var i = 0; i < grdZkTerminalCon.pageRowCount; i++) {
            var existingTermID = grdZkTerminalCon.GetRow(i).cells[0].childNodes[0].textContent;
            if (existingTermID === txtUserTerminalId) {
                getLocalizedText("duplicateTerminalID");
                ImportantInfoDialogPrompt(levelCaption, levelCaption);
                return;
            }
        }
        PageMethods.CreateZkTerminalInstance(ddlTerminalType, selectedTerminalID, txtUserTerminalId, txtDescription, txtSerialNumber, ddlLinkType,
            txtIpAddress, txtIpPort, chkbackToReadFingerPrint, chkAPPosting, chkProfFirmware, txtInfoText1, txtInfoText2, txtInfoText3, txtInfoText4,
ddlFunctionKey1, ddlFunctionKey2, ddlFunctionKey3, ddlFunctionKey4, ddlFunctionKey5, ddlFunctionKey6, ddlFunctionKey7, ddlFunctionKey8, IsActiveTerminal, txtRelaySwitching, chkpersonnelNumberPin, readerInternDesc, readerInternDesc, readerInternDirec, readerInternDirec, readerRelayTime, externalReaderId, OnSave_Callback, OnSaveFail_Callback);
    } else {
        PageMethods.UpdateZkTerminalInstance(ddlCurrentZkTerminalInstance, ddlTerminalType, selectedTerminalID, txtUserTerminalId, txtDescription, txtSerialNumber, ddlLinkType,
            txtIpAddress, txtIpPort, chkbackToReadFingerPrint, chkAPPosting, chkProfFirmware, txtInfoText1, txtInfoText2, txtInfoText3, txtInfoText4,
ddlFunctionKey1, ddlFunctionKey2, ddlFunctionKey3, ddlFunctionKey4, ddlFunctionKey5, ddlFunctionKey6, ddlFunctionKey7, ddlFunctionKey8, IsActiveTerminal, txtRelaySwitching, chkpersonnelNumberPin, readerInternDesc, readerInternDesc, readerInternDirec, readerInternDirec, readerRelayTime, externalReaderId, internReaderId, OnEdit_Callback, onEditFail_Callback);
    }
}

function OnSave_Callback(response) {
    $("#ddlCurrentZkTerminalInstance").append($('<option></option>').val(response.ID).html(response.ID));

    $("#ddlCurrentZkTerminalInstance").val(response.ID).attr("selected", "selected");

    $("#ddlTerminalType").append($('<option></option>').val(response.TerminalId).html(response.TermType));
    $("#ddlTerminalType").val(response.TerminalId).attr("selected", "selected");
    $("#txtTerminalId").val(response.TermID);
    $("#txtDescription").val(response.Description);
    $("#txtSerialNumber").val(response.SerialNumber);
    if (response.ConnectionType === "keine") {
        $("#ddlLinkType option").remove();
        $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
        $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
        $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
        $("#ddlLinkType").val("0").attr("selected", "selected");
    }
    if (response.ConnectionType === "None") {
        $("#ddlLinkType option").remove();
        $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
        $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
        $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
        $("#ddlLinkType").val("0").attr("selected", "selected");
    }
    if (response.ConnectionType === "TCP/IP") {
        $("#ddlLinkType option").remove();
        $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
        $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
        $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
        $("#ddlLinkType").val("1").attr("selected", "selected");
    }
    if (response.ConnectionType === "USB") {
        $("#ddlLinkType option").remove();
        $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
        $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
        $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
        $("#ddlLinkType").val("2").attr("selected", "selected");
    }
    $("#txtIpAddress").val(response.IpAddress);
    if (response.Port === -1) {
        $("#txtIpPort").val("");
    } else {
        $("#txtIpPort").val(response.Port);
    }
    if (response.ZkRelayTime === -1) {
        $("#txtRelaySwitching").val("");
    } else {
        $("#txtRelaySwitching").val(response.ZkRelayTime);
    }
    if (response.HasFPRead === true) {
        $('#chkbackToReadFingerPrint').prop('checked', true);
    } else {
        $('#chkbackToReadFingerPrint').prop('checked', false);
    }
    if (response.HasAPPosting === true) {
        $('#chkAPPosting').prop('checked', true);
    } else {
        $('#chkAPPosting').prop('checked', false);
    }
    if (response.HasPersNoPin === true) {
        $('#chkpersonnelNumberPin').prop('checked', true);
    } else {
        $('#chkpersonnelNumberPin').prop('checked', false);
    }
    if (response.HasProfFirmware === true) {
        $('#chkProfFirmware').prop('checked', true);
    } else {
        $('#chkProfFirmware').prop('checked', false);
    }
    $("#txtInfoText1").val(response.InfoText1);
    $("#txtInfoText2").val(response.InfoText2);
    $("#txtInfoText3").val(response.InfoText3);
    $("#txtInfoText4").val(response.InfoText4);

    $("#ddlFunctionKey1").val(response.FunctionKeyText1);
    $("#ddlFunctionKey2").val(response.FunctionKeyText2);
    $("#ddlFunctionKey3").val(response.FunctionKeyText3);
    $("#ddlFunctionKey4").val(response.FunctionKeyText4);
    $("#ddlFunctionKey5").val(response.FunctionKeyText5);
    $("#ddlFunctionKey6").val(response.FunctionKeyText6);
    $("#ddlFunctionKey7").val(response.FunctionKeyText7);
    $("#ddlFunctionKey8").val(response.FunctionKeyText8);

    var ActiveImgUrl = "/Images/FormImages/Dot_gruen.png";
    var InactiveImgUrl = "/Images/FormImages/Dot_rot.png";
    if (response.IsActive === true) {
        $('.btnblue').css('background-image', 'url(' + ActiveImgUrl + ')');
        $("#lblTIStatus").css("color", "green");
    }
    if (response.IsActive === false) {
        $('.btnblue').css('background-image', 'url(' + InactiveImgUrl + ')');
        $("#lblTIStatus").css("color", "red");
    }

    $("#lblTIId").text(response.readerInternId);
    $("#lblTIInfo").text(response.readerInfoIntern);
    $("#lblTILaserType").text(response.readerTypeIntern);
    $("#txtTIDescription").val(response.internReaderDesc);
    $("#ddlTIDirection").val(response.internDirec).attr("selected", "selected");
    $("#lblTIStatus").text(response.readerActive);
    $("#txtTIRelayTime").val(response.relayTime);
    $("#lblExId").text("1a");
    $("#lblExLaserType").text(response.readerTypeIntern_2);

    grdZkTerminalCon.PerformCallback();
    ShowUserZkTerminals();
}

function OnSaveFail_Callback(response) {

}

function OnEdit_Callback(response) {
    $("#ddlCurrentZkTerminalInstance option[value='0']").remove();

    $("#ddlCurrentZkTerminalInstance option[value='" + response.ID + "']").remove();

    $("#ddlCurrentZkTerminalInstance").append($('<option></option>').val(response.ID).html(response.ID));
    $("#ddlCurrentZkTerminalInstance").val(response.ID).attr("selected", "selected");

    $("#ddlTerminalType").append($('<option></option>').val(response.TerminalId).html(response.TermType));
    $("#ddlTerminalType").val(response.TerminalId).attr("selected", "selected");
    $("#txtTerminalId").val(response.TermID);
    $("#txtDescription").val(response.Description);
    $("#txtSerialNumber").val(response.SerialNumber);
    if (response.ConnectionType === "keine") {
        $("#ddlLinkType option").remove();
        $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
        $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
        $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
        $("#ddlLinkType").val("0").attr("selected", "selected");
    }
    if (response.ConnectionType === "None") {
        $("#ddlLinkType option").remove();
        $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
        $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
        $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
        $("#ddlLinkType").val("0").attr("selected", "selected");
    }
    if (response.ConnectionType === "TCP/IP") {
        $("#ddlLinkType option").remove();
        $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
        $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
        $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
        $("#ddlLinkType").val("1").attr("selected", "selected");
    }
    if (response.ConnectionType === "USB") {
        $("#ddlLinkType option").remove();
        $("#ddlLinkType").append($('<option></option>').val(0).html("keine"));
        $("#ddlLinkType").append($('<option></option>').val(1).html("TCP/IP"));
        $("#ddlLinkType").append($('<option></option>').val(2).html("USB"));
        $("#ddlLinkType").val("2").attr("selected", "selected");
    }
    $("#txtIpAddress").val(response.IpAddress);
    if (response.Port === -1) {
        $("#txtIpPort").val("");
    } else {
        $("#txtIpPort").val(response.Port);
    }
    if (response.ZkRelayTime === -1) {
        $("#txtRelaySwitching").val("");
    } else {
        $("#txtRelaySwitching").val(response.ZkRelayTime);
    }
    if (response.HasFPRead === true) {
        $('#chkbackToReadFingerPrint').prop('checked', true);
    } else {
        $('#chkbackToReadFingerPrint').prop('checked', false);
    }
    if (response.HasAPPosting === true) {
        $('#chkAPPosting').prop('checked', true);
    } else {
        $('#chkAPPosting').prop('checked', false);
    }
    if (response.HasPersNoPin === true) {
        $('#chkpersonnelNumberPin').prop('checked', true);
    } else {
        $('#chkpersonnelNumberPin').prop('checked', false);
    }
    if (response.HasProfFirmware === true) {
        $('#chkProfFirmware').prop('checked', true);
    } else {
        $('#chkProfFirmware').prop('checked', false);
    }
    $("#txtInfoText1").val(response.InfoText1);
    $("#txtInfoText2").val(response.InfoText2);
    $("#txtInfoText3").val(response.InfoText3);
    $("#txtInfoText4").val(response.InfoText4);

    $("#ddlFunctionKey1").val(response.FunctionKeyText1);
    $("#ddlFunctionKey2").val(response.FunctionKeyText2);
    $("#ddlFunctionKey3").val(response.FunctionKeyText3);
    $("#ddlFunctionKey4").val(response.FunctionKeyText4);
    $("#ddlFunctionKey5").val(response.FunctionKeyText5);
    $("#ddlFunctionKey6").val(response.FunctionKeyText6);
    $("#ddlFunctionKey7").val(response.FunctionKeyText7);
    $("#ddlFunctionKey8").val(response.FunctionKeyText8);

    var ActiveImgUrl = "/Images/FormImages/Dot_gruen.png";
    var InactiveImgUrl = "/Images/FormImages/Dot_rot.png";
    if (response.IsActive === true) {
        $('.btnblue').css('background-image', 'url(' + ActiveImgUrl + ')');
        $("#lblTIStatus").css("color", "green");
    }
    if (response.IsActive === false) {
        $('.btnblue').css('background-image', 'url(' + InactiveImgUrl + ')');
        $("#lblTIStatus").css("color", "red");
    }

    $("#lblTIId").text(response.readerInternId);
    $("#lblTIInfo").text(response.readerInfoIntern);
    $("#lblTILaserType").text(response.readerTypeIntern);
    $("#txtTIDescription").val(response.internReaderDesc);
    $("#ddlTIDirection").val(response.internDirec).attr("selected", "selected");
    $("#lblTIStatus").text(response.readerActive);
    $("#txtTIRelayTime").val(response.relayTime);
    $("#lblExId").text("1a");
    $("#lblExLaserType").text(response.readerTypeIntern_2);

    grdZkTerminalCon.PerformCallback();
    ShowUserZkTerminals();
}

function onEditFail_Callback(response) {

}

function SetControlsOnNew() {
    $("#txtTerminalId").val("");
    $("#txtDescription").val("");
    $("#txtSerialNumber").val("");
    $("#ddlLinkType").val("1").attr("selected", "selected");
    $("#txtIpAddress").val("");
    $("#txtIpAddress").val("");
    $("#txtIpPort").val("4370");
    $('#chkbackToReadFingerPrint').prop('checked', false);
    $('#chkAPPosting').prop('checked', false);
    $('#chkpersonnelNumberPin').prop('checked', false);
    $('#chkProfFirmware').prop('checked', false);
    $("#txtInfoText1").val("");
    $("#txtInfoText2").val("");
    $("#txtInfoText3").val("");
    $("#txtInfoText4").val("");
    $("#txtRelaySwitching").val("");
    $("#ddlFunctionKey1").val("100");
    $("#ddlFunctionKey2").val("100");
    $("#ddlFunctionKey3").val("100");
    $("#ddlFunctionKey4").val("100");
    $("#ddlFunctionKey5").val("100");
    $("#ddlFunctionKey6").val("100");
    $("#ddlFunctionKey7").val("100");
    $("#ddlFunctionKey8").val("100");
    var InactiveImgUrl = "/Images/FormImages/Dot_rot.png";
    $('.btnblue').css('background-image', 'url(' + InactiveImgUrl + ')');
}

function SetNextUserTerminalId() {
    var dummyVariable = "";
    PageMethods.GetLatestUserTermID(dummyVariable, GetLatestUserTermID_Callback);
}

function GetLatestUserTermID_Callback(response) {
    var maxNo = response.TermID;
    $("#txtTerminalId").val(maxNo);
    $("#txtDescription").focus();
}

function BackButtonConfirm() {
    getLocalizedText("saveChangesConfirmation");
    var message = levelCaption;
    var box_content = '<div id="overlay"><div id="box_flame"><div id="dialogBox">  <img src="../../Images/FormImages/stop-save2-02.png" alt="Stop" class="stopPic" height="150" width="150" align="middle"> <br/>' + message + '<br/> <button id="btnOk"   onclick="SaveOnBackButton()"></button><button id="btnNo"   onclick="RedirectBackWithoutSaving()"></button><button id="btnCancel"  onclick="CancelOnBackButton()"></button></div></div></div>';
    document.getElementById('confirmDelete').innerHTML = box_content;
    getLocalizedText("yes");
    $('#btnOk').text(levelCaption);
    getLocalizedText("no");
    $('#btnNo').text(levelCaption);
    getLocalizedText("cancel");
    $('#btnCancel').text(levelCaption);
}

function ImportantInfoDialogPrompt(title, message) {
    var boxContent = "<div id=\"overlayDuplicate\"><div id=\"box_flameDuplicate\">" +
        "<div id=\"dialogBoxDuplicate\">  " +
        title + "<img src=\"../../Images/FormImages/greeninfo-01.png\" alt=\"Stop\" class=\"greeninfo\" height=\"50\" width=\"50\"  align=\"right\"> <br/>" + "" + "" + "" +
        "<div id=\"dialogBox2Duplicate\">  " + "<br/> <br/> <br/>" +
     message + "<br/> " +
        "<button id=\"btnDuplicateOK\"  onclick=\"resetImportantInfoDialogDiv()\"></button>" +
        "</div></div></div>";
    document.getElementById("importantInfoDialog").innerHTML = boxContent;
    getLocalizedText("yes");
    $("#btnDuplicateOK").text(levelCaption);
    $("#hiddenFieldIncreament").attr("value", "1");
}

function resetImportantInfoDialogDiv() {
    document.getElementById("importantInfoDialog").innerHTML = "";
    $("#txtTerminalId").focus();
}

function AddExternalReader() {
    $("#lblExId").text("2");
    $("#lblExInfo").text("Extern");
    $("#lblExLaserType").text("LE 100");
}

function ClearReaderControls() {

    $("#lblTIId").text("");
    $("#lblTIInfo").text("");
    $("#lblTILaserType").text("");
    $("#txtTIDescription").val("");
    $("#ddlTIDirection").val("0").attr("selected", "selected");
    $("#lblTIStatus").text("");
    $("#txtTIRelayTime").val("");
    $("#lblExId").text("");
    $("#lblExLaserType").text("");
}

function SetNewInternReader() {
    $("#lblTIId").text("1");
    $("#lblTIInfo").text("Intern");
    $("#lblTILaserType").text("Biometrie");
    $("#txtTIRelayTime").val("5");
    $("#lblTIStatus").text("Inaktiv");
    $("#lblExId").text("1a");
    $("#lblExLaserType").text("RFID");
    $("#lblTIStatus").css("color", "red");
}

function HideUserMoresetting() {
    $("#btnAccess").prop("disabled", false);
    $("#btnMore").removeAttr("disabled");
    $('#btnAccess').css("background-color", "#FEF1C7");
    $('#btnMore').css("background-color", "");
    $(".bttmsec5gridhide").hide();
    $(".bttmsec5").show();
}

function ShowUserMoresetting() {
    $("#btnMore").prop("disabled", true);
    $("#btnAccess").removeAttr("disabled");
    $('#btnMore').css("background-color", "#FEF1C7");
    $('#btnAccess').css("background-color", "");
    $(".bttmsec5").hide();
    $(".bttmsec5gridhide").show();
}

function displayProfileInformation() {
    PageMethods.GetAccessprofileInformation(sendProfileInfoToControls);
}

function sendProfileInfoToControls(response) {
    try {
        $("#txtFvTotalEntries").val(response.ProfilesCount);
        // $("#profileMax").val(response.ProfilesCount);
        $("#txtAccessProfile").val(response.CurrentSelectedProfile);
        if (response.ProfilesCount > 1) {
            $("#fvNavPrev").removeAttr('disabled');
            $("#fvNavNext").removeAttr('disabled');
        }
        else {
            $("#fvNavPrev").attr('disabled', 'disabled');
            $("#fvNavNext").attr('disabled', 'disabled');
        }
        if (response.CurrentSelectedProfile === response.ProfilesCount) {
            $("#fvNavNext").attr('disabled', 'disabled');
        }
        else if (response.CurrentSelectedProfile === 1) {
            $("#fvNavPrev").attr('disabled', 'disabled');
        }
        $("#txtGroupProfileNo1").val(response.GroupNumber);
        $("#txtGroupProfileDescription1").val(response.GroupDescription);
        $("#txtAccessProfileNo").val(response.ProfileNumber);
        $("#txtAccessProfileID").val(response.ProfileID);
        $("#txtAccessDescription").val(response.ProfileDescription);
        $("#txtMemoNotes").val(response.Memo);
    }
    catch (err)
    { }
}

