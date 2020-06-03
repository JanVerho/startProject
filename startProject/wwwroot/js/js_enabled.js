// check and show on index-page if browser has JS enabled

$(document).ready(function () {
    let confirmText = "JavaScript: ingeschakeld!";

    $("#JSTest").text(confirmText);
    $("#JSTest").css({ "color": "green", "font-weight": "normal", "font-style": "italic" });
});