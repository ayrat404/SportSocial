'use strict';

// автофокус
// ---------------
angular.module('shared').directive('aboutVideo', ['$timeout', function ($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            angular.element(element).on('click', function() {
                bootbox.dialog({
                    title: 'Заголовок',
                    message: '<iframe width="460" height="315" src="//www.youtube.com/embed/zWc41BbjlZ4" frameborder="0" allowfullscreen class="payment__video"></iframe>'
                });
            });
        }
    }
}]);