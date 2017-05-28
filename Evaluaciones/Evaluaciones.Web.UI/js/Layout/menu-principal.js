$(function () {
    (function ($) {
        $.fn.accordion = function (custom) {

            var defaults = {
                keepOpen: false,
                startingOpen: false
            }

            var settings = $.extend({}, defaults, custom);

            $(".cl-vnavigation li ul").each(function () {
                $(this).parent().addClass("parent");
            });

            $(".cl-vnavigation li ul li.active").each(function () {
                $(this).parent().show().parent().addClass("open");
            });

            $('.Selected', this).each(function (event, obj) {
                if ($(this).parent().parent().is('ul')) {
                    $(this).parent().parent().show();
                }
            });

            return this.each(function () {
                var obj = $(this);

                $('li a', obj).click(function (event) {
                    var elem = $(this).next();

                    if (elem.is('ul')) {
                        event.preventDefault();

                        if (!settings.keepOpen) {
                            obj.find('ul:visible').not(elem).not(elem.parents('ul:visible')).slideUp();
                        };

                        elem.slideToggle();
                    }
                })
            })
        }
    })(jQuery);

    $('#MenuPrincipal').accordion({ keepOpen: true, startingOpen: '#MenuPrincipal .Selected' });
});