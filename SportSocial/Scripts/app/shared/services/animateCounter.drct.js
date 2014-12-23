'use strict';

// анимация счетчика
// ---------------
angular
    .module('shared')
    .directive('animateCounter', ['utilsSrvc', function (utilsSrvc) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs) {
                var $el = angular.element(element);
                scope.$watch(attrs.ngModel, function (newVal, oldVal) {
                    if (newVal != oldVal) {
                        var classIn,
                            classOut;
                        if (newVal < oldVal) {
                            classIn = 'fadeInDown';
                            classOut = 'fadeOutDown';
                        } else {
                            classIn = 'fadeInUp';
                            classOut = 'fadeOutUp';
                        }
                        utilsSrvc.animation.add($el, classOut, function () {
                            $el.html(newVal);
                            scope.$digest();
                            utilsSrvc.animation.add($el, classIn);
                        });
                    }
                });
            }
        }
    }]);
