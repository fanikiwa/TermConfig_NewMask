
Number.prototype.formatMoney = function (c, d, t) {
    var n = this, c = isNaN(c = Math.abs(c)) ? 2 : c, d = d === undefined ? "."
            : d, t = t === undefined ? "," : t, s = n < 0 ? "-" : "", i = parseInt(n = Math
                    .abs(+n || 0).toFixed(c))
            + "", j = (j = i.length) > 3 ? j % 3 : 0;
    return s + (j ? i.substr(0, j) + t : "")
            + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t)
            + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
};

function formatDate(date) {
    var d = new Date(date), month = '' + (d.getMonth() + 1), day = ''
            + d.getDate(), year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [month, day, year].join('-');
}

function formatDateForControl(date) {
    var d = new Date(date), month = '' + (d.getMonth() + 1), day = ''
            + d.getDate(), year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

function formatTimeForControl(date) {
    var d = new Date(date), hours = '' + d.getHours(), minutes = ''
            + d.getMinutes(), seconds = ''
            + d.getSeconds();

    var DayNight = "PM"
    if (hours < 12)
        DayNight = "AM";

    seconds = addZero(seconds);
    minutes = addZero(minutes);
    //only -12 from hours if it is greater than 12 (if not back at mid night)
    hours = (hours > 12) ? hours - 12 : hours;
    //if 00 then it is 12 am
    hours = (hours === '00') ? 12 : hours;
    hours = addZero(hours);

    var time = [hours, minutes, seconds].join(':');
    return [time, DayNight].join(' ');
}

function getDayName(date) {
    var daysOfTheWeek = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
    var d = new Date(date);
    return daysOfTheWeek[d.getDay()];
}

function getMonthName(date) {
    var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var d = new Date(date);
    return monthNames[d.getMonth()];
}

function decrementDateByMonth(oldDate, offset, offsetType) {
    return formatDateForControl(AddDate(oldDate, offset, offsetType));
}

function decrementDateByYear(oldDate, offset, offsetType) {
    return formatDateForControl(AddDate(oldDate, offset, offsetType));
}

function offsetDate(oldDate, offset, offsetType) {
    return formatDateForControl(AddDate(oldDate, offset, offsetType));
}

function AddDate(oldDate, offset, offsetType) {
    var year = parseInt(oldDate.getFullYear());
    var month = parseInt(oldDate.getMonth());
    var date = parseInt(oldDate.getDate());
    var hour = parseInt(oldDate.getHours());
    var minute = parseInt(oldDate.getMinutes());
    var second = parseInt(oldDate.getSeconds());

    var newDate;
    switch (offsetType) {
        case "Y":
        case "YY":
        case "y":
        case "yy":
            newDate = new Date(year + offset, month, date, hour, minute, second);
            break;

        case "M":
        case "MM":
        case "m":
        case "mm":
            newDate = new Date(year, month + offset, date, hour, minute, second);
            break;

        case "D":
        case "DD":
        case "d":
        case "dd":
            newDate = new Date(year, month, date + offset, hour, minute, second);
            break;

        case "H":
        case "HH":
        case "h":
        case "hh":
            newDate = new Date(year, month, date, hour + offset, minute, second);
            break;

        case "min":
            newDate = new Date(year, month, date, hour, minute + offset, second);
            break;

        case "S":
        case "SS":
        case "s":
        case "ss":
            newDate = new Date(year, month, date, hour, minute, second + offset);
            break;

    }
    return newDate;
}

function addZero(i) {
    if (i < 10) {
        i = "0" + i
    }
    ; // add zero in front of numbers < 10
    return i;
}

function isNumber(n) {
    return $.isNumeric(n);
}

function formatNumberWithCommas(n) {
    if (isNumber(n)) {
        return n.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    } else {
        return n;
    }
}

function updateClock() {
    var currentTime = new Date();

    var currentYear = currentTime.getFullYear();
    var currentMonth = currentTime.getMonth() + 1;
    var currentDate = currentTime.getDate();

    var currentHours = currentTime.getHours();
    var currentMinutes = currentTime.getMinutes();
    var currentSeconds = currentTime.getSeconds();

    // Pad the minutes and seconds with leading zeros, if required
    currentMinutes = (currentMinutes < 10 ? "0" : "") + currentMinutes;
    currentSeconds = (currentSeconds < 10 ? "0" : "") + currentSeconds;

    // Pad the month and date with leading zeros, if required
    currentMonth = (currentMonth < 10 ? "0" : "") + currentMonth;
    currentDate = (currentDate < 10 ? "0" : "") + currentDate;

    // Choose either "AM" or "PM" as appropriate
    var timeOfDay = (currentHours < 12) ? "AM" : "PM";

    // Convert the hours component to 12-hour format if needed
    currentHours = (currentHours > 12) ? currentHours - 12 : currentHours;

    // Convert an hours component of "0" to "12"
    currentHours = (currentHours === 0) ? 12 : currentHours;

    var monthname = getMonthName(currentTime);
    var dayname = getDayName(currentTime);

    // Compose the string for display 
    var currentTimeString = currentHours + ":" + currentMinutes + ":" + currentSeconds + " " + timeOfDay;
    var currentDateString = currentDate + "-" + dayname + "-" + monthname + "-" + currentYear;

    $("#running_time").html("Time: " + currentTimeString);
    $("#running_date").html("Date: " + currentDateString);

}

function extractBrowserInfo() {
    var userAgent = navigator.userAgent.toLowerCase(),
            browser = '',
            version = 0;

    $.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());

    // Is this a version of IE?
    if ($.browser.msie) {
        userAgent = $.browser.version;
        userAgent = userAgent.substring(0, userAgent.indexOf('.'));
        version = userAgent;
        browser = "Internet Explorer";
    }

    // Is this a version of Chrome?
    if ($.browser.chrome) {
        userAgent = userAgent.substring(userAgent.indexOf('chrome/') + 7);
        userAgent = userAgent.substring(0, userAgent.indexOf('.'));
        version = userAgent;
        // If it is chrome then jQuery thinks it's safari so we have to tell it it isn't
        $.browser.safari = false;
        browser = "Chrome";
    }

    // Is this a version of Safari?
    if ($.browser.safari) {
        userAgent = userAgent.substring(userAgent.indexOf('safari/') + 7);
        userAgent = userAgent.substring(0, userAgent.indexOf('.'));
        version = userAgent;
        browser = "Safari";
    }

    // Is this a version of Mozilla?
    if ($.browser.mozilla) {
        //Is it Firefox?
        if (navigator.userAgent.toLowerCase().indexOf('firefox') !== -1) {
            userAgent = userAgent.substring(userAgent.indexOf('firefox/') + 8);
            userAgent = userAgent.substring(0, userAgent.indexOf('.'));
            version = userAgent;
            browser = "Firefox"
        }
            // If not then it must be another Mozilla
        else {
            browser = "Mozilla (not Firefox)"
        }
    }

    // Is this a version of Opera?
    if ($.browser.opera) {
        userAgent = userAgent.substring(userAgent.indexOf('version/') + 8);
        userAgent = userAgent.substring(0, userAgent.indexOf('.'));
        version = userAgent;
        browser = "Opera";
    }

    // Now you have two variables, browser and version
    // which have the right info

    console.log("Browser: " + browser);
    console.log("version: " + version);

    return "Browser: " + browser + ",Version: " + version;
}

