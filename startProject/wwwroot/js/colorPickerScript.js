//Pimp change color with ColorPickerr
    if ($("#demo")) {
    $("#demo").hide();
}
if ($('#colorpicker') != null && $('#color') != null) {
    $('#colorpicker').farbtastic('#color');
    $('#colorpicker').on("click", function () {
        $("body").css("background-color", $("#color").val());
    });
}