'use sctict';

// загрузка/смена аватара
// ---------------
angular
    .module('blog')
    .directive('paySlider', ['$parse', function ($parse) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function ($scope, element, attr, ngModelCtrl) {
                
                // prices
                // ---------------
                var prices = {
                    1: 100,
                    2: 200,
                    3: 300,
                    4: 400,
                    5: 500,
                    6: 600,
                    7: 700,
                    8: 800,
                    9: 900,
                    10: 1000,
                    11: 1100,
                    12: 1200
                }

                // slider init
                // ---------------
                element.noUiSlider({
                    start: 4,
                    step: 1,
                    connect: 'lower',
                    range: {
                        'min': 1,
                        'max': 12
                    }
                });

                // bind to tooltip
                // ---------------
                element.Link('lower').to('-inline-<div class="slider__tooltip slider__tooltip--once"></div>', function (value) {
                    var isOldHidden = value == 1 ? ' hidden' : '',
                        $this = angular.element(this);
                    //if (value == 1) {
                    //    $this.addClass('slider__tooltip--once');
                    //} else {
                    //    $this.removeClass('slider__tooltip--once');
                    //}
                    value = Math.round(value);
                    $(this).html(
                        //'<span class="slider__old' + isOldHidden + '">' + Math.round(prices[value] * 1.3) + 'р. </span>' +
                        '<span class="slider__new">' + prices[value] + 'р. </span>'
                    );
                    ngModelCtrl.$setViewValue(value);
                    ngModelCtrl.$render();
                });

                // values render init
                // ---------------
                element.noUiSlider_pips({
                    mode: 'values',
                    values: [1,2,3,4,5,6,7,8,9,10,11,12],
                    density: 4,
                    format: wNumb({
                        postfix: 'мес.'
                    })
                });
            }
        };
    }]);