function extractclientplatforminfo() {
    var OSName = "Unknown OS";
    if (navigator.appVersion.indexOf("Win") !== -1)
        OSName = "Windows";
    if (navigator.appVersion.indexOf("Mac") !== -1)
        OSName = "MacOS";
    if (navigator.appVersion.indexOf("X11") !== -1)
        OSName = "UNIX";
    if (navigator.appVersion.indexOf("Linux") !== -1)
        OSName = "Linux";
    var isiPad = /ipad/i.test(navigator.userAgent.toLowerCase());
    if (isiPad) {
        OSName = "ipad";
    }
    var isiPhone = /iphone/i.test(navigator.userAgent.toLowerCase());
    if (isiPhone) {
        OSName = "iphone";
    }
    var isiPod = /ipod/i.test(navigator.userAgent.toLowerCase());
    if (isiPod) {
        OSName = "ipod";
    }
    var isAndroid = /android/i.test(navigator.userAgent.toLowerCase());
    if (isAndroid) {
        OSName = "android";
    }
    var isBlackBerry = /blackberry/i.test(navigator.userAgent.toLowerCase());
    if (isBlackBerry) {
        OSName = "blackberry";
    }
    var isWindowsPhone = /windows phone/i.test(navigator.userAgent.toLowerCase());
    if (isWindowsPhone) {
        OSName = "windows phone";
    }

    console.log("Os: " + OSName);
    return OSName;
}

function obtainClientOSInfo() {
    sessionStorage.setItem('clientos', $.client.os);
    console.log("Os: " + $.client.os);
    return $.client.os;
}

function obtainClientBrowserInfo() {
    sessionStorage.setItem('clientbrowser', $.client.browser);
    console.log("Browser: " + $.client.browser);
    return $.client.browser;
}

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

// Removes an element from an array.
// String value: the value to search and remove.
// return: an array with the removed element; false otherwise.
Array.prototype.remove = function (value) {
    var idx = this.indexOf(value);
    if (idx !== -1) {
        return this.splice(idx, 1); // The second parameter is the number of elements to remove.
    }
    return false;
}

