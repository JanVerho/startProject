// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(document).ready(function () {
    //Filter script
    jQuery.fn.filterByText = function (textbox, selectSingleMatch) {
        return this.each(function () {
            var select = this;
            var options = [];
            $(select).find('option').each(function () {
                let option = {
                    value: $(this).val(),
                    text: $(this).text()
                };

                if (option.text !== "Kies een product.") {
                    options.push(option);
                };
            });
            $(select).data('options', options);

            $(textbox).bind('change keyup', function () {
                $(select).empty();
                $(select).scrollTop(0);
                var options = $(select).data('options');;
                var search = $.trim($(this).val());
                var regex = new RegExp(search, 'gi');

                $.each(options, function (i) {
                    var option = options[i];
                    if (option.text.match(regex) !== null) {
                        $(select).append(
                            $('<option>').text(option.text).val(option.value)
                        );
                    }
                });
                if (selectSingleMatch === true &&
                    $(select).children().length === 1) {
                    $(select).children().get(0).selected = true;
                    $(select).attr('size', 1);
                    $(select).css({ "border-color": "lightgreen", "background-color": "Honeydew", "font-weight": "Bold" });
                }
                else {
                    $(select).attr('size', 5);
                    $(select).css({ "border-color": "orange", "background-color": "Lavenderblush" })
                }
            });
        });
    };

    $('#OrderLine_ProductName').filterByText($('#OrderLine_ProductName_textbox'), true);
    /* link to sourceCode used: /http://www.lessanvaezi.com/filter-select-list-options/ */

    // $("#FilterSort_Btn").hide();

    /* $("#FormWeekNrFlowerStart").keyup(function () {
         if ($("#FormWeekNrFlowerStart").val()) {
             $("#FilterSort_Btn").show();
             $("#FilterSort_Btn").append(" Bloei");
         }
         else {
             $("#FilterSort_Btn").hide();
         }
     });*/

    $("#FormWeekNrFlowerStart").keyup(function () {
        houdini("#FilterSort_Btn")
    });
    $("#FormWeekNrFlowerEnd").keyup(function () {
        houdini("#FilterSort_Btn")
    });

    $("#FilterSort_Btn").click(function () {
        $("#FormWeekNrFlowerStart, FormWeekNrFlowerEnd").val('');
        $("#FilterSort_Btn").text("Filteren");;
    });

    /* on load */

    //$('#CheckWeekNrFlowerStart').on('change', function () {
    //    var x = $('#CheckWeekNrFlowerEnd');
    //    if (this.checked)  {
    //        $('#FilterSort_Btn').show();
    //    }
    //    else if  (x.checked){
    //        $('#FilterSort_Btn').show();
    //    }
    //    else  {
    //        $('#FilterSort_Btn').hide();
    //    };

    //});
    //$('#CheckWeekNrFlowerEnd').on('change', function () {
    //    if ((this).attr('checked'))  {
    //        $('#FilterSort_Btn').show();
    //    }
    //    else if  ($('#CheckWeekNrFlowerStart').attr('checked' )){
    //        $('#FilterSort_Btn').show();
    //    }
    //    else {
    //        $('#FilterSort_Btn').hide();
    //    };

    //});
});

function houdini(button) {
    if ($("#FormWeekNrFlowerStart").val() | $("#FormWeekNrFlowerEnd").val()) {
        $(button).show();
        $(button).text("Filter op 'BloeiStart' (W" + $("#FormWeekNrFlowerStart").val() + ") tot 'BloeEind'(W" + $("#FormWeekNrFlowerEnd").val() + ")");
    }
    else {
        $(button).text("Filteren");
    }
};