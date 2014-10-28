'use strict';

angular.module('app').directive('redactor', ['$window', function ($window) {
    return {
        restrict: 'A',
        link: function (scope, element, attributes) {
            element.redactor({
                toolbarFixed: false,
                lang: 'ru',
                minHeight: 300
            });
        }
    };
}]);
