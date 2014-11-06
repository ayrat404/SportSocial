'use strict';

// автофокус
// ---------------
angular.module('shared').directive('ngAutofocus', ['$timeout', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            attrs.$observe('ngAutofocus', function (val) {
                if (val) {
                    $timeout(function () {
                        element[0].focus();
                    });
                }
            });
        }
    }
}]);