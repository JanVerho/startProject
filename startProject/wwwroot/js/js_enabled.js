// check and show on index-page if browser has JS enabled

$(document).ready(function main() {
    executeJS_availableTest();
    checkCookiesAreEnabled();
});

function executeJS_availableTest() {
    let confirmText = "JavaScript: ingeschakeld! - ";

    $("#JSTest").text(confirmText);
    $("#JSTest").css({ "color": "green", "font-weight": "normal", "font-style": "italic" });
}

function checkCookiesAreEnabled() {
    let cookieCheck = "Cookies: ingeschakeld!";
    if (!navigator.cookieEnabled) {
        cookieCheck = "Cookies: niet ingeschakeld!";
        $("#cookie").css({ "color": "red", "font-weight": "bold", "font-style": "italic" });
    }
    else {
        $("#cookie").css({ "color": "green", "font-weight": "normal", "font-style": "italic" });
    }
    $("#cookie").text(cookieCheck);
}