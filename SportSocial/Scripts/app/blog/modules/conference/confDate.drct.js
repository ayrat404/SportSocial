'use strict';

//------ скролл к текущему элементу
angular.module('app').directive('confDate',
    ['$interval',
     'conferenceRqst',
function ($interval, conferenceRqst) {
    return {
        restrict: 'A',
        templateUrl: '/Scripts/templates/blog/conference-counter.html',
        replace: true,
        scope: {
            confDate: '@'
        },
        link: function ($scope, element, attrs) {
            var duration = $scope.confDate,
                interval = 1000;
            
            // запуск таймера
            // ---------------------
            function conferenceCountDown() {
                var conferenceTime = $interval(function () {
                    if (duration == 0) {
                        $interval.cancel(conferenceTime);
                        conferenceRqst.requestTime()
                            .then(function (res) {
                                if (res.url != undefined) {
                                    $scope.url = res.url;
                                } else {
                                    duration = $scope.stamp;
                                    conferenceCountDown();
                                }
                            });
                    } else {
                        duration = duration - interval;
                        $scope.days = duration.days();
                        $scope.hours = duration.hours();
                        $scope.minutes = duration.minutes();
                    }
                }, 1000);
            }
        }
    }
}]);
