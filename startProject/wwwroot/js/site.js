// startProject site.js

//Global var
let sorterenText = "";

$(document).ready(function () {
    //FilterByText
    $('#OrderLine_ProductName').filterByText($('#OrderLine_ProductName_textbox'), true);

    //Pimp extra JQ-scripts
    //1. Txt in button Sort FilterButton
    sorterenText = composeSortText();
    houdini("#FilterSort_Btn")
    setFilterParam();
    setSortParam();
    executeFilterAndSort();

    //2. Coloring Table & Body
    setTableRowColors();
    setBodyColor();
});

//Methodes
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
    /* link to sourceCode used: /http://www.lessanvaezi.com/filter-select-list-options/ */
};

function composeSortText() {
    let textResult = "";
    let x = document.getElementById('CheckWeekNrFlowerStart');
    let y = document.getElementById('CheckWeekNrFlowerEnd');

    if (x != null && y != null) {
        if (x.checked || y.checked) {
            textResult += " & Sorteren"
            if (x.checked) {
                textResult += " op BeginBloei"
            }
            if (y.checked) {
                textResult += " EindeBloei"
            }
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

function setTableRowColors() {
    $("tr:even").css("background-color", "#bcfbee");
    $("tr:odd").css("background-color", "#EFF1F1");
}

function setBodyColor() {
    if (!Cookies.get('ColorPicked_Body') == null) {
        Cookies.set('ColorPicked_Body', '#abe8e0', { expires: 7, path: '' });
    }
    $("body, #color").css("background-color", Cookies.get('ColorPicked_Body'));
}

function setFilterParam() {
    $("#FormWeekNrFlowerStart, #FormWeekNrFlowerEnd").keyup(function () {
        houdini("#FilterSort_Btn")
    });
}

function setSortParam() {
    $("#CheckWeekNrFlowerStart, #CheckWeekNrFlowerEnd").on('change', function () {
        sorterenText = composeSortText();
        houdini("#FilterSort_Btn")
    });
}

function executeFilterAndSort() {
    $("#FilterSort_Btn").click(function () {
        $("#FilterSort_Btn").text("Filteren");
        $(window).scrollTop(0);
    });
}