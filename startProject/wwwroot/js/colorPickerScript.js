// startProject colorPickerScript.js

$(document).ready(function () {
    //Pimp change color with ColorPicker
    checkJSIsEnabled();
    setColorFromCookie();

    if ($('#colorpicker') && $('#color')) {
        pickAndSetNewColor();
        setBodyBackGroundColor();
    };
});

function checkJSIsEnabled() {
    if ($("#demo")) {
        $("#demo").hide();
    }
}

function setColorFromCookie() {
    $("#color").attr('value', (Cookies.get('ColorPicked_Body')));
}

function pickAndSetNewColor() {
    $('#colorpicker').farbtastic('#color');
}

function setBodyBackGroundColor() {
    $('#colorpicker').on("click", function () {
        if (confirmFunction($("#color").val())) {
            $("body").css("background-color", $("#color").val());
            Cookies.set('ColorPicked_Body', $("#color").val(), { expires: 7, path: '' });
            $("#color").attr('value', (Cookies.get('ColorPicked_Body')));
        }
    });
}

function confirmFunction(newColorCode) {
    var txt;
    var r = confirm("Achtergrond wijzigen naar de gekozen kleur(" + newColorCode + ")?");
    if (r == true) {
        txt = "You pressed OK!";
    } else {
        txt = "You pressed Cancel!";
    }
    console.log(txt);
    return r;
}