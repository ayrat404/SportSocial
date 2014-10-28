'use strict';

angular.module('blog').directive('redactor', ['$window', function ($window) {
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