function fetchrssfeedurl() {

    var rssfeedsarray = [{
        rssfeedurl: "http://rss.cnn.com/rss/cnn_topstories.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_world.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_africa.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_americas.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_asia.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_europe.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_meast.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_us.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/money_news_international.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_technology.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_space.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_entertainment.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_sport.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_football.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_golf.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_motorsport.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_tennis.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/edition_travel.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/cnn_freevideo.rss"
    }, {
        rssfeedurl: "http://rss.cnn.com/rss/cnn_latest.rss"
    }];

    var rssfeedOptions = '';
    for (var i = 0; i < rssfeedsarray.length; i++) {
        rssfeedOptions += rssfeedsarray[i].rssfeedurl + "::";
    }

    return rssfeedOptions;

}

var _readrssfeeds = new Array();
var _unreadrssfeeds = fetchrssfeedurl().split('::');

function RandomizeNewsFeed() {
    try {

        if (_unreadrssfeeds.length === 0) {
            _unreadrssfeeds = fetchrssfeedurl().split('::');
            _readrssfeeds = new Array();
        }

        var randomnum = getRandomInt(1, 5);

        if (randomnum === 1) {
            _unreadrssfeeds.sort(function () {
                return 0.1 - Math.random()
            })
        }
        if (randomnum === 2) {
            _unreadrssfeeds.sort(function () {
                return 0.2 - Math.random()
            })
        }
        if (randomnum === 3) {
            _unreadrssfeeds.sort(function () {
                return 0.3 - Math.random()
            })
        }
        if (randomnum === 4) {
            _unreadrssfeeds.sort(function () {
                return 0.4 - Math.random()
            })
        }
        if (randomnum === 5) {
            _unreadrssfeeds.sort(function () {
                return 0.5 - Math.random()
            })
        }

        var unreadfeed = _unreadrssfeeds.shift();
        if (unreadfeed.length !== 0 && unreadfeed !== undefined) {
            var readfeed = _readrssfeeds.unshift(unreadfeed);
            console.log(unreadfeed);
            ConfigureNewsFeed(unreadfeed);
        }

        console.log(_unreadrssfeeds);
        console.log(_readrssfeeds);
    }
    catch (err) {
        ShowErrorNotificationBar(err);
    }
}

function ConfigureNewsFeed(rssfeedurl) {
    try {

        $('.div-newsfeed').breakingNews({
            url: rssfeedurl,
            feedSize: {
                height: '',
                width: ''
            },
            numberToShow: 9999,
            refresh: 5000,
            effect: 'tricker',
            effectDuration: 5,
            onComplete: function () {
                $(this).css("text-decoration", "underline");
            }
        });
    }
    catch (err) {
        ShowErrorNotificationBar(err);
    }
}

function loadjscssfile(filename, filetype) {
    if (filetype === "js") { //if filename is a external JavaScript file
        var fileref = document.createElement('script')
        fileref.setAttribute("language", "javascript")
        fileref.setAttribute("type", "text/javascript")
        fileref.setAttribute("src", filename)
    }
    else if (filetype === "css") { //if filename is an external CSS file
        var fileref = document.createElement("link")
        fileref.setAttribute("rel", "stylesheet")
        fileref.setAttribute("href", filename)
        fileref.setAttribute("type", "text/css")
    }
    if (typeof fileref !== "undefined")
        document.getElementsByTagName("head")[0].appendChild(fileref)
}

function ConfigureCarouselRotator() {
    try {

        var info1 = {
            "lbId": "myslide_1",
            "picWidth": 600,
            "picHeight": 400,
            "picNum": 10,
            "interTime": 5000,
            "picPath": SetSliderImages()
        };

        SSlide(info1);

        $("#myslide_1").removeClass('displaynone');
        $("#myslide_1").addClass('displayblock');
        $("#myslide_1").show();

    }
    catch (err) {
        ShowErrorNotificationBar(err);
    }
}

/**
 * Get a random floating point number between `min` and `max`.
 * 
 * @param {number} min - min number
 * @param {number} max - max number
 * @return {float} a random floating point number
 */
function getRandom(min, max) {
    return Math.random() * (max - min) + min;
}

/**
 * Get a random integer between `min` and `max`.
 * 
 * @param {number} min - min number
 * @param {number} max - max number
 * @return {int} a random integer
 */
function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1) + min);
}

