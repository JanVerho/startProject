// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

let isChecked = false;
let sorterenText = "";

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

    //Pimp scripts
    //Sort FilterButton script
    $("#FormWeekNrFlowerStart").keyup(function () {
        houdini("#FilterSort_Btn")
    });
    $("#FormWeekNrFlowerEnd").keyup(function () {
        houdini("#FilterSort_Btn")
    });

    $("#CheckWeekNrFlowerStart").on('change', function () {
        sorterenText = composeSortText();
        houdini("#FilterSort_Btn")
    });
    $("#CheckWeekNrFlowerEnd").on('change', function () {
        sorterenText = composeSortText();
        houdini("#FilterSort_Btn")
    });

    $("#FilterSort_Btn").click(function () {
        $("#FormWeekNrFlowerStart, FormWeekNrFlowerEnd").val('');
        $("#FilterSort_Btn").text("Filteren");
        $(window).scrollTop(0);
    });

    //Color Tables with a JQ-script
    $("tr:even").css("background-color", "#F4F4F8");
    $("tr:odd").css("background-color", "#EFF1F1");

    //https://jsfiddle.net/JanVerh/h0e2rtv3/8/

    $("#thStart").on("click", function () {
        window.open('https://localhost:44379/?FormWeekNrFlowerStart=&FormWeekNrFlowerEnd=&CheckWeekNrFlowerStart=CheckWeekNrFlowerStart');
    });
});

//Methodes
function composeSortText() {
    let textResult = "";
    let x = document.getElementById('CheckWeekNrFlowerEnd');
    let y = document.getElementById('CheckWeekNrFlowerStart');

    if (y.checked || x.checked) {
        textResult += " & Sorteren"
        if (y.checked) {
            textResult += " op BeginBloei"
        }
        if (x.checked) {
            textResult += " EindeBloei"
        }
    }
    return textResult
};

function houdini(button) {
    if ($("#FormWeekNrFlowerStart").val() || $("#FormWeekNrFlowerEnd").val()) {
        $(button).text("Filteren");
        if ($("#FormWeekNrFlowerStart").val()) {
            $(button).append(" vanaf 'BloeiStart'(W_" + $("#FormWeekNrFlowerStart").val() + ")");
        }
        if ($("#FormWeekNrFlowerEnd").val()) {
            $(button).append(" tot 'BloeiEind'(W_" + $("#FormWeekNrFlowerEnd").val() + ")");
        }
    }
    else {
        $(button).text("Filteren(alle)");
    }
    $(button).append('<br>' + sorterenText);
};