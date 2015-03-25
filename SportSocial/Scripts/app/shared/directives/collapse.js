'use strict';

// автофокус
// ---------------
angular.module('shared').directive('collapse', [function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var $this = angular.element(element),
                $items = angular.element('.clps__item', $this);
            $this.on('click', '.clps__item', function () {
                if (!angular.element(this).hasClass('clps__item--active')) {
                    $items.removeClass('clps__item--active');
                }
                angular.element(this).toggleClass('clps__item--active');
            });
        }
    }
}]);