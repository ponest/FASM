
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

