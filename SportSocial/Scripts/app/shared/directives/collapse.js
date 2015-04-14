'use strict';

// автофокус
// ---------------
angular.module('shared').directive('collapse', [function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var $this = angular.element(element),
                $items = angular.element('.clps__item', $this);
            $this.on('click', '.clps__title', function () {
                var $wrap = angular.element(this).closest('.clps__item');
                if ($wrap.length && !$wrap.hasClass('clps__item--active')) {
                    $items.removeClass('clps__item--active');
                }
                $wrap.toggleClass('clps__item--active');
            });
        }
    }
}]);