'use strict';

angular.module('shared')
    .directive('scrollTo', [function () {

        var navSelector = '.js-nav',
            defaults = {
                speed: 600,
                offset: 0
            };

        // ---------------
        function scrollTo(to, offset) {
            if ($(to).length) {
                offset = angular.extend(defaults.offset, offset);
                if ($(navSelector).length) offset += $(navSelector).outerHeight();
                $('html, body').animate({
                    scrollTop: $(to).offset().top - offset
                }, defaults.speed);
            }
        }

        // ---------------
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var $el = $(element);
                $el.on('click', function (event) {
                    event.preventDefault();
                    scrollTo(attrs.scrollTo, attrs.offset);
                });
            }
        }
    }]);