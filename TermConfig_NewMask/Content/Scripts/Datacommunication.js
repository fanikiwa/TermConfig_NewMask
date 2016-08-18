$(document).ready(function () {

    DisplayDate();
})

    function DisplayDate() {
    $('#lblDate').text(moment().format("DD") + "." + moment().format("MM") + "." + moment().format("YYYY"));
    }