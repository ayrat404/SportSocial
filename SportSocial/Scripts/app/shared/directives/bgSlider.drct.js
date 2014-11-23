'use strict';

// смена картинок на фоне
// ---------------
angular.module('shared').directive('bgSlider', ['$interval', 'utilsSrvc', function ($interval, utilsSrvc) {
    return {
        restrict: 'A',
        scope: {
            bgSlider: '='  
        },
        link: function ($scope, element, attrs) {

            // создание элементов, стили
            // ---------------
            var $img = angular.element('<img>', { class: 'promo__bg-img', alt: 'fortress', src: $scope.bgSlider[0] }),
                $overlay = angular.element('<div>', { class: 'promo__overlay' }),
                index = 0;
            element.append($img);
            element.append($overlay);

            // анимация
            // ---------------
                $interval(function () {
                    index++;
                    index = index >= $scope.bgSlider.length ? 0 : index;
                    $img.attr('src', $scope.bgSlider[index]);
                }, 8000);
        }
    }
}]);
