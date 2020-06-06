$(document).ready(function () {
    //Pimp change color with ColorPickerr
    if ($("#demo")) {
        $("#demo").hide();
    }

    if ($('#colorpicker') && $('#color')) {
        $('#colorpicker').farbtastic('#color');
        $('#colorpicker').on("click", function () {
            Cookies.set('ColorPicked_Body', $("#color").val(), { expires: 7, path: '' });
            if (confirmFunction()) {
                $("body").css("background-color", $("#color").val());
                alert(Cookies.get('ColorPicked_Body'));
            }
        });
    }

    Cookies.set('ColorPicked_Body', $("#color").val(), { expires: 7, path: '' });
    alert(Cookies.get('ColorPicked_Body'));
});

function confirmFunction() {
    var txt;
    var r = confirm("Achtergrond wijzigen naar de gekozen kleur?");
    if (r == true) {
        txt = "You pressed OK!";
    } else {
        txt = "You pressed Cancel!";
    }
    console.log(txt);
    return r;
}