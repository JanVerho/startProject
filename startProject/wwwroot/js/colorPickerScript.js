$(document).ready(function () {
    //Pimp change color with ColorPickerr
    if ($("#demo")) {
        $("#demo").hide();
    }

    alert(Cookies.get('ColorPicked_Body'));

    if ($('#colorpicker') != null && $('#color') != null) {
        $('#colorpicker').farbtastic('#color');
        $('#colorpicker').on("click", function () {
            Cookies.set('ColorPicked_Body', $("#color").val(), { expires: 7, path: '' });

            $("body").css("background-color", $("#color").val());
            alert(Cookies.get('ColorPicked_Body'));
        });
    }

    Cookies.set('ColorPicked_Body', $("#color").val(), { expires: 7, path: '' });
    alert(Cookies.get('ColorPicked_Body'));
});