'use strict';

    //------ скролл к текущему элементу
angular.module('shared').directive('scrollToCurrentPos', ['$timeout', function ($timeout) {
        return {
            restrict: 'A',
            link: function (scope, element, attrs) {
                var dopScroll = attrs.paramDopscroll || 0;
                attrs.$observe('scrollToCurrentPos', function (val) {
                    if (val) {
                        $('html, body').animate({ scrollTop: $(element).offset().top - dopScroll }, 'slow');
                    }
                });
            }
        }
    }]);
