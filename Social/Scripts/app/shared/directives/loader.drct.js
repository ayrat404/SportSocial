'use strict';

angular.module('shared')
    .directive('loader', [function () {
    var spinnerClass = 'loader timer-loader',
        containerClass = 'loader-container',
        wrapClass = 'loader-wrap';

        return {
        restrict: 'A',
        scope: {
            loader: '=loader'
        },
        link: function(scope, element, attrs) {
            scope.$watch('loader', function(newVal, oldVal) {
                if (newVal != oldVal) {
                    var $el = angular.element(element);
                    if (newVal === true) {
                        $el.addClass(containerClass);
                        $el.append(
                            angular.element('<div>', { class: wrapClass }).append(
                            angular.element('<div>', { class: spinnerClass, text: 'Загрузка...' })));
                    } else if (newVal === false) {
                        $el.removeClass(containerClass);
                        $el.find('.' + wrapClass).remove();
                    }

                }
            });
        }
    }
}]);