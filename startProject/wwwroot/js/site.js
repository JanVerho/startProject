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
    // link to sourceCode used: /http://www.lessanvaezi.com/filter-select-list-options/
});