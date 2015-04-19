'use strict';

// автофокус
// ---------------
angular.module('shared').directive('navActive', ['$location', function ($location) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var $links = angular.element(element).find('a'),
                currentPath = $location.$$path;
            angular.forEach($links, function (value, key) {
                if (value.hash.substr(1) === currentPath) {
                    angular.element(value).addClass('active');
                    return false;
                }
            });

            angular.element(element).on('click', 'a', function() {
                if (!angular.element(this).hasClass('active')) {
                    $links.removeClass('active');
                    angular.element(this).addClass('active');
                }
            });

        }
    }
}]);