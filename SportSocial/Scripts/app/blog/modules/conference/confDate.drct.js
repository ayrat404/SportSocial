'use strict';

// блок отсчета времени до конференции
// ---------------
angular.module('blog').directive('confDate',
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
            var diff = moment.duration(+$scope.confDate),
                interval = 1000 * 60;
            count();
            var conferenceTime = $interval(count, interval);

            // запуск таймера
            // ---------------
            function count() {
                diff = moment.duration(diff - interval);
                if (diff.milliseconds() <= 0) {
                    $interval.cancel(conferenceTime);
                    conferenceRqst.requestTime()
                        .then(function (res) {
                        debugger;
                            if (res.data.url != undefined || res.data.url != null) {
                                $scope.url = res.data.url;
                            } else {
                                diff = res.data.stamp;
                                count();
                            }
                        });
                } else {
                    $scope.dh = getVal(0, diff.days());
                    $scope.dm = getVal(1, diff.days());
                    $scope.hh = getVal(0, diff.hours());
                    $scope.hm = getVal(1, diff.hours());
                    $scope.mh = getVal(0, diff.minutes());
                    $scope.mm = getVal(1, diff.minutes());
                }
            }

            // выделяем нужные разряды
            // ---------------
            function getVal(i, v) {
                v = v.toString();
                if (v.length == 1) {
                    v = '0' + v;
                }
                return v[i];
            }
        }
    }
}]);