function GetSliderImages() {

    $.post("../../DAL/RecursiveImageIterator.php", //Required URL of the page on server
            {// Data Sending With Request To Server 

            },
            function (response, status) { // Required Callback Function

                $('html, body').animate({ scrollTop: '0px' }, 500);

                var slider_images_array = response.split('::');
                slider_images_array[0] = slider_images_array[0].substr(1);

                console.log(slider_images_array);
                var _imagesarray = new Array();

                for (i = 0; i < slider_images_array.length; i++) {
                    if (slider_images_array[i].length > 0) {
                        slider_images_array[i] = slider_images_array[i].replace('../Images/sslide\\', '../../Images/sslide/');
                        _imagesarray[i] = slider_images_array[i];
                    }
                }
                _undisplayedsliderimages = _imagesarray;

                return slider_images_array;
            });
}

function SetSliderImages() {

    var imagesarray = RandomizeSliderImages();
    var slider_images_array = new Array();

    for (i = 0; i < imagesarray.length; i++) {
        if (imagesarray[i].length > 0) {
            slider_images_array[i] = imagesarray[i];
            console.log(imagesarray[i]);
        }
    }

    var slider_images_json = JSON.stringify(slider_images_array);
    var sliderimagesjson = JSON.parse(slider_images_json)

    console.log(imagesarray);
    console.log(slider_images_array);
    console.log(slider_images_json);
    console.log(sliderimagesjson);

    return sliderimagesjson;
}

var _displayedsliderimages = new Array();
var _undisplayedsliderimages = new Array();

function RandomizeSliderImages() {
    try {

        var randomnum = getRandomInt(1, 5);

        if (randomnum === 1) {
            _undisplayedsliderimages.sort(function () {
                return 0.1 - Math.random()
            })
        }
        if (randomnum === 2) {
            _undisplayedsliderimages.sort(function () {
                return 0.2 - Math.random()
            })
        }
        if (randomnum === 3) {
            _undisplayedsliderimages.sort(function () {
                return 0.3 - Math.random()
            })
        }
        if (randomnum === 4) {
            _undisplayedsliderimages.sort(function () {
                return 0.4 - Math.random()
            })
        }
        if (randomnum === 5) {
            _undisplayedsliderimages.sort(function () {
                return 0.5 - Math.random()
            })
        }

        var undisplayedimage = _undisplayedsliderimages.shift();
        if (undisplayedimage.length !== 0 && undisplayedimage !== undefined) {
            _displayedsliderimages.unshift(undisplayedimage);
            _undisplayedsliderimages.unshift(undisplayedimage);
            console.log(undisplayedimage);
        }

        console.log(_undisplayedsliderimages);
        console.log(_displayedsliderimages);

        var slider_limit_images = _undisplayedsliderimages.slice(0, 10);
        console.log('Random Slider Images Count = ' + slider_limit_images.length);
        console.log(slider_limit_images);

        for (i = 0; i < slider_limit_images.length; i++) {
            console.log(slider_limit_images[i]);
        }

        return slider_limit_images;
    }
    catch (err) {
        ShowErrorNotificationBar(err);
        return null;
    }
}

function ShowErrorNotificationBar(message) {
    try {

        $.notifyBar({
            cssClass: "error",
            html: message,
            delay: 20000,
            animationSpeed: 200,
            jqObject: '',
            close: false,
            closeText: '&times;',
            closeOnClick: true,
            closeOnOver: false,
            onBeforeShow: null,
            onShow: null,
            onBeforeHide: null,
            onHide: null,
            position: 'top'
        });

    } catch (err) {
        alert(err);
    }
}

function ShowSuccessNotificationBar(message) {
    try {

        $.notifyBar({
            cssClass: "success",
            html: message,
            delay: 20000,
            animationSpeed: 200,
            jqObject: '',
            close: false,
            closeText: '&times;',
            closeOnClick: true,
            closeOnOver: false,
            onBeforeShow: null,
            onShow: null,
            onBeforeHide: null,
            onHide: null,
            position: 'top'
        });

    } catch (err) {
        alert(err);
    }
}

function ClearMessageControls() {
    $('#successmessage').html('');
    $('#errormessage').html('');
    $('#apiResults').html('');
    $('.errorList').remove();
    $('#error-display-div').empty();
    $('#errordisplaydivedit').empty();
    $('#successmessageedit').html('');
    $('#errormessageedit').html('');
    $('#apiresultedit').html('');
}

function createValidationControls() {
    $("#divvalidationresultcontrols").html('<div id="apiResults" style="float: left; clear: both"></div> ' +
            '<div id="successmessage" style="float: left; clear: both"></div>' +
            '<div id="errormessage" style="float: left; clear: both"></div> ' +
            '<div id="error-display-div" class="displaynone" style="float: left; clear: both"></div> ' +
            '<div id="isuccessmessage" style="float: left; clear: both"></div> ');

}

function ClearException() {
    $('.errorList').remove();
    $('#error-display-div').empty();
    $('#errordisplaydivedit').empty();
}
