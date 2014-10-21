'use strict';

    //------ скролл к текущему элементу
angular.module('app').directive('confDate', ['$interval', function ($interval) {
        return {
            restrict: 'A',
            template: '<div>minutes - {{minutes}}, seconds - {{seconds}}</div>',
            replace: true,
            scope: {
                confDate: '@'
            },
            link: function (scope, element, attrs) {
                countdownTimer();
                scope.minutes = scope.confDate;
                scope.seconds = scope.confDate;
                function countdownTimer() {
                    var smsTime = $interval(function () {
                        scope.minutes = scope.minutes - 1;
                        scope.seconds = scope.seconds - 2;
                    }, 1000);
                }
            }
        }
    }]);
