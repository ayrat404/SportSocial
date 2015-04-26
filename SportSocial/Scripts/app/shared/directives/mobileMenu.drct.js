'use strict';

// переключатель мобильного меню
// ---------------
angular.module('shared').directive('mobileMenu', [function () {
    return {
        restrict: 'A',
        scope: {
            el: '@mobileMenu'
        },
        link: function (scope, element, attrs) {
            var $menu = angular.element(element),
                $toggler = angular.element(scope.el);

            $toggler.on('click', function() {
                $menu.toggleClass('open');
            });
        }
    }
}]);