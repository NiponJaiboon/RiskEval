(function ($) {
    var yearpicker,
        yearStart = 2343,
        yearEnd = 2843;

    yearpicker = function (elementId) {
        var target = $(elementId);
        target.empty();

        for (var i = yearStart; i < yearEnd; i++) {
            $(document.createElement('option'))
                .attr('value', this.Value)
                .text(this.Text)
                .appendTo(target);
        }
    };

    if (typeof ender === 'undefined') {
        // here, `this` means `window` in the browser, or `global` on the server
        // add `numeral` as a global object via a string identifier,
        // for Closure Compiler 'advanced' mode
        this['numeral'] = yearpicker;
    }

    /*global define:false */
    if (typeof define === 'function' && define.amd) {
        define([], function () {
            return yearpicker;
        });
    }
}).call(this, jQuery);