'use strict';

    //------ скролл к текущему элементу
angular.module('shared').directive('datepicker', [function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            scope: {
              options: '=datepicker'  
            },
            link: function (scope, element, attrs, ngModel) {
                element.datetimepicker(scope.options);
            }
        }
    }]);
