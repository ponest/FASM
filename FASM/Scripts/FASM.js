
function FlashMsg() {
    (function ($) {
        $.fn.flash_message = function (options) {

            options = $.extend({
                text: 'Done',
                time: 2000,
                how: 'before',
                class_name: ''
            }, options);

            return $(this).each(function () {
                if ($(this).parent().find('.flash_message').get(0))
                    return;

                var message = $('<span />', {
                    'class': 'flash_message ' + options.class_name,
                    text: options.text
                }).hide().fadeIn('fast');

                $(this)[options.how](message);

                message.delay(options.time).fadeOut('normal', function () {
                    $(this).remove();
                });

            });
        };
    })(jQuery);
}
var selector,message,type;
function ShowNotice(selector,message,type) {
    FlashMsg();
    if (type == "Failure") {
        if($(selector).hasClass("SuccessMsg")){
            $(selector).removeClass("SuccessMsg");
        }
        
        $(selector).flash_message({
            text: message,
            how: 'append'
        }).addClass("ErrroMessage");

    } else if (type == "Success") {
        if ($(selector).hasClass("ErrroMessage")) {
            $(selector).removeClass("ErrroMessage");
        }
        
        $(selector).flash_message({
            text: message,
            how: 'append'
        }).addClass("SuccessMsg");
    }
}

$(".app-menu__item").click(function () {
    $(this).addClass("active");
});

//Validating numerals
$(".Numeric").keydown(function (e) {
    // Allow: backspace, delete, tab, escape, enter and .
    if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
        // Allow: Ctrl+A, Command+A
        (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});

//Validfate integers
$(".NumericInt").keydown(function (e) {
    // Allow: backspace,  tab, escape,
    if ($.inArray(e.keyCode, [ 8, 9, 27]) !== -1 ||
        // Allow: Ctrl+A, Command+A
        (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
        // Allow: home, end, left, right, down, up
        (e.keyCode >= 35 && e.keyCode <= 40)) {
        // let it happen, don't do anything
        return;
    }
    // Ensure that it is a number and stop the keypress
    if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
        e.preventDefault();
    }
});